# Stage 1: Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000 

# Set environment variable to bind to all IPs and port 5000
ENV ASPNETCORE_URLS=http://+:5000

# Stage 2: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ThePantry.csproj", "./"]
RUN dotnet restore "./ThePantry.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ThePantry.csproj" -c Release -o /app/build

# Stage 3: Publish the application
FROM build AS publish
RUN dotnet publish "ThePantry.csproj" -c Release -o /app/publish

# Stage 4: Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThePantry.dll"]
