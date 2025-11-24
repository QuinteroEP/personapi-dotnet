# 🔐 personapi-dotnet - Sistema de Login con Autenticación

Sistema de autenticación simple con ASP.NET Core MVC, Entity Framework y SQL Server en Docker.

##  Inicio Rápido (3 pasos)

### Opción 1: Script Automático ⚡
```powershell
.\setup.ps1
```

### Opción 2: Manual 
```powershell
# 1. Levantar contenedores
docker-compose up -d

# 2. Esperar 15 segundos y crear la base de datos
docker exec sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C -i /init-db.sql

# 3. Abrir en navegador
start http://localhost:8080
```

### Para iniciar después (más rápido)
```powershell
.\start.ps1
```

##  Usuario Demo
- **Usuario:** `admin`
- **Contraseña:** `admin123`

##  Funcionalidades
- ✅ Registro de usuarios
- ✅ Login/Logout con cookies
- ✅ Sesión persistente ("Recordarme")
- ✅ Contraseñas hasheadas (SHA256)
- ✅ Entity Framework Core + SQL Server
- ✅ Docker Compose incluido

## 📱 URLs
- **App:** http://localhost:8080
- **Login:** http://localhost:8080/Account/Login
- **Registro:** http://localhost:8080/Account/Register
- **SQL Server:** localhost:1433 (sa/DB4dmin!)

## 🛠️ Comandos Útiles

```powershell
# Ver logs
docker-compose logs -f

# Detener
docker-compose down

# Reiniciar
docker-compose restart

# Rebuild completo
docker-compose down -v
docker-compose up -d --build

# Acceder a SQL Server
docker exec -it sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C
```

##  Estructura del Proyecto

```
personapi-dotnet/
├── Controllers/
│   ├── AccountController.cs    # Login, Register, Logout
│   └── HomeController.cs
├── Data/
│   └── ApplicationDbContext.cs # DbContext de EF Core
├── Models/
│   ├── User.cs                 # Entidad Usuario
│   ├── LoginViewModel.cs
│   └── RegisterViewModel.cs
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   └── Shared/
│       └── _Layout.cshtml      # Navbar con auth
├── init-db.sql                 # Script SQL inicial
├── setup.ps1                   # Configuración completa
├── start.ps1                   # Inicio rápido
└── docker-compose.yml          # Orquestación
```

## 🐛 Solución de Problemas

### La página no carga (error 404 o no funciona)
```powershell
# Verificar que los contenedores están corriendo
docker ps

# Ver logs
docker logs webapp
docker logs sqldb

# Reiniciar
docker-compose restart
```

### Error de conexión a base de datos
```powershell
# Verificar que SQL Server está listo
docker exec sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C -Q "SELECT 1"

# Crear la tabla manualmente
docker exec sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C -i /init-db.sql
```

### Puerto 8080 ya está en uso
Edita `docker-compose.yml` y cambia:
```yaml
ports:
  - "8081:8080"  # Usar puerto 8081 en lugar de 8080
```

##  Nota de Seguridad
 Este es un ejemplo **educativo**. Para producción:
- Usa BCrypt o ASP.NET Identity
- Configura HTTPS
- Usa secretos en variables de entorno
- Implementa rate limiting
- Agrega recuperación de contraseña
- Implementa 2FA

##  Tecnologías
- ASP.NET Core 8.0 MVC
- Entity Framework Core 8.0
- SQL Server 2022
- Docker & Docker Compose
- Bootstrap 5

---

**Desarrollado como proyecto educativo** 🎓
