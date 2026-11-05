-- ============================================================
-- Script de Ajustes Finales del Profesor - CapyCalzados
-- Ejecutar en la base de datos DBSistemaVentas
-- ============================================================
USE DBSistemaVentas
GO

-- ============================================================
-- 1. LIMPIEZA DE ROLES REPETIDOS Y REESTRUCTURACIÓN DE PERFILES
--    1 = Gerente (Analítico: solo gráficos y reportes)
--    2 = Vendedor (Operativo ventas: ventas, clientes sin borrar, ver productos)
--    3 = Manager (Operativo total: compras, productos, proveedores, usuarios, etc.)
-- ============================================================

-- Asegurar que existan los 3 roles base con IDs 1, 2, 3
UPDATE Rol SET Descripcion = 'Gerente' WHERE IdRol = 1;
UPDATE Rol SET Descripcion = 'Vendedor' WHERE IdRol = 2;
UPDATE Rol SET Descripcion = 'Manager' WHERE IdRol = 3;
GO

-- Reasignar usuarios que apunten a roles duplicados (> 3)
UPDATE Usuario SET IdRol = 1 WHERE IdRol IN (SELECT IdRol FROM Rol WHERE IdRol > 3 AND Descripcion = 'Gerente');
UPDATE Usuario SET IdRol = 2 WHERE IdRol IN (SELECT IdRol FROM Rol WHERE IdRol > 3 AND Descripcion = 'Vendedor');
UPDATE Usuario SET IdRol = 3 WHERE IdRol IN (SELECT IdRol FROM Rol WHERE IdRol > 3 AND Descripcion = 'Manager');
UPDATE Usuario SET IdRol = 3 WHERE IdRol > 3;
GO

-- Eliminar permisos de roles duplicados y luego los roles duplicados
DELETE FROM Permiso WHERE IdRol > 3;
DELETE FROM Rol WHERE IdRol > 3;
GO

-- Reconfigurar todos los permisos limpiamente sin duplicados
DELETE FROM Permiso;
GO

-- Permisos GERENTE (IdRol = 1): Estrictamente analítico (solo Inicio, Gráficos y Reportes)
INSERT INTO Permiso(IdRol, NombreMenu) VALUES
(1, 'menuInicio'),
(1, 'menuGraficos'),
(1, 'menuReportes');
GO

-- Permisos VENDEDOR (IdRol = 2): Ventas, Clientes y visualización de Productos
INSERT INTO Permiso(IdRol, NombreMenu) VALUES
(2, 'menuInicio'),
(2, 'menuVentas'),
(2, 'menuClientes'),
(2, 'menuMantenimiento');
GO

-- Permisos MANAGER (IdRol = 3): Perfil operativo total (hace todo)
INSERT INTO Permiso(IdRol, NombreMenu) VALUES
(3, 'menuInicio'),
(3, 'menuVentas'),
(3, 'menuCompras'),
(3, 'menuClientes'),
(3, 'menuMantenimiento'),
(3, 'menuProveedores'),
(3, 'menuUsuarios'),
(3, 'menuReportes'),
(3, 'menuGraficos');
GO

-- ============================================================
-- 2. NUEVOS CAMPOS EN TABLA USUARIO:
--    Nombre, Apellido, Direccion, Telefono
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario' AND COLUMN_NAME = 'Nombre')
    ALTER TABLE Usuario ADD Nombre VARCHAR(80) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario' AND COLUMN_NAME = 'Apellido')
    ALTER TABLE Usuario ADD Apellido VARCHAR(80) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario' AND COLUMN_NAME = 'Direccion')
    ALTER TABLE Usuario ADD Direccion VARCHAR(120) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario' AND COLUMN_NAME = 'Telefono')
    ALTER TABLE Usuario ADD Telefono VARCHAR(50) NULL;
GO

-- Actualizar usuarios existentes con datos completos (Nombre, Apellido, Dirección, Teléfono)
UPDATE Usuario
SET Nombre = 'Martín',
    Apellido = 'Rodríguez (Manager)',
    NombreCompleto = 'Martín Rodríguez (Manager)',
    Direccion = 'Av. 9 de Julio 1450',
    Telefono = '3794112233',
    IdRol = 3,
    Estado = 1
