USE DBSistemaVentas;
GO

-- =======================================================================================
-- 1. CORREGIR CODIFICACIÓN Y DATOS EN NEGOCIO Y USUARIOS
-- =======================================================================================
UPDATE Negocio
SET Nombre = 'CapyCalzados',
    RUC = '30712345678',
    Direccion = 'Av. 9 de Julio 1250 - Corrientes'
WHERE IdNegocio = 1;

UPDATE Usuario
SET Nombre = 'Martín',
    Apellido = 'Rodríguez',
    NombreCompleto = 'Martín Rodríguez',
    Direccion = 'Av. 9 de Julio 1450',
    Telefono = '3794112233',
    Correo = 'manager@capycalzados.com',
    IdRol = 3,
    Estado = 1
WHERE Documento = '1234';

UPDATE Usuario
SET Nombre = 'Camila',
    Apellido = 'Gómez',
    NombreCompleto = 'Camila Gómez',
    Direccion = 'Junín 850',
    Telefono = '3794223344',
    Correo = 'camila@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE Documento = '4321';

UPDATE Usuario
SET Nombre = 'Roberto',
    Apellido = 'Sánchez',
    NombreCompleto = 'Roberto Sánchez',
    Direccion = 'Costanera Gral. San Martín 320',
    Telefono = '3794334455',
    Correo = 'gerente@capycalzados.com',
    IdRol = 1,
    Estado = 1
WHERE Documento = '1111';

UPDATE Usuario
SET Nombre = 'Lucas',
    Apellido = 'Fernández',
    NombreCompleto = 'Lucas Fernández',
    Direccion = 'Av. Independencia 2300',
    Telefono = '3794445566',
    Correo = 'lucas@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE Documento = '2222';

UPDATE Usuario
SET Nombre = 'Sofía',
    Apellido = 'Martínez',
    NombreCompleto = 'Sofía Martínez',
    Direccion = 'Hipólito Yrigoyen 1120',
    Telefono = '3794556677',
    Correo = 'sofia@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE Documento = '3333';
GO

-- =======================================================================================
-- 2. PROVEEDORES: DEJAR ÚNICAMENTE NIKE Y ADIDAS
-- =======================================================================================
IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Nike')
BEGIN
    INSERT INTO Proveedor (Documento, RazonSocial, Correo, Telefono, Estado)
    VALUES ('30701122334', 'Nike', 'ventas@nike.com.ar', '08005556453', 1);
END

IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE RazonSocial = 'Adidas')
BEGIN
    INSERT INTO Proveedor (Documento, RazonSocial, Correo, Telefono, Estado)
    VALUES ('30704455667', 'Adidas', 'contacto@adidas.com.ar', '08005552343', 1);
END

DECLARE @IdProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike' ORDER BY IdProveedor);
DECLARE @IdProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas' ORDER BY IdProveedor);

UPDATE Proveedor
SET Documento = '30701122334',
    RazonSocial = 'Nike',
    Correo = 'ventas@nike.com.ar',
    Telefono = '08005556453',
    Estado = 1
WHERE IdProveedor = @IdProvNike;

UPDATE Proveedor
SET Documento = '30704455667',
    RazonSocial = 'Adidas',
    Correo = 'contacto@adidas.com.ar',
    Telefono = '08005552343',
    Estado = 1
WHERE IdProveedor = @IdProvAdidas;

-- Reasignar referencias antes de borrar otros proveedores
UPDATE Productos SET IdProveedor = @IdProvNike WHERE IdProveedor NOT IN (@IdProvNike, @IdProvAdidas) OR IdProveedor IS NULL;
UPDATE Compra SET IdProveedor = @IdProvNike WHERE IdProveedor NOT IN (@IdProvNike, @IdProvAdidas);

DELETE FROM Proveedor WHERE IdProveedor NOT IN (@IdProvNike, @IdProvAdidas);
GO

