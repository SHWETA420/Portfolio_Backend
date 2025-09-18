# Step 1: Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY Portfolio/Portfolio.csproj ./Portfolio/
RUN dotnet restore "Portfolio/Portfolio.csproj"

# Copy all files and publish
COPY . .
WORKDIR /src/Portfolio
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish

# Step 2: Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Expose port 8080 for Render
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "Portfolio.dll"]
