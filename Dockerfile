# 1. Build Stage - Change 8.0 to 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["GameAPI/GameAPI.csproj", "GameAPI/"]
RUN dotnet restore "GameAPI/GameAPI.csproj"

COPY . .
WORKDIR "/src/GameAPI"
RUN dotnet publish "GameAPI.csproj" -c Release -o /app/publish

# 2. Runtime Stage - Change 8.0 to 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "GameAPI.dll"]