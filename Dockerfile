# Step 1: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Step 2: SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY Portfolio/Portfolio.csproj ./Portfolio/
RUN dotnet restore "Portfolio/Portfolio.csproj"

# Copy all source code and publish
COPY . .
WORKDIR /src/Portfolio
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish

# Step 3: Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Start the app
ENTRYPOINT ["dotnet", "Portfolio.dll"]
