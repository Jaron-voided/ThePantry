# Use the official SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest AS db-setup

# Set environment variables for SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=sPZxKpNKnzmi7P9GXGzdJDRoEBUbRmGjrCQn  
# Ensure this matches entrypoint.sh

# Copy SQL setup scripts to container
COPY create-db-and-user.sql /var/opt/mssql/scripts/create-db-and-user.sql
COPY entrypoint.sh /opt/entrypoint.sh

# Set the entrypoint script to run SQL setup on container start
ENTRYPOINT ["/bin/bash", "/opt/entrypoint.sh"]
