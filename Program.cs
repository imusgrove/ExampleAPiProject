using Microsoft.EntityFrameworkCore;
using RedOrderApi.Data;
using System.Reflection;
using Microsoft.OpenApi.Models;
using RedOrderApi.Services;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDb")));


builder.Services.AddTransient<IOrderService, OrderService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});;

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    using(var scope = app.Services.CreateScope())
    {
        var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        orderContext.Database.EnsureCreated();
        orderContext.Seed();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Api V1");
        c.RoutePrefix = "swagger";
    });
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/orders", async context =>
        {
        });

    });
}



app.UseAuthorization();

app.MapControllers();

app.Run();
