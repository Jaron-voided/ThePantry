#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to be ready to accept connections
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'sPZxKpNKnzmi7P9GXGzdJDRoEBUbRmGjrCQn' -Q "SELECT 1" &> /dev/null
do
  echo "SQL Server is still starting up..."
  sleep 5
done

echo "SQL Server is ready, running initialization script..."

# Run the SQL script to create the database and user
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'sPZxKpNKnzmi7P9GXGzdJDRoEBUbRmGjrCQn' -d master -i /var/opt/mssql/scripts/create-db-and-user.sql

wait