WHERE Documento = '1234';

UPDATE Usuario
SET Nombre = 'Camila',
    Apellido = 'Gómez',
    NombreCompleto = 'Camila Gómez',
    Direccion = 'Junín 850',
    Telefono = '3794223344',
    IdRol = 2,
    Estado = 1
WHERE Documento = '4321';

UPDATE Usuario
SET Nombre = 'Roberto',
    Apellido = 'Sánchez (Gerente)',
    NombreCompleto = 'Roberto Sánchez (Gerente)',
    Direccion = 'Costanera Gral. San Martín 320',
    Telefono = '3794334455',
    IdRol = 1,
    Estado = 1
WHERE Documento = '1111';

-- Si algún usuario quedó con campos nulos, completarlos
UPDATE Usuario
SET Nombre = ISNULL(Nombre, NombreCompleto),
    Apellido = ISNULL(Apellido, ''),
    Direccion = ISNULL(Direccion, 'Sin especificar'),
    Telefono = ISNULL(Telefono, '3794000000')
WHERE Nombre IS NULL OR Apellido IS NULL OR Direccion IS NULL OR Telefono IS NULL;
GO

-- Agregar vendedores adicionales para el Ranking de Vendedores si no existen
IF NOT EXISTS (SELECT 1 FROM Usuario WHERE Documento = '2222')
BEGIN
    INSERT INTO Usuario(Documento, Nombre, Apellido, NombreCompleto, Direccion, Telefono, Correo, Clave, IdRol, Estado)
    VALUES ('2222', 'Lucas', 'Fernández', 'Lucas Fernández', 'Av. Independencia 2300', '3794556677', 'lucas@capycalzados.com', '2222', 2, 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE Documento = '3333')
BEGIN
    INSERT INTO Usuario(Documento, Nombre, Apellido, NombreCompleto, Direccion, Telefono, Correo, Clave, IdRol, Estado)
    VALUES ('3333', 'Sofía', 'Martínez', 'Sofía Martínez', 'Hipólito Yrigoyen 1120', '3794667788', 'sofia@capycalzados.com', '3333', 2, 1);
END
GO

-- ============================================================
-- 3. ACTUALIZAR PROCEDIMIENTOS DE USUARIO (CON NUEVOS CAMPOS)
-- ============================================================
IF OBJECT_ID('SP_REGISTRARUSUARIO', 'P') IS NOT NULL
    DROP PROCEDURE SP_REGISTRARUSUARIO
GO

