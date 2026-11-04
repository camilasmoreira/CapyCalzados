-- =========================================================================
-- Script de actualización: Relación de la tabla Cliente con Venta
-- Base de Datos: DBSistemaVentas
-- =========================================================================
USE DBSistemaVentas
GO

-- 1. Agregar la columna IdCliente como Clave Foránea en la tabla Venta (si no existe)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('Venta') AND name = 'IdCliente'
)
BEGIN
    ALTER TABLE Venta ADD IdCliente INT NULL;
    ALTER TABLE Venta ADD CONSTRAINT FK_Venta_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente);
    PRINT 'Columna IdCliente y clave foránea agregadas a la tabla Venta exitosamente.';
END
ELSE
BEGIN
    PRINT 'La columna IdCliente ya existe en la tabla Venta.';
END
GO

-- 2. Actualizar las ventas existentes asociando el IdCliente según el DocumentoCliente
UPDATE v
SET v.IdCliente = c.IdCliente
FROM Venta v
INNER JOIN Cliente c ON c.Documento = v.DocumentoCliente
WHERE v.IdCliente IS NULL;
PRINT 'Ventas históricas asociadas con sus clientes.';
GO

-- 3. Actualizar el Stored Procedure SP_REGISTRARVENTA
CREATE OR ALTER PROCEDURE SP_REGISTRARVENTA(
    @IdUsuario int,
    @IdCliente int = null,
    @TipoDocumento varchar(50),
    @NumeroDocumento varchar(50),
    @DocumentoCliente varchar(50),
    @NombreCliente varchar(80),
    @MontoPago decimal(10,2),
    @MontoCambio decimal(10,2),
    @MontoTotal decimal(10,2),
    @DetalleVenta [EDetalle_Venta] READONLY,
    @Resultado bit output,
    @Mensaje varchar(500) output
) AS
BEGIN
    BEGIN TRY
        DECLARE @idventa int = 0
        SET @Resultado = 1
        SET @Mensaje = ''

        BEGIN TRANSACTION registro

        INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, DocumentoCliente, NombreCliente, MontoPago, MontoCambio, MontoTotal)
        VALUES(@IdUsuario, IIF(@IdCliente = 0, NULL, @IdCliente), @TipoDocumento, @NumeroDocumento, @DocumentoCliente, @NombreCliente, @MontoPago, @MontoCambio, @MontoTotal)
        
        SET @idventa = SCOPE_IDENTITY()

        INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal)
        SELECT @idventa, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleVenta

        COMMIT TRANSACTION registro
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
        ROLLBACK TRANSACTION registro
    END CATCH
END
GO

-- 4. Actualizar el Stored Procedure SP_REPORTEVENTAS
CREATE OR ALTER PROCEDURE SP_REPORTEVENTAS(
    @fechainicio date,
    @fechafin date
)
AS
BEGIN
    SELECT 
        CONVERT(char(10), v.FechaRegistro, 103) AS [FechaRegistro], 
        v.TipoDocumento,
        v.NumeroDocumento,
        v.MontoTotal,
        u.NombreCompleto AS [UsuarioRegistro],
        v.DocumentoCliente,
        v.NombreCliente,
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
    WHERE v.FechaRegistro BETWEEN @fechainicio AND @fechafin
END
GO

-- 5. Actualizar SP_DATOSGRAFICOS (con conteos correctos)
CREATE OR ALTER PROCEDURE SP_DATOSGRAFICOS
    @totVentas int out,
    @totCompras int out,
    @totProveedores int out,
    @totProd int out,
    @totCat int out,
    @totUser int out,
    @totClientes int out
AS
BEGIN
    SET @totVentas = ISNULL((SELECT SUM(MontoTotal) FROM Venta), 0)
    SET @totCompras = ISNULL((SELECT SUM(c.MontoTotal) FROM Compra AS c), 0)
    SET @totProveedores = ISNULL((SELECT COUNT(IdProveedor) FROM Proveedor), 0)
    SET @totProd = ISNULL((SELECT COUNT(IdProducto) FROM Productos), 0)
    SET @totCat = ISNULL((SELECT COUNT(IdCategoria) FROM Categorias), 0)
    SET @totUser = ISNULL((SELECT COUNT(IdUsuario) FROM Usuario), 0)
    SET @totClientes = ISNULL((SELECT COUNT(IdCliente) FROM Cliente), 0)
END
GO

-- 6. Actualizar datos de zapatería (Nombre del negocio y categorías sugeridas si están vacías)
UPDATE Negocio 
SET Nombre = 'CapyCalzados - Venta de Calzados' 
WHERE Nombre LIKE '%GK%' OR Nombre = 'GK Innovatech';

UPDATE Usuario
SET Correo = 'juan@capycalzados.com'
WHERE Correo LIKE '%kevingaray%';

IF NOT EXISTS (SELECT 1 FROM Categorias)
BEGIN
    INSERT INTO Categorias(Descripcion, Estado) VALUES
    ('Zapatillas Urbanas', 1),
    ('Zapatillas Deportivas', 1),
    ('Zapatos de Vestir', 1),
    ('Botas y Borcegos', 1),
    ('Sandalias y Ojotas', 1),
    ('Mocasines', 1);
    PRINT 'Categorías de calzados insertadas.';
END
GO

PRINT 'Actualización completada correctamente.';
