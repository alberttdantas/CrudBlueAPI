FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CrudBlue.API/CrudBlue.API.csproj", "CrudBlue.API/"]
RUN dotnet restore "CrudBlue.API/CrudBlue.API.csproj"
COPY . .
WORKDIR "/src/CrudBlue.API"
RUN dotnet build "CrudBlue.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudBlue.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudBlue.API.dll"]