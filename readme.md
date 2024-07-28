# Design Document

## Assumptions
- Using .NET 8 Web API for developing the RESTful service.
- Mock repository is used to simulate data access, avoiding the need for a database.
- Basic validation is implemented using data annotations in the Article model.

## Design Decisions
- **Models**: Article model is placed in the `Models` directory for better project structure.
- **Repositories**: Defined an interface `IArtRepo` and implemented it in `MockArtRepo` for data access.
- **Controllers**: `ArtController` handles all API requests related to articles.
- **Routes**: Attributed routing to define routes directly on controller actions
- **JSON Serialization**: Handled automatically by .NET 8 Web API.
- **Error Handling**: Returns appropriate HTTP status codes and error messages for various error scenarios.

## Bonus Features
- Added basic validation for the Article model using data annotations.
- Considered token-based authentication (register then login for obtaining the token), pagination.
- Implemented custom JsonConverter (DateTimeConverter) for custom datetime formatting
