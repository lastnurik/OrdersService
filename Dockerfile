
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrdersService.API/OrdersService.API.csproj", "OrdersService.API/"]
COPY ["OrdersService.Application/OrdersService.Application.csproj", "OrdersService.Application/"]
COPY ["OrdersService.Domain/OrdersService.Domain.csproj", "OrdersService.Domain/"]
COPY ["OrdersService.Infrastructure/OrdersService.Infrastructure.csproj", "OrdersService.Infrastructure/"]

RUN dotnet restore "OrdersService.API/OrdersService.API.csproj"

COPY . .
RUN dotnet publish "OrdersService.API/OrdersService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "OrdersService.API.dll"]
