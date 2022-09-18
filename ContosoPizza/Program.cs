using ContosoPizza.Data;
using ContosoPizza.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzaContext>(options => 
    options.UseNpgsql(builder.Configuration["DbContext:ConnectionString"]));

builder.Services.AddSqlite<PromotionsContext>("Data Source=.\\Promotions\\Promotions.db");

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.CreateDbIfNotExists();

app.Run();
