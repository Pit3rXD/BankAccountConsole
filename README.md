# BankApp

## Overview

BankApp is a desktop banking application built with WPF and .NET. It allows users to manage a bank account — register, log in, check their balance, make deposits and withdrawals, and review their full transaction history.

The project was built to practice and demonstrate knowledge of C# desktop development, with a focus on clean architecture and design patterns.

## Features

- User registration with password confirmation
- Secure login and logout
- Account balance view with account number and owner details
- Deposit and withdrawal with insufficient funds validation
- Transaction history with date, type, amount and running balance

## Architecture

The application is built using the MVVM pattern. ViewModels handle the logic for each screen and communicate with the rest of the application through interfaces. Data access is abstracted behind the IAccountRepository interface, currently implemented with JSON file storage. Business logic is handled by AuthService and TransactionService. Dependencies are wired together in App.xaml.cs using the Composition Root pattern.

## Technology Stack

- Language: C#
- Framework: .NET 8
- UI: WPF (Windows Presentation Foundation)
- Architecture: MVVM
- Data storage: JSON (current)

## Getting Started

### Prerequisites

- Windows OS
- .NET 8 SDK
- Visual Studio 2022

### Installation

```bash
git clone https://github.com/PiotrLukaszewiczDev/BankAccountConsole.git
```

Open `BankAccountConsole.sln` in Visual Studio, set `BankAccountConsole` as the startup project and press F5.

## Roadmap

- SQL Database - replace JSON storage with Entity Framework Core and SQL Server
- REST API - add ASP.NET Core Web API layer

## Author

Piotr Lukaszewicz
GitHub: https://github.com/PiotrLukaszewiczDev