-- =======================================================================================
-- 3. LLENAR TABLA DE CLIENTES CON DATOS REALISTAS Y BIEN FORMATEADOS
-- =======================================================================================
UPDATE Venta SET IdCliente = NULL;
DELETE FROM Cliente;
DBCC CHECKIDENT ('Cliente', RESEED, 0);

INSERT INTO Cliente (Documento, NombreCompleto, Direccion, Correo, Telefono, Estado) VALUES
('40123456', 'Juan Ignacio Pérez', 'Av. Libertad 2150', 'juan.perez@gmail.com', '3794601122', 1),
('38456789', 'María Laura González', 'San Lorenzo 940', 'mlgonzalez@hotmail.com', '3794602233', 1),
('42987654', 'Carlos Andrés López', 'Av. Maipú 1820', 'carlos.lopez@gmail.com', '3794603344', 1),
('39543210', 'Valentina Ramírez', 'Pellegrini 1105', 'valen.ramirez@yahoo.com', '3794604455', 1),
('41223344', 'Diego Nicolás Torres', 'Córdoba 760', 'diego.torres@gmail.com', '3794605566', 1),
('37889900', 'Florencia Benítez', 'Belgrano 1540', 'flor.benitez@gmail.com', '3794606677', 1),
('43112233', 'Matías Ezequiel Acosta', 'Av. Cazadores Correntinos 3100', 'matias.acosta@outlook.com', '3794607788', 1),
('36778899', 'Lucía Soledad Romero', 'Catamarca 630', 'lucia.romero@gmail.com', '3794608899', 1),
('44556677', 'Joaquín Medina', 'Salta 890', 'joaquin.medina@gmail.com', '3794609900', 1),
('35667788', 'Agustina Cabrera', 'La Rioja 1210', 'agus.cabrera@gmail.com', '3794610011', 1);
GO

-- =======================================================================================
-- 4. ACTUALIZAR Y LLENAR PRODUCTOS (SOLO ZAPATILLAS DEPORTIVAS DE NIKE Y ADIDAS)
-- =======================================================================================
DELETE FROM Detalle_Venta;
DELETE FROM Venta;
DELETE FROM Detalle_Compra;
DELETE FROM Compra;
DELETE FROM Productos;

DBCC CHECKIDENT ('Productos', RESEED, 0);
DBCC CHECKIDENT ('Compra', RESEED, 0);
DBCC CHECKIDENT ('Detalle_Compra', RESEED, 0);
DBCC CHECKIDENT ('Venta', RESEED, 0);
DBCC CHECKIDENT ('Detalle_Venta', RESEED, 0);

DECLARE @IdCatDeportiva INT = (SELECT TOP 1 IdCategoria FROM Categorias WHERE Descripcion LIKE '%Deportiva%' ORDER BY IdCategoria);
IF @IdCatDeportiva IS NULL SET @IdCatDeportiva = (SELECT TOP 1 IdCategoria FROM Categorias ORDER BY IdCategoria);

DECLARE @ProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike' ORDER BY IdProveedor);
DECLARE @ProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas' ORDER BY IdProveedor);

