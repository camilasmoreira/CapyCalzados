USE DBSistemaVentas;
GO

-- =======================================================================================
-- 1. ACTUALIZAR USUARIOS CON DNI ARGENTINO (8 DÍGITOS) Y CONTRASEÑA '1234' PARA TODOS
-- =======================================================================================
UPDATE Usuario
SET Documento = '35123456',
    Clave = '1234',
    Nombre = 'Martín',
    Apellido = 'Rodríguez',
    NombreCompleto = 'Martín Rodríguez',
    Direccion = 'Av. 9 de Julio 1450',
    Telefono = '3794112233',
    Correo = 'manager@capycalzados.com',
    IdRol = 3,
    Estado = 1
WHERE IdUsuario = 1;

UPDATE Usuario
SET Documento = '42333444',
    Clave = '1234',
    Nombre = 'Camila',
    Apellido = 'Gómez',
    NombreCompleto = 'Camila Gómez',
    Direccion = 'Junín 850',
    Telefono = '3794223344',
    Correo = 'camila@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE IdUsuario = 2;

UPDATE Usuario
SET Documento = '28111222',
    Clave = '1234',
    Nombre = 'Roberto',
    Apellido = 'Sánchez',
    NombreCompleto = 'Roberto Sánchez',
    Direccion = 'Costanera Gral. San Martín 320',
    Telefono = '3794334455',
    Correo = 'gerente@capycalzados.com',
    IdRol = 1,
    Estado = 1
WHERE IdUsuario = 4;

UPDATE Usuario
SET Documento = '40555666',
    Clave = '1234',
    Nombre = 'Lucas',
    Apellido = 'Fernández',
    NombreCompleto = 'Lucas Fernández',
    Direccion = 'Av. Independencia 2300',
    Telefono = '3794445566',
    Correo = 'lucas@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE IdUsuario = 5;

UPDATE Usuario
SET Documento = '43777888',
    Clave = '1234',
    Nombre = 'Sofía',
    Apellido = 'Martínez',
    NombreCompleto = 'Sofía Martínez',
    Direccion = 'Hipólito Yrigoyen 1120',
    Telefono = '3794556677',
    Correo = 'sofia@capycalzados.com',
    IdRol = 2,
    Estado = 1
WHERE IdUsuario = 6;

-- Actualizar Documento de Proveedores (Nike y Adidas) a 8 dígitos para compatibilidad total con el validador
UPDATE Proveedor SET Documento = '30111222', RazonSocial = 'Nike', Correo = 'ventas@nike.com.ar', Telefono = '3794801122', Estado = 1 WHERE RazonSocial = 'Nike';
UPDATE Proveedor SET Documento = '30333444', RazonSocial = 'Adidas', Correo = 'contacto@adidas.com.ar', Telefono = '3794803344', Estado = 1 WHERE RazonSocial = 'Adidas';
GO

-- =======================================================================================
-- 2. AGREGAR COLUMNAS NOMBRE Y APELLIDO A LA TABLA CLIENTE Y PROCEDIMIENTOS ALMACENADOS
-- =======================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Cliente') AND name = 'Nombre')
BEGIN
    ALTER TABLE Cliente ADD Nombre VARCHAR(80) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Cliente') AND name = 'Apellido')
BEGIN
    ALTER TABLE Cliente ADD Apellido VARCHAR(80) NULL;
END
GO

