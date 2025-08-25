# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["JenkinProjectRazor/JenkinProjectRazor.csproj", "JenkinProjectRazor/"]
RUN dotnet restore "JenkinProjectRazor/JenkinProjectRazor.csproj"

# Copy the rest of the source code and build the project
COPY . .
WORKDIR "/src/JenkinProjectRazor"
RUN dotnet build "JenkinProjectRazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JenkinProjectRazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JenkinProjectRazor.dll"]
