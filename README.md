# TaskManager

TaskManager is a .NET 8 application designed to manage tasks and users efficiently. 
This application leverages various modern technologies and libraries to provide a robust and scalable solution.

## Features

- **Task Management**: Create, update, delete, and retrieve tasks.
- **User Management**: Manage user information and authentication.
- **Authentication**: Secure the API using JWT Bearer authentication.
- **Validation**: Use FluentValidation for model validation.
- **In-Memory Database**: Utilize Entity Framework Core In-Memory database for development and testing.
- **API Documentation**: Integrated with Swagger for API documentation and testing.

## Technologies Used

- **.NET 8**
- **FluentValidation.AspNetCore**: For model validation.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: For JWT Bearer authentication.
- **Microsoft.EntityFrameworkCore.InMemory**: For in-memory database.
- **Swashbuckle.AspNetCore**: For Swagger API documentation.
- **Swashbuckle.AspNetCore.Annotations**: For Swagger annotations.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:
    git clone https://github.com/yourusername/TaskManager.git
cd TaskManager
1. 
2. Restore the dependencies:
    dotnet restore

3. Run the application:
1. 
    dotnet run TaskManagementAPI

    4. Open the browser and navigate to `https://localhost:5001/swagger` to view the API documentation.

## Testing

The TaskManager solution includes a separate project for testing the main project. 
This project uses xUnit and Moq for unit testing.

### Running Tests

To run the tests, use the following command:
    
## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.