using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IClientService
{
    Task<Result<Page<ClientResponse>>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<Result<ClientResponse>> GetOne(Guid id);

    Task<Result<ClientResponse>> Create(ClientCreateRequest clientCreateRequest);

    Task<Result<ClientResponse>> Update(ClientUpdateRequest clientUpdateRequest, Guid id);
}

public class ClientService(IUserRepository repository, IEmailService emailService, IAccountRepository accountRepository, ICardRepository cardRepository) : IClientService
{
    private readonly IUserRepository    m_UserRepository    = repository;
    private readonly IEmailService      m_EmailService      = emailService;
    private readonly IAccountRepository m_AccountRepository = accountRepository;
    private readonly ICardRepository    m_CardRepository    = cardRepository;

    public async Task<Result<Page<ClientResponse>>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        userFilterQuery.Role = Role.Client;

        var page = await m_UserRepository.FindAll(userFilterQuery, pageable);

        var clientResponses = page.Items.Where(client => client.Role == Role.Client)
                                  .Select(client => client.ToClient()
                                                          .ToResponse())
                                  .ToList();

        return Result.Ok(new Page<ClientResponse>(clientResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<ClientResponse>> GetOne(Guid id)
    {
        var user = await m_UserRepository.FindById(id);

        if (user is null || user.Role != Role.Client)
            return Result.NotFound<ClientResponse>($"No Client found with Id: {id}");

        return Result.Ok(user.ToClient()
                             .ToResponse());
    }

    public async Task<Result<ClientResponse>> Create(ClientCreateRequest clientCreateRequest)
    {
        var user = await m_UserRepository.Add(clientCreateRequest.ToClient()
                                                                 .ToUser());

        await m_EmailService.Send(EmailType.UserActivateAccount, user);

        return Result.Ok(user.ToClient()
                             .ToResponse());
    }

    public async Task<Result<ClientResponse>> Update(ClientUpdateRequest clientUpdateRequest, Guid id)
    {
        var dbUser = await m_UserRepository.FindById(id);

        if (dbUser is null || dbUser.Role != Role.Client)
            return Result.NotFound<ClientResponse>($"No Client found with Id: {id}");

        var user = await m_UserRepository.Update(dbUser.Update(clientUpdateRequest));

        return Result.Ok(user.ToClient()
                             .ToResponse());
    }
}
