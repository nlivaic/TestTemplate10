#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TestTemplate10.WorkerServices/TestTemplate10.WorkerServices.csproj", "src/TestTemplate10.WorkerServices/"]
COPY ["src/TestTemplate10.Core/TestTemplate10.Core.csproj", "src/TestTemplate10.Core/"]
COPY ["src/TestTemplate10.Common/TestTemplate10.Common.csproj", "src/TestTemplate10.Common/"]
RUN dotnet restore "src/TestTemplate10.WorkerServices/TestTemplate10.WorkerServices.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate10.WorkerServices"
RUN dotnet build "TestTemplate10.WorkerServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate10.WorkerServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/TestTemplate10.WorkerServices/entrypoint.sh", "src/entrypoint.sh"]
COPY ["src/TestTemplate10.WorkerServices/wait-for-it.sh", "src/wait-for-it.sh"]
CMD ["/bin/bash", "src/entrypoint.sh"]
