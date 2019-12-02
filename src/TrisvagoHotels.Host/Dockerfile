FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.101-buster AS build
WORKDIR /src
COPY ["src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj", "src/TrisvagoHotels.Host/"]
COPY ["src/TrisvagoHotels.DataContext/TrisvagoHotels.DataContext.csproj", "src/TrisvagoHotels.DataContext/"]
COPY ["src/TrisvagoHotels.Mappings/TrisvagoHotels.Mappings.csproj", "src/TrisvagoHotels.Mappings/"]
COPY ["src/TrisvagoHotels.Model/TrisvagoHotels.Model.csproj", "src/TrisvagoHotels.Model/"]
COPY ["src/TrisvagoHotels.Api/TrisvagoHotels.Api.csproj", "src/TrisvagoHotels.Api/"]
COPY ["src/TrisvagoHotels.DataContracts/TrisvagoHotels.DataContracts.csproj", "src/TrisvagoHotels.DataContracts/"]
COPY ["src/TrisvagoHotels.Uow/TrisvagoHotels.Uow.csproj", "src/TrisvagoHotels.Uow/"]
COPY ["src/TrisvagoHotels.Services/TrisvagoHotels.Services.csproj", "src/TrisvagoHotels.Services/"]
COPY ["src/TrisvagoHotels.Providers/TrisvagoHotels.Providers.csproj", "src/TrisvagoHotels.Providers/"]
COPY ["src/TrisvagoHotels.Repositories/TrisvagoHotels.Repositories.csproj", "src/TrisvagoHotels.Repositories/"]
RUN dotnet restore "src/TrisvagoHotels.Host/TrisvagoHotels.Host.csproj"
COPY . .
WORKDIR "/src/src/TrisvagoHotels.Host"
RUN dotnet build "TrisvagoHotels.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrisvagoHotels.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrisvagoHotels.Host.dll"]