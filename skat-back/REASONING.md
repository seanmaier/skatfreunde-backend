**Last modified: 08-05-2025**

# REASONING

Decision-making process for the code implementation.

# Introduction

This document hold information about the reasoning of implementation designs. Nothing in this document is set in stone.
It is used to
roughly describe the design decisions and the reasoning behind them. Discussions and TODOS are marked here to improve
the code and the architecture.

# Models & Database

The models are used to store information about the Database tables. They are used in the `AppDbContext.cs`, to define
and
create the database with the model Builder. Each model is registered with a DbSet<T> property.
To create the database, create a migration first with:

```
dotnet ef migrations add <MigrationTitle>
``` 

Then run the command:

```
dotnet ef database update
```

The Models should **NOT** be used as a DTO, because they are not used to
transfer data!

A BaseModel is created to hold the common properties of all models. The BaseModel is inherited by all models.

The current models are:

| Model            | Definition                                            |
|------------------|-------------------------------------------------------|
| Users            | Users who are using the private website               |
| Players          | A player in the game                                  |
| BlogPosts        | A Blogpost which can be published                     |
| MatchSessions    | A Session of the game. Can contain multiple rounds.   |
| MatchRounds      | A round of the game. Can contain multiple PlayerStats |
| PlayerRoundStats | Junction of Rounds and Player table                   |

# Controllers

The controllers are used to handle the requests from the client. They are used to get and post data to the database.

For basic CRUD Operations there is a `BaseController` which is inherited. If the implementation gets too complex, it is
better to just write a complete new controller.

# Services

The services are used to handle the business logic of the application. They are used to get and post data to the
database.

## Interfaces

For better structuring and testing, each service has an interface. The interface is used to define the methods which are
used in the service. `IService` is the base interface for all services. It is used to define the common methods which
are used in all services. When implementing business specific logic, add the method to the specific interface, like
`IUserService`. The interfaces are in addition used to inject the services in the controllers.

## TODOS

- [ ] Should the data get updated by accessing the database directly or by using ef core methods?

# DTOs

The DTOs are used to transfer data between the client and the server. They are used to define the structure of the data
which is sent to the client. They should only transfer the necessary data. When Creating basic CRUD operations, create
different
DTOs for the create and update methods, even if they have the same properties. This is to improve the readability of the
code and to quickly add changes when necessary.

# Utilities

The utilities are used to hold the common methods which are used in the application. Things like logging, error
handling, validation are stored here.

## Logging

The logging is used to log the information about the application. It can be added to the services and controllers to
ensure better debugging.