#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GOLDLOAN/GOLDLOAN.csproj", "GOLDLOAN/"]
COPY ["RedisCacheDemo/RedisCacheDemo.csproj", "RedisCacheDemo/"]
COPY ["TokenManager/TokenManager.csproj", "TokenManager/"]
RUN dotnet restore "./GOLDLOAN/GOLDLOAN.csproj"
COPY . .
WORKDIR "/src/GOLDLOAN"
RUN dotnet build "./GOLDLOAN.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GOLDLOAN.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GOLDLOAN.dll"]