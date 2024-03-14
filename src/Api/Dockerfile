#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/OptixTechnicalTest.csproj", "src/Api/"]
COPY ["src/Client/OptixTechnicalTest.Client.csproj", "src/Client/"]
COPY ["src/Domain/OptixTechnicalTest.Domain.csproj", "src/Domain/"]
RUN dotnet restore "./src/Api/OptixTechnicalTest.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "./OptixTechnicalTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OptixTechnicalTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf