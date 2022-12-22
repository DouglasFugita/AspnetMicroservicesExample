#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApps/AspnetRunBasics6/AspnetRunBasics6.csproj", "WebApps/AspnetRunBasics6/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "WebApps/AspnetRunBasics6/AspnetRunBasics6.csproj"
COPY . .
WORKDIR "/src/WebApps/AspnetRunBasics6"
RUN dotnet build "AspnetRunBasics6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspnetRunBasics6.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspnetRunBasics6.dll"]