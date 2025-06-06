using Bank.Permissions.Services;

using Moq;

namespace Bank.ExchangeService.Test.Services;

public class TestAuthorizationService : IAuthorizationService
{
    public Guid                           UserId      { get; set; }
    public Permissions.Domain.Permissions Permissions { get; set; }

    public string RegenerateToken()
    {
        throw new NotImplementedException();
    }

    public string ConfirmationCode()
    {
        throw new NotImplementedException();
    }

    public string GenerateTokenFor(Guid userId, Permissions.Domain.Permissions permissions)
    {
        throw new NotImplementedException();
    }

    public bool IsConfirmationCodeValid(string? confirmationCode)
    {
        throw new NotImplementedException();
    }

    public IAuthorizationService AuthorizationService => CreateAuthorizationService();

    private IAuthorizationService CreateAuthorizationService()
    {
        var mockAuthorizationService = new Mock<IAuthorizationService>();

        mockAuthorizationService.SetupGet(x => x.UserId)
                                .Returns(UserId);

        mockAuthorizationService.SetupGet(x => x.Permissions)
                                .Returns(Permissions);

        mockAuthorizationService.As<ITokenGeneratorTestOnly>()
                                .Setup(x => x.GenerateToken(It.IsAny<Guid>()))
                                .Returns<Guid>(id => JwtTestHelper.GenerateMockJwtToken(id, Permissions));

        mockAuthorizationService.Setup(x => x.GenerateTokenFor(It.IsAny<Guid>(), It.IsAny<Permissions.Domain.Permissions>()))
                                .Returns<Guid, Permissions.Domain.Permissions>((id, perms) => JwtTestHelper.GenerateMockJwtToken(id, perms));

        return mockAuthorizationService.Object;
    }
}
