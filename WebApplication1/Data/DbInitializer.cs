using WebApplication1.Models;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { FirstName = "John", LastName = "Doe", Age = 25 },
                new User { FirstName = "Jane", LastName = "Smith", Age = 30 },
                new User { FirstName = "Alice", LastName = "Johnson", Age = 35 }
            );
            context.SaveChanges();
        }
       

        if (!context.Companies.Any())
        {
            context.Companies.AddRange(
                new Company { Name = "Google", Industry = "Technology" },
                new Company { Name = "Apple", Industry = "Technology" },
                new Company { Name = "Amazon", Industry = "E-commerce" },
                new Company { Name = "Tesla", Industry = "Automotive" },
                new Company { Name = "Microsoft", Industry = "Technology" }
            );
            context.SaveChanges();
        }
    }
}
