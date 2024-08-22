FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HuilianMedical.Backend/HuilianMedical.Backend/HuilianMedical.Backend.csproj", "HuilianMedical.Backend/HuilianMedical.Backend/"]
COPY ["HuiLianMedical.Share/HuiLianMedical.Share.csproj", "HuiLianMedical.Share/"]
RUN dotnet restore "HuilianMedical.Backend/HuilianMedical.Backend/HuilianMedical.Backend.csproj"
COPY . .
WORKDIR "/src/HuilianMedical.Backend/HuilianMedical.Backend"
RUN dotnet build "HuilianMedical.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HuilianMedical.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HuilianMedical.Backend.dll"]
