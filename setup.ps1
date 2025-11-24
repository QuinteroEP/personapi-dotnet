# Script para inicializar la aplicación con Docker
# Ejecutar con: .\setup.ps1

Write-Host "=== Configuración de personapi-dotnet con Login ===" -ForegroundColor Cyan
Write-Host ""

# 1. Verificar si Docker está corriendo
Write-Host "[1/5] Verificando Docker..." -ForegroundColor Yellow
try {
    docker --version | Out-Null
    if ($LASTEXITCODE -ne 0) {
        throw "Docker no está instalado o no está en el PATH"
    }
    Write-Host "✓ Docker detectado" -ForegroundColor Green
} catch {
    Write-Host "✗ Error: Docker no está disponible. Instálalo desde https://www.docker.com/" -ForegroundColor Red
    exit 1
}

# 2. Detener contenedores previos si existen
Write-Host "[2/5] Limpiando contenedores previos..." -ForegroundColor Yellow
docker-compose down -v 2>$null
Write-Host "✓ Limpieza completada" -ForegroundColor Green

# 3. Levantar los contenedores
Write-Host "[3/5] Levantando contenedores (esto puede tomar unos minutos)..." -ForegroundColor Yellow
docker-compose up -d --build
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Error al levantar los contenedores" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Contenedores levantados" -ForegroundColor Green

# 4. Esperar a que SQL Server esté listo
Write-Host "[4/5] Esperando a que SQL Server esté listo..." -ForegroundColor Yellow
$maxAttempts = 30
$attempt = 0
$ready = $false

while (-not $ready -and $attempt -lt $maxAttempts) {
    $attempt++
    Write-Host "  Intento $attempt de $maxAttempts..." -ForegroundColor Gray
    
    $result = docker exec sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C -Q "SELECT 1" 2>&1
    
    if ($LASTEXITCODE -eq 0) {
        $ready = $true
        Write-Host "✓ SQL Server está listo" -ForegroundColor Green
    } else {
        Start-Sleep -Seconds 2
    }
}

if (-not $ready) {
    Write-Host "✗ Timeout esperando SQL Server. Intenta manualmente: docker logs sqldb" -ForegroundColor Red
    exit 1
}

# 5. Inicializar la base de datos
Write-Host "[5/5] Inicializando base de datos y creando usuario demo..." -ForegroundColor Yellow
docker exec -it sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DB4dmin!" -C -i /init-db.sql
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Error al ejecutar el script SQL" -ForegroundColor Red
    Write-Host "  Puedes intentar manualmente con:" -ForegroundColor Yellow
    Write-Host "  docker exec -it sqldb /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'DB4dmin!' -C -i /init-db.sql" -ForegroundColor Yellow
    exit 1
}
Write-Host "✓ Base de datos inicializada" -ForegroundColor Green

# Resumen
Write-Host ""
Write-Host "=== ¡Configuración completada! ===" -ForegroundColor Green
Write-Host ""
Write-Host " Aplicación web: http://localhost:8080" -ForegroundColor Cyan
Write-Host "  SQL Server: localhost:1433" -ForegroundColor Cyan
Write-Host ""
Write-Host "👤 Usuario demo:" -ForegroundColor White
Write-Host "   Usuario: admin" -ForegroundColor Yellow
Write-Host "   Contraseña: admin123" -ForegroundColor Yellow
Write-Host ""
Write-Host "🔧 Comandos útiles:" -ForegroundColor White
Write-Host "   Ver logs: docker-compose logs -f" -ForegroundColor Gray
Write-Host "   Detener: docker-compose down" -ForegroundColor Gray
Write-Host "   Reiniciar: docker-compose restart" -ForegroundColor Gray
Write-Host ""
