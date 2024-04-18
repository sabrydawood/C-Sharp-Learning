># .net 8 GAME CURD API

>Install ALL Dependency

```bash
dotnet restore
```

>Database Migrations

```bash
dotnet ef migrations add InitialCreate --output-dir Database\Migrations
dotnet ef migrations add SeedGeneres --output-dir Database\Migrations
```

>Run Application (Dev Mode)

```bash
dotnet run
```

>[Go To Main](http://localhost:5182)
