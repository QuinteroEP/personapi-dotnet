# Sistema de Login con Autenticación - personapi-dotnet

##  Características

Sistema de autenticación simple basado en cookies con:
- ✅ Registro de usuarios
- ✅ Login/Logout
- ✅ Sesión persistente con "Recordarme"
- ✅ Hash de contraseñas (SHA256)
- ✅ Entity Framework Core con SQL Server
- ✅ Docker Compose con SQL Server incluido

##  Inicio Rápido

### 1. Levantar los contenedores

```powershell
docker-compose up -d
```

Esto levantará:
- SQL Server en puerto 1433
- La aplicación web en puerto 8080

### 2. Inicializar la base de datos

Ejecuta el script SQL para crear la tabla Users y un usuario demo:

```powershell
docker exec -it sqldb /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -i /init-db.sql
```

O copia el archivo y ejecútalo:

```powershell
docker cp init-db.sql sqldb:/init-db.sql
docker exec -it sqldb /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -i /init-db.sql
```

### 3. Aplicar migraciones de Entity Framework (alternativa)

Si prefieres usar migraciones de EF Core en lugar del script SQL:

```powershell
cd personapi-dotnet
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Acceder a la aplicación

Abre tu navegador en: **http://localhost:8080**

## 👤 Usuario Demo

Después de ejecutar el script SQL:
- **Usuario:** `admin`
- **Contraseña:** `admin123`

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
│   ├── LoginViewModel.cs       # Modelo para login
│   └── RegisterViewModel.cs    # Modelo para registro
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml        # Vista de login
│   │   └── Register.cshtml     # Vista de registro
│   └── Shared/
│       └── _Layout.cshtml      # Layout con navbar de auth
├── Program.cs                  # Configuración de auth y EF
└── appsettings.json           # Cadena de conexión
```

## 🔧 Configuración

### Cadena de Conexión (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver,1433;Database=persona_db;User Id=sa;Password=DB4dmin!;TrustServerCertificate=true"
  }
}
```

### Autenticación (Program.cs)

- Autenticación basada en cookies
- Sesión expira en 24 horas (o 30 días con "Recordarme")
- LoginPath: `/Account/Login`
- LogoutPath: `/Account/Logout`

## 🛠️ Comandos Útiles

### Ver logs de los contenedores
```powershell
docker-compose logs -f
```

### Acceder al SQL Server
```powershell
docker exec -it sqldb /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "DB4dmin!"
```

### Detener los contenedores
```powershell
docker-compose down
```

### Rebuild completo
```powershell
docker-compose down -v
docker-compose build --no-cache
docker-compose up -d
```

## 🔒 Seguridad

 **IMPORTANTE:** Este es un ejemplo educativo. Para producción:

1. **Usa BCrypt o ASP.NET Identity** en lugar de SHA256
2. Configura HTTPS
3. Implementa validación de contraseñas más fuerte
4. Agrega protección contra ataques de fuerza bruta
5. Usa variables de entorno para contraseñas sensibles
6. Implementa recuperación de contraseña
7. Agrega verificación de email

##  Próximos Pasos

- [ ] Migrar a ASP.NET Identity para funciones avanzadas
- [ ] Agregar roles y permisos
- [ ] Implementar JWT para APIs
- [ ] Agregar autenticación de dos factores (2FA)
- [ ] Recuperación de contraseña por email
- [ ] Integración con OAuth (Google, Facebook)

## 🐛 Troubleshooting

### Error de conexión a SQL Server

Si no puedes conectar a SQL Server:

```powershell
# Verificar que el contenedor está corriendo
docker ps

# Ver logs del contenedor SQL
docker logs sqldb

# Reiniciar el contenedor
docker restart sqldb
```

### La aplicación no encuentra la base de datos

Asegúrate de que:
1. El contenedor SQL Server está corriendo
2. Has ejecutado el script `init-db.sql`
3. La cadena de conexión en `appsettings.json` es correcta

### Puerto 8080 o 1433 ya está en uso

Edita `docker-compose.yml` y cambia los puertos:

```yaml
ports:
  - "8081:80"  # Para la web
  - "1434:1433"  # Para SQL Server
```

##  Recursos

- [ASP.NET Core Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Docker Compose](https://docs.docker.com/compose/)
