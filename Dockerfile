# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Portfolio/Portfolio.csproj ./Portfolio/
RUN dotnet restore "Portfolio/Portfolio.csproj"

COPY . .
WORKDIR /src/Portfolio
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Bind ASP.NET Core to Render's port
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Portfolio.dll"]
