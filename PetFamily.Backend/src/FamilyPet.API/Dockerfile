﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FamilyPet.API/FamilyPet.API.csproj", "src/FamilyPet.API/"]
COPY ["src/PetFamily.Application/PetFamily.Application.csproj", "src/PetFamily.Application/"]
COPY ["src/PetFamily.Domain/PetFamily.Domain.csproj", "src/PetFamily.Domain/"]
COPY ["src/PetFamily.Infrastructure/PetFamily.Infrastructure.csproj", "src/PetFamily.Infrastructure/"]
RUN dotnet restore "src/FamilyPet.API/FamilyPet.API.csproj"
COPY . .
WORKDIR "/src/src/FamilyPet.API"
RUN dotnet build "./FamilyPet.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FamilyPet.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyPet.API.dll"]
