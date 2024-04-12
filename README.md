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
- docker-compose -f docker-compose.yml -f docker-compose.ovveride.yml up -d --remove-orphans --build
## Application URLs - Development Environment
- Product API: http://localhost:5002/api/products