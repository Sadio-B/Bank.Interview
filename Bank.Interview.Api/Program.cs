using Bank.Interview.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();

await app.InitializeDatabase();

app.ConfigurePipeline().Run();