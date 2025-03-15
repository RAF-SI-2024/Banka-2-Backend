using Bank.LoanService.Database.Seeders;
using Bank.UserService.Configurations;
using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.HostedServices;

public class DatabaseHostedService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
{
    private readonly IHttpClientFactory m_HttpClientFactory = httpClientFactory;
    private readonly IServiceProvider   m_ServiceProvider   = serviceProvider;

    private ApplicationContext Context =>
    m_ServiceProvider.CreateScope()
                     .ServiceProvider.GetRequiredService<ApplicationContext>();

    public void OnApplicationStarted()
    {
        if (Configuration.Database.CreateDrop)
            Context.Database.EnsureDeletedAsync()
                   .Wait();

        Context.Database.EnsureCreatedAsync()
               .Wait();

        Context.SeedClient()
               .Wait();

        Context.SeedEmployee()
               .Wait();

        Context.SeedCurrency()
               .Wait();

        Context.SeedCountry()
               .Wait();

        Context.SeedCompany()
               .Wait();

        Context.SeedAccountType()
               .Wait();

        Context.SeedAccount()
               .Wait();

        Context.SeedLoanTypes()
               .Wait();

        Context.SeedLoans()
               .Wait();

        Context.SeedInstallments()
               .Wait();

        Context.SeedAccountCurrency()
               .Wait();

        Context.SeedCadType()
               .Wait();

        Context.SeedCard()
               .Wait();

        Context.SeedTransactionCode()
               .Wait();

        Context.SeedExchange(httpClientFactory.CreateClient())
               .Wait();
    }

    public void OnApplicationStopped() { }
}
