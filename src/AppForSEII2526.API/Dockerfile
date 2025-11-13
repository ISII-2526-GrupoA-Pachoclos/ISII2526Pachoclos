FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /AppForSEII2526.API
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out
# Build runtime image
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY --from=build /
ENTRYPOINT ["dotnet", "out/AppForSEII2526.API.dll"]