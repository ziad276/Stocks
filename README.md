# Stocks

A full-stack stock trading web application built with ASP.NET Core MVC. Users can place buy and sell orders for stocks, with real-time price data fetched from the Finnhub API.

## Features

- Place and manage buy/sell stock orders
- Real-time stock price data via Finnhub API
- Input validation with Data Annotations and custom validation logic
- Service layer architecture with interface contracts
- Dependency Injection throughout
- Unit tested with xUnit

## Tech Stack

- **Backend:** C#, ASP.NET Core MVC
- **Architecture:** Service Layer + Repository pattern, Dependency Injection
- **Testing:** xUnit
- **Frontend:** HTML, CSS, JavaScript

## Project Structure
Stocks/
├── ServiceContracts/   # Service interfaces and DTOs
├── Services/           # Concrete service implementations
└── Stocks/             # Controllers, Views, Models

## Getting Started

1. Clone the repo
2. Add your Finnhub API key to `appsettings.json`
3. Run with `dotnet run`
