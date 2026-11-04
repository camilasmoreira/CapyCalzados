
--Creacion de la base de datos
CREATE DATABASE DBSistemaVentas
GO
--Conectarse a la base de datos
USE DBSistemaVentas
GO


--Creacion de las tablas
CREATE TABLE Rol(
IdRol int primary key identity,
Descripcion varchar(50),
FechaRegistro datetime default getdate()
)
GO

CREATE TABLE Permiso(
IdPermiso int primary key identity,
IdRol int references Rol(IdRol),
NombreMenu varchar(100),
FechaRegistro datetime default getDate()
)
GO


create TABLE Proveedor(
IdProveedor int primary key identity,
Documento varchar(50) unique,
RazonSocial varchar(80),
Direccion varchar(100),
Correo varchar(100) unique,
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getDate()
)
GO


CREATE TABLE Cliente(
IdCliente int primary key identity,
Documento varchar(50) unique,
NombreCompleto varchar(100),
Direccion varchar(100),
Correo varchar(100) unique,
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Usuario(
IdUsuario int primary key identity,
Documento varchar(50) unique,
NombreCompleto varchar(100),
Correo varchar(100) unique,
Clave varchar(80),
IdRol int references Rol(IdRol),
Estado bit,
FechaRegistro datetime default getDate()
)
GO



CREATE TABLE Categorias(
IdCategoria int primary key identity,
Descripcion varchar(80) unique,
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Productos(
IdProducto int primary key identity,
Codigo int unique,
Nombre varchar(80),
Descripcion varchar(100),
Marca varchar(80), --Se agrega el campo marca
IdCategoria int references Categorias(IdCategoria),
Stock int not null default 0,
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Compra(
IdCompra int primary key identity,
IdUsuario int references Usuario(IdUsuario),
IdProveedor int references Proveedor(IdProveedor),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
MontoTotal decimal(30,2),
FechaRegistro datetime default getDate(),
Estado bit default 0
)
GO



CREATE TABLE Detalle_Compra(
IdDetalleCompra int primary key identity,
IdCompra int references Compra(IdCompra),
IdProducto int references Productos(IdProducto),
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
Cantidad int,
Total decimal(30,2),
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Venta(
IdVenta int primary key identity,
IdUsuario int references Usuario(IdUsuario),
IdCliente int references Cliente(IdCliente),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
DocumentoCliente varchar(50),
NombreCliente varchar(80),
MontoPago decimal(30,2),
MontoCambio decimal(30,2),
MontoTotal decimal(30,2),
--CodCupon int references Cupon(CodCupon), Para segunda entrega implementar sistema de cupones
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Detalle_Venta(
IdDetalleVenta int primary key identity,
IdVenta int references Venta(IdVenta),
IdProducto int references Productos(IdProducto),
PrecioVenta decimal(10,2) default 0,
Cantidad int,
SubTotal decimal(30,2),
FechaRegistro datetime default getDate()
)
GO



CREATE TABLE Negocio(
IdNegocio int primary key identity,
Nombre varchar(60),
RUC varchar(60),
Direccion varchar(60),
Logo varbinary(Max) NULL
)
go



--Creacion de los procedimientos

CREATE PROCEDURE SP_REGISTRARUSUARIO
(
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
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

    -- Validar si existe el documento
    IF EXISTS(SELECT 1 FROM Usuario WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El documento ya está registrado para otro usuario.';
        RETURN;
    END

    -- Validar si existe el correo
    IF EXISTS(SELECT 1 FROM Usuario WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado para otro usuario.';
        RETURN;
    END

    -- Si no existen ni el documento ni el correo, proceder con la inserción
    INSERT INTO Usuario (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
    VALUES (@Documento, @NombreCompleto, @Correo, @Clave, @IdRol, @Estado);

    -- Obtener el Id del usuario recién insertado
    SET @IdUsuarioResultado = SCOPE_IDENTITY();

    -- Mensaje de éxito
    SET @Mensaje = 'Usuario registrado exitosamente.';
END;
GO

CREATE PROCEDURE SP_EDITARUSUARIO
(
    @IdUsuario INT,
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
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

    -- Validar si el documento ya existe en otro usuario
    IF EXISTS(SELECT 1 FROM Usuario WHERE Documento = @Documento AND IdUsuario != @IdUsuario)
    BEGIN
        SET @Mensaje = 'El documento ya está registrado para otro usuario.';
        RETURN;
    END

    -- Validar si el correo ya existe en otro usuario
    IF EXISTS(SELECT 1 FROM Usuario WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado para otro usuario.';
        RETURN;
    END

    -- Actualizar el usuario si no existen duplicados de documento ni correo
    UPDATE Usuario 
    SET 
        Documento = @Documento,
        NombreCompleto = @NombreCompleto,
        Correo = @Correo,
        Clave = @Clave,
        IdRol = @IdRol,
        Estado = @Estado
    WHERE IdUsuario = @IdUsuario;

    -- Respuesta exitosa y mensaje
    SET @Respuesta = 1;
    SET @Mensaje = 'Usuario actualizado exitosamente.';
END;
GO



create PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @bandera bit = 1

	if exists (select * from Compra c 
	inner join Usuario u on u.IdUsuario = c.IdUsuario
	where u.IdUsuario = @IdUsuario)
	begin
		set @bandera = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una compra\n'
	end


		if exists (select * from Venta v 
	inner join Usuario u on u.IdUsuario = v.IdUsuario
	where u.IdUsuario = @IdUsuario)
	begin
		set @bandera = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una venta\n'
	end


	if(@bandera = 1)
	begin
		Update Usuario set
		Estado = 0
		where IdUsuario = @IdUsuario
		--delete from Usuario where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end


end
go

/*Procedimiento para registrar una categoria*/

Create procedure SP_REGISTRARCATEGORIA(
@Descripcion varchar(80),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	set @Mensaje = ''
	set @Resultado = 0
	if not exists (select * from Categorias where Descripcion = @Descripcion)
	begin
		insert into Categorias(Descripcion,Estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	else
	set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end

go


/*Procedimiento modificar Categorias*/

Create procedure SP_EDITARCATEGORIA(
@IdCategoria int,
@Descripcion varchar(80),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''


	if not exists(select * from Categorias WHERE Descripcion = @Descripcion and IdCategoria != @IdCategoria)
	begin
UPDATE Categorias
SET 
    Descripcion = @Descripcion, -- Nueva descripción para la categoría
    Estado = @Estado            -- Nuevo estado para la categoría
WHERE 
    IdCategoria = @IdCategoria  -- ID de la categoría a actualizar
	end
	else
		begin
			set @Resultado = 0
			set @Mensaje = 'No se puede editar la descripcion de una categoria'
		end
end
go

create PROC SP_ELIMINARCATEGORIA(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

	if not exists(select * from Categorias c 
	inner join Productos p on p.IdCategoria = c.IdCategoria
	where c.IdCategoria = @IdCategoria
	)
	begin
		update Categorias set
		Estado = 0
		where IdCategoria = @IdCategoria
		--delete top(1) from Categorias where IdCategoria = @IdCategoria
	end
	else
		begin
			set @Resultado = 0
			set @Mensaje = 'No se puede eliminar la categoria'
		end

end
go





Create procedure SP_REGISTRARPRODUCTO(
@Codigo int,
@Nombre varchar(80),
@Descripcion varchar(100),
@Marca varchar(80),
@IdCategoria int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 0
	if not exists(select * from Productos where Codigo = @Codigo)
	begin
		insert into Productos(Codigo,Nombre,Descripcion,Marca,IdCategoria,Estado) values (@Codigo,@Nombre,@Descripcion,@Marca,@IdCategoria,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'Ya existe un producto con el mismo codigo'
end
GO

create procedure SP_EDITARPRODUCTO(
@IdProducto int,
@Codigo int,
@Nombre varchar(80),
@Descripcion varchar(100),
@Marca varchar(80),
@IdCategoria int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 1
	if not exists(select * from Productos where Codigo = @Codigo and IdProducto != @IdProducto )
	
		update Productos set
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		Marca = @Marca,
		IdCategoria = @IdCategoria,
		Estado = @Estado
		where IdProducto = @IdProducto
	
	else
	begin
		set @Mensaje = 'Ya existe un producto con el mismo codigo'
		set @Resultado = 0
	end
	
end
GO

create PROC SP_ELIMINARPRODUCTO(
@IdProducto int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	set @Mensaje = ''
	
	declare @bandera bit = 1

	if exists(select * from Detalle_Compra dc 
	inner join Productos p on p.IdProducto = dc.IdProducto
	where p.IdProducto = @IdProducto
	)
	begin
		set @bandera = 0
		set @Resultado =0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque esta relacionado a una compra\n'
	end
		if exists(select * from Detalle_Venta dv 
	inner join Productos p on p.IdProducto = dv.IdProducto
	where p.IdProducto = @IdProducto
	)
	begin
		set @bandera = 0
		set @Resultado =0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque esta relacionado a una venta\n'
	end
	if(@bandera = 1)
		begin
			update Productos set
			Estado = 0
			where IdProducto = @IdProducto
			set @Resultado = 1
		
		end
	
end
go




Create PROCEDURE SP_REGISTRARCLIENTE
(
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
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

    -- Validar si el documento ya existe
    IF EXISTS(SELECT 1 FROM Cliente WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El número de documento ya existe.';
        RETURN;
    END

    -- Validar si el correo ya existe
    IF EXISTS(SELECT 1 FROM Cliente WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado.';
        RETURN;
    END

    -- Si no existen duplicados de documento ni correo, registrar al cliente
    INSERT INTO Cliente (Documento, NombreCompleto, Direccion, Correo, Telefono, Estado)
    VALUES (@Documento, @NombreCompleto, @Direccion, @Correo, @Telefono, @Estado);

    -- Obtener el Id del cliente recién insertado
    SET @Resultado = SCOPE_IDENTITY();

    -- Mensaje de éxito
    SET @Mensaje = 'Cliente registrado exitosamente.';
END;
GO


create PROCEDURE SP_EDITARCLIENTE
(
    @IdCliente INT,
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
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

    -- Validar si el documento ya existe en otro cliente
    IF EXISTS(SELECT 1 FROM Cliente WHERE Documento = @Documento AND IdCliente != @IdCliente)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El número de documento ya está registrado para otro cliente.';
        RETURN;
    END

    -- Validar si el correo ya existe en otro cliente
    IF EXISTS(SELECT 1 FROM Cliente WHERE Correo = @Correo AND IdCliente != @IdCliente)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El correo ya está registrado para otro cliente.';
        RETURN;
    END

    -- Si no existen duplicados de documento ni correo, actualizar el cliente
    UPDATE Cliente 
    SET 
        Documento = @Documento,
        NombreCompleto = @NombreCompleto,
        Direccion = @Direccion,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
    WHERE IdCliente = @IdCliente;

    -- Mensaje de éxito
    SET @Mensaje = 'Cliente editado exitosamente.';
END;
GO



create PROC SP_ELIMINARCLIENTE(
@IdCliente int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

		if exists(select * from Cliente where IdCliente = @IdCliente)
			begin
				update Cliente set
				Estado = 0
				where IdCliente = @IdCliente
				--delete top(1) from Cliente where IdCliente = @IdCliente
			end
		else
			begin
				set @Resultado = 0
				set @Mensaje = 'No se puede eliminar el cliente'
			end
end
go



Create PROCEDURE SP_REGISTRARPROVEEDOR
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

    -- Validar si el documento ya existe
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Documento = @Documento)
    BEGIN
        SET @Mensaje = 'El número de documento ya existe.';
        RETURN;
    END

    -- Validar si el correo ya existe
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo)
    BEGIN
        SET @Mensaje = 'El correo ya está registrado.';
        RETURN;
    END

    -- Si no existen duplicados de documento ni correo, registrar al proveedor
    INSERT INTO Proveedor (Documento, RazonSocial, Direccion, Correo, Telefono, Estado)
    VALUES (@Documento, @RazonSocial, @Direccion, @Correo, @Telefono, @Estado);

    -- Obtener el Id del proveedor recién insertado
    SET @Resultado = SCOPE_IDENTITY();

    -- Mensaje de éxito
    SET @Mensaje = 'Proveedor registrado exitosamente.';
END;
GO

create PROCEDURE SP_EDITARPROVEEDOR
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

    -- Validar si el documento ya existe en otro proveedor
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Documento = @Documento AND IdProveedor != @IdProveedor)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El número de documento ya está registrado para otro proveedor.';
        RETURN;
    END

    -- Validar si el correo ya existe en otro proveedor
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo AND IdProveedor != @IdProveedor)
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'El correo ya está registrado para otro proveedor.';
        RETURN;
    END

    -- Si no existen duplicados de documento ni correo, actualizar el proveedor
    UPDATE Proveedor 
    SET 
        Documento = @Documento,
        RazonSocial = @RazonSocial,
        Direccion = @Direccion,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
    WHERE IdProveedor = @IdProveedor;

    -- Mensaje de éxito
    SET @Mensaje = 'Proveedor editado exitosamente.';
END;
GO


Create procedure SP_ELIMINARPROVEEDOR(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

		if not exists(select * from Proveedor p
		inner join Compra c on p.IdProveedor = c.IdProveedor
		where p.IdProveedor = @IdProveedor)
			begin
				update Proveedor set
				Estado = 0
				where IdProveedor = @IdProveedor
				--delete top(1) from Proveedor where IdProveedor = @IdProveedor
			end
		else
			begin
				set @Resultado = 0
				set @Mensaje = 'No se puede eliminar el proveedor, se encuentra relacionado con una compra'
			end
end
go


/*PROCEDIMIENTO PARA REGISTRAR UNA COMPRA*/

Create TYPE [dbo].[EDetalle_Compra] as table(
	[IdProducto] int NULL,
	[PrecioCompra]	decimal(18,2) NULL,
	[PrecioVenta]	decimal(18,2) NULL,
	[Cantidad] int NULL,
	[Total] decimal(18,2) NULL
)
go




create procedure SP_REGISTRARCOMPRA(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(50),
@NumeroDocumento varchar(50),
@MontoTotal decimal(10,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin

	begin try
		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into Compra(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal,Estado)
		values(@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal,0)
		set @idcompra = SCOPE_IDENTITY()


		insert into Detalle_Compra(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,Total)
		select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,Total from @DetalleCompra

		update p set 
		p.PrecioCompra = dc.PrecioCompra,
		P.PrecioVenta = dc.PrecioVenta
		from Productos p
		inner join @DetalleCompra dc on dc.IdProducto = p.IdProducto


		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
		
	end catch
end

go



--Procedimiento para validar una compra y que actualice el stock
CREATE PROCEDURE SP_VALIDARCOMPRA(
    @IdCompra INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    -- Verificamos si la compra existe y su estado es '0' (no validada)
    IF EXISTS(SELECT * FROM Compra WHERE IdCompra = @IdCompra AND Estado = 0)
    BEGIN
        -- Verificamos si existen detalles para la compra especificada
        IF EXISTS(SELECT * FROM Detalle_Compra WHERE IdCompra = @IdCompra)
        BEGIN
            -- Actualizamos los stocks de los productos
            UPDATE p
            SET p.Stock = p.Stock + dc.Cantidad
            FROM Productos p
            INNER JOIN Detalle_Compra dc ON dc.IdProducto = p.IdProducto
            WHERE dc.IdCompra = @IdCompra;

            -- Actualizamos el estado de la compra a '1' (validada)
            UPDATE Compra
            SET Estado = 1
            WHERE IdCompra = @IdCompra;

            -- Devolvemos que la operación fue exitosa
            SET @Resultado = 1;
            SET @Mensaje = 'Compra validada y stock actualizado correctamente.';
        END
        ELSE
        BEGIN
            -- Si no se encuentran detalles de la compra
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró ningún detalle para la compra especificada.';
        END
    END
    ELSE
    BEGIN
        -- Si la compra ya fue validada o no existe
        SET @Resultado = 0;
        SET @Mensaje = 'La compra no existe o ya ha sido validada.';
    END
END
GO


/*PROCEDIMIENTO PARA REGISTRAR UNA VENTA*/

CREATE TYPE [dbo].[EDetalle_Venta] as table(
	[IdProducto] int NULL,
	[PrecioVenta]	decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)
go


Create procedure SP_REGISTRARVENTA(
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
)as
begin

	begin try
		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into Venta(IdUsuario,IdCliente,TipoDocumento,NumeroDocumento,DocumentoCliente,NombreCliente,MontoPago,MontoCambio,MontoTotal)
		values(@IdUsuario,IIF(@IdCliente = 0, null, @IdCliente),@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoPago,@MontoCambio,@MontoTotal)
		set @idventa = SCOPE_IDENTITY()

		
	
		insert into Detalle_Venta(idventa,IdProducto,PrecioVenta,Cantidad,SubTotal)
		select @idventa,IdProducto,PrecioVenta,Cantidad,SubTotal from @DetalleVenta


		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
		
	end catch
end

go



create procedure SP_REPORTECOMPRAS(
    @fechainicio date, -- Cambiar a tipo de dato date
    @fechafin date,    -- Cambiar a tipo de dato date
    @idproveedor int
)
AS
BEGIN
    -- El formato de la fecha solo es relevante para la salida, no para las comparaciones
    SELECT 
        CONVERT(char(10), c.FechaRegistro, 103) AS [FechaRegistro], 
        c.TipoDocumento,
        c.NumeroDocumento,
        c.MontoTotal,
        u.NombreCompleto AS [UsuarioRegistro],
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
    WHERE c.FechaRegistro BETWEEN @fechainicio AND @fechafin
      AND pr.IdProveedor = IIF(@idproveedor = 0, pr.IdProveedor, @idproveedor)
END
go


create procedure SP_REPORTEVENTAS(
    @fechainicio date, -- Cambiar a tipo de dato date
    @fechafin date     -- Cambiar a tipo de dato date
)
AS
BEGIN
    -- El formato de la fecha solo es relevante para la salida, no para las comparaciones
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
go


Create procedure SP_DATOSGRAFICOS
@totVentas int out,
@totCompras int out,
@totProveedores int out,
@totProd int out,
@totCat int out,
@totUser int out,
@totClientes int out
as
set @totVentas = isnull((select sum(MontoTotal) as TotVentas from Venta), 0)
set @totCompras = isnull((select sum(c.MontoTotal) as TotCompras from Compra as c), 0)
set @totProveedores = isnull((select count(IdProveedor) as TotProveedores from Proveedor), 0)
set @totProd = isnull((select count(IdProducto) as TotProd from Productos), 0)
set @totCat = isnull((select count(IdCategoria) as TotCat from Categorias), 0)
set @totUser = isnull((select count(IdUsuario) as TotUser from Usuario), 0)
set @totClientes = isnull((select count(IdCliente) as TotClientes from Cliente), 0)
go

create procedure SP_PRODPREFERIDOS
as
select top 5 c.Descripcion+' ' +p.Descripcion as Producto, count(dv.IdProducto) as cantSalidas
from Detalle_Venta as dv
inner join Productos as p on p.IdProducto = dv.IdProducto
inner join Categorias as c on c.IdCategoria = p.IdCategoria
group by dv.IdProducto,c.Descripcion,p.Descripcion
order by count(2) desc
go

create PROCEDURE SP_GRAFICOSVFECHAS
    @fechaDesde DATE,
    @fechaHasta DATE,
    @totVentas INT OUT,
    @totCompras INT OUT
AS
BEGIN
    -- Total de ventas en el rango de fechas
    SET @totVentas = (
        SELECT SUM(MontoTotal) as TotVentas
        FROM Venta v 
        WHERE v.FechaRegistro BETWEEN @fechaDesde AND @fechaHasta 
    );

    -- Total de compras en el mismo rango de fechas
    SET @totCompras = (
        SELECT SUM(MontoTotal) as TotCompras
        FROM Compra c 
        WHERE c.FechaRegistro BETWEEN @fechaDesde AND @fechaHasta
    );
END
GO




create procedure SP_PRODCATEGORIAS
as
select c.Descripcion, count(c.IdCategoria) as Cant 
from  Productos as p
inner join Categorias c on c.IdCategoria=p.IdCategoria
group by c.IdCategoria, c.Descripcion
order by count(2)
go



--Inserts a la base de datos
Insert into Rol (Descripcion)
values('Gerente')
go

Insert into Rol (Descripcion)
values('Vendedor')
go

Insert into Rol (Descripcion)
values('Manager')
go

--Inserta los permisos
insert into PERMISO(IdRol,NombreMenu) values
  (1,'menuUsuarios'),
  (1,'menuMantenimiento'),
  (1,'menuVentas'),
  (1,'menuCompras'),
  (1,'menuClientes'),
  (1,'menuProveedores'),
  (1,'menuReportes'),
  (1,'menuInicio'),
  (1,'menuGraficos')
  go

  insert into PERMISO(IdRol,NombreMenu) values
  (2,'menuMantenimiento'),
  (2,'menuVentas'),
  (2,'menuCompras'),
  (2,'menuClientes'),
  (2,'menuProveedores'),
  (2,'menuInicio')
  go

insert into PERMISO(IdRol,NombreMenu) values
  (3,'menuMantenimiento'),
  (3,'menuUsuarios'),
  (3,'menuClientes'),
  (3,'menuProveedores'),
  (3,'menuInicio')
  go


--Inserta un usuario (Administrador) con estas credenciales
Insert into Usuario(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
VALUES('1234','Tester','test@gmail.com','1234',1,1)
go

insert into Negocio(Nombre,RUC,Direccion) 
values('CapyCalzados - Venta de Calzados',10000,'Av. Corrientes 1234')
go

-- Categorias de Calzados
insert into Categorias(Descripcion, Estado) values
('Zapatillas Urbanas', 1),
('Zapatillas Deportivas', 1),
('Zapatos de Vestir', 1),
('Botas y Borcegos', 1),
('Sandalias y Ojotas', 1),
('Mocasines', 1)
go

-- Cliente de prueba
insert into Cliente(Documento, NombreCompleto, Correo, Telefono, Estado)
values('40123456', 'Juan Pérez', 'juan.perez@email.com', '1145678901', 1)
go

-- Proveedor de calzados de prueba
insert into Proveedor(Documento, RazonSocial, Correo, Telefono, Estado)
values('30-71234567-8', 'Distribuidora Central Calzados S.A.', 'ventas@districalzados.com', '1155554444', 1)
go

-- Productos de calzado de prueba (Nombre, Talle/Color en Descripción, Marca)
insert into Productos(Codigo, Nombre, Descripcion, Marca, IdCategoria, Stock, PrecioCompra, PrecioVenta, Estado) values
(1001, 'Nike Air Max SC', 'Talle 41 - Negro/Blanco', 'Nike', 2, 15, 45000.00, 75000.00, 1),
(1002, 'Adidas Superstar', 'Talle 40 - Blanco Rayas Negras', 'Adidas', 1, 20, 42000.00, 69000.00, 1),
(1003, 'Zapato Oxford Cuero', 'Talle 42 - Marrón Suela', 'Briganti', 3, 10, 55000.00, 92000.00, 1),
(1004, 'Bota Chelsea Clásica', 'Talle 38 - Suela Gamuzado', 'Grimoldi', 4, 8, 48000.00, 81000.00, 1),
(1005, 'Mocasín Náutico', 'Talle 43 - Azul Marino', 'Heyas', 6, 12, 38000.00, 62000.00, 1)
go

Create table BackupR(
 idBackup int identity primary key,
 fecha_registro date
)

insert into BackupR(fecha_registro) values (GETDATE())


CREATE FUNCTION comparar(
    @fecha_registro DATE
)
RETURNS BIT
AS
BEGIN
    DECLARE @ultimafecha DATE;

    -- Obtener la última fecha registrada en la tabla BackupR
    SET @ultimafecha = (SELECT MAX(fecha_registro) FROM BackupR);

    -- Retornar 1 si la diferencia es mayor a 7 días, de lo contrario retornar 0
    RETURN CASE 
        WHEN DATEDIFF(DAY, @ultimafecha, @fecha_registro) > 7 THEN 1
        ELSE 0
    END;
END;
