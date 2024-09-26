public class CompanyAnalyzer : ICompanyAnalyzer
{
    private readonly IConfiguration _configuration;

    public CompanyAnalyzer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Company> GetCompanies()
    {
        var compSection = _configuration.GetSection("Companies");
        var microsoft = compSection.GetSection("Microsoft").Get<Company>();
        var apple = compSection.GetSection("Apple").Get<Company>();
        var google = compSection.GetSection("Google").Get<Company>();
        return new List<Company>()
        {
            (microsoft is not null) ? microsoft : new Company("Microsoft", 0),
            (apple is not null) ? apple : new Company("Apple", 0),
            (google is not null) ? google : new Company("Google", 0)
        };
    }

    public Company GetCompanyMWP(List<Company> companies) => companies.MaxBy(x => x.Employees);
}