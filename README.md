# net-docker-grpc-example

What is that?

Set of exapmle project to show how to work with .NET/gRPC/docker together

# What are done?
- gRPC service (grpc-service
- WebApi Service with swagger UI(web-api)
- Mongo DB(mongo)
- Docker compose script that are build and run all containers

# Still in progress
-	Communication between WebApi and gRPC
-	Communication between gRPC and Mongo
-	Internal network between containers
-	Connection strings inside docker compose

# How to Install ?
`git clone https://github.com/T-REX-XP/net-docker-grpc-example
cd net-docker-grpc-example
docker compose build
docker compose up
`

# Endpints
-	http://localhost:8080/swagger/index.html
-	http://localhost:8080/api/movies

# Suggestions and Notes:
-	The following containers should be connected only via the internal network: grpc-service, mongo
-	The container with name web-api has exposed port to the internet: 80/443 that should be used for consumers, also the swagger UI will be available as well
-	I suggesting adding OAuth authorization to the WebApi to avoid exposing sensitive information.

# Container dependencies:
 - grpc-service depends on mongo
 - web-api depends on grpc-service

# Controllers:
-	All actions of the controller should be async
- Only for the PoC, the swagger is available in production mode and without authentication. I’m not recommending to do the same in a real life. At least authorization on the swaggers or whitelist on the firewall should be implemented. Potentially this configuration may lead the security issue in future

