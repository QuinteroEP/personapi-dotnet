Pagina web basica que ofrece un CRUD de 4 entidades

# Tecnologias
<ul>
  <li>.NET 9</li>
  <li>SQL</li>
  <li>Swagger 3</li>
  <li>Docker</li>
</ul>

# Entorno de desarrollo
<ul>
  <li>Visual Studio 2022</li>
  <li>SQL Server Management Studio 20</li>
</ul>

## Configuracion
### Complementos
<ul>
  <li>Desarrollo ASP.NET y web</li>
  <li>Almacenamiento y procesamiento de datos</li>
  <li>Plantillas de proyecto y elementos de .Net Framework</li>
  <li>Caracteristicas avanzadas de ASP.NET</li>
</ul>

## Paquetes de NuGet
<ul>
  <li>Microsoft.EntityFrameworkCore</li>
  <li>Microsoft.EntityFrameworkCore.SqlServer</li>
  <li>Microsoft.EntityFrameworkCore.Tools</li>
  <li>Swashbuckle.AspNetCore (Para Swagger)</li>
</ul>

# Despliegue
<ol>
  <li>En la terminal, navegar a la carpeta del proyecto con <i>cd [path]\personapi-dotnet\personapi-dotnet.</i></li>
  <li>Levantar el docker compose con docker-compose build y docker-compose up.</li>
  <li>Esperar aproximadamente 1 minuto para que la base de datos SQL se inicialice y acepte conexiones.</li>
  <li>Ejecutar el siguiente comando para cargar toda la base de datos: <i>sqlcmd -S localhost,1433 -U SA -P DB4dmin! -i init.sql</i></li>
  <li>Acceder a la página por localhost:8080</li>
</ol>

# Universidad Javeriana
## Pablo Enrique Quintero, Juan Diego Romero, Kamilt Bejarano Diaz
