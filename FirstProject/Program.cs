var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CalcService>();
builder.Services.AddTransient<TimeService>();
builder.Configuration
    .AddJsonFile("books.json")
    .AddJsonFile("users.json");
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
