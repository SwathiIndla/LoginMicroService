# Use the .NET Core SDK as the base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj and restore dependencies (optimizing caching)
#COPY LoginMicroService/Program.cs /app/
#COPY LoginMicroService/WeatherForecast.cs /app/

#COPY LoginMicroService/AuthMicroService.csproj /app/LoginMicroService/

#COPY LoginMicroService/ .

COPY . .
RUN dotnet restore LoginMicroService/AuthMicroService.csproj

# Copy the remaining application code
COPY . ./

# Build the application inside the container

RUN dotnet publish LoginMicroService/AuthMicroService.csproj -c Release -o out
# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "LoginMicroService.dll"]
