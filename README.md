A solution for Account Balance with DDD and CQRS. The full contains Web API application which receives commands and queries to return JSON.

#### Requirements
* [Visual Studio 2017 + Update 3](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes)
* [.NET SDK 2.0](https://www.microsoft.com/net/download/core)
* [Docker](https://docs.docker.com/docker-for-windows/install/)

#### Environment setup

*If you already have valid connections strings for MongoDB you could skip this topic and go to the Running the applications topic.*

* Run the `./up-mongodb.sh` script to run MongoDB as Docker Containers. Please wait until the ~400mb download to be complete.

```
$ ./up-kafka-mongodb.sh
Pulling mongodb (mongo:latest)...
latest: Pulling from library/mongo
Digest: sha256:2c55bcc870c269771aeade05fc3dd3657800540e0a48755876a1dc70db1e76d9
Status: Downloaded newer image for mongo:latest
Creating setup_mongodb_1 ...
Creating setup_mongodb_1
Creating setup_mongodb_1 ... done
```
* Check if the data layer is ready with the following commands:

```
$ docker images
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
mongo               latest              d22888af0ce0        17 hours ago        361MB
```

```
$ docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                                            NAMES
ba28cf144478        mongo               "docker-entrypoint..."   2 days ago          Up 2 days           0.0.0.0:27017->27017/tcp                         setup_mongodb_1
```

If MongoDB is running good it will be working with the `mongodb://10.0.75.1:27017` connection string.
