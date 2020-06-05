FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /project
COPY ["/src/BookCart.API/BookCart.API.csproj", "/src/BookCart.API/"]
RUN dotnet restore "/src/BookCart.API/BookCart.API.csproj"
COPY . .
WORKDIR "/project/src/BookCart.API"
RUN dotnet build "BookCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookCart.API.dll"]
