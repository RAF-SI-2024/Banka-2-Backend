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

    Task<Result<Page<AccountResponse>>> FindAllAccounts(Guid clientId, AccountFilterQuery filter, Pageable pageable);

    Task<Result<ClientResponse>> GetOne(Guid id);

    Task<Result<ClientResponse>> Create(ClientCreateRequest clientCreateRequest);

    Task<Result<ClientResponse>> Update(ClientUpdateRequest clientUpdateRequest, Guid id);
}

public class ClientService(IUserRepository repository, IEmailService emailService, IAccountRepository accountRepository) : IClientService
{
    private readonly IUserRepository    m_UserRepository    = repository;
    private readonly IEmailService      m_EmailService      = emailService;
    private readonly IAccountRepository m_AccountRepository = accountRepository;

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

    public async Task<Result<Page<AccountResponse>>> FindAllAccounts(Guid clientId, AccountFilterQuery filter, Pageable pageable)
    {
        var page = await m_AccountRepository.FindAllByClientId(clientId, pageable);

        var accountResponses = page.Items.Select(account => account.ToResponse())
                                   .ToList();

        return Result.Ok(new Page<AccountResponse>(accountResponses, page.PageNumber, page.PageSize, page.TotalElements));
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
        var oldUser = await m_UserRepository.FindById(id);

        if (oldUser is null || oldUser.Role != Role.Client)
            return Result.NotFound<ClientResponse>($"No Client found with Id: {id}");

        var user = await m_UserRepository.Update(oldUser, clientUpdateRequest.ToClient(oldUser.ToClient())
                                                                             .ToUser());

        return Result.Ok(user.ToClient()
                             .ToResponse());
    }
}
