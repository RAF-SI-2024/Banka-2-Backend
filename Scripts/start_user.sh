#!/bin/bash

# Script to run docker-compose for the bank.user service and handle failures

# Define the service name
SERVICE_NAME="bank.user"

# Run docker-compose up for the specified service
echo "Starting $SERVICE_NAME service and its dependencies..."
docker-compose up $SERVICE_NAME

# Check if the command failed
if [ $? -ne 0 ]; then
    echo "ERROR: Failed to start $SERVICE_NAME service or its dependencies."
    echo "Check the logs for more details."
    read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
    exit 1
fi

# If successful
echo "$SERVICE_NAME service and its dependencies started successfully."
read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
exit 0