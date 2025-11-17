# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .sln and .csproj files to restore dependencies
COPY *.sln .
COPY src/BartugWeb.WebApi/*.csproj src/BartugWeb.WebApi/
COPY src/Core/BartugWeb.ApplicationLayer/*.csproj src/Core/BartugWeb.ApplicationLayer/
COPY src/Core/BartugWeb.DomainLayer/*.csproj src/Core/BartugWeb.DomainLayer/
COPY src/External/BartugWeb.InfrastructureLayer/*.csproj src/External/BartugWeb.InfrastructureLayer/
COPY src/External/BartugWeb.PersistanceLayer/*.csproj src/External/BartugWeb.PersistanceLayer/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the WebApi project for release
WORKDIR /app/src/BartugWeb.WebApi
RUN dotnet publish -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Create a non-root user
RUN addgroup --system appgroup && adduser --system --ingroup appgroup --no-create-home appuser
RUN chown -R appuser:appgroup /app
USER appuser

# Expose port 8080 for the application
EXPOSE 8080
 
# Set the entrypoint for the container
ENTRYPOINT ["dotnet", "BartugWeb.WebApi.dll"]