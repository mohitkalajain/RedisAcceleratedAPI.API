# Redis Accelerated API

This API project is created using .NET Core 6, demonstrating how to leverage Redis and SQL Server to improve application performance.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [NuGet Packages](#nuget-packages)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Redis Accelerated API serves as a demonstration of how to use Redis as a caching layer alongside SQL Server to enhance the performance of data retrieval operations. By caching frequently accessed data in Redis, the API minimizes database calls and reduces latency, leading to a smoother user experience.

## Features

- **Redis Caching:** Utilize Redis to cache data and reduce load times for frequently accessed data.
- **SQL Server Integration:** Connect to SQL Server for persistent data storage and retrieval.
- **Performance Optimization:** Showcase improved performance metrics through caching strategies.

## Technologies Used

- [.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Redis](https://redis.io/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## NuGet Packages

This project includes the following NuGet packages:

- [EntityFrameworkCore](https://www.nuget.org/packages/EntityFrameworkCore) `6.0.35`
- [EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/EntityFrameworkCore.SqlServer) `6.0.35`
- [EntityFrameworkCore.Tools](https://www.nuget.org/packages/EntityFrameworkCore.Tools) `6.0.35`
- [StackExchange.Redis](https://www.nuget.org/packages/StackExchange.Redis) `2.8.16`

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET Core 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Redis](https://redis.io/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/mohitkalajain/RedisAcceleratedAPI.API.git
   cd RedisAcceleratedAPI.API

### Usage

1. Once the API is running, you can use tools like Swagger or Postman to test the endpoints. Here are a few examples of endpoints you can call:

   ```bash
   GET /api/products: Retrieve a list of products with Redis caching enabled.
   GET /api/products/{id}: Retrieve a specific product by ID with caching.

 You can also explore additional endpoints by referring to the Swagger UI once the application is running.

### Contributing

Contributions are welcome! If you would like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/YourFeature
3. Make your changes and commit them
   ```bash
   git commit -m 'Add some feature'
4. Push to the branch
   ```bash
   git push origin feature/YourFeature
5. Open a Pull Request.

   
### License

This project is open source and available for use under the terms of the MIT License. See the LICENSE file for details.

    


