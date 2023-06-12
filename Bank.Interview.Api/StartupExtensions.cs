using Bank.Interview.Application;
using Bank.Interview.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bank.Interview.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddApplicationServices();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddCors(corsOption =>
            {
                corsOption.AddPolicy("CorsBankPolicy", options =>
                {
                    options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CorsBankPolicy");

            app.MapControllers();

            return app;
        }

        public static async Task InitializeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var bankContext = scope.ServiceProvider.GetRequiredService<BankContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            if (bankContext is not null)
            {
                try
                {
                    await bankContext.Database.EnsureDeletedAsync();
                    await bankContext.Database.MigrateAsync();

                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "An error occured during database Initialization");
                }
            }

        }

    }
}
