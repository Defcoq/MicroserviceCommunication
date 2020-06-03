#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BookCart.API/BookCart.API.csproj", "src/BookCart.API/"]
COPY ["src/BookCart.Domain/BookCart.Domain.csproj", "src/BookCart.Domain/"]
COPY ["src/BookCart.Services/BookCart.Services.csproj", "src/BookCart.Services/"]
COPY ["src/Library/EventBus/EventBus/EventBus.csproj", "src/Library/EventBus/EventBus/"]
COPY ["src/Library/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Library/EventBus/EventBusRabbitMQ/"]
COPY ["src/BookCart.EFRepository/BookCart.EFRepository.csproj", "src/BookCart.EFRepository/"]
RUN dotnet restore "src/BookCart.API/BookCart.API.csproj"
COPY . .
WORKDIR "/src/src/BookCart.API"
RUN dotnet build "BookCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookCart.API.dll"]
