# Estágio de Runtime (ASP.NET 9.0)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Estágio de Build (SDK 9.0)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish src/FinTrack.Api/FinTrack.Api.csproj -c Release -o /app/out

# Estágio Final / Execução
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FinTrack.Api.dll"]