# Use the official .NET 8 SDK Alpine image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Felix.csproj", "./"]
RUN dotnet restore

# Copy the rest of your application's source code
COPY . .

# Build the application in Release mode
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 8 ASP.NET Alpine runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the application runs on
EXPOSE 5286

# Set the entry point to run your application
ENTRYPOINT ["dotnet", "Felix.dll"]