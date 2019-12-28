FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.101-buster AS build
WORKDIR /src

COPY ../*.sln .
COPY . .

RUN dotnet restore
COPY . .
WORKDIR /src/TrisvagoHotels.Host
RUN dotnet build "TrisvagoHotels.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]