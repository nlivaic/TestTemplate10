#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TestTemplate10.Api/TestTemplate10.Api.csproj", "src/TestTemplate10.Api/"]
COPY ["src/TestTemplate10.Data/TestTemplate10.Data.csproj", "src/TestTemplate10.Data/"]
COPY ["src/TestTemplate10.Common/TestTemplate10.Common.csproj", "src/TestTemplate10.Common/"]
COPY ["src/TestTemplate10.Core/TestTemplate10.Core.csproj", "src/TestTemplate10.Core/"]
RUN dotnet restore "src/TestTemplate10.Api/TestTemplate10.Api.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate10.Api"
RUN dotnet build "TestTemplate10.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate10.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate10.Api.dll"]