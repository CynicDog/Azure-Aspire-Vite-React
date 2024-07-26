var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var db_username = builder.AddParameter("psql-username", secret: true); 
var db_password = builder.AddParameter("psql-password", secret: true);
var db_name = "aspiringreact";
var weatherDatabase = builder.AddPostgres(
        "postgres",  
        userName: db_username, 
        password: db_password)
    .WithEnvironment("POSTGRES_DB", db_name)
    .WithBindMount(
        "../AspireReact.ApiService/data/postgres",
        "/docker-entrypoint-initdb.d")
    .AddDatabase(db_name);

// To get inside the container, run: docker exec -it {PID} psql -U AspireIsAwesome -d aspiringreact 

var apiService = builder
    .AddProject<Projects.AspireReact_ApiService>("apiservice")
    .WithReference(weatherDatabase);

builder.AddNpmApp("react", "../aspiring-react")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(env: "PORT")
    .PublishAsDockerFile();

builder.Build().Run();