-- 8 Zapatillas Deportivas Activas (Nike y Adidas) + 2 Zapatillas Deportivas Inactivas (Nike y Adidas)
INSERT INTO Productos (Codigo, Nombre, Descripcion, IdCategoria, Stock, PrecioCompra, PrecioVenta, Estado, Marca, IdProveedor) VALUES
('1001', 'Nike Air Zoom Pegasus 40', 'Talle 41 - Running Deportivo Negro y Blanco', @IdCatDeportiva, 18, 52000.00, 88000.00, 1, 'Nike', @ProvNike),
('1002', 'Adidas Ultraboost Light', 'Talle 40 - Running Deportivo Blanco y Negro', @IdCatDeportiva, 15, 58000.00, 98000.00, 1, 'Adidas', @ProvAdidas),
('1003', 'Nike Revolution 6 Next', 'Talle 42 - Running Deportivo Gris y Rojo', @IdCatDeportiva, 22, 44000.00, 74000.00, 1, 'Nike', @ProvNike),
('1004', 'Adidas Duramo SL 2.0', 'Talle 41 - Running Deportivo Azul Marino', @IdCatDeportiva, 20, 46000.00, 78000.00, 1, 'Adidas', @ProvAdidas),
('1005', 'Nike Metcon 9 Training', 'Talle 43 - Entrenamiento Deportivo Negro', @IdCatDeportiva, 12, 56000.00, 94000.00, 1, 'Nike', @ProvNike),
('1006', 'Adidas Supernova Stride', 'Talle 40 - Running Deportivo Verde y Blanco', @IdCatDeportiva, 16, 50000.00, 85000.00, 1, 'Adidas', @ProvAdidas),
('1007', 'Nike Air Max Alpha Trainer 5', 'Talle 42 - Fitness Deportivo Negro y Dorado', @IdCatDeportiva, 14, 51000.00, 86000.00, 1, 'Nike', @ProvNike),
('1008', 'Adidas Adizero SL Running', 'Talle 39 - Competición Deportivo Blanco', @IdCatDeportiva, 10, 54000.00, 92000.00, 1, 'Adidas', @ProvAdidas),
('1009', 'Nike Downshifter 11 Running', 'Talle 41 - Deportivo Discontinuado Gris', @IdCatDeportiva, 0, 39000.00, 65000.00, 0, 'Nike', @ProvNike),
('1010', 'Adidas Runfalcon 2.0 TR', 'Talle 42 - Deportivo Discontinuado Negro', @IdCatDeportiva, 0, 40000.00, 68000.00, 0, 'Adidas', @ProvAdidas);
GO

-- =======================================================================================
-- 5. CARGAR COMPRAS Y VENTAS DE PRUEBA CON CLIENTES, PROVEEDORES NIKE/ADIDAS Y ACENTOS CORRECTOS
-- =======================================================================================
DECLARE @IdManager INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '1234');
DECLARE @IdVendCamila INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '4321');
DECLARE @IdVendLucas INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '2222');
DECLARE @IdVendSofia INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '3333');

DECLARE @ProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike');
DECLARE @ProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas');

-- Compras de stock a Nike y Adidas
INSERT INTO Compra (IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, FechaRegistro, Estado) VALUES
(@IdManager, @ProvNike, 'Factura A', '00001', 960000.00, DATEADD(DAY, -60, GETDATE()), 1),
(@IdManager, @ProvAdidas, 'Factura A', '00002', 1040000.00, DATEADD(DAY, -25, GETDATE()), 1),
(@IdManager, @ProvNike, 'Factura A', '00003', 856000.00, DATEADD(DAY, -6, GETDATE()), 1);

INSERT INTO Detalle_Compra (IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total, FechaRegistro) VALUES
(1, 1, 52000.00, 88000.00, 10, 520000.00, DATEADD(DAY, -60, GETDATE())),
(1, 3, 44000.00, 74000.00, 10, 440000.00, DATEADD(DAY, -60, GETDATE())),
(2, 2, 58000.00, 98000.00, 10, 580000.00, DATEADD(DAY, -25, GETDATE())),
(2, 4, 46000.00, 78000.00, 10, 460000.00, DATEADD(DAY, -25, GETDATE())),
(3, 5, 56000.00, 94000.00, 8, 448000.00, DATEADD(DAY, -6, GETDATE())),
(3, 7, 51000.00, 86000.00, 8, 408000.00, DATEADD(DAY, -6, GETDATE()));

