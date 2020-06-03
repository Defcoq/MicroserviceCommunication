#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BookShop.API/BookShop.API.csproj", "src/BookShop.API/"]
COPY ["src/BookShop.EFRepository/BookShop.EFRepository.csproj", "src/BookShop.EFRepository/"]
COPY ["src/BookShop.Domain/BookShop.Domain.csproj", "src/BookShop.Domain/"]
COPY ["src/Library/EventBus/EventBus/EventBus.csproj", "src/Library/EventBus/EventBus/"]
COPY ["src/Library/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Library/EventBus/EventBusRabbitMQ/"]
COPY ["src/BookShop.Services/BookShop.Services.csproj", "src/BookShop.Services/"]
RUN dotnet restore "src/BookShop.API/BookShop.API.csproj"
COPY . .
WORKDIR "/src/src/BookShop.API"
RUN dotnet build "BookShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.API.dll"]
