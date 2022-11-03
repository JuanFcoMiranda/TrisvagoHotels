#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["**/dependencies.props", "**/dependencies.props"]
COPY ["Directory.Build.props", "src/"]

COPY ["**/TrisvagoHotels.Host.csproj", "src/TrisvagoHotels.Host/"]

RUN dotnet restore "**/TrisvagoHotels.Host.csproj"

COPY . .

WORKDIR "src/TrisvagoHotels.Host"
RUN dotnet build "TrisvagoHotels.Host.csproj" -c Release -o /app --no-restore

FROM build AS publish
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]