# BankApp

## Overview

BankApp is a desktop banking application built with WPF and ASP.NET Core Web API. It lets users register an account, log in securely, view their balance, make deposits and withdrawals, and review their full transaction history.

The project was built to practice and demonstrate knowledge of full-stack C# development — a WPF client consuming a real REST API, rather than talking to local storage directly.

## Features

- User registration with BCrypt password hashing
- Secure login with JWT (JSON Web Token) authentication
- Account balance view with account number and owner details
- Deposit and withdrawal, with server-side insufficient-funds validation surfaced back to the UI
- Transaction history with date, type, amount and running balance
- Transfer between accounts

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern on the client and a layered architecture on the API. In WPF, ViewModels handle screen logic and talk to the API exclusively through the `IApiService` interface, backed by `HttpClient`; dependencies are wired in `App.xaml.cs` using the Composition Root pattern.

The API follows Controllers → Services → Repositories → Entity Framework Core → SQL Server. Passwords are hashed with BCrypt, sessions are stateless via JWT Bearer tokens, and domain exceptions (insufficient funds, duplicate username, missing account) are mapped to proper HTTP status codes with readable messages instead of generic server errors.

`BankAccountCore` was the original local, JSON-file-based implementation the project started from. It's being phased out now that the WPF client is fully migrated to the API.

## Technology Stack

- **Language:** C#
- **UI:** WPF (Windows Presentation Foundation), .NET 9
- **API:** ASP.NET Core Web API, .NET 10
- **ORM:** Entity Framework Core
- **Database:** SQL Server (LocalDB for local development)
- **Architecture:** MVVM, Repository Pattern, Dependency Injection
- **Security:** BCrypt password hashing, JWT Bearer tokens

## Getting Started

### Prerequisites

- .NET 9 SDK and .NET 10 SDK
- Visual Studio 2022
- SQL Server LocalDB (installed with Visual Studio's "Data storage and processing" workload) or a full SQL Server instance

### Installation

```bash
git clone https://github.com/PiotrLukaszewiczDev/BankApp.git
```

Open `BankApp.sln` in Visual Studio, update the connection string in `BankApp.Api/appsettings.json`, for example:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BankAppDb;Trusted_Connection=True;TrustServerCertificate=True"
```

and apply migrations from the Package Manager Console (with `BankApp.Api` as the default project):

```
Update-Database
```

The WPF client needs the API running to do anything useful. Right-click the solution → *Properties* → *Configure Startup Projects* → *Multiple startup projects* → set both `BankApp.Api` and `WpfBankAccount` to **Start**, then press F5. This launches the API (Swagger UI at `/swagger`) and the WPF client together.

Most API endpoints require a JWT: call `POST /api/auth/login` in Swagger, copy the returned token, then click *Authorize* and paste it as `Bearer <token>` to test endpoints directly.

## Roadmap

- [x] User registration and login
- [x] Deposit and withdrawal
- [x] Transaction history
- [x] REST API with EF Core and SQL Server
- [x] BCrypt password hashing
- [x] JWT authentication
- [x] Connect WPF client to REST API
- [x] Retire legacy JSON storage code from `BankAccountCore` (kept as a small shared library for `TransactionType` and `IAccountNumberGenerator`)
- [x] Transfers between accounts
- [ ] Automated tests

## Author

Piotr Łukaszewicz
GitHub: [github.com/PiotrLukaszewiczDev](https://github.com/PiotrLukaszewiczDev)
