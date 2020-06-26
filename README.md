# Blazor Demo

Launch BlazorAppDemo.sln under src

or
```
dotnet run --project src/Server/BlazorAppDemo.Server.csproj
```


## Entity Framework Migrations

```
dotnet tool install --global dotnet-ef
```

if updating from earlier version,
```
dotnet tool update -g dotnet-ef
```

Entity Framework Migrations

For any migration (`dotnet ef migrations`) or database commands (`dotnet ef database`), always append `--project src/Server/BlazorAppDemo.Server.csproj`



```
dotnet ef database update --project src/Server/BlazorAppDemo.Server.csproj 
```

This will update the dev db. For staging, prefix command with ```ASPNETCORE_ENVIRONMENT=Staging```


