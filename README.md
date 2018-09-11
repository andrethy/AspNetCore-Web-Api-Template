# AspNetCore Web API Template
My suggestion of what an ASP.NET Core Web API using .NET Core 2.1 could look like. This is a template to make you start faster, by already utilizing some well-used techniques, and having the basics and most vital elements already implemented. 
The template is open for modifications, so please leave comments and suggestions! :-)


## Who is this template for, and why use it?
This template if for all that is interested in seeing how a web API could be designed using the .NET Core framework. It is by no means a perfect fit for all use cases, but it is an easy way to start a project where its purpose is to design a web API with simple CRUD functionality. It could also be used as inspiration of how to use some of the included packages / middleware, such as the logging framework or how error handling could be dealt with.

## Areas of interest:

**Clean architecture**  
The architecture of this template follows the clean architecture. The architecture is built on three projects: Web (UI), Core and Infrastructure.
I recommend to read a more thorough introduction, which can be found here: https://github.com/ardalis/CleanArchitecture - Written by Steve Smith (a Microsoft MVP and ASP.NET contributor)

**Error handling**
The error handling implemented in the template have been focusing on providing meaningful messages for mobile app developers. All errors consist of 2 parts: an error code and an error message.
The error code is an integer that represents a specific exception (either thrown by you or the application)
![A screenshot of ExceptionType which indicates how a number represents an error](https://i.imgur.com/C8bpfu2.png)

A custom exception by the name **ApiException** has also been made to support this **ExceptionType** enum.
**ApiException** inherits from **ArgumentException** as it works as a small adjustment from this exception. Instead of only taking a message, the **ApiException** receives the **ExceptionType** and a message as parameter,  which is retrieveable in the error handling class: **ErrorHandlingMiddleware**.

The error handling middleware is used as a global exception catcher, and in this class a proper exception message is created to give the request caller a meaningful idea of what went wrong.
All exceptions will be showed as an internal server error with a status code of 500, unless the exception is of type **ApiException**, which means it is an error that you (the developer) have thrown, and therefore needs to be handled.
A snippet of the class **ErrorHandlingMiddleware**:

![](https://i.imgur.com/TkVTWDP.png)

If you look at the picture, you will notice that there's an ExceptionType with the name "InvalidPropertyValue". All ApiExceptions with the ErrorType of InvalidPropertyValue are automatically thrown, as I have included what I call a **ValidateModelStateFilter**. **ValidateModelStateFilter** is a filter that injects itself into AspNetCore's lifecycle, so its code will be executed for each *async* action in your controllers. What it does, is to always check if the ModelState is valid, and if it isn't, it will create an error message stating which properties are invalid, and throw a new **ApiException** with this message to be handled by the **ErrorHandlingMiddleware**.


**Dependency injection**
If you are familiar with the .NET Core framework, it is not unknown to you that dependency injection is built-in, which makes it a breeze to inject dependencies:
![](https://i.imgur.com/fNDJ1CR.png)


**Integration tests**
For testing purposes, SQLite has been used to incorporate an easy setup of a testing database, because SQLite supports a InMemory database. This means it can use a relational database without having one "physically" installed somewhere. 

Included in this template, is a class **BaseTest** provided, which all test classes are meant to inherit from, as this **BaseTest** will ensure that each test has a new database with seeded data (provided through the included **DbInitializer**). This ensures that each test will be independent of each other and to provide you with meaningful data for your tests (as long as you update this seeder!)


# Packages included:
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
        Swashbuckle is responsible for making the Web API Swagger compatible. This provides a nice, easy to use interface to test all your actions.
* [Serilog](https://github.com/serilog/serilog-aspnetcore)
        Logging framework to easily log to files, database or the console.
* [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore)
        An ORM from Microsoft to make it fast and easy to communicate with a database. This template uses a MSSQL server, but a list of database providers is available here: https://docs.microsoft.com/en-us/ef/core/providers/
* [SQLite](https://github.com/aspnet/Microsoft.Data.Sqlite)
		The database provider used for testing


