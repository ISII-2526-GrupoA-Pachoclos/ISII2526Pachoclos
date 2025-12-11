FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ISII2526Pachoclos.LogViewer.csproj", "ISII2526Pachoclos.LogViewer/"]
RUN dotnet restore "ISII2526Pachoclos.LogViewer/ISII2526Pachoclos.LogViewer.csproj"

COPY . ISII2526Pachoclos.LogViewer/

WORKDIR "/src/ISII2526Pachoclos.LogViewer"
RUN dotnet build "ISII2526Pachoclos.LogViewer.csproj" -c Release -o /app/build
RUN dotnet publish "ISII2526Pachoclos.LogViewer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 5672

# Punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "ISII2526Pachoclos.LogViewer.dll"]