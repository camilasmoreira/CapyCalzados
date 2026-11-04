-- ============================================================
-- Script de actualizaciones completas - CapyCalzados v2
-- Ejecutar en la base de datos DBSistemaVentas
-- ============================================================
USE DBSistemaVentas
GO

-- ============================================================
-- 1. Proveedores: Nike, Adidas y Otros proveedores
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Nike')
    INSERT INTO Proveedor(Documento, RazonSocial, Direccion, Correo, Telefono, Estado)
    VALUES ('30000001', 'Nike', 'Av. Libertador 1000', 'contacto@nike.com', '1140000001', 1);
GO

IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Adidas')
    INSERT INTO Proveedor(Documento, RazonSocial, Direccion, Correo, Telefono, Estado)
    VALUES ('30000002', 'Adidas', 'Av. Santa Fe 2000', 'contacto@adidas.com', '1140000002', 1);
GO

IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Otros proveedores')
BEGIN
    IF EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Otros')
        UPDATE Proveedor SET RazonSocial = 'Otros proveedores' WHERE RazonSocial = 'Otros';
    ELSE
        INSERT INTO Proveedor(Documento, RazonSocial, Direccion, Correo, Telefono, Estado)
        VALUES ('30000003', 'Otros proveedores', ' Parque Industrial', 'otros@proveedores.com', '1140000003', 1);
END
GO

-- ============================================================
-- 2. Relación Producto -> Proveedor en Base de Datos
--    Como saber qué proveedor es de qué producto
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'Productos' AND COLUMN_NAME = 'IdProveedor')
BEGIN
    ALTER TABLE Productos ADD IdProveedor INT NULL REFERENCES Proveedor(IdProveedor);
    PRINT 'Columna IdProveedor agregada a la tabla Productos.';
END
GO

-- Vincular productos existentes con su proveedor según Marca o última compra
UPDATE p
SET p.IdProveedor = pr.IdProveedor
FROM Productos p
INNER JOIN Proveedor pr ON UPPER(LTRIM(RTRIM(pr.RazonSocial))) = UPPER(LTRIM(RTRIM(p.Marca)))
WHERE p.IdProveedor IS NULL;
GO

DECLARE @IdProvOtros INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Otros proveedores');
UPDATE Productos
SET IdProveedor = @IdProvOtros,
    Marca = CASE WHEN Marca NOT IN ('Nike', 'Adidas', 'Otros proveedores') THEN 'Otros proveedores' ELSE Marca END
WHERE IdProveedor IS NULL AND @IdProvOtros IS NOT NULL;
GO

-- ============================================================
-- 3. SOLO ZAPATILLAS URBANAS Y DEPORTIVAS en Categorías
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Descripcion = 'Zapatillas Urbanas')
    INSERT INTO Categorias(Descripcion, Estado) VALUES ('Zapatillas Urbanas', 1);
GO

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Descripcion = 'Zapatillas Deportivas')
    INSERT INTO Categorias(Descripcion, Estado) VALUES ('Zapatillas Deportivas', 1);
GO

UPDATE Categorias SET Estado = 1 WHERE Descripcion IN ('Zapatillas Urbanas', 'Zapatillas Deportivas');
GO

DECLARE @IdCatUrbana INT = (SELECT TOP 1 IdCategoria FROM Categorias WHERE Descripcion = 'Zapatillas Urbanas');

-- Reasignar productos que estaban en categorías eliminadas hacia Zapatillas Urbanas
UPDATE Productos
SET IdCategoria = @IdCatUrbana
WHERE IdCategoria NOT IN (
    SELECT IdCategoria FROM Categorias WHERE Descripcion IN ('Zapatillas Urbanas', 'Zapatillas Deportivas')
);
GO

-- Eliminar las categorías que NO sean Zapatillas Urbanas ni Zapatillas Deportivas
DELETE FROM Categorias
WHERE Descripcion NOT IN ('Zapatillas Urbanas', 'Zapatillas Deportivas');
GO

-- ============================================================
-- 4. Tabla Venta: eliminar DocumentoCliente y NombreCliente
--    innecesarios y agregar FormaPago
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'Venta' AND COLUMN_NAME = 'FormaPago')
BEGIN
    ALTER TABLE Venta ADD FormaPago VARCHAR(50) NOT NULL DEFAULT 'Efectivo';
    PRINT 'Columna FormaPago agregada a la tabla Venta.';
