using Microsoft.EntityFrameworkCore;
using RedisAcceleratedAPI.API.Data;
using RedisAcceleratedAPI.API.Data.Seed;
using RedisAcceleratedAPI.API.Repository.Implementation;
using RedisAcceleratedAPI.API.Repository.Interface;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<IProductService, ProductService>();
static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
//Database
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Redis configuration
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration["Redis:ConnectionString"], true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Call the seeding method before running the app
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();  // This applies any pending migrations
    await SeedDatabase(app);
}


app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();

app.Run();
