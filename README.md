# DOTNET-PRICING-SVC
This is a dotnet RESTFul webservice that manages Pricing Data storing it in a PostgreSQL database which is determined by a Dockerfile.

---

### PostgreSQL Docker Container
Create an image based on DockerFile
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

---

### dotnet Commands

```bash
dotnet ef migrations add initial
dotnet ef database update
```

---

### Ubuntu Snap
In case the dotnet installation has been done through snap:
```
export PATH="$PATH:~/.dotnet/tools"
export DOTNET_ROOT=/snap/dotnet-sdk/current
```