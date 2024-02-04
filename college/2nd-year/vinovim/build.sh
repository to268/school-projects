#!/usr/bin/env bash

echo "[*] Creating environment"

composer install || exit 1
cp -f .env.example .env
docker-compose up -d

sleep 4
docker exec -it postgresql psql -U postgres vinovim -c 'CREATE SCHEMA vinovim'

docker exec -it laravel sed -i 's/;extension=pdo_pgsql/extension=pdo_pgsql/' /opt/bitnami/php/etc/php.ini
docker exec -it laravel php artisan key:generate
docker exec -it laravel php artisan migrate:fresh --seed

echo "[*] Done !"
