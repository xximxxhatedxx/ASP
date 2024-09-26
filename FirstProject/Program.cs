var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("config.json")
    .AddXmlFile("config.xml")
    .AddIniFile("config.ini");

builder.Services.AddSingleton<ICompanyAnalyzer, CompanyAnalyzer>();

var app = builder.Build();

var person = app.Configuration.GetSection("Person").Get<Person>();

app.MapGet("/", async (ICompanyAnalyzer companyAnalyzer, HttpResponse response) =>
{
    var companies = companyAnalyzer.GetCompanies();
    var winner = companyAnalyzer.GetCompanyMWP(companies);

    await response.WriteAsync($"My information : {person}\n");
    await response.WriteAsync($"Most employees: {winner}\n");
    foreach (var company in companies) await response.WriteAsync(company.ToString());
});

app.Run();