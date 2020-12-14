#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Learning_AspNet_Core/Learning_AspNet_Core.csproj", "Learning_AspNet_Core/"]
RUN dotnet restore "Learning_AspNet_Core/Learning_AspNet_Core.csproj"
COPY . .
WORKDIR "/src/Learning_AspNet_Core"
RUN dotnet build "Learning_AspNet_Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Learning_AspNet_Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Learning_AspNet_Core.dll"]