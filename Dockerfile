# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj files and restore
COPY src/Portfolio.API/*.csproj ./src/Portfolio.API/
COPY src/Portfolio.Application/*.csproj ./src/Portfolio.Application/
COPY src/Portfolio.Domain/*.csproj ./src/Portfolio.Domain/
COPY src/Portfolio.Infrastructure/*.csproj ./src/Portfolio.Infrastructure/

# Restore using the main project (it will restore dependencies)
RUN dotnet restore src/Portfolio.API/Portfolio.API.csproj

# Copy everything else
COPY . .

# Build
RUN dotnet build src/Portfolio.API/Portfolio.API.csproj -c Release -o /app/build

# Publish
RUN dotnet publish src/Portfolio.API/Portfolio.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set environment
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Copy published files
COPY --from=build /app/publish .

# Run
ENTRYPOINT ["dotnet", "Portfolio.API.dll"]