To create and start the database, from the project root:

```
docker run --name hotel-db \
           -p 5432:5432 \
           -e POSTGRES_USER=hotel \
           -e POSTGRES_PASSWORD=hotel \
           -e POSTGRES_DB=hotel \
           -v "$(pwd)/init.sql:/docker-entrypoint-initdb.d/init.sql" \
           -d postgis/postgis
```

To start the database (if created already, for example after reboot):
```
docker start hotel-db
```

To stop the database:
```
docker stop hotel-db
```

To delete the database:
```
docker rm hotel-db
```

To check status of the database:
```
docker ps -a | grep hotel-db
```