-- Actualizar los 10 clientes con DNI argentino, Nombre y Apellido separados
UPDATE Cliente SET Documento = '40123456', Nombre = 'Juan Ignacio',   Apellido = 'Pérez',    NombreCompleto = 'Juan Ignacio Pérez',    Direccion = 'Av. Libertad 2150',              Correo = 'juan.perez@gmail.com',    Telefono = '3794601122', Estado = 1 WHERE IdCliente = 1;
UPDATE Cliente SET Documento = '38456789', Nombre = 'María Laura',    Apellido = 'González', NombreCompleto = 'María Laura González',  Direccion = 'San Lorenzo 940',                Correo = 'mlgonzalez@hotmail.com',  Telefono = '3794602233', Estado = 1 WHERE IdCliente = 2;
UPDATE Cliente SET Documento = '42987654', Nombre = 'Carlos Andrés',  Apellido = 'López',    NombreCompleto = 'Carlos Andrés López',   Direccion = 'Av. Maipú 1820',                 Correo = 'carlos.lopez@gmail.com',  Telefono = '3794603344', Estado = 1 WHERE IdCliente = 3;
UPDATE Cliente SET Documento = '39543210', Nombre = 'Valentina',      Apellido = 'Ramírez',  NombreCompleto = 'Valentina Ramírez',     Direccion = 'Pellegrini 1105',                Correo = 'valen.ramirez@yahoo.com', Telefono = '3794604455', Estado = 1 WHERE IdCliente = 4;
UPDATE Cliente SET Documento = '41223344', Nombre = 'Diego Nicolás',  Apellido = 'Torres',   NombreCompleto = 'Diego Nicolás Torres',  Direccion = 'Córdoba 760',                    Correo = 'diego.torres@gmail.com',  Telefono = '3794605566', Estado = 1 WHERE IdCliente = 5;
UPDATE Cliente SET Documento = '37889900', Nombre = 'Florencia',      Apellido = 'Benítez',  NombreCompleto = 'Florencia Benítez',     Direccion = 'Belgrano 1540',                  Correo = 'flor.benitez@gmail.com',  Telefono = '3794606677', Estado = 1 WHERE IdCliente = 6;
UPDATE Cliente SET Documento = '43112233', Nombre = 'Matías Ezequiel',Apellido = 'Acosta',   NombreCompleto = 'Matías Ezequiel Acosta',Direccion = 'Av. Cazadores Correntinos 3100', Correo = 'matias.acosta@outlook.com',Telefono = '3794607788', Estado = 1 WHERE IdCliente = 7;
UPDATE Cliente SET Documento = '36778899', Nombre = 'Lucía Soledad',  Apellido = 'Romero',   NombreCompleto = 'Lucía Soledad Romero',  Direccion = 'Catamarca 630',                  Correo = 'lucia.romero@gmail.com',  Telefono = '3794608899', Estado = 1 WHERE IdCliente = 8;
UPDATE Cliente SET Documento = '44556677', Nombre = 'Joaquín',        Apellido = 'Medina',   NombreCompleto = 'Joaquín Medina',        Direccion = 'Salta 890',                      Correo = 'joaquin.medina@gmail.com',Telefono = '3794609900', Estado = 1 WHERE IdCliente = 9;
UPDATE Cliente SET Documento = '35667788', Nombre = 'Agustina',       Apellido = 'Cabrera',  NombreCompleto = 'Agustina Cabrera',      Direccion = 'La Rioja 1210',                  Correo = 'agus.cabrera@gmail.com',  Telefono = '3794610011', Estado = 1 WHERE IdCliente = 10;
GO

-- Actualizar SP_REGISTRARCLIENTE y SP_EDITARCLIENTE para soportar Nombre y Apellido
ALTER PROC [dbo].[SP_REGISTRARCLIENTE](
    @Documento varchar(50),
    @NombreCompleto varchar(100),
    @Direccion varchar(50),
    @Correo varchar(100),
    @Telefono varchar(50),
    @Estado bit,
    @Nombre varchar(80) = '',
    @Apellido varchar(80) = '',
    @Resultado int output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    SET @Resultado = 0
    DECLARE @NombreFinal varchar(100) = @NombreCompleto
    IF (@Nombre <> '' OR @Apellido <> '')
        SET @NombreFinal = LTRIM(RTRIM(@Nombre + ' ' + @Apellido))

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
    BEGIN
        INSERT INTO CLIENTE(Documento, Nombre, Apellido, NombreCompleto, Direccion, Correo, Telefono, Estado)
        VALUES (@Documento, @Nombre, @Apellido, @NombreFinal, @Direccion, @Correo, @Telefono, @Estado)

        SET @Resultado = SCOPE_IDENTITY()
    END
    ELSE
        SET @Mensaje = 'El número de documento ya existe'
END
GO

ALTER PROC [dbo].[SP_EDITARCLIENTE](
    @IdCliente int,
    @Documento varchar(50),
    @NombreCompleto varchar(100),
    @Direccion varchar(50),
    @Correo varchar(100),
    @Telefono varchar(50),
    @Estado bit,
    @Nombre varchar(80) = '',
    @Apellido varchar(80) = '',
    @Resultado bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    SET @Resultado = 1
    DECLARE @NombreFinal varchar(100) = @NombreCompleto
    IF (@Nombre <> '' OR @Apellido <> '')
        SET @NombreFinal = LTRIM(RTRIM(@Nombre + ' ' + @Apellido))

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento AND IdCliente != @IdCliente)
    BEGIN
        UPDATE CLIENTE SET
            Documento = @Documento,
            Nombre = @Nombre,
            Apellido = @Apellido,
            NombreCompleto = @NombreFinal,
            Direccion = @Direccion,
            Correo = @Correo,
            Telefono = @Telefono,
            Estado = @Estado
        WHERE IdCliente = @IdCliente
    END
    ELSE
    BEGIN
        SET @Resultado = 0
        SET @Mensaje = 'El número de documento ya existe'
    END
END
GO
