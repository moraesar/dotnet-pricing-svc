## PostgreSQL Docker Container
Create image based on DockerFile
```
sudo docker build . -t postgres-sql-image
```

Run Postgres container
```
sudo docker run -d --name db-container -p 5432:5432 postgres-sql-image
```

Access psql for investigating database data
```
sudo docker exec -it db-container psql -U postgres -d pricing
``` 
## dotnet Commands

```bash
dotnet ef migrations add initial
dotnet ef database update
```

## ON UBUNTU
In case de dotnet installation has been done through snap:
```
export PATH="$PATH:~/.dotnet/tools"
export DOTNET_ROOT=/snap/dotnet-sdk/current
```