# FreshCart - Grocery Delivery & Smart Cart API

FreshCart is a comprehensive grocery delivery platform built with ASP.NET Core 8 Web API, featuring an intelligent Smart Cart system, real-time delivery tracking, and multi-store support.

## 🚀 Features

### Core Features
- **Multi-Store Grocery Platform**: Support for multiple grocery stores under one platform
- **Smart Cart Engine**: AI-powered cart with suggestions, substitutions, and budget tracking
- **Real-Time Delivery Tracking**: Live GPS tracking with SignalR
- **Recurring Shopping Lists**: Automated recurring grocery orders
- **Advanced Search & Discovery**: Full-text search with filters and personalized recommendations
- **Comprehensive Review System**: Product and store reviews with manager responses
- **Flexible Promotion System**: Coupons, discounts, BOGO offers, and bundle deals

### Technical Features
- **Clean Architecture**: Domain-driven design with CQRS pattern
- **JWT Authentication**: Secure authentication with refresh tokens
- **Real-Time Communication**: SignalR for live updates
- **Distributed Caching**: Redis for performance optimization
- **Background Jobs**: Hangfire for scheduled tasks
- **Comprehensive Logging**: Serilog with structured logging
- **API Documentation**: OpenAPI/Swagger with JWT support

## 🏗️ Architecture

The application follows Clean Architecture principles:

```
├── Domain/                 # Core business entities and rules
│   └── Entities/          # Domain entities (User, Product, Order, etc.)
├── Application/           # Application logic and interfaces
│   ├── Commands/          # CQRS Commands
│   ├── Queries/           # CQRS Queries
│   ├── DTOs/             # Data Transfer Objects
│   ├── Validators/        # FluentValidation rules
│   └── Mappings/         # AutoMapper profiles
├── Infrastructure/        # External concerns
│   ├── Data/             # Entity Framework DbContext
│   └── Services/         # External service implementations
└── Controllers/          # API Controllers
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8 Web API
- **Database**: SQL Server 2022 with Entity Framework Core 8
- **Caching**: Redis (StackExchange.Redis)
- **Authentication**: JWT Bearer tokens
- **Real-Time**: SignalR
- **Background Jobs**: Hangfire
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Logging**: Serilog
- **Documentation**: Swagger/OpenAPI
- **External Services**: Stripe, SendGrid, Twilio, Azure Blob Storage

## 🚦 Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Redis Server
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-repo/freshcart.git
   cd freshcart
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection strings**
   
   Update `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FreshCartDb_Dev;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true",
       "Redis": "localhost:6379"
     }
   }
   ```

4. **Create and seed database**
   ```bash
   dotnet ef database update
   ```

5. **Start Redis** (if using Docker)
   ```bash
   docker run -d -p 6379:6379 redis:alpine
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```

7. **Access Swagger UI**
   
   Navigate to: `https://localhost:7000/swagger`

## 📚 API Documentation

### Authentication Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/v1/auth/register` | Register new user |
| POST | `/api/v1/auth/login` | User login |
| POST | `/api/v1/auth/logout` | User logout |
| GET | `/api/v1/auth/verify-email` | Verify email address |
| POST | `/api/v1/auth/forgot-password` | Request password reset |
| POST | `/api/v1/auth/reset-password` | Reset password |

### Product Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/products/search` | Search products with filters |
| GET | `/api/v1/products` | Get products by category/store |
| GET | `/api/v1/products/{id}` | Get product details |
| GET | `/api/v1/products/barcode/{upc}` | Lookup by barcode |
| GET | `/api/v1/products/recommendations` | Personalized recommendations |

### Cart Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/cart` | Get current cart |
| POST | `/api/v1/cart/items` | Add item to cart |
| PATCH | `/api/v1/cart/items/{id}` | Update cart item |
| DELETE | `/api/v1/cart/items/{id}` | Remove cart item |
| POST | `/api/v1/cart/substitute` | Apply product substitution |
| POST | `/api/v1/cart/voice-add` | Add item via voice |

## 🔧 Configuration

### JWT Settings
```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "FreshCart",
    "Audience": "FreshCartUsers",
    "ExpiryMinutes": 15
  }
}
```

### External Services
```json
{
  "ExternalServices": {
    "Stripe": {
      "PublishableKey": "pk_test_...",
      "SecretKey": "sk_test_...",
      "WebhookSecret": "whsec_..."
    },
    "SendGrid": {
      "ApiKey": "SG...",
      "FromEmail": "noreply@freshcart.com"
    },
    "GoogleMaps": {
      "ApiKey": "AIza..."
    }
  }
}
```

## 🧪 Testing

Run unit tests:
```bash
dotnet test
```

## 📦 Deployment

### Docker Deployment
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FreshCart.csproj", "."]
RUN dotnet restore "./FreshCart.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FreshCart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreshCart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreshCart.dll"]
```

### Azure App Service
1. Create App Service with .NET 8 runtime
2. Configure connection strings in Application Settings
3. Deploy using GitHub Actions or Azure DevOps

## 🔒 Security Features

- **JWT Authentication** with refresh tokens
- **Password Hashing** using BCrypt
- **Rate Limiting** to prevent abuse
- **CORS** configuration for frontend integration
- **Security Headers** (HSTS, XSS Protection, etc.)
- **Input Validation** with FluentValidation
- **SQL Injection Protection** via EF Core

## 📊 Monitoring & Logging

- **Structured Logging** with Serilog
- **Health Checks** for database and Redis
- **Application Insights** integration ready
- **Performance Monitoring** with built-in metrics

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

For support and questions:
- Create an issue on GitHub
- Email: support@freshcart.com
- Documentation: [Wiki](https://github.com/your-repo/freshcart/wiki)

## 🗺️ Roadmap

- [ ] Native mobile apps (React Native)
- [ ] Machine learning recommendations
- [ ] Multi-language support
- [ ] Advanced analytics dashboard
- [ ] Integration with POS systems
- [ ] Voice ordering via Alexa/Google Assistant

---

**FreshCart** - Revolutionizing grocery delivery with smart technology 🛒✨