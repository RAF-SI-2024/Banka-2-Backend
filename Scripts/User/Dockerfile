FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bank.Application/Bank.Application.csproj", "Bank.Application/"]
COPY ["Bank.Database/Bank.Database.csproj", "Bank.Database/"]
COPY ["Bank.UserService/Bank.UserService.csproj", "Bank.UserService/"]
RUN dotnet restore "Bank.UserService/Bank.UserService.csproj"
COPY . .
WORKDIR "/src/Bank.UserService"
RUN dotnet build "Bank.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bank.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.UserService.dll"]
