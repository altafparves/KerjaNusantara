# KerjaNusantara.API

The backend API for the KerjaNusantara application, built with ASP.NET Core.

## Prerequisites

- .NET 9.0 SDK

## Getting Started

1.  Navigate to the API directory:
    ```bash
    cd KerjaNusantara.API
    ```

2.  Restore dependencies:
    ```bash
    dotnet restore
    ```

3.  Run the application:
    ```bash
    dotnet run
    ```

## API Documentation

The API Documentation is available via Swagger UI.
Once the application is running, navigate to:
`https://localhost:<port>/swagger`

(Check the console output for the specific port, typically 5001 or similar).

## Architecture

This project follows a Clean Architecture approach:
-   **KerjaNusantara.Domain**: Core entities.
-   **KerjaNusantara.Repository**: Data access layer.
-   **KerjaNusantara.Services**: Business logic layer.
-   **KerjaNusantara.API**: Entry point, Controllers.

## System Overview & Data Flow

The system uses a layered architecture to ensure separation of concerns. Data flows from the API layer down to the Repository layer for storage, and results bubble back up to the user.

### Architecture Diagram

![KerjaNusantara API Architecture](C:/Users/ASUS/.gemini/antigravity/brain/2b264eb8-59aa-421a-bcb5-d7228292bc36/api_architecture_diagram_1765884172709.png)

### Data Flow Diagram

```mermaid
sequenceDiagram
    participant C as Client
    participant Ctrl as Controller
    participant Svc as Service
    participant Repo as Repository
    participant DB as JSON Storage

    C->>Ctrl: HTTP Request (e.g., POST /api/jobs)
    Ctrl->>Svc: Call Business Method (e.g., CreateJob)
    Svc->>Svc: Validate Logic & Business Rules
    Svc->>Repo: Request Data/Action (e.g., Add Job)
    Repo->>DB: Serialize & Write to File
    DB-->>Repo: Acknowledge
    Repo-->>Svc: Return Entity
    Svc-->>Ctrl: Return Result
    Ctrl-->>C: HTTP Response (JSON)
```

### Component Interaction

1.  **Client Request**: The client sends an HTTP request (GET, POST, PUT, DELETE) to an API Endpoint.
2.  **Controller**: The Controller accepts the request, validates the input format (DTOs), and selects the appropriate Service.
3.  **Service**: The Service applies core business logic—checking permissions, calculating values (e.g., matching scores), and ensuring data integrity.
4.  **Repository**: The Service delegates data persistence to the Repository, which handles the low-level details of reading/writing to the `data/*.json` files.

### High-Level Architecture (Visual)

```mermaid
graph TD
    Client([👤 Client/User])
    
    subgraph API_Layer ["API Layer (Controllers)"]
        JobsCtrl[Jobs Controller]
        UserCtrl[User Controller]
    end

    subgraph Service_Layer ["Service Layer (Business Logic)"]
        JobSvc[⚙️ Job Service]
        UserSvc[⚙️ User Service]
        MatchSvc[⚙️ Matching Service]
    end

    subgraph Repo_Layer ["Repository Layer (Data Access)"]
        JobRepo[📂 Job Repository]
        UserRepo[📂 User Repository]
    end

    subgraph Storage ["Data Storage"]
        JobDB[(jobs.json)]
        UserDB[(users.json)]
    end

    Client -->|HTTP Request| API_Layer
    JobsCtrl -->|Calls| JobSvc
    UserCtrl -->|Calls| UserSvc
    
    JobSvc -->|Read/Write| JobRepo
    UserSvc -->|Read/Write| UserRepo
    
    JobSvc -.->|Uses| MatchSvc
    
    JobRepo -->|Persist| JobDB
    UserRepo -->|Persist| UserDB
    
    style Client fill:#f9f,stroke:#333,stroke-width:2px
    style API_Layer fill:#e1f5fe,stroke:#01579b
    style Service_Layer fill:#fff3e0,stroke:#e65100
    style Repo_Layer fill:#e8f5e9,stroke:#1b5e20
    style Storage fill:#f3e5f5,stroke:#4a148c
```


