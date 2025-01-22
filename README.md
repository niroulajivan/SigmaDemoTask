# SigmaDemoTask
Simple task to develop REST API. 


Candidate Management System
==========================
This is a demo project to register and update candidates using a single endpoint. It is designed to showcase basic API implementation with proper layering and testing.

Project Architecture
The project is developed using Clean Architecture principles. It is divided into four layers:

Main Project: Contains the controller and API endpoint.
Business Layer: Includes business logic and services.
Core Layer: Defines the domain models and Repository (interfaces).
Data Layer: Handles database operations through the repository pattern.

Endpoint
=============================

 /api/Candidate/AddUpdateCandidate

This is the only API endpoint in the project. It checks the candidate's email:
If the email is new, the candidate is added.
If the email already exists, the data is updated.
Design Pattern
The project uses the Repository Pattern in the data layer to abstract database operations and make them more manageable.

Database
================
The project uses Microsoft SQL Server (MSSQL) as the database. All required migrations are created and applied using Entity Framework Core.

Unit Testing
======================
Testing is implemented to ensure the project works properly:
Service Layer Test: Tests the business logic for adding or updating a candidate using mocked repository methods.
Repository Layer Test: Tests the database operations using a real SQL Server database.


Technologies Used
=========================
Backend: ASP.NET Core 8
Database: Microsoft SQL Server (MSSQL)
ORM: Entity Framework Core
Unit Testing: xUnit
API Documentation: Swagger UI


Features
==============
Register a new candidate or update an existing one based on the email.
Fully tested business and repository layers to ensure reliability.
Designed to follow clean and modular architecture.

How to Run
==================
Clone the repository.
Update the appsettings.json file with your MSSQL connection string.
Build and run the application.
Access the Swagger UI at http://localhost:{port}/swagger to test the API.