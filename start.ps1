# Script rápido para iniciar la aplicación
# Ejecutar: .\start.ps1

Write-Host " Iniciando personapi-dotnet..." -ForegroundColor Cyan

# Verificar si los contenedores están corriendo
$running = docker ps --filter "name=webapp" --filter "status=running" --format "{{.Names}}"

if ($running -eq "webapp") {
    Write-Host "✓ La aplicación ya está corriendo" -ForegroundColor Green
} else {
    Write-Host "Levantando contenedores..." -ForegroundColor Yellow
    docker-compose up -d
    Start-Sleep -Seconds 5
    Write-Host "✓ Contenedores iniciados" -ForegroundColor Green
}

Write-Host ""
Write-Host "📱 Abriendo navegador en http://localhost:8080" -ForegroundColor Cyan
start http://localhost:8080

Write-Host ""
Write-Host "👤 Credenciales de prueba:" -ForegroundColor White
Write-Host "   Usuario: admin" -ForegroundColor Yellow
Write-Host "   Contraseña: admin123" -ForegroundColor Yellow
Write-Host ""
Write-Host "🔧 Para detener: docker-compose down" -ForegroundColor Gray
