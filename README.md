# History Timeline Learning Application API with .NET Core 3.1

This repository contains the API as well as the entirety of back end for written in C# and .NET Core 3.1.
This API is used by the [frond-end part of the project](https://github.com/A-Petrauskas/history-react-app).

## Built Using:
- Dependency Injection
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [xUnit.net](https://github.com/xunit/xunit)

## Web Application Architecture
The three-tier architecture is a well-established software application architecture that divides applications into three physical and logical tiers: the presentation tier, the application tier and the data tier.

<img src="https://user-images.githubusercontent.com/72355656/177051654-1a7f290c-d8a4-4018-a5cc-e37472906979.png" width="400">

## Back end
The backend application project was implemented adhering to the Repository Pattern which provides a boundary between the Data Access Layer (DAL) and the Business Layer (BL).
<img src="https://user-images.githubusercontent.com/72355656/177051576-682a820d-ce74-4bc6-8ca0-f490ba59f2c9.png" width="850">

The “Repositories” project consists off “Entities”, “Interfaces”, database connection serving file and a “Repository” class file for each of the collections: “Events” and “Levels”.
Entities are single instances of the domain objects saved in MongoDB as documents.

The “Services” project consists off “Contracts”, “Interfaces” and services: “EventsService”, “LevelsService” and “GameService”.
The contracts are objects that are used for business logic and calculations in the services namespace as well as for the controller responses. When information from the database is required by the services the request goes through the repositories (DAL). Repositories return an entity object which then needs to be mapped to a contract object. For this task AutoMapper was used.

AutoMapper is injected via the constructor dependency injection into each service class. According to the mappings defined in “MappingProfile.cs”, entity objects are mapped to contracts and vice versa. 

- Tested with unit tests ([xUnit.net](https://github.com/xunit/xunit)) 


## API endpoints
To design the API’s endpoints, the application’s pages, windows, and transitions between them I made a state machine diagram. 
This diagram helps document the routines and protocols of my API in addition to clarifying the structure and behavior of the application.
In the diagram the states are pages or screens and the transitions are API calls. 
Transition arrows which require an API call are denoted with both the endpoint name and its HTTP method. Some transitions to the main menu page are omitted for clearness.

<img src="https://user-images.githubusercontent.com/72355656/177051961-9eee3e17-b00a-423a-95b4-3a715835ef42.png" width="800">

- Tested using [Postman](https://www.postman.com/) 

The API has these endpoints (as auto-documented by [Swagger](https://github.com/swagger-api)):

<img src="https://user-images.githubusercontent.com/72355656/177051316-235fa387-8e6a-4d08-bcbc-a85fc852c05e.png" width="400">