END
GO

UPDATE Venta SET FormaPago = 'Efectivo' WHERE FormaPago IS NULL OR FormaPago = '';
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
           WHERE TABLE_NAME = 'Venta' AND COLUMN_NAME = 'DocumentoCliente')
BEGIN
    ALTER TABLE Venta DROP COLUMN DocumentoCliente;
    PRINT 'Columna DocumentoCliente eliminada de la tabla Venta.';
END
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
           WHERE TABLE_NAME = 'Venta' AND COLUMN_NAME = 'NombreCliente')
BEGIN
    ALTER TABLE Venta DROP COLUMN NombreCliente;
    PRINT 'Columna NombreCliente eliminada de la tabla Venta.';
END
GO

-- ============================================================
-- 5. Actualizar SP_REGISTRARCOMPRA y SP_VALIDARCOMPRA
-- ============================================================
IF OBJECT_ID('SP_REGISTRARCOMPRA', 'P') IS NOT NULL
    DROP PROCEDURE SP_REGISTRARCOMPRA
GO

CREATE PROCEDURE SP_REGISTRARCOMPRA(
    @IdUsuario int,
    @IdProveedor int,
    @TipoDocumento varchar(50),
    @NumeroDocumento varchar(50),
    @MontoTotal decimal(30,2),
    @DetalleCompra [EDetalle_Compra] READONLY,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idcompra int = 0
        SET @Resultado = 1
        SET @Mensaje = ''

        IF EXISTS (SELECT 1 FROM Compra WHERE NumeroDocumento = @NumeroDocumento)
        BEGIN
            SET @Resultado = 0
            SET @Mensaje = 'La compra con el número de documento ' + @NumeroDocumento + ' ya está ingresada.'
            RETURN
        END

        BEGIN TRANSACTION registro

        INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, Estado)
        VALUES(@IdUsuario, @IdProveedor, @TipoDocumento, @NumeroDocumento, @MontoTotal, 1)

        SET @idcompra = SCOPE_IDENTITY()

        INSERT INTO Detalle_Compra(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total)
        SELECT @idcompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total FROM @DetalleCompra

        UPDATE p SET 
            p.PrecioCompra = dc.PrecioCompra,
            p.PrecioVenta = dc.PrecioVenta,
            p.Stock = p.Stock + dc.Cantidad,
            p.IdProveedor = @IdProveedor
        FROM Productos p
        INNER JOIN @DetalleCompra dc ON dc.IdProducto = p.IdProducto

        COMMIT TRANSACTION registro
        SET @Mensaje = 'Compra registrada exitosamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION registro
    END CATCH
END
GO

IF OBJECT_ID('SP_VALIDARCOMPRA', 'P') IS NOT NULL
    DROP PROCEDURE SP_VALIDARCOMPRA
GO

CREATE PROCEDURE SP_VALIDARCOMPRA(
    @IdCompra INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    IF EXISTS(SELECT * FROM Compra WHERE IdCompra = @IdCompra AND Estado = 0)
    BEGIN
        IF EXISTS(SELECT * FROM Detalle_Compra WHERE IdCompra = @IdCompra)
        BEGIN
            UPDATE p
            SET p.Stock = p.Stock + dc.Cantidad
            FROM Productos p
            INNER JOIN Detalle_Compra dc ON dc.IdProducto = p.IdProducto
            WHERE dc.IdCompra = @IdCompra;

            UPDATE Compra
            SET Estado = 1
            WHERE IdCompra = @IdCompra;

            SET @Resultado = 1;
            SET @Mensaje = 'Compra validada y stock actualizado correctamente.';
        END
        ELSE
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró ningún detalle para la compra especificada.';
        END
    END
    ELSE
    BEGIN
        SET @Resultado = 1;
        SET @Mensaje = 'La compra ya se encuentra validada y con el stock actualizado.';
    END
END
GO

-- ============================================================
-- 6. Actualizar SP_REGISTRARVENTA y SP_REPORTEVENTAS
-- ============================================================
IF OBJECT_ID('SP_REGISTRARVENTA', 'P') IS NOT NULL
    DROP PROCEDURE SP_REGISTRARVENTA
GO

CREATE PROCEDURE SP_REGISTRARVENTA(
    @IdUsuario int,
    @IdCliente int = null,
    @TipoDocumento varchar(50),
    @NumeroDocumento varchar(50),
    @DocumentoCliente varchar(50) = '',
    @NombreCliente varchar(80) = '',
    @FormaPago varchar(50) = 'Efectivo',
    @MontoPago decimal(30,2),
    @MontoCambio decimal(30,2),
    @MontoTotal decimal(30,2),
    @DetalleVenta [EDetalle_Venta] READONLY,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idventa int = 0
        SET @Resultado = 1
        SET @Mensaje = ''

        IF EXISTS (SELECT 1 FROM Venta WHERE NumeroDocumento = @NumeroDocumento)
        BEGIN
            SET @Resultado = 0
            SET @Mensaje = 'La venta con el número de documento ' + @NumeroDocumento + ' ya está ingresada.'
            RETURN
        END

        BEGIN TRANSACTION registro

        INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal)
        VALUES(@IdUsuario, IIF(@IdCliente = 0, NULL, @IdCliente), @TipoDocumento, @NumeroDocumento, ISNULL(@FormaPago, 'Efectivo'), @MontoPago, @MontoCambio, @MontoTotal)

        SET @idventa = SCOPE_IDENTITY()

        INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal)
        SELECT @idventa, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleVenta

        COMMIT TRANSACTION registro
        SET @Mensaje = 'Venta registrada exitosamente.'
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION registro
    END CATCH
