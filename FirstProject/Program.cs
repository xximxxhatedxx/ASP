var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Random random = new Random();
Company company = new Company(1, "company1");

app.Run(async (context) => {
    await context.Response.WriteAsync(company + "\n");
    await context.Response.WriteAsync(random.Next(0, 101).ToString());
});

app.Run();
