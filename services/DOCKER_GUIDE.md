# Docker Guide For This Project

For LocalDB + SSMS setup, see [SSMS_SETUP.md](SSMS_SETUP.md).

This project runs with Docker Compose. The `docker-compose.yml` file in this folder starts the complete local stack: SQL Server and all three APIs.

Use Docker Compose for every stack operation.

## Run the complete local stack

```powershell
cd "C:\Users\MaegeshSuresh\Desktop\Insurance\Insurance Policy Management Platform\services"
Copy-Item .env.example .env
notepad .env
docker compose -f docker-compose.yml up --build -d
```

Set a strong `MSSQL_SA_PASSWORD` in `.env` before starting. Do not commit `.env` or share its password.

This stack creates the databases automatically and connects the APIs to the `sqlserver` container over the Docker network. It does not use LocalDB.

Open:

- Premium API: `http://localhost:7001/swagger`
- Payment API: `http://localhost:7002/swagger`
- Notification API: `http://localhost:7003/swagger`

You can connect SSMS to `localhost,1433` using SQL Server Authentication with login `sa` and the password in `.env`.

## Stop the complete stack

```powershell
docker compose -f docker-compose.yml down
```

To also delete the containerized databases:

```powershell
docker compose -f docker-compose.yml down -v
```

## One-time check

```powershell
docker --version
docker compose version
docker ps
```

## Run one service

The full stack is recommended because the APIs need the SQL Server container. To rebuild and start only one API after the stack has already started:

```powershell
docker compose -f docker-compose.yml up --build -d notification-service
```

Replace `notification-service` with `payment-service` or `premium-service` as needed.

## Docker Building Blocks

- `Containerfile` defines an image. It is built by Docker with `docker build`.
- `docker-compose.yml` defines the containers, network, persistent SQL Server volume, and startup order.
- Docker Compose asks the Docker engine to create and run those resources.
- SQL Server runs in the `insurance-sqlserver` container. SSMS is just a Windows client for viewing that database.

The word `dockerfile` remains in Compose because that is the standard field name for the Docker build definition.
