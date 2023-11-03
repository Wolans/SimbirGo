using Microsoft.EntityFrameworkCore;

using SimbirGo.Bll;
using SimbirGo.Bll.DbConfiguration;
using SimbirGo.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SimbirGoDbContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("SimbirGo");

    options.UseNpgsql(connectionString);
});

builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc(
            "v1",
            new()
            {
                Version = "v1",
                Title = "SimbirGo API",
                Description = "Cервис для:"
                    + "\n- получения информации о доступном транспорте"
                    + "\n- настройки условий аренды транспорта"
                    + "\n- оплаты аренды транспорта"
                    + "\n- хранения истории аренд пользователя"
                    + "\n- хранения истории аренд транспорта"
            });
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IRentalService, RentalService>();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        ;
    });

// For ASP.Net Core 7.0 and higher
app.MapControllers();

// Before ASP.Net Core 7.0
// app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
