# Intruction
This project is a university assignment completed with my classmate, Pedro Azambuja. The application is a Deposit Rental Administrator, allowing users to rent deposits for specified periods, while an admin can manage these rentals. It’s a local application developed in C# with .NET on the backend, utilizing an Azure SQL database hosted in a Docker container and Entity Framework as the ORM. For the frontend, we used Blazor and Bootstrap.

## Methodology
The goal of this project was to apply newly acquired skills, including Test-Driven Development (TDD), Gitflow, Clean Code practices, and SOLID principles. Detailed explanations of how these skills were implemented can be found in the "Entrega 1" and "Entrega 2" folders. Be advised that these documents are in Spanish.


## Used technologies
- .net C#
- Docker
- AzureSQL
- Blazor
- Bootstrap
- EntityFramework
- Git

## How to Run

### Prerequisites

- Docker installed and running on your computer
- .NET SDK 6 installed
- Git (optional, but useful)

1. Clone the Git repository:
    ```bash
    git clone https://github.com/Zelaschi/Deposito.git
    ```
2. Run the Docker container:
    ```bash
    docker-compose up
    ```
3. Run the application:
    - Navigate to `Entrega2/Publish`
    - Execute `InterfazDeUsuario.exe`