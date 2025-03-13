using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.HostedServices;

public class ExchangeHostedService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
{
    private readonly IServiceProvider   m_ServiceProvider   = serviceProvider;
    private readonly IHttpClientFactory m_HttpClientFactory = httpClientFactory;
    private          Timer?             m_Timer;

    public void OnApplicationStarted()
    {
        Initialize();
    }

    public void OnApplicationStopped() { }

    private void Initialize()
    {
        var midnight          = DateTime.Today.AddDays(1);
        var timeLeftUntilNext = midnight.Subtract(DateTime.UtcNow);

        m_Timer = new Timer(async _ => await FetchExchanges(), null, timeLeftUntilNext, TimeSpan.FromDays(1));
    }

    private async Task FetchExchanges()
    {
        using var scope   = m_ServiceProvider.CreateScope();
        var       context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        await context.SeedExchange(m_HttpClientFactory.CreateClient(), true);
    }
}
