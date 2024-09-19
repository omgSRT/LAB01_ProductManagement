# Use Linux-based .NET 6 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use Linux-based .NET 6 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LAB01_ProductManagement.csproj", "./"]
RUN dotnet restore "./YourProjectName.csproj"
COPY . .
WORKDIR "/src/LAB01_ProductManagement"
RUN dotnet build "LAB01_ProductManagement.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "LAB01_ProductManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LAB01_ProductManagement.dll"]