CREATE PROCEDURE SP_REGISTRARUSUARIO
(
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80) = '',
    @Apellido VARCHAR(80) = '',
    @NombreCompleto VARCHAR(100) = '',
    @Direccion VARCHAR(120) = '',
    @Telefono VARCHAR(50) = '',
    @Correo VARCHAR(100),
    @Clave VARCHAR(80),
    @IdRol INT,
    @Estado BIT,
    @IdUsuarioResultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @IdUsuarioResultado = 0;
    SET @Mensaje = '';

    IF EXISTS(SELECT 1 FROM Usuario WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El documento ya está registrado para otro usuario.';
        RETURN;
    END

    IF EXISTS(SELECT 1 FROM Usuario WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado para otro usuario.';
        RETURN;
    END

    IF (@NombreCompleto = '' OR @NombreCompleto IS NULL)
        SET @NombreCompleto = LTRIM(RTRIM(ISNULL(@Nombre, '') + ' ' + ISNULL(@Apellido, '')));

    INSERT INTO Usuario (Documento, Nombre, Apellido, NombreCompleto, Direccion, Telefono, Correo, Clave, IdRol, Estado)
    VALUES (@Documento, @Nombre, @Apellido, @NombreCompleto, @Direccion, @Telefono, @Correo, @Clave, @IdRol, @Estado);

    SET @IdUsuarioResultado = SCOPE_IDENTITY();
    SET @Mensaje = 'Usuario registrado exitosamente.';
END
GO

IF OBJECT_ID('SP_EDITARUSUARIO', 'P') IS NOT NULL
    DROP PROCEDURE SP_EDITARUSUARIO
GO

CREATE PROCEDURE SP_EDITARUSUARIO
(
    @IdUsuario INT,
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80) = '',
    @Apellido VARCHAR(80) = '',
    @NombreCompleto VARCHAR(100) = '',
    @Direccion VARCHAR(120) = '',
    @Telefono VARCHAR(50) = '',
    @Correo VARCHAR(100),
    @Clave VARCHAR(80),
    @IdRol INT,
    @Estado BIT,
    @Respuesta BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Respuesta = 0;
    SET @Mensaje = '';

    IF EXISTS(SELECT 1 FROM Usuario WHERE Documento = @Documento AND IdUsuario != @IdUsuario)
    BEGIN
        SET @Mensaje = 'El documento ya está registrado para otro usuario.';
        RETURN;
    END

    IF EXISTS(SELECT 1 FROM Usuario WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado para otro usuario.';
        RETURN;
    END

    IF (@NombreCompleto = '' OR @NombreCompleto IS NULL)
        SET @NombreCompleto = LTRIM(RTRIM(ISNULL(@Nombre, '') + ' ' + ISNULL(@Apellido, '')));

    UPDATE Usuario 
    SET 
        Documento = @Documento,
        Nombre = @Nombre,
        Apellido = @Apellido,
        NombreCompleto = @NombreCompleto,
        Direccion = @Direccion,
        Telefono = @Telefono,
        Correo = @Correo,
        Clave = @Clave,
        IdRol = @IdRol,
        Estado = @Estado
    WHERE IdUsuario = @IdUsuario;

    SET @Respuesta = 1;
    SET @Mensaje = 'Usuario actualizado exitosamente.';
END
GO

IF OBJECT_ID('SP_ELIMINARUSUARIO', 'P') IS NOT NULL
    DROP PROCEDURE SP_ELIMINARUSUARIO
GO

CREATE PROCEDURE SP_ELIMINARUSUARIO(
    @IdUsuario int,
    @Respuesta bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    SET @Respuesta = 1;
    SET @Mensaje = 'Usuario dado de baja (Inactivo) correctamente.';
    UPDATE Usuario SET Estado = 0 WHERE IdUsuario = @IdUsuario;
END
GO

-- ============================================================
-- 4. PRODUCTOS Y MANEJO DE PRODUCTOS INACTIVOS
-- ============================================================
IF OBJECT_ID('SP_ELIMINARPRODUCTO', 'P') IS NOT NULL
    DROP PROCEDURE SP_ELIMINARPRODUCTO
GO

CREATE PROCEDURE SP_ELIMINARPRODUCTO(
    @IdProducto int,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    -- Baja lógica: pasa el producto a No Activo (Estado = 0) sin perder historial de compras/ventas
    UPDATE Productos
    SET Estado = 0
    WHERE IdProducto = @IdProducto;

    SET @Resultado = 1;
    SET @Mensaje = 'El producto fue marcado como No Activo correctamente.';
END
GO

-- Asegurar catálogo de zapatillas urbanas y deportivas (incluyendo productos activos e inactivos)
DECLARE @IdCatUrb INT = (SELECT TOP 1 IdCategoria FROM Categorias WHERE Descripcion = 'Zapatillas Urbanas');
DECLARE @IdCatDep INT = (SELECT TOP 1 IdCategoria FROM Categorias WHERE Descripcion = 'Zapatillas Deportivas');
DECLARE @IdProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike');
DECLARE @IdProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas');
DECLARE @IdProvOtros INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Otros proveedores');

-- Actualizar nombres de productos 3, 4 y 5 a modelos de zapatillas coherentes con el rubro
UPDATE Productos SET Nombre = 'Nike Court Vision Low', Descripcion = 'Talle 42 - Blanco/Negro', Marca = 'Nike', IdCategoria = @IdCatUrb, IdProveedor = @IdProvNike, Stock = 18, PrecioCompra = 48000, PrecioVenta = 82000, Estado = 1 WHERE IdProducto = 3;
UPDATE Productos SET Nombre = 'Adidas Duramo SL', Descripcion = 'Talle 41 - Azul Running', Marca = 'Adidas', IdCategoria = @IdCatDep, IdProveedor = @IdProvAdidas, Stock = 14, PrecioCompra = 46000, PrecioVenta = 78000, Estado = 1 WHERE IdProducto = 4;
UPDATE Productos SET Nombre = 'Puma Smash V2 Classic', Descripcion = 'Talle 40 - Gamuza Negro', Marca = 'Otros proveedores', IdCategoria = @IdCatUrb, IdProveedor = @IdProvOtros, Stock = 0, PrecioCompra = 38000, PrecioVenta = 64000, Estado = 0 WHERE IdProducto = 5;

IF NOT EXISTS (SELECT 1 FROM Productos WHERE Codigo = 1006)
BEGIN
    INSERT INTO Productos(Codigo, Nombre, Descripcion, Marca, IdCategoria, IdProveedor, Stock, PrecioCompra, PrecioVenta, Estado)
    VALUES (1006, 'Nike Revolution 6 Next', 'Talle 43 - Gris/Rojo Running', 'Nike', @IdCatDep, @IdProvNike, 22, 50000.00, 86000.00, 1);
END

IF NOT EXISTS (SELECT 1 FROM Productos WHERE Codigo = 1007)
BEGIN
    INSERT INTO Productos(Codigo, Nombre, Descripcion, Marca, IdCategoria, IdProveedor, Stock, PrecioCompra, PrecioVenta, Estado)
    VALUES (1007, 'Adidas Forum Low', 'Talle 39 - Blanco/Azul Urbano', 'Adidas', @IdCatUrb, @IdProvAdidas, 16, 54000.00, 92000.00, 1);
END

IF NOT EXISTS (SELECT 1 FROM Productos WHERE Codigo = 1008)
BEGIN
    INSERT INTO Productos(Codigo, Nombre, Descripcion, Marca, IdCategoria, IdProveedor, Stock, PrecioCompra, PrecioVenta, Estado)
    VALUES (1008, 'New Balance 574 Core', 'Talle 42 - Gris Clásico (Discontinuado)', 'Otros proveedores', @IdCatUrb, @IdProvOtros, 2, 44000.00, 74000.00, 0);
END
GO

-- ============================================================
-- 5. PROCEDIMIENTOS DE ESTADÍSTICAS CON FILTRO DE FECHAS
-- ============================================================
IF OBJECT_ID('SP_GRAFICOSVFECHAS', 'P') IS NOT NULL
    DROP PROCEDURE SP_GRAFICOSVFECHAS
GO

CREATE PROCEDURE SP_GRAFICOSVFECHAS
    @fechaDesde DATE,
    @fechaHasta DATE,
    @totVentas INT OUT,
    @totCompras INT OUT
AS
BEGIN
    SET @totVentas = ISNULL((
        SELECT CAST(SUM(MontoTotal) AS INT)
        FROM Venta v 
        WHERE CONVERT(date, v.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta 
    ), 0);

    SET @totCompras = ISNULL((
        SELECT CAST(SUM(MontoTotal) AS INT)
        FROM Compra c 
        WHERE CONVERT(date, c.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta
    ), 0);
END
GO

IF OBJECT_ID('SP_PRODPREFERIDOS', 'P') IS NOT NULL
    DROP PROCEDURE SP_PRODPREFERIDOS
GO

CREATE PROCEDURE SP_PRODPREFERIDOS
    @fechaDesde DATE = NULL,
    @fechaHasta DATE = NULL
AS
BEGIN
    IF @fechaDesde IS NULL SET @fechaDesde = '2000-01-01';
    IF @fechaHasta IS NULL SET @fechaHasta = '2099-12-31';

    SELECT TOP 6
        p.Nombre AS Producto,
        SUM(dv.Cantidad) AS cantSalidas
    FROM Detalle_Venta dv
    INNER JOIN Venta v ON v.IdVenta = dv.IdVenta
    INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
    WHERE CONVERT(date, v.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta
    GROUP BY p.IdProducto, p.Nombre
    ORDER BY SUM(dv.Cantidad) DESC;
END
GO

-- ============================================================
-- 6. CARGA DE DATOS DE PRUEBA (VENTAS Y COMPRAS PARA GRÁFICOS)
-- ============================================================
-- Solo insertar si hay menos de 10 ventas en la tabla Venta
IF (SELECT COUNT(*) FROM Venta) < 10
BEGIN
    DECLARE @IdVendCamila INT = ISNULL((SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '4321'), 1);
    DECLARE @IdVendLucas INT = ISNULL((SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '2222'), @IdVendCamila);
    DECLARE @IdVendSofia INT = ISNULL((SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '3333'), @IdVendCamila);
    DECLARE @IdManager INT = ISNULL((SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '1234'), 1);
    DECLARE @IdCli INT = (SELECT TOP 1 IdCliente FROM Cliente);

    DECLARE @P1 INT = (SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1001); -- Nike Air Max SC ($75000)
    DECLARE @P2 INT = (SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1002); -- Adidas Superstar ($69000)
    DECLARE @P3 INT = (SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1003); -- Nike Court Vision ($82000)
    DECLARE @P4 INT = (SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1004); -- Adidas Duramo SL ($78000)
    DECLARE @P6 INT = ISNULL((SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1006), @P1); -- Nike Revolution 6 ($86000)
    DECLARE @P7 INT = ISNULL((SELECT TOP 1 IdProducto FROM Productos WHERE Codigo = 1007), @P2); -- Adidas Forum Low ($92000)

    DECLARE @ProvNike INT = ISNULL((SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike'), 1);
    DECLARE @ProvAdidas INT = ISNULL((SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas'), 1);

    DECLARE @IdV INT;
    DECLARE @IdC INT;

    -- Compras de prueba (distintas fechas)
    INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, FechaRegistro, Estado)
    VALUES (@IdManager, @ProvNike, 'Factura A', '00001', 900000.00, DATEADD(DAY, -45, GETDATE()), 1);
    SET @IdC = SCOPE_IDENTITY();
    INSERT INTO Detalle_Compra(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total, FechaRegistro)
    VALUES (@IdC, @P1, 45000, 75000, 10, 450000, DATEADD(DAY, -45, GETDATE())),
           (@IdC, @P3, 45000, 82000, 10, 450000, DATEADD(DAY, -45, GETDATE()));

    INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, FechaRegistro, Estado)
    VALUES (@IdManager, @ProvAdidas, 'Factura A', '00002', 880000.00, DATEADD(DAY, -20, GETDATE()), 1);
    SET @IdC = SCOPE_IDENTITY();
    INSERT INTO Detalle_Compra(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total, FechaRegistro)
    VALUES (@IdC, @P2, 42000, 69000, 10, 420000, DATEADD(DAY, -20, GETDATE())),
           (@IdC, @P4, 46000, 78000, 10, 460000, DATEADD(DAY, -20, GETDATE()));

    INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, FechaRegistro, Estado)
    VALUES (@IdManager, @ProvNike, 'Factura B', '00003', 500000.00, DATEADD(DAY, -5, GETDATE()), 1);
    SET @IdC = SCOPE_IDENTITY();
    INSERT INTO Detalle_Compra(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total, FechaRegistro)
    VALUES (@IdC, @P6, 50000, 86000, 10, 500000, DATEADD(DAY, -5, GETDATE()));

    -- Ventas de prueba distribuidas en el año, mes y última semana
    -- Venta 1 (Hace 120 días)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura B', '00001', 'Efectivo', 150000, 0, 150000, DATEADD(DAY, -120, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P1, 75000, 2, 150000, DATEADD(DAY, -120, GETDATE()));

    -- Venta 2 (Hace 90 días)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendLucas, @IdCli, 'Factura B', '00002', 'Tarjeta de Crédito', 229000, 0, 229000, DATEADD(DAY, -90, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P2, 69000, 2, 138000, DATEADD(DAY, -90, GETDATE())),
           (@IdV, @P7, 91000, 1, 91000, DATEADD(DAY, -90, GETDATE()));

    -- Venta 3 (Hace 60 días)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendSofia, @IdCli, 'Factura A', '00003', 'Transferencia', 246000, 0, 246000, DATEADD(DAY, -60, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P3, 82000, 3, 246000, DATEADD(DAY, -60, GETDATE()));

    -- Venta 4 (Hace 25 días - Último mes)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura B', '00004', 'Tarjeta de Débito', 153000, 0, 153000, DATEADD(DAY, -25, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P1, 75000, 1, 75000, DATEADD(DAY, -25, GETDATE())),
           (@IdV, @P4, 78000, 1, 78000, DATEADD(DAY, -25, GETDATE()));

    -- Venta 5 (Hace 18 días - Último mes)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendLucas, @IdCli, 'Factura B', '00005', 'Efectivo', 180000, 8000, 172000, DATEADD(DAY, -18, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P6, 86000, 2, 172000, DATEADD(DAY, -18, GETDATE()));

    -- Venta 6 (Hace 14 días - Último mes)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendSofia, @IdCli, 'Factura B', '00006', 'Tarjeta de Crédito', 276000, 0, 276000, DATEADD(DAY, -14, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P7, 92000, 3, 276000, DATEADD(DAY, -14, GETDATE()));

    -- Venta 7 (Hace 10 días - Último mes)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura A', '00007', 'Transferencia', 219000, 0, 219000, DATEADD(DAY, -10, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P1, 75000, 2, 150000, DATEADD(DAY, -10, GETDATE())),
           (@IdV, @P2, 69000, 1, 69000, DATEADD(DAY, -10, GETDATE()));

    -- Venta 8 (Hace 6 días - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura B', '00008', 'Efectivo', 160000, 10000, 150000, DATEADD(DAY, -6, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P1, 75000, 2, 150000, DATEADD(DAY, -6, GETDATE()));

    -- Venta 9 (Hace 5 días - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendLucas, @IdCli, 'Factura B', '00009', 'Tarjeta de Débito', 230000, 0, 230000, DATEADD(DAY, -5, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P2, 69000, 2, 138000, DATEADD(DAY, -5, GETDATE())),
           (@IdV, @P7, 92000, 1, 92000, DATEADD(DAY, -5, GETDATE()));

    -- Venta 10 (Hace 4 días - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendSofia, @IdCli, 'Factura B', '00010', 'Transferencia', 164000, 0, 164000, DATEADD(DAY, -4, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P3, 82000, 2, 164000, DATEADD(DAY, -4, GETDATE()));

    -- Venta 11 (Hace 3 días - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura A', '00011', 'Tarjeta de Crédito', 316000, 0, 316000, DATEADD(DAY, -3, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P6, 86000, 2, 172000, DATEADD(DAY, -3, GETDATE())),
           (@IdV, @P4, 72000, 2, 144000, DATEADD(DAY, -3, GETDATE()));

    -- Venta 12 (Hace 2 días - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendLucas, @IdCli, 'Factura B', '00012', 'Efectivo', 200000, 16000, 184000, DATEADD(DAY, -2, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P7, 92000, 2, 184000, DATEADD(DAY, -2, GETDATE()));

    -- Venta 13 (Hace 1 día - Última semana)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendCamila, @IdCli, 'Factura B', '00013', 'Transferencia', 225000, 0, 225000, DATEADD(DAY, -1, GETDATE()));
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P1, 75000, 3, 225000, DATEADD(DAY, -1, GETDATE()));

    -- Venta 14 (Hoy)
    INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal, FechaRegistro)
    VALUES (@IdVendSofia, @IdCli, 'Factura B', '00014', 'Tarjeta de Débito', 155000, 0, 155000, GETDATE());
    SET @IdV = SCOPE_IDENTITY();
    INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro)
    VALUES (@IdV, @P2, 69000, 1, 69000, GETDATE()),
           (@IdV, @P6, 86000, 1, 86000, GETDATE());
END
GO
