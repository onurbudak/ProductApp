using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductApp.Persistence.Context;

public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=.;Database=ProductAppDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
