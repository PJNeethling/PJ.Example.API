#!/bin/bash

echo "Starting..."

echo "Deploying Flyway scripts"

docker-compose -p "pj-example-db" -f docker-compose.db.yml up pj-example-db-flyway

docker-compose -p "pj-example-db" -f docker-compose.db.yml rm -f -s -v pj-example-db-flyway

read -n 1 -s -r -p "Press any key to continue"