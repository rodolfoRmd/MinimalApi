FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MinimalAPI.csproj", "./"]
RUN dotnet restore "MinimalAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MinimalAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalAPI.dll"]
