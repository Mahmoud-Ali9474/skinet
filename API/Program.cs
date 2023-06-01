using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("Default")!);
            });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        await context.Database.EnsureCreatedAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    
}
catch (Exception ex)
{
    // var logger = loggerFactory.CreateLogger<Program>();
    // logger.LogError("an error ocurr when migrating data.");
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
