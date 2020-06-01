FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /project
COPY ["src/BookShop.API/BookShop.API.csproj", "src/BookShop.API/"]
COPY . .
WORKDIR "/project/src/BookShop.API"
RUN dotnet build "BookShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.API.dll"]