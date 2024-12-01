using GrpcServer;
using GrpcServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString(nameof(TestTrainsContext)) ?? throw new InvalidOperationException($"Connection string {nameof(TestTrainsContext)} not found.");

builder.Services.AddDbContext<TestTrainsContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(TestTrainsContext))));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<TrainService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
