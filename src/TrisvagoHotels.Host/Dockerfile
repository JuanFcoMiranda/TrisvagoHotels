#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-buster-slim AS build
WORKDIR /src

#COPY ["build/dependencies.props", "src/build/dependencies.props"]
#COPY ["Directory.Build.props", "src/"]

COPY ["TrisvagoHotels.Host.csproj", "src/TrisvagoHotels.Host/"]
COPY ["TrisvagoHotels.Api.csproj", "src/TrisvagoHotels.Api/"] 
COPY ["src/TrisvagoHotels.Model.csproj", "src/TrisvagoHotels.Model/"]
COPY ["src/TrisvagoHotels.DataContracts.csproj", "src/TrisvagoHotels.DataContracts/"]
COPY ["src/TrisvagoHotels.DataContext.csproj", "src/TrisvagoHotels.DataContext/"]
COPY ["src/TrisvagoHotels.Mappings.csproj", "src/TrisvagoHotels.Mappings/"]
COPY ["src/TrisvagoHotels.Providers.csproj", "src/TrisvagoHotels.Providers/"]
COPY ["src/TrisvagoHotels.Repositories.csproj", "src/TrisvagoHotels.Repositories/"]
COPY ["src/TrisvagoHotels.Uow.csproj", "src/TrisvagoHotels.Uow/"]
COPY ["src/TrisvagoHotels.Services.csproj", "src/TrisvagoHotels.Services/"]

RUN dotnet restore "src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj"

COPY . .

WORKDIR "/src/src/TrisvagoHotels.Host"
RUN dotnet build "TrisvagoHotels.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]