# Todo Manager API

A robust task management API built with .NET 6, featuring authentication, task management, and AI-powered task interpretation.

## 🚀 Technology Stack Analyzer

- **[Database]**: PostgreSQL
- **[Authentication]**: 
  - JWT Bearer Authentication
  - OAuth 2.0 (Google and GitHub)
- **[Swagger]**: API Documentation
- **[Background Jobs]**: Hangfire
- **[AI Integration]**: OpenAI API
- **[Email Service]**: SendGrid
- **[Reports]**: (QuestPDF, EPPlus)
- **[Code Quality]**:
  - StyleCop for code analysis
  - SonarAnalyzer for code quality
  - Microsoft Code Analysis

## 📋 API Endpoints

### Authentication (`/api/v1/auth`)
- `POST /` - Sign in with credentials
- `GET /signin-google` - Google OAuth login
- `GET /signin-github` - GitHub OAuth login

### Tasks (`/api/v1/task`)
- `GET /uid/{taskUid}` - Get a specific task
- `GET /tasks` - Get all tasks for the authenticated user
- `POST /` - Create a new task
- `PUT /uid/{taskUid}/update` - Update an existing task
- `POST /uid/{taskUid}/complete` - Mark a task as completed
- `DELETE /uid/{taskUid}` - Delete a task

### AI Task Interpretation (`/api/v1/taskai`)
- `POST /interpret` - Interpret natural language into a structured task

### Reports (`/api/v1/report`)
- `GET /{format}` - Generate daily task reports (PDF or Excel)

### Users (`/api/v1/user`)
- `POST /` - Create a new user

## 🔧 Configuration

The application requires the following configuration in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "SqlConnectionString": "Your PostgreSQL connection string"
  },
  "JwtSettings": {
    "Secret": "Your JWT secret",
    "Issuer": "Your JWT issuer",
    "Audience": "Your JWT audience",
    "ExpirationMinutes": 60
  },
  "SendGrid": {
    "ApiKey": "Your SendGrid API key",
    "FromEmail": "Your sender email",
    "FromName": "Your sender name"
  },
  "Authentication": {
    "Google": {
      "ClientId": "Your Google OAuth client ID",
      "ClientSecret": "Your Google OAuth client secret"
    },
    "GitHub": {
      "ClientId": "Your GitHub OAuth client ID",
      "ClientSecret": "Your GitHub OAuth client secret"
    },
    "OpenAI": {
      "ClientSecret": "Your OpenAI API key"
    }
  }
}
```

## 🏗️ Project Structure

The solution follows a clean architecture pattern with the following projects:

- `Api.GRRInnovations.TodoManager` - Main API project
- `Api.GRRInnovations.TodoManager.Application` - Application layer
- `Api.GRRInnovations.TodoManager.Domain` - Domain models and interfaces
- `Api.GRRInnovations.TodoManager.Infrastructure` - Infrastructure implementations
- `Api.GRRInnovations.TodoManager.Interfaces` - Shared interfaces
- `Api.GRRInnovations.TodoManager.Security` - Security-related implementations
- `Api.GRRInnovations.TodoManager.Tests` - Test project

## 🔒 Security Features

- JWT-based authentication
- OAuth 2.0 integration with Google and GitHub
- Secure password handling
- Role-based authorization
- HTTPS enforcement

## 📊 Features

- Task management (CRUD operations)
- Task categorization
- Task completion tracking
- AI-powered task interpretation
- Daily task reports (PDF/Excel)
- Background job processing
- Email notifications
- Multi-provider authentication

## 🚀 Getting Started

1. Clone the repository
2. Configure the `appsettings.json` with your credentials
3. Run the database migrations
4. Start the application

The API will be available at `https://localhost:7022` with Swagger documentation at `/swagger`. 

## 💬 Contact

If you have questions or want to talk about Feature Flags and .NET best practices, reach out:

📱 [LinkedIn](https://www.linkedin.com/in/gabriel-ribeiro96/)
