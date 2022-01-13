#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/LabManagamentSchedule.Worker.Consumer/LabManagamentSchedule.Worker.Consumer.csproj", "src/LabManagamentSchedule.Worker.Consumer/"]
RUN dotnet restore "src/LabManagamentSchedule.Worker.Consumer/LabManagamentSchedule.Worker.Consumer.csproj"
COPY . .
WORKDIR "/src/src/LabManagamentSchedule.Worker.Consumer"
RUN dotnet build "LabManagamentSchedule.Worker.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LabManagamentSchedule.Worker.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabManagamentSchedule.Worker.Consumer.dll"]