# OpenSearch .NET ToDo API

A .NET 9 web API project demonstrating integration with OpenSearch for student data management.

## Overview

This project provides a simple API to interact with an OpenSearch instance, allowing you to:
- View all OpenSearch indices
- Retrieve student records from the OpenSearch database
- Query student data

## Prerequisites

- .NET 9 SDK
- Running OpenSearch instance (default: https://localhost:9200)
- OpenSearch credentials (default in code: admin/C#abcd@1234)

## Setup and Installation

1. Clone this repository
```bash
git clone [repository-url]
cd Opensearch-Dotnet-Todo
```

2. Ensure you have a running OpenSearch instance
   - You can run OpenSearch in Docker with:
   ```bash
   docker run -d -p 9200:9200 -p 9600:9600 -e "discovery.type=single-node" opensearchproject/opensearch:latest
   ```

3. Build and run the application
```bash
dotnet build
dotnet run --project Opensearch-Dotnet-Todo
```

4. The API will be accessible at http://localhost:5171

## Student Data Structure

The system stores student information with the following structure:

```json
{
  "Id": 1002,
  "FirstName": "Jane",
  "LastName": "Smith",
  "GradYear": 2023,
  "Gpa": 3.92
}
```

## API Endpoints

### 1. Check API Status
```
GET /
```
Returns a simple "Hello World!" message to verify the API is running.

### 2. List All OpenSearch Indices
```
GET /opensearch/indices
```
Returns a list of all indices in your OpenSearch instance.

### 3. Retrieve All Students
```
GET /students/
```
Returns all student documents stored in the "students" index.

## Testing Commands

You can use the included `test-commands.sh` script to test the API functionality:

```bash
chmod +x test-commands.sh
./test-commands.sh
```

This will make requests to:
- List all indices
- Retrieve all students
- Create a test student document directly in OpenSearch

## Security Note

The application is configured to accept any SSL certificate from the OpenSearch server (`ServerCertificateValidationCallback((o, certificate, chain, errors) => true)`). This should be modified for production environments to validate certificates properly.

## Dependencies

- OpenSearch.Net: Low-level OpenSearch client library
- OpenSearch.Client: High-level OpenSearch client library

## Development

This project uses a slim builder pattern with minimal API endpoints. It's designed to be a starting point for building more complex OpenSearch applications.

The connection to OpenSearch is configured in the `Program.cs` file, along with the API endpoints.
