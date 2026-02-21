FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TomoPlan.Web/TomoPlan.Web.csproj", "TomoPlan.Web/"]
RUN dotnet restore "./TomoPlan.Web/TomoPlan.Web.csproj"
COPY . .
WORKDIR "/src/TomoPlan.Web"
RUN dotnet build "./TomoPlan.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TomoPlan.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TomoPlan.Web.dll"]