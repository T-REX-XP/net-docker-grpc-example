# net-docker-grpc-example

# What is that?

Set of example projects to show how to work with .NET/gRPC/docker together

# What are done?
- gRPC service (grpc-service) (actions: GetById,Create)
- WebApi Service
- swagger UI(web-api)
- Mongo DB(mongo)
- Docker compose script that are build and run all containers
-	Communication between WebApi and gRPC

# Still in progress
-	Communication between gRPC and Mongo
-	Internal network between containers
-	Connection strings inside docker compose


## List of containers
![Screenshot](imgs/docker_containers_list.png)

# How to Install ?
`git clone https://github.com/T-REX-XP/net-docker-grpc-example && cd net-docker-grpc-example && docker compose build && docker compose up`




# Endpints
-	https://localhost:8083/swagger/index.html
-	https://localhost:8083/api/movies

# Suggestions and Notes:

## Network structure:
-	The following containers should be connected only via the internal network: **grpc-service, mongo, mongo-express,web-api,proxy**
-	The container with name **proxy** has exposed port to the host: **80/443** that should be used for consumers, the swagger UI will be available as well
![Screenshot](imgs/architecture_diagramm.png)

## Reverse proxy
Theare is an additional service has been added: Proxy
It's Nginx web server that configured as reverse proxy with HTTPS certificates and potentialy can be used as load balancer
![Screenshot](imgs/_architecture_diagramm_feature.png)

## Container dependencies:
 - **mongo express** depends on mongo
 - **grpc-service** depends on mongo
 - **web-api** depends on grpc-service
 - **proxy** depends on web-api

 

## Controllers:
-	All actions of the controller should be async
- Only for the PoC, the swagger is available in production mode and without authentication. I’m not recommending to do the same in a real life. At least authorization on the swaggers or whitelist on the firewall should be implemented. Potentiall, current configuration may lead the security issue in future.

## Connections:
- Should be passed as env variables inside docker-compose.yml, now placed inside the following locations: Mongo DB: GrpcService1\appsettings.json -> MoviesDatabaseSettings
