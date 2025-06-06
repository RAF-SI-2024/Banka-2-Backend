using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class ClientSteps(ScenarioContext scenarioContext, IClientService clientService, ClientController clientController)
{
    private readonly IClientService   m_ClientService    = clientService;
    private readonly ScenarioContext  m_ScenarioContext  = scenarioContext;
    private readonly ClientController m_ClientController = clientController;

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

    [Given(@"a valid client create request")]
    public void GivenAValidClientCreateRequest()
    {
        m_ScenarioContext[Constant.ClientCreateRequest] = Example.Entity.Client.CreateRequest;
    }

    [When(@"a POST request is sent to the client creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheClientCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<ClientCreateRequest>(Constant.ClientCreateRequest);

        var createResult = await m_ClientController.Create(createRequest);

        m_ScenarioContext[Constant.ClientCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful client creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulClientCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<ClientResponse>>(Constant.ClientCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid client update request and client Id")]
    public void GivenAValidClientUpdateRequestAndClientId()
    {
        m_ScenarioContext[Constant.ClientId]            = Example.Entity.Client.Id2;
        m_ScenarioContext[Constant.ClientUpdateRequest] = Example.Entity.Client.UpdateRequest;
    }

    [When(@"a PUT request is sent to the client update endpoint")]
    public async Task WhenAPutRequestIsSentToTheClientUpdateEndpoint()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.ClientId);

        var updateRequest = m_ScenarioContext.Get<ClientUpdateRequest>(Constant.ClientUpdateRequest);

        var updateResult = await m_ClientController.Update(updateRequest, clientId);

        m_ScenarioContext[Constant.ClientUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful client update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulClientUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<ClientResponse>>(Constant.ClientUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all clients")]
    public async Task WhenAGetRequestIsSentToFetchAllClients()
    {
        var getClientsResult = await m_ClientController.GetAll(new UserFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetClients] = getClientsResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all clients")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllClients()
    {
        var getClientsResult = m_ScenarioContext.Get<ActionResult<Page<ClientResponse>>>(Constant.GetClients);

        getClientsResult.Result.ShouldBeOfType<OkObjectResult>();
        getClientsResult.ShouldNotBeNull();
    }

    [Given(@"a client Id to fetch")]
    public void GivenAClientIdToFetch()
    {
        m_ScenarioContext[Constant.ClientId] = Example.Entity.Client.Id2;
    }

    [When(@"a GET request is sent to fetch a client by Id")]
    public async Task WhenAGetRequestIsSentToFetchAClientById()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.ClientId);

        var getClientResult = await m_ClientController.GetOne(clientId);

        m_ScenarioContext[Constant.GetClient] = getClientResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the client")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheClient()
    {
        var getClientResult = m_ScenarioContext.Get<ActionResult<ClientResponse>>(Constant.GetClient);

        getClientResult.Result.ShouldBeOfType<OkObjectResult>();
        getClientResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string CreateRequest       = "ClientCreateRequest";
    public const string CreateResult        = "ClientCreateResult";
    public const string GetResult           = "ClienteGetResult";
    public const string UpdateRequest       = "ClientUpdateRequest";
    public const string UpdateResult        = "ClientUpdateResult";
    public const string Id                  = "ClientId";
    public const string ClientCreateRequest = "ClientCreateRequest";
    public const string ClientCreateResult  = "ClientCreateResult";
    public const string ClientUpdateRequest = "ClientUpdateRequest";
    public const string ClientUpdateResult  = "ClientUpdateResult";
    public const string GetClients          = "GetClients";
    public const string GetClient           = "GetClient";
    public const string ClientId            = "ClientId";
}
