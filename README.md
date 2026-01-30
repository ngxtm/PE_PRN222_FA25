# Real Estate Management System - PRN222 Practical Exam Example

## Overview

This project serves as an instructional example for students preparing for the PRN222 practical exam at FPT University. It demonstrates a clean 3-tier architecture implementation with real-time features.

## Features

- **Contract Management**: Full CRUD operations with validation
- **Broker Assignment**: Link contracts to brokers
- **User Authentication**: Session-based login with role-based access
- **Real-time Updates**: SignalR integration for live data refresh
- **Search & Filter**: Find contracts by title, property type, or date

## Tech Stack

- .NET 9.0 / ASP.NET Core Razor Pages
- Entity Framework Core 9.0
- SQL Server
- SignalR

## Project Structure

```
PE_PRN222_FA25_MinhNTSE196686/
├── DAL/                    # Data Access Layer
│   ├── Entities/           # EF Core models
│   ├── Repositories/       # Data access logic
│   └── Validations/        # Custom validation attributes
├── BLL/                    # Business Logic Layer
│   └── Services/           # Business services
└── RealEstateManagement/   # Presentation Layer
    ├── Pages/              # Razor Pages
    ├── Hubs/               # SignalR hubs
    └── Filter/             # Authorization filters
```

## Getting Started

1. Update the connection string in `appsettings.json`
2. Run the SQL script from `docs/` to create the database
3. Build and run the project

## License

For educational purposes only.
