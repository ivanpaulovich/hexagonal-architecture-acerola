![Flow of Control: Customer Registration](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/images/logo.png)
# Architectural Style
A solution with Ports and Adapters.

![Flow of Control: Customer Registration](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/images/Acerola-Flow-Of-Control.png)

# Requirements
* [Visual Studio 2017 with Update 3](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes)
* [.NET SDK 2.0](https://www.microsoft.com/net/download/core)
* [Docker](https://docs.docker.com/docker-for-windows/install/)

# Main Architectural Concepts
![Flow of Control: Customer Registration](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/images/hexagonal-arhcitecture-alistair-cockburn.gif)

Allow an application to equally be driven by users, programs, automated test or batch scripts, and to be developed and tested in isolation from its eventual run-time devices and databases.

As events arrive from the outside world at a port, a technology-specific adapter converts it into a usable procedure call and passes it to the application. The application is blissfully ignorant of the nature of the input device. When the application has something to send out, it sends it out through a port to an adapter, which creates the appropriate signals needed by the receiving technology (human or automated). The application has a semantically sound interaction with the adapters on all sides of it, without actually knowing the nature of the things on the other side of the adapters. Check out [Alistair Cockburn blog post.](http://alistair.cockburn.us/Hexagonal+architecture)

## DDD
The use cases of this project is to manage an account balance with deposit and credits and its concepts is enforced by the Domain and Application. Also we use the Eric Evans terms like Entities, Value Object, Aggregates, Aggregate Root and etc. And everything is on a single Bounded Context.

## TDD
From the beginning of the project we developed Unit Tests and that helped us to enforce the business rules and to create an application that prevents bugs intead of finding them. We also have Use Case tests and Mapping Tests and a more sophistecated Integration Tests. 

## SOLID
The SOLID principles are all over the the solution. Knwoleadge of SOLID is not a prerequisite to understand and run the solution but it is highly recommended.

## Microservice
Even though the definition of microservice may be different for different professionals. We have tried to value some aspects like Continous Delivery, modelled around Business Domain and Independent Deployment.

## Logging
Loggin is a detail. We plugged Serilog and configured it to redirect every log message to files.

## Docker
Docker is a detail of this architecture. And it was implemented to help us make a faster and reliable deployment. You could pull the [Manga latest image any time.](https://hub.docker.com/r/ivanpaulovich/acerola/)

## Mongo DB
Mongo DB is a detail. At infrastructure layer we implemented the ICustomerWriteOnlyRepository to update the Mongo database.

## .NET Core 2.0
.NET Core is a detail. Almost everything in this code base could be ported to older versions.

# Environment setup

* Run the `./prerequisites.sh` script to download the MongoDB image and run it as a Docker container. 
Please wait until the ~400mb download to be complete.

```
$ ./prerequisites.sh
Pulling mongodb (mongo:latest)...
latest: Pulling from library/mongo
Digest: sha256:2c55bcc870c269771aeade05fc3dd3657800540e0a48755876a1dc70db1e76d9
Status: Downloaded newer image for mongo:latest
Creating setup_mongodb_1 ...
Creating setup_mongodb_1
Creating setup_mongodb_1 ... done
```
* Check Mongo image with the the following commands:

```
$ docker images
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
mongo               latest              d22888af0ce0        17 hours ago        361MB
$ docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                                            NAMES
ba28cf144478        mongo               "docker-entrypoint..."   2 days ago          Up 2 days           0.0.0.0:27017->27017/tcp                         setup_mongodb_1
```

If everything goes well MongoDB will be running with the following connection string `mongodb://10.0.75.1:27017`.

# Running the latest Docker Build ![ivanpaulovich/acerola](https://dockerbuildbadges.quelltext.eu/status.svg?organization=ivanpaulovich&repository=acerola)

If you like you can run the latest Docker image of this project as following:

```
$ docker run -p 8000:80 -d \
	-e modules__2__properties__ConnectionString=mongodb://10.0.75.1:27017 \
	--name acerola-backend \
	ivanpaulovich/acerola:latest
```
Then navigate to http://localhost:8000/swagger and play with de Web API.

# We are live on Azure

![Live on Azure](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/images/Swagger.png)

You can play with the latest build by navigating to [the Swagger client](http://grape.westus2.cloudapp.azure.com:8000/swagger "Acerola Swagger").

This source code and website should be used only for learning purposes and **all data will be erased weekly**.