END
GO

IF OBJECT_ID('SP_REPORTEVENTAS', 'P') IS NOT NULL
    DROP PROCEDURE SP_REPORTEVENTAS
GO

CREATE PROCEDURE SP_REPORTEVENTAS(
    @fechainicio date,
    @fechafin date
)
AS
BEGIN
    SELECT 
        CONVERT(char(10), v.FechaRegistro, 103) AS [FechaRegistro], 
        v.TipoDocumento,
        v.NumeroDocumento,
        ISNULL(v.FormaPago, 'Efectivo') AS [FormaPago],
        v.MontoTotal,
        u.NombreCompleto AS [UsuarioRegistro],
        ISNULL(c.Documento, '') AS [DocumentoCliente],
        ISNULL(c.NombreCompleto, 'Consumidor Final') AS [NombreCliente],
        p.Codigo AS [CodigoProducto],
        p.Nombre AS [NombreProducto],
        ca.Descripcion AS [Categoria],
        dv.PrecioVenta,
        dv.Cantidad,
        dv.SubTotal
    FROM Venta v
    INNER JOIN Usuario u ON u.IdUsuario = v.IdUsuario
    LEFT JOIN Cliente c ON c.IdCliente = v.IdCliente
    INNER JOIN Detalle_Venta dv ON dv.IdVenta = v.IdVenta
    INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
    INNER JOIN Categorias ca ON ca.IdCategoria = p.IdCategoria
    WHERE CONVERT(date, v.FechaRegistro) BETWEEN @fechainicio AND @fechafin
END
GO

-- ============================================================
-- 7. Permisos del rol Vendedor:
--    No puede comprar, no ve categorías ni proveedores, solo
--    visualiza productos y gestiona ventas/clientes (sin eliminar).
-- ============================================================
DELETE FROM Permiso 
WHERE IdRol IN (SELECT IdRol FROM Rol WHERE Descripcion = 'Vendedor' OR IdRol = 2)
  AND NombreMenu IN ('menuCompras', 'menuRegistrarCompra', 'menuVerDetalleCompra', 'Categorias', 'menuProveedores');
GO

IF NOT EXISTS (SELECT 1 FROM Permiso WHERE IdRol = 2 AND NombreMenu = 'menuMantenimiento')
    INSERT INTO Permiso(IdRol, NombreMenu) VALUES (2, 'menuMantenimiento');
GO

IF NOT EXISTS (SELECT 1 FROM Permiso WHERE IdRol = 2 AND NombreMenu = 'menuVentas')
    INSERT INTO Permiso(IdRol, NombreMenu) VALUES (2, 'menuVentas');
GO

IF NOT EXISTS (SELECT 1 FROM Permiso WHERE IdRol = 2 AND NombreMenu = 'menuClientes')
    INSERT INTO Permiso(IdRol, NombreMenu) VALUES (2, 'menuClientes');
GO
