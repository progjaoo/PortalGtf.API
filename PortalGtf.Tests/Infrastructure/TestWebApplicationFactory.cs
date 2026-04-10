using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortalGtf.Core.Entities;

namespace PortalGtf.Tests.Infrastructure;

public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["App:BaseUrl"] = "http://localhost",
                ["Jwt:Issuer"] = "PortalGtf.Tests",
                ["Jwt:Audience"] = "PortalGtf.Tests",
                ["Jwt:Key"] = "portalgtf-tests-chave-super-segura-123"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<PortalGtfNewsDbContext>>();
            services.RemoveAll<PortalGtfNewsDbContext>();
            services.RemoveAll(typeof(IDbContextOptionsConfiguration<PortalGtfNewsDbContext>));

            var contextDescriptors = services
                .Where(descriptor =>
                    descriptor.ServiceType.IsGenericType &&
                    descriptor.ServiceType.GenericTypeArguments.Length == 1 &&
                    descriptor.ServiceType.GenericTypeArguments[0] == typeof(PortalGtfNewsDbContext))
                .ToList();

            foreach (var descriptor in contextDescriptors)
            {
                services.Remove(descriptor);
            }

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<PortalGtfNewsDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PortalGtfNewsDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        await TestDataSeeder.SeedAsync(db);
    }

    public async Task DisposeAsync()
    {
        await base.DisposeAsync();
        if (_connection != null)
            await _connection.DisposeAsync();
    }
}
