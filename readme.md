# ASP.NET Project

## Overview

This repository contains an ASP.NET project that demonstrates [brief description of your project's purpose].

## Prerequisites

To run this project, you'll need:

- .NET SDK (recommended version 7.0 or later)
- Visual Studio 2022 or Visual Studio Code
- SQL Server (if applicable)

## Installation

### Clone the Repository

```bash
git clone https://github.com/bimaega15/Fwd-Programmer-.Net-dan-SQL-Server-ERP-Ega.git
cd my-test-net
```

### Restore Dependencies

```bash
dotnet restore
```

### Database Setup (if applicable)

1. Update the connection string in `appsettings.json` to match your local SQL Server
2. Apply migrations to create the database:

```bash
dotnet ef database update
```

## Running the Application

### Using Visual Studio

1. Open the solution file (.sln) in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Press F5 to run the application in debug mode

### Using Command Line

```bash
dotnet build
dotnet run
```

The application will be available at `https://localhost:5001` and `http://localhost:5000` by default.

## Project Structure

- **Controllers/**: Contains API controllers
- **Models/**: Data models and entities
- **Views/**: UI templates
- **Services/**: Business logic and services
- **Data/**: Database context and configurations

## Documentation

- System Analyst documentation is available at [SystemAnalyst/SystemAnalyst-Transaksi.pdf](SystemAnalyst/SystemAnalyst-Transaksi.pdf)
- Database backup file is available at [Database/Employee.bak](Database/Employee.bak). You can restore this backup using SQL Server Management Studio by right-clicking on Databases, selecting "Restore Database", and following the wizard to import the .bak file.

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

[Specify your license information]
