using Bank.Permissions.Services;

using Microsoft.Extensions.DependencyInjection;

using Moq;

namespace Bank.ExchangeService.Test.Services;

public class TestAuthorizationServiceFactory(IServiceScopeFactory serviceScopeFactory) : IAuthorizationServiceFactory
{
    public Guid                           UserId      { set; get; }
    public Permissions.Domain.Permissions Permissions { set; get; }

    public IAuthorizationService AuthorizationService => CreateAuthorizationService();

    private IAuthorizationService CreateAuthorizationService()
    {
        var mockAuthorizationService = new Mock<IAuthorizationService>();

        mockAuthorizationService.SetupGet(authorizationService => authorizationService.UserId)
                                .Returns(UserId);

        mockAuthorizationService.SetupGet(authorizationService => authorizationService.Permissions)
                                .Returns(Permissions);

        mockAuthorizationService.As<ITokenGeneratorTestOnly>()
                                .Setup(x => x.GenerateToken(It.IsAny<Guid>()))
                                .Returns<Guid>(id => JwtTestHelper.GenerateMockJwtToken(id, Permissions));

        mockAuthorizationService.Setup(x => x.GenerateTokenFor(It.IsAny<Guid>(), It.IsAny<Permissions.Domain.Permissions>()))
                                .Returns<Guid, Permissions.Domain.Permissions>((id, perms) => JwtTestHelper.GenerateMockJwtToken(id, perms));

        return mockAuthorizationService.Object;
    }
}

public interface ITokenGeneratorTestOnly
{
    string GenerateToken(Guid userId);
}
