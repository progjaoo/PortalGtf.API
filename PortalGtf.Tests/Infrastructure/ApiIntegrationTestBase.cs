using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using PortalGtf.Core.Entities;

namespace PortalGtf.Tests.Infrastructure;

public abstract class ApiIntegrationTestBase : IClassFixture<TestWebApplicationFactory>
{
    protected readonly TestWebApplicationFactory Factory;
    protected readonly HttpClient Client;

    protected ApiIntegrationTestBase(TestWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }

    protected async Task<T?> ReadAsync<T>(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task WithDbContextAsync(Func<PortalGtfNewsDbContext, Task> action)
    {
        var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PortalGtfNewsDbContext>();
        await action(context);
    }

    protected async Task<TResult> WithDbContextAsync<TResult>(Func<PortalGtfNewsDbContext, Task<TResult>> action)
    {
        var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PortalGtfNewsDbContext>();
        return await action(context);
    }
}
