# MiTutor Backend

Backend del sistema **Mi Tutor** - Sistema de Gestión de Tutorías Universitarias

## 📋 Descripción

Mi Tutor es un sistema de apoyo a la gestión del proceso de tutoría universitaria, diseñado para facilitar el registro, seguimiento y evaluación de las tutorías académicas. Permite configurar distintos modelos de tutoría según las necesidades de cada programa académico, gestionar tutores, estudiantes y citas, así como registrar resultados e indicadores que apoyen la toma de decisiones. 

Este repositorio contiene el backend del sistema, construido con tecnologías . NET, proporcionando una API REST robusta y escalable para soportar todas las operaciones del sistema.

## ✨ Características Principales

- 🎓 **Gestión de Tutores y Estudiantes**: Registro y administración completa de usuarios
- 📅 **Sistema de Citas**: Programación y seguimiento de sesiones de tutoría
- 🔧 **Modelos Configurables**: Adaptación a diferentes esquemas de tutoría universitaria
- 📊 **Reportes e Indicadores**: Generación de métricas para la toma de decisiones
- 🔐 **Autenticación y Autorización**: Sistema seguro de gestión de accesos
- 📱 **API RESTful**: Endpoints bien documentados y estructurados

## 🛠️ Tecnologías

- **Framework**: ASP.NET Core
- **Lenguaje**: C# (. NET)
- **Base de Datos**: SQL Server 2019
- **ORM**: Entity Framework Core
- **Autenticación**: JWT (JSON Web Tokens)
- **Documentación API**: Swagger/OpenAPI
- **Containerización**: Docker

## 📋 Requisitos Previos

- [. NET SDK](https://dotnet.microsoft.com/download) (versión 6.0 o superior)
- [Docker](https://www.docker.com/) (opcional, para containerización)
- Base de datos (SQL Server 2019)
- IDE recomendado: Visual Studio 2022 o Visual Studio Code

## 🚀 Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/ReactGPT/MiTutor-backend.git
cd MiTutor-backend
```

### 2. Configurar variables de entorno

Crear un archivo `appsettings.Development.json` en la raíz del proyecto:

```json
{
  "ConnectionStrings": {
    "DefaultConnection":  "Server=localhost;Database=MiTutor;User Id=usuario;Password=contraseña;"
  },
  "Jwt": {
    "Key": "tu-clave-secreta-jwt",
    "Issuer": "MiTutor",
    "Audience": "MiTutorClient",
    "ExpireMinutes": 60
  },
  "Logging": {
    "LogLevel":  {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 3. Restaurar dependencias

```bash
dotnet restore
```

### 4. Aplicar migraciones de base de datos

```bash
dotnet ef database update
```

### 5. Ejecutar el proyecto

```bash
dotnet run
```

La API estará disponible en:  `https://localhost:5001` o `http://localhost:5000`

## 📚 Documentación de la API

Una vez que el proyecto esté en ejecución, accede a la documentación interactiva de Swagger:

```
https://localhost:5001/swagger
```

### Endpoints Principales

#### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar nuevo usuario
- `POST /api/auth/refresh` - Renovar token

#### Tutores
- `GET /api/tutores` - Listar tutores
- `GET /api/tutores/{id}` - Obtener tutor por ID
- `POST /api/tutores` - Crear tutor
- `PUT /api/tutores/{id}` - Actualizar tutor
- `DELETE /api/tutores/{id}` - Eliminar tutor

#### Estudiantes
- `GET /api/estudiantes` - Listar estudiantes
- `GET /api/estudiantes/{id}` - Obtener estudiante por ID
- `POST /api/estudiantes` - Crear estudiante
- `PUT /api/estudiantes/{id}` - Actualizar estudiante

#### Citas/Tutorías
- `GET /api/citas` - Listar citas
- `GET /api/citas/{id}` - Obtener cita por ID
- `POST /api/citas` - Programar cita
- `PUT /api/citas/{id}` - Actualizar cita
- `DELETE /api/citas/{id}` - Cancelar cita

#### Reportes
- `GET /api/reportes/indicadores` - Obtener indicadores generales
- `GET /api/reportes/tutor/{id}` - Reporte por tutor
- `GET /api/reportes/estudiante/{id}` - Reporte por estudiante

## 🗂️ Estructura del Proyecto

```
MiTutor-backend/
├── Controllers/          # Controladores de la API
├── Models/              # Modelos de datos
├── DTOs/                # Data Transfer Objects
├── Services/            # Lógica de negocio
├── Repositories/        # Capa de acceso a datos
├── Data/                # Contexto de base de datos
├── Migrations/          # Migraciones de EF Core
├── Middleware/          # Middleware personalizado
├── Helpers/             # Utilidades y helpers
├── appsettings.json     # Configuración de aplicación
└── Program.cs           # Punto de entrada

```

## 📝 Licencia

[Especificar licencia - MIT, Apache 2.0, etc.]

## 👥 Equipo

- **Organización**: [Equipo](https://github.com/ReactGPT)

---

**Desarrollado con ❤️ para mejorar la experiencia de tutoría universitaria**
