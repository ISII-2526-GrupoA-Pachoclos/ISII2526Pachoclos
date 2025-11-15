# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo los archivos necesarios para restaurar (optimiza caché)
COPY AppForSEII2526.API/*.csproj ./AppForSEII2526.API/
# Si hay más proyectos (ej. ApplicationCore, Infrastructure), copiarlos también

# Restaurar dependencias
RUN dotnet restore AppForSEII2526.API/AppForSEII2526.API.csproj

# Copiar todo el código fuente
COPY . .

# Publicar la aplicación
WORKDIR /src/AppForSEII2526.API
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080

# Copiar solo los archivos publicados
COPY --from=build /app/publish .

# Ejecutar la app
ENTRYPOINT ["dotnet", "AppForSEII2526.API.dll"]