-- 14 Ventas distribuidas en el año, último mes y última semana vinculadas a los clientes
INSERT INTO Venta (IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, MontoPago, MontoCambio, MontoTotal, FechaRegistro, FormaPago) VALUES
(@IdVendCamila, 1, 'Factura B', '00001', 180000.00, 4000.00, 176000.00, DATEADD(DAY, -120, GETDATE()), 'Efectivo'),
(@IdVendLucas,  2, 'Factura B', '00002', 196000.00, 0.00,    196000.00, DATEADD(DAY, -95, GETDATE()),  'Tarjeta de Crédito'),
(@IdVendSofia,  3, 'Factura A', '00003', 222000.00, 0.00,    222000.00, DATEADD(DAY, -70, GETDATE()),  'Transferencia'),
(@IdVendCamila, 4, 'Factura B', '00004', 160000.00, 4000.00, 156000.00, DATEADD(DAY, -45, GETDATE()),  'Efectivo'),
(@IdVendLucas,  5, 'Factura B', '00005', 266000.00, 0.00,    266000.00, DATEADD(DAY, -24, GETDATE()),  'Tarjeta de Débito'),
(@IdVendSofia,  6, 'Factura B', '00006', 170000.00, 0.00,    170000.00, DATEADD(DAY, -19, GETDATE()),  'Transferencia'),
(@IdVendCamila, 7, 'Factura A', '00007', 274000.00, 0.00,    274000.00, DATEADD(DAY, -14, GETDATE()),  'Tarjeta de Crédito'),
(@IdVendLucas,  8, 'Factura B', '00008', 190000.00, 2000.00, 188000.00, DATEADD(DAY, -10, GETDATE()),  'Efectivo'),
(@IdVendCamila, 9, 'Factura B', '00009', 184000.00, 0.00,    184000.00, DATEADD(DAY, -6, GETDATE()),   'Transferencia'),
(@IdVendSofia, 10, 'Factura B', '00010', 172000.00, 0.00,    172000.00, DATEADD(DAY, -5, GETDATE()),   'Tarjeta de Débito'),
(@IdVendLucas,  1, 'Factura A', '00011', 264000.00, 0.00,    264000.00, DATEADD(DAY, -4, GETDATE()),   'Tarjeta de Crédito'),
(@IdVendCamila, 2, 'Factura B', '00012', 235000.00, 1000.00, 234000.00, DATEADD(DAY, -3, GETDATE()),   'Efectivo'),
(@IdVendSofia,  3, 'Factura B', '00013', 196000.00, 0.00,    196000.00, DATEADD(DAY, -2, GETDATE()),   'Transferencia'),
(@IdManager,    5, 'Factura A', '00014', 250000.00, 2000.00, 248000.00, DATEADD(DAY, -1, GETDATE()),   'Efectivo');

INSERT INTO Detalle_Venta (IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro) VALUES
(1,  1, 88000.00, 2, 176000.00, DATEADD(DAY, -120, GETDATE())),
(2,  2, 98000.00, 2, 196000.00, DATEADD(DAY, -95, GETDATE())),
(3,  3, 74000.00, 3, 222000.00, DATEADD(DAY, -70, GETDATE())),
(4,  4, 78000.00, 2, 156000.00, DATEADD(DAY, -45, GETDATE())),
(5,  1, 88000.00, 2, 176000.00, DATEADD(DAY, -24, GETDATE())),
(5,  6, 90000.00, 1,  90000.00, DATEADD(DAY, -24, GETDATE())),
(6,  6, 85000.00, 2, 170000.00, DATEADD(DAY, -19, GETDATE())),
(7,  2, 98000.00, 2, 196000.00, DATEADD(DAY, -14, GETDATE())),
(7,  4, 78000.00, 1,  78000.00, DATEADD(DAY, -14, GETDATE())),
(8,  5, 94000.00, 2, 188000.00, DATEADD(DAY, -10, GETDATE())),
(9,  8, 92000.00, 2, 184000.00, DATEADD(DAY, -6, GETDATE())),
(10, 7, 86000.00, 2, 172000.00, DATEADD(DAY, -5, GETDATE())),
(11, 1, 88000.00, 3, 264000.00, DATEADD(DAY, -4, GETDATE())),
(12, 4, 78000.00, 3, 234000.00, DATEADD(DAY, -3, GETDATE())),
(13, 2, 98000.00, 2, 196000.00, DATEADD(DAY, -2, GETDATE())),
(14, 3, 74000.00, 2, 148000.00, DATEADD(DAY, -1, GETDATE())),
(14, 5, 100000.00, 1, 100000.00, DATEADD(DAY, -1, GETDATE()));
GO
