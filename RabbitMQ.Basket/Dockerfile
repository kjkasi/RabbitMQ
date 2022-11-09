#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RabbitMQ.Basket/RabbitMQ.Basket.csproj", "RabbitMQ.Basket/"]
RUN dotnet restore "RabbitMQ.Basket/RabbitMQ.Basket.csproj"
COPY . .
WORKDIR "/src/RabbitMQ.Basket"
RUN dotnet build "RabbitMQ.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMQ.Basket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQ.Basket.dll"]