FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Documentmanager.Api.dll"]