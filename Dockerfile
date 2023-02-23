#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM node:18.14.0-alpine3.17 as frontend
WORKDIR /frontend
COPY Frontend .
RUN npm ci
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Backend/Lavyn.Web/Lavyn.Web.csproj", "Lavyn.Web/"]
COPY ["Backend/Lavyn.Business/Lavyn.Business.csproj", "Lavyn.Business/"]
COPY ["Backend/Lavyn.Core/Lavyn.Core.csproj", "Lavyn.Core/"]
COPY ["Backend/Lavyn.Domain/Lavyn.Domain.csproj", "Lavyn.Domain/"]
COPY ["Backend/Lavyn.Persistence/Lavyn.Persistence.csproj", "Lavyn.Persistence/"]
RUN dotnet restore "Lavyn.Web/Lavyn.Web.csproj"
COPY ./Backend .
WORKDIR "/src/Lavyn.Web"
RUN dotnet build "Lavyn.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lavyn.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=frontend /frontend/dist/lavyn wwwroot

ENTRYPOINT ["dotnet", "Lavyn.Web.dll"]