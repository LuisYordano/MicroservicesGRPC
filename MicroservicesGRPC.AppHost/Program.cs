var builder = DistributedApplication.CreateBuilder(args);

var grpcbackend = builder.AddProject<Projects.GrpcBackend>("grpcbackend");

builder.AddProject<Projects.BlazorFrontend>("blazorfrontend").WithReference(grpcbackend);

builder.Build().Run();
