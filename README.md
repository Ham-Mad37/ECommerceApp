# 🛒 ECommerceApp

> A production-ready E-Commerce REST API built with ASP.NET Core.

## 📌 Overview

ECommerceApp is a backend REST API for an e-commerce platform, designed with maintainability, separation of concerns, security, and scalability in mind.

The API provides the core functionality required by an e-commerce system, including product management, shopping cart operations, order processing, authentication, and database persistence.

## ✨ Features

* 🔐 Authentication & Authorization
* 👤 User management
* 📦 Product management
* 🛒 Shopping cart
* 🧾 Order management
* 🗄️ Entity Framework Core
* 🔑 JWT Bearer Authentication
* ✅ Request validation
* ⚠️ Centralized error handling
* 📚 Swagger / OpenAPI documentation
* 💉 Dependency Injection
* 🔄 Database migrations

## 🛠️ Technology Stack

| Technology            | Purpose              |
| --------------------- | -------------------- |
| ASP.NET Core          | Web API              |
| C#                    | Programming language |
| Entity Framework Core | ORM                  |
| SQL Server            | Database             |
| JWT                   | Authentication       |
| Swagger / OpenAPI     | API documentation    |
| Git / GitHub          | Version control      |

## 📁 Project Structure

```text
ECommerceApp
│
├── ECommerceApp.Api
│   ├── Controllers
│   ├── ...
│   ├── Program.cs
│   ├── appsettings.json
│   └── ...
│
├── ECommerceApp.slnx
└── README.md
```

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

* .NET SDK
* SQL Server
* Git

### Clone the repository

```bash
git clone https://github.com/Ham-Mad37/ECommerceApp.git
cd ECommerceApp
```

### Configure the database

Update the connection string in your configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

### Configure JWT

Configure the required JWT settings using environment variables or user secrets.

> Never commit production secrets, passwords, connection strings, or JWT signing keys to source control.

### Apply migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

## 📚 API Documentation

When running in development, Swagger provides interactive API documentation.

```text
https://localhost:<port>/swagger
```

You can use Swagger to:

* Explore available endpoints
* Send HTTP requests
* Authenticate using JWT
* Inspect request/response models

## 🔐 Authentication

The API uses JWT Bearer authentication.

After authentication, include the access token in requests:

```http
Authorization: Bearer <your-token>
```

Protected endpoints require a valid JWT.

## 🛒 E-Commerce Flow

The main business flow follows:

```text
User
 │
 ▼
Browse Products
 │
 ▼
Add Product to Cart
 │
 ▼
Manage Cart
 │
 ▼
Checkout
 │
 ▼
Create Order
 │
 ▼
Order Items
 │
 ▼
Order Processing
```

## 🧪 Testing

API endpoints can be tested using:

* Swagger
* Postman
* `.http` files
* Automated integration/unit tests

## 🔧 Configuration

Application configuration should be provided through environment-specific configuration and environment variables.

Production secrets should never be stored directly in `appsettings.json`.

## 📈 Production Considerations

For production deployment, configure:

* Production database
* Secure JWT signing key
* HTTPS
* Environment-specific configuration
* Logging and monitoring
* Database migrations
* CORS policy
* Authentication and authorization
* Secret management

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch

```bash
git checkout -b feature/my-feature
```

3. Commit your changes

```bash
git commit -m "Add my feature"
```

4. Push the branch

```bash
git push origin feature/my-feature
```

5. Open a Pull Request

## 📄 License

This project is available under the license included in the repository.
