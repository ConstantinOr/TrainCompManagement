FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["TrainComponentManagementSystem/TrainComponentManagementSystem.csproj", "TrainComponentManagementSystem/"]
RUN dotnet restore "TrainComponentManagementSystem/TrainComponentManagementSystem.csproj"
COPY . .
WORKDIR "/src/TrainComponentManagementSystem"
RUN dotnet build "TrainComponentManagementSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrainComponentManagementSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainComponentManagementSystem.dll"]
