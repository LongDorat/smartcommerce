var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddProject<Projects.SmartCommerce_Server>("server")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.AddProject<Projects.SmartCommerce_ApiService>("smartcommerce-apiservice");

builder.Build().Run();
