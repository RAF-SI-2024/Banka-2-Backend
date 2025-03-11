#!/bin/bash

DATABASE_VOLUME_NAME="user_database"

echo "Clearing $DATABASE_VOLUME_NAME data..."
docker volume rm $DATABASE_VOLUME_NAME

if [ $? -ne 0 ]; then
    echo "ERROR: Failed to clear $DATABASE_VOLUME_NAME data."
    read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
    exit 1
fi

echo "$DATABASE_VOLUME_NAME data are cleared successfully."
read -t 10 -n 1 -s -r -p "Press any key to continue..." || echo
exit 0