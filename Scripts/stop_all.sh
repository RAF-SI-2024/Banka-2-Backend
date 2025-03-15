#!/bin/bash

echo "Stopping all services..."
docker-compose down

if [ $? -ne 0 ]; then
    echo "ERROR: Failed to services."
    read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
    exit 1
fi

echo "All services were stopped successfully."
read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
exit 0