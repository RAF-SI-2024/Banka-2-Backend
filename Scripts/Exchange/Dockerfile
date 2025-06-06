FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 9080
EXPOSE 9081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bank.Application/Bank.Application.csproj", "Bank.Application/"]
COPY ["Bank.Database/Bank.Database.csproj", "Bank.Database/"]
COPY ["Bank.Http/Bank.Http.csproj", "Bank.Http/"]
COPY ["Bank.Link/Bank.Link.csproj", "Bank.Link/"]
COPY ["Bank.OpenApi/Bank.OpenApi.csproj", "Bank.OpenApi/"]
COPY ["Bank.Permissions/Bank.Permissions.csproj", "Bank.Permissions/"]
COPY ["Bank.ExchangeService/Bank.ExchangeService.csproj", "Bank.ExchangeService/"]
RUN dotnet restore "Bank.ExchangeService/Bank.ExchangeService.csproj"
COPY . .
WORKDIR "/src/Bank.ExchangeService"
RUN dotnet build "Bank.ExchangeService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bank.ExchangeService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.ExchangeService.dll"]
