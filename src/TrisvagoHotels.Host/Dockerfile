FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.102-buster AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.102-buster AS build
WORKDIR /src

COPY ["build/dependencies.props", "src/build/dependencies.props"]
COPY ["Directory.Build.props", "src/"]

COPY ["src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj", "src/TrisvagoHotels.Host/"]
COPY ["src/TrisvagoHotels.Api/TrisvagoHotels.Api.csproj", "src/TrisvagoHotels.Api/"]
COPY ["src/TrisvagoHotels.DataContext/TrisvagoHotels.DataContext.csproj", "src/TrisvagoHotels.DataContext/"]
COPY ["src/TrisvagoHotels.DataContracts/TrisvagoHotels.DataContracts.csproj", "src/TrisvagoHotels.DataContracts/"]
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
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]
