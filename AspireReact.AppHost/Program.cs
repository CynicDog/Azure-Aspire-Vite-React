var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireReact_ApiService>("apiservice");

// builder.AddProject<Projects.AspireReact_Web>("webfrontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(cache)
//     .WithReference(apiService);

builder.AddNpmApp("react", "../aspiring-react")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(env: "PORT")
    .PublishAsDockerFile();

builder.Build().Run();
