using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using SimbirGo.Bll.DbConfiguration;

namespace SimbirGo.Migrations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SimbirGoDbContext>
{
    public SimbirGoDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<SimbirGoDbContext>();

        var connectionString = configuration.GetConnectionString("SimbirGo");

        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

        return new SimbirGoDbContext(builder.Options);
    }
}