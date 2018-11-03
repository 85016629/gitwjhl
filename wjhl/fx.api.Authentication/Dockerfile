FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY fx.api.Authentication/fx.api.Authentication.csproj fx.api.Authentication/
RUN dotnet restore fx.api.Authentication/fx.api.Authentication.csproj
COPY . .
WORKDIR /src/fx.api.Authentication
RUN dotnet build fx.api.Authentication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish fx.api.Authentication.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "fx.api.Authentication.dll"]
