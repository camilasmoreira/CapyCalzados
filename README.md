# CapyCalzados — Sistema de Gestión de Ventas

Sistema de escritorio para la gestión integral de compras, ventas, inventario y reportes de una tienda de calzados, desarrollado en **C# (.NET Framework 4.7.2)** con **Windows Forms** y **SQL Server** bajo una arquitectura en capas.

---

## Características Principales

- **Autenticación y Control de Acceso por Roles**: Menús e interfaces dinámicas según el perfil del usuario (*Manager*, *Gerente*, *Vendedor*).
- **Gestión de Ventas y Compras**: Registro detallado de transacciones, cálculo automático de montos/vuelto y actualización de stock en tiempo real.
- **Generación de Comprobantes y Exportación**:
  - Descarga de facturas/comprobantes en formato **PDF** (`iTextSharp`).
  - Exportación de listados y reportes a **Excel** (`ClosedXML`).
- **Administración de Catálogos (ABM / CRUD)**:
  - Productos y Categorías.
  - Clientes y Proveedores.
  - Usuarios y Datos del Negocio (incluyendo logo corporativo).
- **Reportes y Estadísticas**:
  - Dashboard con indicadores clave (KPIs) e historial gráfico de ventas, categorías y productos preferidos.
  - Reportes detallados de compras y ventas filtrados por rango de fechas y proveedor.
- **Seguridad y Respaldo**: Módulo integrado de **Backup y Restauración** de la base de datos SQL Server.

---

## Arquitectura del Proyecto

El sistema está estructurado en **4 capas** para separar responsabilidades y facilitar el mantenimiento:

```text
CapyCalzados/
├── CapaEntidad/        # Modelos de datos (Usuario, Producto, Venta, Compra, etc.)
├── CapaDatos/          # Acceso a datos ADO.NET, conexión a SQL Server y ejecución de Stored Procedures
├── CapaNegocio/        # Lógica de negocio y validaciones
├── CapaPresentacion/   # Interfaz gráfica (Windows Forms, Modales, Gráficos y Reportes)
└── DBSistema.sql       # Script unificado de Base de Datos (Esquema + Stored Procedures + Datos de prueba)
```

---

## Tecnologías y Librerías Utilizadas

- **Lenguaje y Framework**: C# / .NET Framework 4.7.2 (Windows Forms)
- **Base de Datos**: Microsoft SQL Server / LocalDB (`DBSistemaVentas`)
- **Librerías (NuGet)**:
  - `FontAwesome.Sharp` & `MaterialSkin.2` — Iconografía y diseño moderno de interfaz.
  - `ClosedXML` — Exportación de tablas y reportes a Excel (`.xlsx`).
  - `iTextSharp` & `itextsharp.xmlworker` — Generación de comprobantes en PDF a partir de plantillas HTML.

---

## Requisitos e Instalación

### 1. Requisitos Previos
- **Visual Studio 2019 / 2022** (con carga de trabajo *Desarrollo de escritorio de .NET*).
- **SQL Server** (Express, Developer o **SQL Server LocalDB**).

### 2. Configuración de la Base de Datos
1. Abrir **SQL Server Management Studio (SSMS)** o conectarse a la instancia `(localdb)\MSSQLLocalDB`.
2. Abrir y ejecutar el archivo **`DBSistema.sql`** incluido en la raíz del repositorio.
   - *Este script creará automáticamente la base de datos `DBSistemaVentas`, las tablas, funciones, procedimientos almacenados y cargará un lote completo de datos de prueba.*

### 3. Configuración de la Cadena de Conexión
En el archivo `CapaPresentacion/App.config`, verificar que la cadena de conexión apunte a tu instancia de SQL Server (por defecto usa `LocalDB`):

```xml
<connectionStrings>
  <add name="cadena_conexion" 
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSistemaVentas;Integrated Security=True;Encrypt=False" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Ejecución
1. Abrir la solución `CapyCalzados.sln` en Visual Studio.
2. Restaurar los paquetes NuGet si es necesario.
3. Compilar y ejecutar el proyecto (`F5`), asegurándose de que **`CapaPresentacion`** sea el proyecto de inicio.

---

## Perfiles y Usuarios de Prueba

El script `DBSistema.sql` incluye usuarios precargados para probar cada rol del sistema.  
**Contraseña para todos los usuarios:** `1234`

| Rol | DNI (Usuario) | Nombre | Permisos / Alcance |
| :--- | :---: | :--- | :--- |
| **Manager** (`IdRol = 3`) | `35123456` | Martín Rodríguez | **Acceso Total**: Compras, Ventas, Productos, Proveedores, Clientes, Usuarios, Reportes, Gráficos y Backup. |
| **Gerente** (`IdRol = 1`) | `28111222` | Roberto Sánchez | **Analítico**: Inicio, Gráficos estadísticos y Reportes. |
| **Vendedor** (`IdRol = 2`) | `42333444` | Camila Gómez | **Operativo de Ventas**: Registro de Ventas, Clientes y consulta de Productos. |
| **Vendedor** (`IdRol = 2`) | `40555666` | Lucas Fernández | **Operativo de Ventas**: Registro de Ventas, Clientes y consulta de Productos. |
| **Vendedor** (`IdRol = 2`) | `43777888` | Sofía Martínez | **Operativo de Ventas**: Registro de Ventas, Clientes y consulta de Productos. |

---

## Contexto Académico

Proyecto desarrollado en el marco de la asignatura **Taller de Programación II** — **Universidad Nacional del Nordeste (UNNE)**.
