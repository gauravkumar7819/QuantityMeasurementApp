# Use the official .NET 8 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["QuantityMeasurement.API/QuantityMeasurement.API.csproj", "QuantityMeasurement.API/"]
COPY ["QuantityMeasurement.Business/QuantityMeasurement.Business.csproj", "QuantityMeasurement.Business/"]
COPY ["QuantityMeasurement.Repository/QuantityMeasurement.Repository.csproj", "QuantityMeasurement.Repository/"]
COPY ["QuantityMeasurement.Model/QuantityMeasurement.Model.csproj", "QuantityMeasurement.Model/"]
COPY ["QuantityMeasurement.Application/QuantityMeasurement.Application.csproj", "QuantityMeasurement.Application/"]
COPY ["QuantityApp/QuantityApp.csproj", "QuantityApp/"]

# Restore dependencies
RUN dotnet restore "QuantityMeasurement.API/QuantityMeasurement.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/QuantityMeasurement.API"
RUN dotnet build "QuantityMeasurement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuantityMeasurement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables for Render
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the application
ENTRYPOINT ["dotnet", "QuantityMeasurement.API.dll"]