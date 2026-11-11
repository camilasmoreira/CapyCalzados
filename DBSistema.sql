-- =======================================================================================
-- SCRIPT UNIFICADO DE BASE DE DATOS - CAPYCALZADOS (DBSistemaVentas)
-- Incluye: Creación de Base de Datos, Tablas, Tipos, Funciones, Procedimientos y Datos
-- =======================================================================================

IF DB_ID('DBSistemaVentas') IS NULL
BEGIN
    CREATE DATABASE DBSistemaVentas;
END
GO

USE DBSistemaVentas;
GO

-- =======================================================================================
-- 1. LIMPIEZA PREVIA DE OBJETOS (Permite re-ejecutar el script limpiamente)
-- =======================================================================================
IF OBJECT_ID('SP_REGISTRARUSUARIO', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARUSUARIO;
IF OBJECT_ID('SP_EDITARUSUARIO', 'P') IS NOT NULL DROP PROCEDURE SP_EDITARUSUARIO;
IF OBJECT_ID('SP_ELIMINARUSUARIO', 'P') IS NOT NULL DROP PROCEDURE SP_ELIMINARUSUARIO;
IF OBJECT_ID('SP_REGISTRARCATEGORIA', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARCATEGORIA;
IF OBJECT_ID('SP_EDITARCATEGORIA', 'P') IS NOT NULL DROP PROCEDURE SP_EDITARCATEGORIA;
IF OBJECT_ID('SP_ELIMINARCATEGORIA', 'P') IS NOT NULL DROP PROCEDURE SP_ELIMINARCATEGORIA;
IF OBJECT_ID('SP_REGISTRARPRODUCTO', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARPRODUCTO;
IF OBJECT_ID('SP_EDITARPRODUCTO', 'P') IS NOT NULL DROP PROCEDURE SP_EDITARPRODUCTO;
IF OBJECT_ID('SP_ELIMINARPRODUCTO', 'P') IS NOT NULL DROP PROCEDURE SP_ELIMINARPRODUCTO;
IF OBJECT_ID('SP_REGISTRARCLIENTE', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARCLIENTE;
IF OBJECT_ID('SP_EDITARCLIENTE', 'P') IS NOT NULL DROP PROCEDURE SP_EDITARCLIENTE;
IF OBJECT_ID('SP_ELIMINARCLIENTE', 'P') IS NOT NULL DROP PROCEDURE SP_ELIMINARCLIENTE;
IF OBJECT_ID('SP_REGISTRARPROVEEDOR', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARPROVEEDOR;
IF OBJECT_ID('SP_EDITARPROVEEDOR', 'P') IS NOT NULL DROP PROCEDURE SP_EDITARPROVEEDOR;
IF OBJECT_ID('SP_ELIMINARPROVEEDOR', 'P') IS NOT NULL DROP PROCEDURE SP_ELIMINARPROVEEDOR;
IF OBJECT_ID('SP_REGISTRARCOMPRA', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARCOMPRA;
IF OBJECT_ID('SP_VALIDARCOMPRA', 'P') IS NOT NULL DROP PROCEDURE SP_VALIDARCOMPRA;
IF OBJECT_ID('SP_REGISTRARVENTA', 'P') IS NOT NULL DROP PROCEDURE SP_REGISTRARVENTA;
IF OBJECT_ID('SP_REPORTECOMPRAS', 'P') IS NOT NULL DROP PROCEDURE SP_REPORTECOMPRAS;
IF OBJECT_ID('SP_REPORTEVENTAS', 'P') IS NOT NULL DROP PROCEDURE SP_REPORTEVENTAS;
IF OBJECT_ID('SP_DATOSGRAFICOS', 'P') IS NOT NULL DROP PROCEDURE SP_DATOSGRAFICOS;
IF OBJECT_ID('SP_PRODPREFERIDOS', 'P') IS NOT NULL DROP PROCEDURE SP_PRODPREFERIDOS;
IF OBJECT_ID('SP_GRAFICOSVFECHAS', 'P') IS NOT NULL DROP PROCEDURE SP_GRAFICOSVFECHAS;
IF OBJECT_ID('SP_PRODCATEGORIAS', 'P') IS NOT NULL DROP PROCEDURE SP_PRODCATEGORIAS;
IF OBJECT_ID('dbo.comparar', 'FN') IS NOT NULL DROP FUNCTION dbo.comparar;
GO

IF TYPE_ID('dbo.EDetalle_Compra') IS NOT NULL DROP TYPE dbo.EDetalle_Compra;
IF TYPE_ID('dbo.EDetalle_Venta') IS NOT NULL DROP TYPE dbo.EDetalle_Venta;
GO

IF OBJECT_ID('Detalle_Venta', 'U') IS NOT NULL DROP TABLE Detalle_Venta;
IF OBJECT_ID('Venta', 'U') IS NOT NULL DROP TABLE Venta;
IF OBJECT_ID('Detalle_Compra', 'U') IS NOT NULL DROP TABLE Detalle_Compra;
IF OBJECT_ID('Compra', 'U') IS NOT NULL DROP TABLE Compra;
IF OBJECT_ID('Productos', 'U') IS NOT NULL DROP TABLE Productos;
IF OBJECT_ID('Categorias', 'U') IS NOT NULL DROP TABLE Categorias;
IF OBJECT_ID('Permiso', 'U') IS NOT NULL DROP TABLE Permiso;
IF OBJECT_ID('Usuario', 'U') IS NOT NULL DROP TABLE Usuario;
IF OBJECT_ID('Cliente', 'U') IS NOT NULL DROP TABLE Cliente;
IF OBJECT_ID('Proveedor', 'U') IS NOT NULL DROP TABLE Proveedor;
IF OBJECT_ID('Rol', 'U') IS NOT NULL DROP TABLE Rol;
IF OBJECT_ID('Negocio', 'U') IS NOT NULL DROP TABLE Negocio;
IF OBJECT_ID('BackupR', 'U') IS NOT NULL DROP TABLE BackupR;
GO

-- =======================================================================================
-- 2. CREACIÓN DE LAS TABLAS
-- =======================================================================================
CREATE TABLE Rol(
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(50),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Permiso(
    IdPermiso INT PRIMARY KEY IDENTITY(1,1),
    IdRol INT REFERENCES Rol(IdRol),
    NombreMenu VARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Proveedor(
    IdProveedor INT PRIMARY KEY IDENTITY(1,1),
    Documento VARCHAR(50) UNIQUE,
    RazonSocial VARCHAR(80),
    Direccion VARCHAR(100),
    Correo VARCHAR(100) UNIQUE,
    Telefono VARCHAR(50),
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Cliente(
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Documento VARCHAR(50) UNIQUE,
    Nombre VARCHAR(80) NOT NULL,
    Apellido VARCHAR(80) NOT NULL,
    Direccion VARCHAR(100),
    Correo VARCHAR(100) UNIQUE,
    Telefono VARCHAR(50),
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Usuario(
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Documento VARCHAR(50) UNIQUE,
    Nombre VARCHAR(80) NOT NULL,
    Apellido VARCHAR(80) NOT NULL,
    Direccion VARCHAR(120) NULL,
    Telefono VARCHAR(50) NULL,
    Correo VARCHAR(100) UNIQUE,
    Clave VARCHAR(80),
    IdRol INT REFERENCES Rol(IdRol),
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Categorias(
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(80) UNIQUE,
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Productos(
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Codigo INT UNIQUE,
    Nombre VARCHAR(80),
    Descripcion VARCHAR(100),
    Marca VARCHAR(80),
    IdCategoria INT REFERENCES Categorias(IdCategoria),
    IdProveedor INT NULL REFERENCES Proveedor(IdProveedor),
    Stock INT NOT NULL DEFAULT 0,
    PrecioCompra DECIMAL(10,2) DEFAULT 0,
    PrecioVenta DECIMAL(10,2) DEFAULT 0,
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Compra(
    IdCompra INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT REFERENCES Usuario(IdUsuario),
    IdProveedor INT REFERENCES Proveedor(IdProveedor),
    TipoDocumento VARCHAR(50),
    NumeroDocumento VARCHAR(50),
    MontoTotal DECIMAL(30,2),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);
GO

CREATE TABLE Detalle_Compra(
    IdDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    IdCompra INT REFERENCES Compra(IdCompra),
    IdProducto INT REFERENCES Productos(IdProducto),
    PrecioCompra DECIMAL(10,2) DEFAULT 0,
    PrecioVenta DECIMAL(10,2) DEFAULT 0,
    Cantidad INT,
    Total DECIMAL(30,2),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Venta(
    IdVenta INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT REFERENCES Usuario(IdUsuario),
    IdCliente INT NULL REFERENCES Cliente(IdCliente),
    TipoDocumento VARCHAR(50),
    NumeroDocumento VARCHAR(50),
    FormaPago VARCHAR(50) NOT NULL DEFAULT 'Efectivo',
    MontoPago DECIMAL(30,2),
    MontoCambio DECIMAL(30,2),
    MontoTotal DECIMAL(30,2),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Detalle_Venta(
    IdDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    IdVenta INT REFERENCES Venta(IdVenta),
    IdProducto INT REFERENCES Productos(IdProducto),
    PrecioVenta DECIMAL(10,2) DEFAULT 0,
    Cantidad INT,
    SubTotal DECIMAL(30,2),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Negocio(
    IdNegocio INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(60),
    RUC VARCHAR(60),
    Direccion VARCHAR(60),
    Logo VARBINARY(MAX) NULL
);
GO

CREATE TABLE BackupR(
    idBackup INT IDENTITY(1,1) PRIMARY KEY,
    fecha_registro DATE
);
GO

-- =======================================================================================
-- 3. TIPOS DE TABLA Y FUNCIONES
-- =======================================================================================
CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
    [IdProducto] INT NULL,
    [PrecioCompra] DECIMAL(18,2) NULL,
    [PrecioVenta] DECIMAL(18,2) NULL,
    [Cantidad] INT NULL,
    [Total] DECIMAL(18,2) NULL
);
GO

CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
    [IdProducto] INT NULL,
    [PrecioVenta] DECIMAL(18,2) NULL,
    [Cantidad] INT NULL,
    [SubTotal] DECIMAL(18,2) NULL
);
GO

CREATE FUNCTION dbo.comparar(
    @fecha_registro DATE
)
RETURNS BIT
AS
BEGIN
    DECLARE @ultimafecha DATE;
    SET @ultimafecha = (SELECT MAX(fecha_registro) FROM BackupR);

    RETURN CASE 
        WHEN DATEDIFF(DAY, @ultimafecha, @fecha_registro) > 7 THEN 1
        ELSE 0
    END;
END;
GO

-- =======================================================================================
-- 4. PROCEDIMIENTOS ALMACENADOS
-- =======================================================================================

-- USUARIOS
CREATE PROCEDURE SP_REGISTRARUSUARIO
(
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80),
    @Apellido VARCHAR(80),
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

    INSERT INTO Usuario (Documento, Nombre, Apellido, Direccion, Telefono, Correo, Clave, IdRol, Estado)
    VALUES (@Documento, @Nombre, @Apellido, @Direccion, @Telefono, @Correo, @Clave, @IdRol, @Estado);

    SET @IdUsuarioResultado = SCOPE_IDENTITY();
    SET @Mensaje = 'Usuario registrado exitosamente.';
END;
GO

CREATE PROCEDURE SP_EDITARUSUARIO
(
    @IdUsuario INT,
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80),
    @Apellido VARCHAR(80),
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

    UPDATE Usuario 
    SET 
        Documento = @Documento,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Direccion = @Direccion,
        Telefono = @Telefono,
        Correo = @Correo,
        Clave = @Clave,
        IdRol = @IdRol,
        Estado = @Estado
    WHERE IdUsuario = @IdUsuario;

    SET @Respuesta = 1;
    SET @Mensaje = 'Usuario actualizado exitosamente.';
END;
GO

CREATE PROCEDURE SP_ELIMINARUSUARIO(
    @IdUsuario INT,
    @Respuesta BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Respuesta = 1;
    SET @Mensaje = 'Usuario dado de baja (Inactivo) correctamente.';
    UPDATE Usuario SET Estado = 0 WHERE IdUsuario = @IdUsuario;
END;
GO

-- CATEGORÍAS
CREATE PROCEDURE SP_REGISTRARCATEGORIA(
    @Descripcion VARCHAR(80),
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Mensaje = '';
    SET @Resultado = 0;
    IF NOT EXISTS (SELECT * FROM Categorias WHERE Descripcion = @Descripcion)
    BEGIN
        INSERT INTO Categorias(Descripcion, Estado) VALUES (@Descripcion, @Estado);
        SET @Resultado = SCOPE_IDENTITY();
    END
    ELSE
        SET @Mensaje = 'No se puede repetir la descripcion de una categoria';
END;
GO

CREATE PROCEDURE SP_EDITARCATEGORIA(
    @IdCategoria INT,
    @Descripcion VARCHAR(80),
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF NOT EXISTS(SELECT * FROM Categorias WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
    BEGIN
        UPDATE Categorias
        SET Descripcion = @Descripcion,
            Estado = @Estado
        WHERE IdCategoria = @IdCategoria;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'No se puede editar la descripcion de una categoria';
    END
END;
GO

CREATE PROCEDURE SP_ELIMINARCATEGORIA(
    @IdCategoria INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF NOT EXISTS(SELECT * FROM Categorias c 
        INNER JOIN Productos p ON p.IdCategoria = c.IdCategoria
        WHERE c.IdCategoria = @IdCategoria)
    BEGIN
        UPDATE Categorias SET Estado = 0 WHERE IdCategoria = @IdCategoria;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'No se puede eliminar la categoria';
    END
END;
GO

-- PRODUCTOS
CREATE PROCEDURE SP_REGISTRARPRODUCTO(
    @Codigo INT,
    @Nombre VARCHAR(80),
    @Descripcion VARCHAR(100),
    @Marca VARCHAR(80),
    @IdCategoria INT,
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';
    IF NOT EXISTS(SELECT * FROM Productos WHERE Codigo = @Codigo)
    BEGIN
        INSERT INTO Productos(Codigo, Nombre, Descripcion, Marca, IdCategoria, Estado)
        VALUES (@Codigo, @Nombre, @Descripcion, @Marca, @IdCategoria, @Estado);
        SET @Resultado = SCOPE_IDENTITY();
    END
    ELSE
        SET @Mensaje = 'Ya existe un producto con el mismo codigo';
END;
GO

CREATE PROCEDURE SP_EDITARPRODUCTO(
    @IdProducto INT,
    @Codigo INT,
    @Nombre VARCHAR(80),
    @Descripcion VARCHAR(100),
    @Marca VARCHAR(80),
    @IdCategoria INT,
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';
    IF NOT EXISTS(SELECT * FROM Productos WHERE Codigo = @Codigo AND IdProducto != @IdProducto)
    BEGIN
        UPDATE Productos SET
            Codigo = @Codigo,
            Nombre = @Nombre,
            Descripcion = @Descripcion,
            Marca = @Marca,
            IdCategoria = @IdCategoria,
            Estado = @Estado
        WHERE IdProducto = @IdProducto;
    END
    ELSE
    BEGIN
        SET @Mensaje = 'Ya existe un producto con el mismo codigo';
        SET @Resultado = 0;
    END
END;
GO

CREATE PROCEDURE SP_ELIMINARPRODUCTO(
    @IdProducto INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    UPDATE Productos
    SET Estado = 0
    WHERE IdProducto = @IdProducto;

    SET @Resultado = 1;
    SET @Mensaje = 'El producto fue marcado como No Activo correctamente.';
END;
GO

-- CLIENTES
CREATE PROCEDURE SP_REGISTRARCLIENTE(
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80),
    @Apellido VARCHAR(80),
    @Direccion VARCHAR(100),
    @Correo VARCHAR(100),
    @Telefono VARCHAR(50),
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF EXISTS (SELECT 1 FROM Cliente WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El número de documento ya existe.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Cliente WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado.';
        RETURN;
    END

    INSERT INTO Cliente(Documento, Nombre, Apellido, Direccion, Correo, Telefono, Estado)
    VALUES (@Documento, @Nombre, @Apellido, @Direccion, @Correo, @Telefono, @Estado);

    SET @Resultado = SCOPE_IDENTITY();
    SET @Mensaje = 'Cliente registrado exitosamente.';
END;
GO

CREATE PROCEDURE SP_EDITARCLIENTE(
    @IdCliente INT,
    @Documento VARCHAR(50),
    @Nombre VARCHAR(80),
    @Apellido VARCHAR(80),
    @Direccion VARCHAR(100),
    @Correo VARCHAR(100),
    @Telefono VARCHAR(50),
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF EXISTS (SELECT 1 FROM Cliente WHERE Documento = @Documento AND IdCliente != @IdCliente)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El número de documento ya está registrado para otro cliente.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Cliente WHERE Correo = @Correo AND IdCliente != @IdCliente)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El correo ya está registrado para otro cliente.';
        RETURN;
    END

    UPDATE Cliente SET
        Documento = @Documento,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Direccion = @Direccion,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
    WHERE IdCliente = @IdCliente;

    SET @Mensaje = 'Cliente editado exitosamente.';
END;
GO

CREATE PROCEDURE SP_ELIMINARCLIENTE(
    @IdCliente INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF EXISTS(SELECT * FROM Cliente WHERE IdCliente = @IdCliente)
    BEGIN
        UPDATE Cliente SET Estado = 0 WHERE IdCliente = @IdCliente;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'No se puede eliminar el cliente';
    END
END;
GO

-- PROVEEDORES
CREATE PROCEDURE SP_REGISTRARPROVEEDOR
(
    @Documento VARCHAR(50),
    @RazonSocial VARCHAR(80),
    @Direccion VARCHAR(100),
    @Correo VARCHAR(100),
    @Telefono VARCHAR(50),
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    IF EXISTS (SELECT 1 FROM Proveedor WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El número de documento ya existe.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado.';
        RETURN;
    END

    INSERT INTO Proveedor (Documento, RazonSocial, Direccion, Correo, Telefono, Estado)
    VALUES (@Documento, @RazonSocial, @Direccion, @Correo, @Telefono, @Estado);

    SET @Resultado = SCOPE_IDENTITY();
    SET @Mensaje = 'Proveedor registrado exitosamente.';
END;
GO

CREATE PROCEDURE SP_EDITARPROVEEDOR
(
    @IdProveedor INT,
    @Documento VARCHAR(50),
    @RazonSocial VARCHAR(80),
    @Direccion VARCHAR(100),
    @Correo VARCHAR(100),
    @Telefono VARCHAR(50),
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF EXISTS (SELECT 1 FROM Proveedor WHERE Documento = @Documento AND IdProveedor != @IdProveedor)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El número de documento ya está registrado para otro proveedor.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo AND IdProveedor != @IdProveedor)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El correo ya está registrado para otro proveedor.';
        RETURN;
    END

    UPDATE Proveedor 
    SET 
        Documento = @Documento,
        RazonSocial = @RazonSocial,
        Direccion = @Direccion,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
    WHERE IdProveedor = @IdProveedor;

    SET @Mensaje = 'Proveedor editado exitosamente.';
END;
GO

CREATE PROCEDURE SP_ELIMINARPROVEEDOR(
    @IdProveedor INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = '';

    IF NOT EXISTS(SELECT * FROM Proveedor p
        INNER JOIN Compra c ON p.IdProveedor = c.IdProveedor
        WHERE p.IdProveedor = @IdProveedor)
    BEGIN
        UPDATE Proveedor SET Estado = 0 WHERE IdProveedor = @IdProveedor;
    END
    ELSE
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'No se puede eliminar el proveedor, se encuentra relacionado con una compra';
    END
END;
GO

-- COMPRAS
CREATE PROCEDURE SP_REGISTRARCOMPRA(
    @IdUsuario INT,
    @IdProveedor INT,
    @TipoDocumento VARCHAR(50),
    @NumeroDocumento VARCHAR(50),
    @MontoTotal DECIMAL(30,2),
    @DetalleCompra [EDetalle_Compra] READONLY,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idcompra INT = 0;
        SET @Resultado = 1;
        SET @Mensaje = '';

        IF EXISTS (SELECT 1 FROM Compra WHERE NumeroDocumento = @NumeroDocumento)
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'La compra con el número de documento ' + @NumeroDocumento + ' ya está ingresada.';
            RETURN;
        END

        BEGIN TRANSACTION registro;

        INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, Estado)
        VALUES(@IdUsuario, @IdProveedor, @TipoDocumento, @NumeroDocumento, @MontoTotal, 1);

        SET @idcompra = SCOPE_IDENTITY();

        INSERT INTO Detalle_Compra(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total)
        SELECT @idcompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total FROM @DetalleCompra;

        UPDATE p SET 
            p.PrecioCompra = dc.PrecioCompra,
            p.PrecioVenta = dc.PrecioVenta,
            p.Stock = p.Stock + dc.Cantidad,
            p.IdProveedor = @IdProveedor
        FROM Productos p
        INNER JOIN @DetalleCompra dc ON dc.IdProducto = p.IdProducto;

        COMMIT TRANSACTION registro;
        SET @Mensaje = 'Compra registrada exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION registro;
    END CATCH
END;
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
END;
GO

-- VENTAS
CREATE PROCEDURE SP_REGISTRARVENTA(
    @IdUsuario INT,
    @IdCliente INT = NULL,
    @TipoDocumento VARCHAR(50),
    @NumeroDocumento VARCHAR(50),
    @DocumentoCliente VARCHAR(50) = '',
    @NombreCliente VARCHAR(80) = '',
    @FormaPago VARCHAR(50) = 'Efectivo',
    @MontoPago DECIMAL(30,2),
    @MontoCambio DECIMAL(30,2),
    @MontoTotal DECIMAL(30,2),
    @DetalleVenta [EDetalle_Venta] READONLY,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idventa INT = 0;
        SET @Resultado = 1;
        SET @Mensaje = '';

        IF EXISTS (SELECT 1 FROM Venta WHERE NumeroDocumento = @NumeroDocumento)
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'La venta con el número de documento ' + @NumeroDocumento + ' ya está ingresada.';
            RETURN;
        END

        BEGIN TRANSACTION registro;

        INSERT INTO Venta(IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, FormaPago, MontoPago, MontoCambio, MontoTotal)
        VALUES(@IdUsuario, IIF(@IdCliente = 0, NULL, @IdCliente), @TipoDocumento, @NumeroDocumento, ISNULL(@FormaPago, 'Efectivo'), @MontoPago, @MontoCambio, @MontoTotal);

        SET @idventa = SCOPE_IDENTITY();

        INSERT INTO Detalle_Venta(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal)
        SELECT @idventa, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleVenta;

        COMMIT TRANSACTION registro;
        SET @Mensaje = 'Venta registrada exitosamente.';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION registro;
    END CATCH
END;
GO

-- REPORTES Y GRÁFICOS
CREATE PROCEDURE SP_REPORTECOMPRAS(
    @fechainicio DATE,
    @fechafin DATE,
    @idproveedor INT
)
AS
BEGIN
    SELECT 
        CONVERT(CHAR(10), c.FechaRegistro, 103) AS [FechaRegistro], 
        c.TipoDocumento,
        c.NumeroDocumento,
        c.MontoTotal,
        LTRIM(RTRIM(ISNULL(u.Nombre, '') + ' ' + ISNULL(u.Apellido, ''))) AS [UsuarioRegistro],
        pr.Documento AS [DocumentoProveedor], 
        pr.RazonSocial,
        p.Codigo AS [CodigoProducto],
        p.Nombre AS [NombreProducto],
        ca.Descripcion AS [Categoria],
        dc.PrecioCompra,
        dc.PrecioVenta,
        dc.Cantidad,
        dc.Total AS [SubTotal]
    FROM Compra c
    INNER JOIN Usuario u ON u.IdUsuario = c.IdUsuario
    INNER JOIN Proveedor pr ON pr.IdProveedor = c.IdProveedor
    INNER JOIN Detalle_Compra dc ON dc.IdCompra = c.IdCompra
    INNER JOIN Productos p ON p.IdProducto = dc.IdProducto
    INNER JOIN Categorias ca ON ca.IdCategoria = p.IdCategoria
    WHERE CONVERT(DATE, c.FechaRegistro) BETWEEN @fechainicio AND @fechafin
      AND pr.IdProveedor = IIF(@idproveedor = 0, pr.IdProveedor, @idproveedor);
END;
GO

CREATE PROCEDURE SP_REPORTEVENTAS(
    @fechainicio DATE,
    @fechafin DATE,
    @idusuario INT = 0
)
AS
BEGIN
    SELECT 
        CONVERT(CHAR(10), v.FechaRegistro, 103) AS [FechaRegistro], 
        v.TipoDocumento,
        v.NumeroDocumento,
        ISNULL(v.FormaPago, 'Efectivo') AS [FormaPago],
        v.MontoTotal,
        LTRIM(RTRIM(ISNULL(u.Nombre, '') + ' ' + ISNULL(u.Apellido, ''))) AS [UsuarioRegistro],
        ISNULL(c.Documento, '') AS [DocumentoCliente],
        ISNULL(NULLIF(LTRIM(RTRIM(ISNULL(c.Nombre, '') + ' ' + ISNULL(c.Apellido, ''))), ''), 'Consumidor Final') AS [NombreCliente],
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
    WHERE CONVERT(DATE, v.FechaRegistro) BETWEEN @fechainicio AND @fechafin
      AND (@idusuario = 0 OR v.IdUsuario = @idusuario);
END;
GO

CREATE PROCEDURE SP_DATOSGRAFICOS
    @totVentas INT OUT,
    @totCompras INT OUT,
    @totProveedores INT OUT,
    @totProd INT OUT,
    @totCat INT OUT,
    @totUser INT OUT,
    @totClientes INT OUT
AS
BEGIN
    SET @totVentas = ISNULL((SELECT CAST(SUM(MontoTotal) AS INT) FROM Venta), 0);
    SET @totCompras = ISNULL((SELECT CAST(SUM(c.MontoTotal) AS INT) FROM Compra AS c), 0);
    SET @totProveedores = ISNULL((SELECT COUNT(IdProveedor) FROM Proveedor), 0);
    SET @totProd = ISNULL((SELECT COUNT(IdProducto) FROM Productos), 0);
    SET @totCat = ISNULL((SELECT COUNT(IdCategoria) FROM Categorias), 0);
    SET @totUser = ISNULL((SELECT COUNT(IdUsuario) FROM Usuario), 0);
    SET @totClientes = ISNULL((SELECT COUNT(IdCliente) FROM Cliente), 0);
END;
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
        WHERE CONVERT(DATE, v.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta 
    ), 0);

    SET @totCompras = ISNULL((
        SELECT CAST(SUM(MontoTotal) AS INT)
        FROM Compra c 
        WHERE CONVERT(DATE, c.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta
    ), 0);
END;
GO

CREATE PROCEDURE SP_PRODPREFERIDOS
    @fechaDesde DATE = NULL,
    @fechaHasta DATE = NULL
AS
BEGIN
    IF @fechaDesde IS NULL SET @fechaDesde = '2000-01-01';
    IF @fechaHasta IS NULL SET @fechaHasta = '2099-12-31';

    SELECT TOP 5
        p.Nombre AS Producto,
        SUM(dv.Cantidad) AS cantSalidas
    FROM Detalle_Venta dv
    INNER JOIN Venta v ON v.IdVenta = dv.IdVenta
    INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
    WHERE CONVERT(DATE, v.FechaRegistro) BETWEEN @fechaDesde AND @fechaHasta
    GROUP BY p.IdProducto, p.Nombre
    ORDER BY SUM(dv.Cantidad) DESC;
END;
GO

CREATE PROCEDURE SP_PRODCATEGORIAS
AS
BEGIN
    SELECT c.Descripcion, COUNT(c.IdCategoria) AS Cant 
    FROM Productos AS p
    INNER JOIN Categorias c ON c.IdCategoria = p.IdCategoria
    GROUP BY c.IdCategoria, c.Descripcion
    ORDER BY COUNT(2);
END;
GO

-- =======================================================================================
-- 5. CARGA DE DATOS INICIALES (SEED DATA COMPLETO Y LIMPIO)
-- =======================================================================================

-- Datos del Negocio
INSERT INTO Negocio(Nombre, RUC, Direccion) 
VALUES ('CapyCalzados', '30712345678', 'Av. 9 de Julio 1250 - Corrientes');
GO

-- Roles (1 = Gerente, 2 = Vendedor, 3 = Manager)
INSERT INTO Rol (Descripcion) VALUES
('Gerente'),
('Vendedor'),
('Manager');
GO

-- Permisos GERENTE (IdRol = 1): Estrictamente analítico
INSERT INTO Permiso(IdRol, NombreMenu) VALUES
(1, 'menuInicio'),
(1, 'menuGraficos'),
(1, 'menuReportes');

-- Permisos VENDEDOR (IdRol = 2): Ventas, Clientes, visualización de Productos y Reporte de sus propias Ventas
INSERT INTO Permiso(IdRol, NombreMenu) VALUES
(2, 'menuInicio'),
(2, 'menuVentas'),
(2, 'menuClientes'),
(2, 'menuMantenimiento'),
(2, 'menuReportes');

-- Permisos MANAGER (IdRol = 3): Perfil operativo total
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

-- Usuarios (DNI Argentino de 8 dígitos y contraseña '1234' para todos)
INSERT INTO Usuario (Documento, Nombre, Apellido, Direccion, Telefono, Correo, Clave, IdRol, Estado) VALUES
('35123456', 'Martín',  'Rodríguez', 'Av. 9 de Julio 1450',           '3794112233', 'manager@capycalzados.com', '1234', 3, 1),
('42333444', 'Camila',  'Gómez',     'Junín 850',                     '3794223344', 'camila@capycalzados.com',  '1234', 2, 1),
('28111222', 'Roberto', 'Sánchez',   'Costanera Gral. San Martín 320','3794334455', 'gerente@capycalzados.com', '1234', 1, 1),
('40555666', 'Lucas',   'Fernández', 'Av. Independencia 2300',        '3794445566', 'lucas@capycalzados.com',   '1234', 2, 1),
('43777888', 'Sofía',   'Martínez',  'Hipólito Yrigoyen 1120',        '3794556677', 'sofia@capycalzados.com',   '1234', 2, 1);
GO

-- Categorías
INSERT INTO Categorias(Descripcion, Estado) VALUES
('Zapatillas Urbanas', 1),
('Zapatillas Deportivas', 1);
GO

-- Proveedores (Nike y Adidas)
INSERT INTO Proveedor (Documento, RazonSocial, Direccion, Correo, Telefono, Estado) VALUES
('30111222', 'Nike',   'Av. Libertador 1000', 'ventas@nike.com.ar',     '3794801122', 1),
('30333444', 'Adidas', 'Av. Santa Fe 2000',   'contacto@adidas.com.ar', '3794803344', 1);
GO

-- Clientes (10 clientes con DNI argentino, Nombre y Apellido separados)
INSERT INTO Cliente (Documento, Nombre, Apellido, Direccion, Correo, Telefono, Estado) VALUES
('40123456', 'Juan Ignacio',    'Pérez',    'Av. Libertad 2150',              'juan.perez@gmail.com',     '3794601122', 1),
('38456789', 'María Laura',     'González', 'San Lorenzo 940',                'mlgonzalez@hotmail.com',   '3794602233', 1),
('42987654', 'Carlos Andrés',   'López',    'Av. Maipú 1820',                 'carlos.lopez@gmail.com',   '3794603344', 1),
('39543210', 'Valentina',       'Ramírez',  'Pellegrini 1105',                'valen.ramirez@yahoo.com',  '3794604455', 1),
('41223344', 'Diego Nicolás',   'Torres',   'Córdoba 760',                    'diego.torres@gmail.com',   '3794605566', 1),
('37889900', 'Florencia',       'Benítez',  'Belgrano 1540',                  'flor.benitez@gmail.com',   '3794606677', 1),
('43112233', 'Matías Ezequiel', 'Acosta',   'Av. Cazadores Correntinos 3100', 'matias.acosta@outlook.com','3794607788', 1),
('36778899', 'Lucía Soledad',   'Romero',   'Catamarca 630',                  'lucia.romero@gmail.com',   '3794608899', 1),
('44556677', 'Joaquín',         'Medina',   'Salta 890',                      'joaquin.medina@gmail.com', '3794609900', 1),
('35667788', 'Agustina',        'Cabrera',  'La Rioja 1210',                  'agus.cabrera@gmail.com',   '3794610011', 1);
GO

-- Productos (8 Zapatillas Activas + 2 Inactivas de Nike y Adidas)
DECLARE @IdCatDeportiva INT = (SELECT TOP 1 IdCategoria FROM Categorias WHERE Descripcion = 'Zapatillas Deportivas');
DECLARE @ProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike');
DECLARE @ProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas');

INSERT INTO Productos (Codigo, Nombre, Descripcion, IdCategoria, Stock, PrecioCompra, PrecioVenta, Estado, Marca, IdProveedor) VALUES
(1001, 'Nike Air Zoom Pegasus 40',     'Talle 41 - Running Deportivo Negro y Blanco', @IdCatDeportiva, 18, 52000.00, 88000.00, 1, 'Nike',   @ProvNike),
(1002, 'Adidas Ultraboost Light',      'Talle 40 - Running Deportivo Blanco y Negro', @IdCatDeportiva, 15, 58000.00, 98000.00, 1, 'Adidas', @ProvAdidas),
(1003, 'Nike Revolution 6 Next',       'Talle 42 - Running Deportivo Gris y Rojo',    @IdCatDeportiva, 22, 44000.00, 74000.00, 1, 'Nike',   @ProvNike),
(1004, 'Adidas Duramo SL 2.0',         'Talle 41 - Running Deportivo Azul Marino',    @IdCatDeportiva, 20, 46000.00, 78000.00, 1, 'Adidas', @ProvAdidas),
(1005, 'Nike Metcon 9 Training',       'Talle 43 - Entrenamiento Deportivo Negro',    @IdCatDeportiva, 12, 56000.00, 94000.00, 1, 'Nike',   @ProvNike),
(1006, 'Adidas Supernova Stride',      'Talle 40 - Running Deportivo Verde y Blanco', @IdCatDeportiva, 16, 50000.00, 85000.00, 1, 'Adidas', @ProvAdidas),
(1007, 'Nike Air Max Alpha Trainer 5', 'Talle 42 - Fitness Deportivo Negro y Dorado', @IdCatDeportiva, 14, 51000.00, 86000.00, 1, 'Nike',   @ProvNike),
(1008, 'Adidas Adizero SL Running',    'Talle 39 - Competición Deportivo Blanco',     @IdCatDeportiva, 10, 54000.00, 92000.00, 1, 'Adidas', @ProvAdidas),
(1009, 'Nike Downshifter 11 Running',  'Talle 41 - Deportivo Discontinuado Gris',     @IdCatDeportiva, 0,  39000.00, 65000.00, 0, 'Nike',   @ProvNike),
(1010, 'Adidas Runfalcon 2.0 TR',      'Talle 42 - Deportivo Discontinuado Negro',    @IdCatDeportiva, 0,  40000.00, 68000.00, 0, 'Adidas', @ProvAdidas);
GO

-- Compras y Ventas de prueba
DECLARE @IdManager INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '35123456');
DECLARE @IdVendCamila INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '42333444');
DECLARE @IdVendLucas INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '40555666');
DECLARE @IdVendSofia INT = (SELECT TOP 1 IdUsuario FROM Usuario WHERE Documento = '43777888');

DECLARE @ProvNike INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Nike');
DECLARE @ProvAdidas INT = (SELECT TOP 1 IdProveedor FROM Proveedor WHERE RazonSocial = 'Adidas');

INSERT INTO Compra (IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal, FechaRegistro, Estado) VALUES
(@IdManager, @ProvNike,   'Factura A', '00001', 960000.00,  DATEADD(DAY, -60, GETDATE()), 1),
(@IdManager, @ProvAdidas, 'Factura A', '00002', 1040000.00, DATEADD(DAY, -25, GETDATE()), 1),
(@IdManager, @ProvNike,   'Factura A', '00003', 856000.00,  DATEADD(DAY, -6,  GETDATE()), 1);

INSERT INTO Detalle_Compra (IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, Total, FechaRegistro) VALUES
(1, 1, 52000.00, 88000.00, 10, 520000.00, DATEADD(DAY, -60, GETDATE())),
(1, 3, 44000.00, 74000.00, 10, 440000.00, DATEADD(DAY, -60, GETDATE())),
(2, 2, 58000.00, 98000.00, 10, 580000.00, DATEADD(DAY, -25, GETDATE())),
(2, 4, 46000.00, 78000.00, 10, 460000.00, DATEADD(DAY, -25, GETDATE())),
(3, 5, 56000.00, 94000.00, 8,  448000.00, DATEADD(DAY, -6,  GETDATE())),
(3, 7, 51000.00, 86000.00, 8,  408000.00, DATEADD(DAY, -6,  GETDATE()));

INSERT INTO Venta (IdUsuario, IdCliente, TipoDocumento, NumeroDocumento, MontoPago, MontoCambio, MontoTotal, FechaRegistro, FormaPago) VALUES
(@IdVendCamila, 1,  'Factura B', '00001', 180000.00, 4000.00, 176000.00, DATEADD(DAY, -120, GETDATE()), 'Efectivo'),
(@IdVendLucas,  2,  'Factura B', '00002', 196000.00, 0.00,    196000.00, DATEADD(DAY, -95,  GETDATE()), 'Tarjeta de Crédito'),
(@IdVendSofia,  3,  'Factura A', '00003', 222000.00, 0.00,    222000.00, DATEADD(DAY, -70,  GETDATE()), 'Transferencia'),
(@IdVendCamila, 4,  'Factura B', '00004', 160000.00, 4000.00, 156000.00, DATEADD(DAY, -45,  GETDATE()), 'Efectivo'),
(@IdVendLucas,  5,  'Factura B', '00005', 266000.00, 0.00,    266000.00, DATEADD(DAY, -24,  GETDATE()), 'Tarjeta de Débito'),
(@IdVendSofia,  6,  'Factura B', '00006', 170000.00, 0.00,    170000.00, DATEADD(DAY, -19,  GETDATE()), 'Transferencia'),
(@IdVendCamila, 7,  'Factura A', '00007', 274000.00, 0.00,    274000.00, DATEADD(DAY, -14,  GETDATE()), 'Tarjeta de Crédito'),
(@IdVendLucas,  8,  'Factura B', '00008', 190000.00, 2000.00, 188000.00, DATEADD(DAY, -10,  GETDATE()), 'Efectivo'),
(@IdVendCamila, 9,  'Factura B', '00009', 184000.00, 0.00,    184000.00, DATEADD(DAY, -6,   GETDATE()), 'Transferencia'),
(@IdVendSofia,  10, 'Factura B', '00010', 172000.00, 0.00,    172000.00, DATEADD(DAY, -5,   GETDATE()), 'Tarjeta de Débito'),
(@IdVendLucas,  1,  'Factura A', '00011', 264000.00, 0.00,    264000.00, DATEADD(DAY, -4,   GETDATE()), 'Tarjeta de Crédito'),
(@IdVendCamila, 2,  'Factura B', '00012', 235000.00, 1000.00, 234000.00, DATEADD(DAY, -3,   GETDATE()), 'Efectivo'),
(@IdVendSofia,  3,  'Factura B', '00013', 196000.00, 0.00,    196000.00, DATEADD(DAY, -2,   GETDATE()), 'Transferencia'),
(@IdManager,    5,  'Factura A', '00014', 250000.00, 2000.00, 248000.00, DATEADD(DAY, -1,   GETDATE()), 'Efectivo');

INSERT INTO Detalle_Venta (IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal, FechaRegistro) VALUES
(1,  1, 88000.00,  2, 176000.00, DATEADD(DAY, -120, GETDATE())),
(2,  2, 98000.00,  2, 196000.00, DATEADD(DAY, -95,  GETDATE())),
(3,  3, 74000.00,  3, 222000.00, DATEADD(DAY, -70,  GETDATE())),
(4,  4, 78000.00,  2, 156000.00, DATEADD(DAY, -45,  GETDATE())),
(5,  1, 88000.00,  2, 176000.00, DATEADD(DAY, -24,  GETDATE())),
(5,  6, 90000.00,  1,  90000.00, DATEADD(DAY, -24,  GETDATE())),
(6,  6, 85000.00,  2, 170000.00, DATEADD(DAY, -19,  GETDATE())),
(7,  2, 98000.00,  2, 196000.00, DATEADD(DAY, -14,  GETDATE())),
(7,  4, 78000.00,  1,  78000.00, DATEADD(DAY, -14,  GETDATE())),
(8,  5, 94000.00,  2, 188000.00, DATEADD(DAY, -10,  GETDATE())),
(9,  8, 92000.00,  2, 184000.00, DATEADD(DAY, -6,   GETDATE())),
(10, 7, 86000.00,  2, 172000.00, DATEADD(DAY, -5,   GETDATE())),
(11, 1, 88000.00,  3, 264000.00, DATEADD(DAY, -4,   GETDATE())),
(12, 4, 78000.00,  3, 234000.00, DATEADD(DAY, -3,   GETDATE())),
(13, 2, 98000.00,  2, 196000.00, DATEADD(DAY, -2,   GETDATE())),
(14, 3, 74000.00,  2, 148000.00, DATEADD(DAY, -1,   GETDATE())),
(14, 5, 100000.00, 1, 100000.00, DATEADD(DAY, -1,   GETDATE()));
GO

-- Registro inicial de Backup
INSERT INTO BackupR(fecha_registro) VALUES (GETDATE());
GO
