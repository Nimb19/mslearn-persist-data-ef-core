using ContosoPizza.Data;
using ContosoPizza.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var pgConnString = ;
builder.Services.AddDbContext<PizzaContext>(options =>
            options.UseNpgsql(new NpgsqlConnectionStringBuilder(builder.Configuration["DbContext:ConnectionString"])
            {
                IntegratedSecurity = false,
                Username = builder.Configuration["DbContext:Login"],
                Password = builder.Configuration["DbContext:Password"],
            }.ToString()));

// Add the PromotionsContext

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add the CreateDbIfNotExists method call

app.Run();
