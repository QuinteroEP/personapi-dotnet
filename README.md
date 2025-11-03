# Tecnologias
-.NET 9

-SQL

# Despliegue
1. En la terminal, navegar a la carpeta del proyecto con cd [path]/personapi-dotnet\personapi-dotnet.
2. Levantar el docker compose con docker-compose build y docker-compose up.
3. Esperar aproximadamente 1 minuto para que la base de datos SQL se inicialice y acepte conexiones.
4. Ejecutar el siguiente comando para cargar toda la base de datos:
5. sqlcmd -S localhost,1433 -U SA -P DB4dmin! -i init.sql
6. Acceder a la página por localhost:8080

## Universidad Javeriana
Pablo Enrique Quintero, Juan Diego Romero, Kamilt Bejarano Diaz
