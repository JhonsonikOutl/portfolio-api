# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file
COPY Portfolio.slnx .

# Copy csproj files
COPY src/Portfolio.API/*.csproj ./src/Portfolio.API/
COPY src/Portfolio.Application/*.csproj ./src/Portfolio.Application/
COPY src/Portfolio.Domain/*.csproj ./src/Portfolio.Domain/
COPY src/Portfolio.Infrastructure/*.csproj ./src/Portfolio.Infrastructure/

# Restore
RUN dotnet restore Portfolio.slnx

# Copy everything else
COPY . .

# Build
RUN dotnet build Portfolio.slnx -c Release -o /app/build

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