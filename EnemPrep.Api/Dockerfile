FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia apenas os .csproj primeiro para cache de restore
COPY ["EnemPrep.Api/EnemPrep.Api.csproj", "EnemPrep.Api/"]
COPY ["EnemPrep.Application/EnemPrep.Application.csproj", "EnemPrep.Application/"]
COPY ["EnemPrep.Domain/EnemPrep.Domain.csproj", "EnemPrep.Domain/"]
COPY ["EnemPrep.Infrastructure/EnemPrep.Infrastructure.csproj", "EnemPrep.Infrastructure/"]
RUN dotnet restore "EnemPrep.Api/EnemPrep.Api.csproj"

# Copia todo o código e publica
COPY . .
WORKDIR "/src/EnemPrep.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EnemPrep.Api.dll"]
