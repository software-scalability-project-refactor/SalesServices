## Run Project

1.
```
docker compose up -d
```
2.
```
cd .\SalesService\
```
3.
```
dotnet build 
```
4.
```
dotnet ef migrations add InitialMigrations
```
5.
```
dotnet ef database update
```
6.
```
dotnet run
```