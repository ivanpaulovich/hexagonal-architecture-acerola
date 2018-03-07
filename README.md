![Acerola](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/docs/logo-icon.png) Acerola: Hexagonal Architecture 
=========
[![Acerola latest Docker build](https://dockerbuildbadges.quelltext.eu/status.svg?organization=ivanpaulovich&repository=acerola)](https://hub.docker.com/r/ivanpaulovich/acerola/)

Acerola is a Service Template for helping you to build evolvable, adaptable and maintainable applications with Hexagonal Architecture. It follows the principles from the [Alistair Cockburn blog post.](http://alistair.cockburn.us/Hexagonal+architecture) and has a Domain built on Domain-Driven Design. It is easy for you to start your new microservice based on its guidelines and patterns.

## Compiling from source

To run Acerola from source, clone this repository to your machine, compile and test it:

```sh
git clone https://github.com/ivanpaulovich/acerola.git
cd acerola/source/WebAPI/Acerola.UI
dotnet run
```

## The Architecture
![Hexagonal Architecture](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/docs/hexagonal-arhcitecture-alistair-cockburn.gif)

Allow an application to equally be driven by users, programs, automated test or batch scripts, and to be developed and tested in isolation from its eventual run-time devices and databases.

As events arrive from the outside world at a port, a technology-specific adapter converts it into a usable procedure call and passes it to the application. The application is blissfully ignorant of the nature of the input device. When the application has something to send out, it sends it out through a port to an adapter, which creates the appropriate signals needed by the receiving technology (human or automated). The application has a semantically sound interaction with the adapters on all sides of it, without actually knowing the nature of the things on the other side of the adapters.

| Concept | Description |
| --- | --- |
| DDD | The Use Cases of the Account Balance are the Ubiquitious Language designed in the Domain and Application layers, we use the Eric Evans terms like Entities, Value Object, Aggregates Root and Bounded Context. |
| TDD | From the beginning of the project we developed Unit Tests that helped us to enforce the business rules and to create an application that prevents bugs intead of finding them. We also have more sophisticated tests like Use Case Tests, Mapping Tests and Integration Tests. |
| SOLID | The SOLID principles are all over the the solution. The knowledge of SOLID is not a prerequisite but it is highly recommended. |
| Entity-Boundary-Interactor (EBI) | The goal of EBI architecture is to produce a software implementation agnostic to technology, framework, or database. The result is focus on  use cases and input/output. |
| Microservice | We designed the software around the Business Domain, having Continous Delivery and Independent Deployment. |
| Logging | Logging is a detail. We plugged Serilog and configured it to redirect every log message to the file system. |
| Docker | Docker is a detail. It was implemented to help us make a faster and reliable deployment. |
| MongoDB | MongoDB is a detail. You could create new Data Access implementation and setup it with Autofac. |
| .NET Core 2.0 | .NET Core is a detail. Almost everything in this code base could be ported to other versions. |
| CQRS | **[CQRS](https://martinfowler.com/bliki/CQRS.html)** is an acronym for *Command Query Responsibility Segregation*. This pattern allow splitting our conceptual business model into two representations. The main representation resides on the Command Stack, to perform creates, updates and deletions. The display model resides inside the Query stack, where we can build a Query Model that make easier to aggregate information to display to clients and UI. |

## Flow of Control: The Register Use Case

![Flow of Control: Customer Registration](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/docs/Flow-Of-Control.png)

## Requirements
* [Visual Studio 2017 with Update 3](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes)
* [.NET SDK 2.0](https://www.microsoft.com/net/download/core)
* [Docker](https://docs.docker.com/docker-for-windows/install/)

## Prerequisites Setup

The only one prerequisite to run the Web API is a valid connection string to MongoDB. To help you run it without hard work follow the steps on [prerequisites setup](https://github.com/ivanpaulovich/acerola/wiki/Prerequisites-setup) page.

## Running the latest Docker Build

You can run the Docker container of this project with the following command:

```sh
$ docker run -d -p 8000:80 \
	-e modules__2__properties__ConnectionString=mongodb://10.0.75.1:27017 \
	--name acerola \
	ivanpaulovich/acerola:latest
```
Then navigate to http://localhost:8000/swagger and play with de Swagger.

## Live Demo on Azure

[![Acerola Live Demo on Azure](https://raw.githubusercontent.com/ivanpaulovich/acerola/master/docs/Swagger.png)](http://grape.westus2.cloudapp.azure.com:8000/swagger)

You can play with the latest build of [Acerola](http://grape.westus2.cloudapp.azure.com:8000/swagger "Acerola").
> This source code and website should be used only for learning purposes and **all data will be erased weekly**.
