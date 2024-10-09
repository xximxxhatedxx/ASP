var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CalcService>();
builder.Services.AddTransient<TimeService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
