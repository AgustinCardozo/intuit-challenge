# Backend - Intuit Challenge

```bash
backend/
├── src/ClienteApi.sln                 
│   ├── Cliente.Api/                    
│   ├── Cliente.Application/            
│   ├── Cliente.Domain/                 
│   └── Cliente.Infrastructure/         
├── test/                               
│   └── Cliente.Application.Tests/
│   └── Cliente.Infrastructure.Tests/ 
├── docker-compose.yml                  
├── schema.sql                          
└── README.md
```

## Características

- Arquitectura en capas (Domain, Application, Infrastructure, Api)
- Acceso a datos con Entity Framework Core (Database First)
- Manejo de excepciones y validaciones
- Configuración por entorno (`appsettings.*.json`)
- Documentación Swagger (si está habilitada)

## Requisitos

- .NET 7 SDK
- Docker y Docker Compose
- SQL Server (local o en contenedor)

## Configuración rápida

### 1. Base de datos con Docker Compose

El archivo `docker-compose.yml` levanta un contenedor SQL Server listo para usar:

```bash
docker-compose up -d
```

Esto crea una base de datos `ClientesDb` con usuario y contraseña definidos en el compose.

### 2. Restaurar y actualizar modelos (Database First)

Si cambias el esquema de la base de datos, actualiza los modelos con:

```bash
# Desde backend/src/Cliente.Infrastructure
# Ajusta la cadena de conexión según tu entorno
Scaffold-DbContext "Host=localhost;Port=5433;Database=intuit_cliente_db;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Project Cliente.Infrastructure -StartupProject Cliente.Api -OutputDir Cliente.Model -Context ClienteDbContext -Force -ContextDir Data
```

### 3. Ejecutar la API

```bash
# Desde backend/src/Cliente.Api
 dotnet restore
 dotnet run
```

La API estará disponible en `https://localhost:5001` o el puerto configurado.

## Variables de entorno

- Configura la cadena de conexión en `appsettings.Development.json` o mediante variables de entorno.

## Endpoints principales

- `GET /api/clientes` - Listar clientes
- `POST /api/clientes` - Crear cliente
- `PUT /api/clientes/{id}` - Actualizar cliente
- `DELETE /api/clientes/{id}` - Eliminar cliente

## Notas

- El script `schema.sql` puede usarse para inicializar la base de datos manualmente si lo prefieres.
- El proyecto está preparado para ampliarse fácilmente con nuevas entidades y casos de uso.

---
Desarrollado por Agustin Cardozo para challenge técnico.
