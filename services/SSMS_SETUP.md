# SSMS Setup Guide (LocalDB)

This project is configured to use SQL Server LocalDB for local development.

## 1) Connect in SSMS

Use these values in the Connect dialog:

- Server name: `(localdb)\\MSSQLLocalDB`
- Authentication: `Windows Authentication`

## 2) Create the service databases

Open [sqlserver/create-databases.sql](sqlserver/create-databases.sql) in SSMS and execute it.

This creates:

- `premium_db`
- `payment_db`
- `notification_db`

## 3) Verify app connection strings

The APIs already use LocalDB in appsettings:

- [NotificationService/src/NotificationService.API/appsettings.json](NotificationService/src/NotificationService.API/appsettings.json)
- [PaymentService/src/PaymentService.API/appsettings.json](PaymentService/src/PaymentService.API/appsettings.json)
- [PremiumService/src/PremiumService.API/appsettings.json](PremiumService/src/PremiumService.API/appsettings.json)

Expected format:

`Server=(localdb)\\MSSQLLocalDB;Database=<db_name>;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True`

## 4) Run the APIs locally (recommended)

Run each service without containers:

```powershell
cd "./PremiumService"
dotnet run --project ./src/PremiumService.API
```

```powershell
cd "./PaymentService"
dotnet run --project ./src/PaymentService.API
```

```powershell
cd "./NotificationService"
dotnet run --project ./src/NotificationService.API
```

## Note on Docker

LocalDB is not reachable from Linux containers. If you run Docker Compose, point the compose `*_CONNECTION_STRING` to a reachable SQL Server instance (SQL Server service, SQL container, or remote SQL Server), not LocalDB.
