# Todo Management System - ASP.NET Core Minimal API

This project implements a complete todo list management system using ASP.NET Core Minimal API with Entity Framework Core and SQLite in-memory database.

## Architecture Overview

The application follows clean architecture principles with clear separation of concerns:

```txt
McpTodoServer.ContainerApp/
├── Models/           # Entity models
├── Data/            # Database context
├── Repositories/    # Data access layer
└── Services/        # Business logic layer
```

## Features Implemented

### 1. **Todo Item Entity Model** (`Models/TodoItem.cs`)

- **ID**: Auto-incrementing primary key
- **Text**: Required text content (max 500 characters)
- **IsCompleted**: Boolean flag for completion status
- **CreatedAt**: Timestamp when item was created
- **UpdatedAt**: Timestamp when item was last modified

### 2. **Database Context** (`Data/TodoDbContext.cs`)

- SQLite in-memory database configuration
- Entity Framework Core with proper entity configuration
- Database indexes for performance optimization
- Proper column constraints and default values

### 3. **Repository Layer** (`Repositories/`)

- **ITodoRepository**: Interface defining data operations
- **TodoRepository**: Implementation with full CRUD operations
- **Five Core Operations**:
  - **Create**: Add new todo items
  - **List**: Retrieve all todo items (ordered by completion status and creation date)
  - **Update**: Modify todo item text
  - **Complete**: Mark todo items as completed
  - **Delete**: Remove todo items
- **Additional Operations**:
  - **GetById**: Retrieve specific todo item
- Comprehensive error handling and logging
- Input validation and sanitization

### 4. **Service Layer** (`Services/`)

- **ITodoService**: Interface for business logic operations
- **TodoService**: Implementation with enhanced functionality
- **Additional Features**:
  - **Statistics**: Calculate completion metrics
  - Enhanced logging and error handling
  - Business logic validation

### 5. **Database Configuration**

- **SQLite In-Memory Database**: Fast, ephemeral storage
- **Entity Framework Core 9.0**: Latest ORM features
- **Database Initialization**: Automatic schema creation on startup
- **Connection Management**: Proper handling of in-memory database lifecycle

## Technical Implementation Details

### Database Schema

```sql
CREATE TABLE "TodoItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TodoItems" PRIMARY KEY AUTOINCREMENT,
    "Text" TEXT NOT NULL,
    "IsCompleted" INTEGER NOT NULL DEFAULT 0,
    "CreatedAt" TEXT NOT NULL DEFAULT (datetime('now')),
    "UpdatedAt" TEXT NULL
);

-- Performance indexes
CREATE INDEX "IX_TodoItems_CreatedAt" ON "TodoItems" ("CreatedAt");
CREATE INDEX "IX_TodoItems_IsCompleted" ON "TodoItems" ("IsCompleted");
```

### Dependency Injection Configuration

- **DbContext**: Configured with SQLite in-memory provider
- **Repository**: Registered as scoped service
- **Service**: Registered as scoped service
- **Logging**: Console and debug logging in development

### Error Handling

- Comprehensive exception handling at all layers
- Structured logging with correlation IDs
- Input validation with proper error messages
- Null reference protection

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Compatible IDE (Visual Studio, VS Code, Rider)

### Running the Application

```bash
cd workshop/src/McpTodoServer.ContainerApp
dotnet build
dotnet run
```

### Testing the Implementation

The application includes a demo endpoint at `/todo-demo` that demonstrates all five operations:

```http
GET http://localhost:5242/todo-demo
```

This endpoint will:

1. Create three todo items
2. List all items
3. Update one item's text
4. Complete one item
5. Generate statistics
6. Delete one item
7. Return final state

## Code Quality Features

### Logging

- Structured logging throughout all layers
- Different log levels for different scenarios
- Correlation tracking for request tracing

### Validation

- Input validation at service boundaries
- Data annotation validation on entities
- Business rule validation in services

### Performance

- Database indexes on frequently queried columns
- Efficient LINQ queries
- Proper async/await usage throughout

### Maintainability

- Clean separation of concerns
- Interface-based design for testability
- Comprehensive XML documentation
- Consistent naming conventions

## Package Dependencies

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="9.0.7" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.7" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.*" />
```

## Database Initialization

The application automatically:

1. Creates an in-memory SQLite database on startup
2. Applies the schema and creates all tables
3. Sets up indexes for optimal performance
4. Maintains the database connection throughout the application lifecycle

## Notes

- **No API endpoints**: As requested, no REST API endpoints are implemented for todo operations
- **No initial data**: Database starts empty as specified
- **In-memory only**: Data is lost when application restarts
- **Development ready**: Includes enhanced logging and debugging features
- **Production considerations**: Connection string and logging can be configured via appsettings.json
