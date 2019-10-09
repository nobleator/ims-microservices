# Courtesy of: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-2.2
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80/tcp
# EXPOSE 443/tcp
# ENV ASPNETCORE_URLS https://+:443;http://+:80
# ENTRYPOINT ["dotnet", "delivery-service.dll", "--server.urls", "http://*:80"]
ENTRYPOINT ["dotnet", "delivery-service.dll"]
