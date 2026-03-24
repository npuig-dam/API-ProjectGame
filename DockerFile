# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
# This uses the 'GameAPI' folder seen in your image
COPY ["GameAPI/GameAPI.csproj", "GameAPI/"]
RUN dotnet restore "GameAPI/GameAPI.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/GameAPI"

# Build and publish the app
RUN dotnet publish "GameAPI.csproj" -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Port configuration for Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Start the API
ENTRYPOINT ["dotnet", "GameAPI.dll"]