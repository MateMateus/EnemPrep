FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY ["EnemPrep.Web/EnemPrep.Web.csproj", "EnemPrep.Web/"]
RUN dotnet restore "EnemPrep.Web/EnemPrep.Web.csproj"

COPY EnemPrep.Web/ EnemPrep.Web/
WORKDIR "/src/EnemPrep.Web"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EnemPrep.Web.dll"]
