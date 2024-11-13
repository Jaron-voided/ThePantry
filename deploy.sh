#!/bin/bash

# Azure Container Registry
ACR_NAME="pantrycontainerregistry.azurecr.io"
FRONTEND_NAME="pantry-frontend"
BACKEND_NAME="pantry-backend"
SQL_NAME="pantry-sql"
RESOURCE_GROUP="pantry-resource-group"

# Log in to Azure CLI
echo "Logging into Azure..."
az acr login --name $ACR_NAME

# Rebuild, Tag, and Push Images
echo "Building and pushing Docker images..."

# Frontend
docker build -t $ACR_NAME/$FRONTEND_NAME:latest -f ./pantry-frontend/Dockerfile ./pantry-frontend
docker push $ACR_NAME/$FRONTEND_NAME:latest

# Backend
docker build -t $ACR_NAME/$BACKEND_NAME:latest -f ./ThePantry/Dockerfile ./ThePantry
docker push $ACR_NAME/$BACKEND_NAME:latest

# SQL Server
docker build -t $ACR_NAME/$SQL_NAME:latest -f ./sql-scripts/Dockerfile ./sql-scripts
docker push $ACR_NAME/$SQL_NAME:latest

# Restarting Web Apps
echo "Restarting Azure Web Apps..."

# Restart Frontend
az webapp restart --name pantry-frontend-webapp --resource-group $RESOURCE_GROUP

# Restart Backend
az webapp restart --name pantry-backend-webapp --resource-group $RESOURCE_GROUP

# Restart SQL Server (if necessary)
az webapp restart --name pantry-sql-webapp --resource-group $RESOURCE_GROUP

echo "Deployment completed."
