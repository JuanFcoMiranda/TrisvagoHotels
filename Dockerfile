#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0.201-buster-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.201-buster-slim-amd64  AS build
WORKDIR /src

COPY ["build/dependencies.props", "src/build/dependencies.props"]
COPY ["Directory.Build.props", "src/"]

COPY ["src/TrisvagoHotels.Api/TrisvagoHotels.Api.csproj", "src/TrisvagoHotels.Api/"]
COPY ["src/TrisvagoHotels.DataContext/TrisvagoHotels.DataContext.csproj", "src/TrisvagoHotels.DataContext/"]
COPY ["src/TrisvagoHotels.DataContracts/TrisvagoHotels.DataContracts.csproj", "src/TrisvagoHotels.DataContracts/"]
COPY ["src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj", "src/TrisvagoHotels.Host/"]
COPY ["src/TrisvagoHotels.Mappings/TrisvagoHotels.Mappings.csproj", "src/TrisvagoHotels.Mappings/"]
COPY ["src/TrisvagoHotels.Model/TrisvagoHotels.Model.csproj", "src/TrisvagoHotels.Model/"]
COPY ["src/TrisvagoHotels.Providers/TrisvagoHotels.Providers.csproj", "src/TrisvagoHotels.Providers/"]
COPY ["src/TrisvagoHotels.Repositories/TrisvagoHotels.Repositories.csproj", "src/TrisvagoHotels.Repositories/"]
COPY ["src/TrisvagoHotels.Services/TrisvagoHotels.Services.csproj", "src/TrisvagoHotels.Services/"]
COPY ["src/TrisvagoHotels.Uow/TrisvagoHotels.Uow.csproj", "src/TrisvagoHotels.Uow/"]

RUN dotnet restore "src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj"

COPY . .
WORKDIR "/src/src/TrisvagoHotels.Host"
RUN dotnet build "TrisvagoHotels.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]