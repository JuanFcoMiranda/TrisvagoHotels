FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src

# Restore
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

CMD dotnet TrisvagoHotels.Host.dll