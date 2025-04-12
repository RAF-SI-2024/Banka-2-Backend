using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class ClientSteps(ScenarioContext scenarioContext, IClientService clientService)
{
    private readonly IClientService  m_ClientService   = clientService;
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;

    [Given(@"client create request")]
    public void GivenClientCreateRequest()
    {
        m_ScenarioContext[Constant.CreateRequest] = Example.Entity.Client.CreateRequest;
    }

    [When(@"client is created in the database")]
    public async Task WhenClientIsCreatedInTheDatabase()
    {
        var clientCreateRequest = m_ScenarioContext.Get<ClientCreateRequest>(Constant.CreateRequest);

        var createClientResult = await m_ClientService.Create(clientCreateRequest);

        m_ScenarioContext[Constant.CreateResult] = createClientResult;
    }

    [When(@"client is fetched by Id")]
    public async Task WhenClientIsFetchedById()
    {
        var createClientResult = m_ScenarioContext.Get<Result<ClientResponse>>(Constant.CreateResult);

        var getClientResult = createClientResult.Value != null ? await m_ClientService.GetOne(createClientResult.Value.Id) : Result.BadRequest<ClientResponse>();

        m_ScenarioContext[Constant.GetResult] = getClientResult;
    }

    [Then(@"client details should match the created client")]
    public void ThenClientDetailsShouldMatchTheCreatedClient()
    {
        var getClientResult = m_ScenarioContext.Get<Result<ClientResponse>>(Constant.GetResult);

        getClientResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getClientResult.Value.ShouldNotBeNull();
        getClientResult.Value.FirstName.ShouldBe(Example.Entity.Client.CreateRequest.FirstName);
        getClientResult.Value.LastName.ShouldBe(Example.Entity.Client.CreateRequest.LastName);
        getClientResult.Value.DateOfBirth.ShouldBe(Example.Entity.Client.CreateRequest.DateOfBirth);
        getClientResult.Value.Gender.ShouldBe(Example.Entity.Client.CreateRequest.Gender);
        getClientResult.Value.UniqueIdentificationNumber.ShouldBe(Example.Entity.Client.CreateRequest.UniqueIdentificationNumber);
        getClientResult.Value.Email.ShouldBe(Example.Entity.Client.CreateRequest.Email);
        getClientResult.Value.PhoneNumber.ShouldBe(Example.Entity.Client.CreateRequest.PhoneNumber);
        getClientResult.Value.Address.ShouldBe(Example.Entity.Client.CreateRequest.Address);
    }

    [Given(@"client update request and Id")]
    public void GivenClientUpdateRequestAndId()
    {
        m_ScenarioContext[Constant.UpdateRequest] = Example.Entity.Client.UpdateRequest;
        m_ScenarioContext[Constant.Id]            = Example.Entity.Client.Id1;
    }

    [When(@"client is updated in the database")]
    public async Task WhenClientIsUpdatedInTheDatabase()
    {
        var clientUpdateRequest = m_ScenarioContext.Get<ClientUpdateRequest>(Constant.UpdateRequest);

        var id = m_ScenarioContext.Get<Guid>(Constant.Id);

        var updateClientResult = await m_ClientService.Update(clientUpdateRequest, id);

        var getClientResult = updateClientResult.Value != null ? await m_ClientService.GetOne(updateClientResult.Value.Id) : Result.BadRequest<ClientResponse>();

        m_ScenarioContext[Constant.UpdateResult] = getClientResult;
    }

    [Then(@"client details should match the updated client")]
    public void ThenClientDetailsShouldMatchTheUpdatedClient()
    {
        var updateClientResult = m_ScenarioContext.Get<Result<ClientResponse>>(Constant.UpdateResult);

        updateClientResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        updateClientResult.Value.ShouldNotBeNull();
        updateClientResult.Value.FirstName.ShouldBe(Example.Entity.Client.UpdateRequest.FirstName);
        updateClientResult.Value.LastName.ShouldBe(Example.Entity.Client.UpdateRequest.LastName);
        updateClientResult.Value.PhoneNumber.ShouldBe(Example.Entity.Client.UpdateRequest.PhoneNumber);
        updateClientResult.Value.Address.ShouldBe(Example.Entity.Client.UpdateRequest.Address);
        updateClientResult.Value.Activated.ShouldBe(Example.Entity.Client.UpdateRequest.Activated);
    }

    [When(@"all clients are fetched from the database")]
    public async Task WhenAllClientsAreFetchedFromTheDatabase()
    {
        var getClientsResult = await m_ClientService.FindAll(new UserFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetResult] = getClientsResult;
    }

    [Then(@"all clients and only clients should be returned")]
    public void ThenAllClientsAndOnlyClientsShouldBeReturned()
    {
        var getClientsResult = m_ScenarioContext.Get<Result<Page<ClientResponse>>>(Constant.GetResult);

        getClientsResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getClientsResult.Value.ShouldNotBeNull();
        getClientsResult.Value.Items.ShouldAllBe(client => client.Role == Role.Client);
    }

    [Given(@"client Id")]
    public void GivenClientId()
    {
        m_ScenarioContext[Constant.IdForAccount] = Example.Entity.Client.Id2;
    }

    [When(@"all accounts are fetched from the database")]
    public async Task WhenAllAccountsAreFetchedFromTheDatabase()
    {
        var id                = m_ScenarioContext.Get<Guid>(Constant.IdForAccount);
        var getAccountsResult = await m_ClientService.FindAllAccounts(id, new AccountFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.AccountResult] = getAccountsResult;
    }

    [Then(@"all accounts  should be returned")]
    public void ThenAllAccountsShouldBeReturned()
    {
        var accountResult = m_ScenarioContext.Get<Result<Page<AccountResponse>>>(Constant.AccountResult);

        accountResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountResult.Value.ShouldNotBeNull();
        accountResult.Value.Items.ShouldNotBeEmpty();
        accountResult.Value.Items.ShouldAllBe(account => account.Client.Id == m_ScenarioContext.Get<Guid>(Constant.IdForAccount));
    }

    [Given(@"client Id which has cards")]
    public void GivenClientIdWhichHasCards()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Client.Id2;
    }

    [When(@"all cards are fetched from the database for the client")]
    public async Task WhenAllCardsAreFetchedFromTheDatabaseForTheClient()
    {
        var id = m_ScenarioContext.Get<Guid>(Constant.Id);

        var getCardsResult = await m_ClientService.FindAllCards(id);

        m_ScenarioContext[Constant.CardsResult] = getCardsResult;
    }

    [Then(@"all cards  should be returned")]
    public void ThenAllCardsShouldBeReturned()
    {
        var cardsResult = m_ScenarioContext.Get<Result<List<CardResponse>>>(Constant.CardsResult);

        cardsResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardsResult.Value.ShouldNotBeNull();
        cardsResult.Value.ShouldNotBeEmpty();
        cardsResult.Value.ShouldAllBe(card => card.Account.Client.Id == m_ScenarioContext.Get<Guid>(Constant.Id));
    }
}

file static class Constant
{
    public const string CreateRequest = "ClientCreateRequest";
    public const string CreateResult  = "ClientCreateResult";
    public const string GetResult     = "ClienteGetResult";
    public const string UpdateRequest = "ClientUpdateRequest";
    public const string UpdateResult  = "ClientUpdateResult";
    public const string Id            = "ClientId";
    public const string IdForAccount  = "ClientIdForAccount";
    public const string AccountResult = "AccountResult";
    public const string CardsResult   = "CardsResult";
}
