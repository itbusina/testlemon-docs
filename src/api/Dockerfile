# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy the project file and references. Then restore any dependencies (use .csproj for the project name)
WORKDIR /app

# Copy project files
COPY ./src/api/ ./src/api/

# Publish app to out folder
WORKDIR /app/src/api
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/api/out .

# Expose the port your application will run on
EXPOSE 8080

# entry point.
ENTRYPOINT ["dotnet", "api.dll"]