# Test: Building a REST API for Articles (.NET)

### Author
- Ciprian Prunoiu

## Objective:

## This test aims to assess key concepts in building a REST API for articles using .NET. It will include:

### .NET API framework: Creating controllers, routing, and handling HTTP requests.
### Data models: Defining models representing articles and handling data access (simulated data or mock repository).
### RESTful principles: Designing resource-oriented endpoints following REST conventions.
### Serialization: Converting data objects to JSON for API responses.

## Scenario:

## Develop a basic REST API for managing articles using .NET Web API. Any minimum two of the following functionalities are required:

### GET /api/articles: Retrieves a list of all articles.
### GET /api/articles/{id}: Retrieves a single article by its ID.
### POST /api/articles: Creates a new article.
### PUT /api/articles/{id}: Updates an existing article by its ID. 
### DELETE /api/articles/{id}: Deletes an article by its ID.

## Technical Requirements:

### Use .NET 8
### Define a model class representing an article with properties like id, title, content, and publishedDate.
### Implement appropriate HTTP verbs (GET, POST, PUT, DELETE) for endpoints.
### Handle error scenarios, returning appropriate HTTP status codes and error messages.
### Use JSON serialization for request and response payloads.

## Bonus points:

### Implement basic authentication or authorization mechanisms.
### Include pagination for retrieving large lists of articles.
### Add validation logic for article data on creation and update.
### Utilize dependency injection for data access layer.

## Deliverables:

### Gitlab / Github source code for the completed API project.

### A brief document outlining any assumptions and design decisions made.

## Evaluation Criteria:

### Functionality and correctness of the implemented endpoints.
### Adherence to RESTful principles and best practices.
### Code quality, readability, and maintainability.
### Documentation and clarity of design choices.

## Additional Notes:

### Using .NET and related libraries for this project.
### No specific database connection is required. Simulating data or using a mock repository is acceptable.
### Feel free to ask clarifying questions

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
