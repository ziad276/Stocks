# Stocks

A stocks management web application built with ASP.NET Core MVC, developed as a hands-on project alongside an ASP.NET Core course. The project is continuously improved as new concepts are covered.

## Features

- Create and manage buy/sell stock orders
- Input validation with data annotations and custom validation logic
- Service layer pattern with interface contracts
- Unit tested with xUnit

## Tech Stack

- **Backend:** C#, ASP.NET Core MVC
- **Architecture:** Service layer + Dependency Injection
- **Testing:** xUnit
- **Frontend:** HTML, CSS, JavaScript

## Project Structure
Stocks/
├── ServiceContracts/   # Service interfaces and DTOs
├── Services/           # Concrete service implementations
└── Stocks/             # Controllers, Views, Models
