## Aspnetcore Microservices


**Development Environment:**

1. Use docker-compose
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.ovveride.yml up -d --remove-orphans
```

## Application URLs - Local Environment (Docker Container)
- Product API: http://localhost:6002/api/products

## Docker Application URLs - Local Environment (Docker Container)
- Portainer: http://localhost:9000 - user: admin ; pass: admin123456789
- Kibana: http://localhost:5601 - user: elastic ; pass: admin
- RabbitMQ: http://localhost:15672 - username:guest ; pass: guest

2. Use Visual Studio 2023:
## Install Environment:
- https://dotnet.microsoft.com/download/dotnet/6.0
- Install package Microsoft.EntityFrameworkCore.Analyzers for Product.API

## Docker Command: 
- cd src
- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build
## Application URLs - Development Environment
- Product API: http://localhost:5002/api/products


## Customer.API
Install PostgreSql, PgAdmin4
Query Tool run command:
	- CREATE USER admin WITH PASSWORD 'Anhnguyen18';
	- Login with supper User change role user admin -> supper user.

## Ordering.API

- Connectionstring Sql Server: Server=ANHNGUYEN\\SQLEXPRESS;Database=OrderDB;User Id=sa;Password=Anhnguyen18;Trusted_Connection=True;
- Add-Migration Update_Configuration_OrderStatus -Project "Ordering.Infrastructure" -StartupProject "Ordering.API" -OutputDir "Persistence\Migrations"
- Enable account sa:
- run command in query tool: USE mydatabase					
                             exec sp_changedbowner 'sa', 'true'
- Docker: ConnectionStrings:DefaultConnectionString=Server=orderdb;Database=OrderDB;User Id=sa;Password=Anhnguyen18;MultipleActiveResultSets=True;
- Configuration SMTP(google):
	1. Create App Password in google account (Enable 2 step authentication in Google account)
	2. Configure: SMTP setting with password app created.
	3. Install:  Mailkit - using client mail.

## Product.API
- Install MySql + My SqlWorkBench.
- Run Mirgration create database 
- ProductContextSeed: Create sample data and insert into the database. Sample data is automatically added when running the program

## Basket.API
- Crud Basket API with database using Redis.
- Publish and Consume Messages with MassTransit and RabbitMQ in .Net 6
- Implementing gRPC Services in .NET 6 (client)
- DDD, Event Sourcing, CQRS parttern for Basket.API and Ordering API

## Inventory API
- Implementing gRPC Services in .NET 6 (server)

## Hangfire API
- BackgroundJobScheduled with HangFire. (Auto Send Mail reminder payment to customer when basket update)

## API GateWay
- Implement api gateway in .net core
- Config RateLimit, Caching respone, QOS, Authentication on API gate way
- Ocelot json config routing for child APIs