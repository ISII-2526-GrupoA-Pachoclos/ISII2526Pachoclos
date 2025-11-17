FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /AppForSEII2526.API

COPY . ./
RUN dotnet restore 

RUN dotnet publish -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 

WORKDIR /AppForSEII2526.API
COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "out/AppForSEII2526.API.dll"]
EXPOSE 8080