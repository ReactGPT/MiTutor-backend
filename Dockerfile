FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["MiTutor/MiTutor.csproj", "MiTutor/"]
RUN dotnet restore "./MiTutor/MiTutor.csproj"

COPY . .
WORKDIR "/src/MiTutor"
RUN dotnet build "./MiTutor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiTutor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY mkdir -p /app/certificates
COPY MiTutor/certificates/cert.pfx /app/certificates

ENTRYPOINT ["dotnet", "MiTutor.dll"]
