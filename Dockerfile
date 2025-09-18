# Step 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY Portfolio/Portfolio.csproj ./Portfolio/
RUN dotnet restore "Portfolio/Portfolio.csproj"

# Copy everything else and build
COPY . .
WORKDIR /src/Portfolio
RUN dotnet publish "Portfolio.csproj" -c Release -o /app

# Step 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Expose port 8080 for Render
EXPOSE 8080

# MongoDB connection string (can be overridden via Render Environment Variables)
ENV MONGO_URI="mongodb+srv://shwetasingh201901_db_user:SdBNRisZGB3AN8iS@cluster0.fhazvb4.mongodb.net/PortfolioDb?retryWrites=true&w=majority"

# Dynamically bind to the port Render provides
ENV PORT=8080
ENV ASPNETCORE_URLS=http://*:$PORT

# Start the app
ENTRYPOINT ["dotnet", "Portfolio.dll"]
