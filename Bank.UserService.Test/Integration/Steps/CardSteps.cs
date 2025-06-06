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
public class CardSteps(ScenarioContext scenarioContext, ICardService cardService, CardController cardController)
{
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;
    private readonly ICardService    m_CardService     = cardService;
    private readonly CardController  m_CardController  = cardController;

    [Given(@"card create request")]
    public void GivenCardCreateRequest()
    {
        m_ScenarioContext[Constant.CardCreateRequest] = Example.Entity.Card.CreateRequest;
    }

    [When(@"card is created in the database")]
    public async Task WhenCardIsCreatedInTheDatabase()
    {
        var cardCreateRequest = m_ScenarioContext.Get<CardCreateRequest>(Constant.CardCreateRequest);

        var createCardResult = await m_CardService.Create(cardCreateRequest);

        m_ScenarioContext[Constant.CardCreateResult] = createCardResult;
    }

    [When(@"card is fetched by Id")]
    public async Task WhenCardIsFetchedById()
    {
        var createCardResult = m_ScenarioContext.Get<Result<CardResponse>>(Constant.CardCreateResult);

        var getCardResult = createCardResult.Value != null ? await m_CardService.GetOne(createCardResult.Value.Id) : Result.BadRequest<CardResponse>();

        m_ScenarioContext[Constant.CardGetResult] = getCardResult;
    }

    [Then(@"card details should match the created card")]
    public void ThenCardDetailsShouldMatchTheCreatedCard()
    {
        var getCardResult = m_ScenarioContext.Get<Result<CardResponse>>(Constant.CardGetResult);

        getCardResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getCardResult.Value.ShouldNotBeNull();
        getCardResult.Value.Name.ShouldBe(Example.Entity.Card.CreateRequest.Name);
        getCardResult.Value.Limit.ShouldBe(Example.Entity.Card.CreateRequest.Limit);
        getCardResult.Value.Status.ShouldBe(Example.Entity.Card.CreateRequest.Status);
        getCardResult.Value.Type.Id.ShouldBe(Example.Entity.Card.CreateRequest.CardTypeId);
        getCardResult.Value.Account.Id.ShouldBe(Example.Entity.Card.CreateRequest.AccountId);
    }

    [Given(@"card update status request and Id")]
    public void GivenCardUpdateStatusRequestAndId()
    {
        m_ScenarioContext[Constant.CardId]              = Example.Entity.Card.Id;
        m_ScenarioContext[Constant.StatusUpdateRequest] = Example.Entity.Card.StatusUpdateRequest;
    }

    [When(@"card status is updated in the database")]
    public async Task WhenCardStatusIsUpdatedInTheDatabase()
    {
        var cardId = m_ScenarioContext.Get<Guid>(Constant.CardId);

        var statusUpdateRequest = m_ScenarioContext.Get<CardUpdateStatusRequest>(Constant.StatusUpdateRequest);

        var updateCardResult = await m_CardService.Update(statusUpdateRequest, cardId);

        m_ScenarioContext[Constant.StatusUpdateResult] = updateCardResult;
    }

    [Then(@"card status should change")]
    public void ThenCardStatusShouldChange()
    {
        var updateCardResult = m_ScenarioContext.Get<Result<CardResponse>>(Constant.StatusUpdateResult);

        updateCardResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        updateCardResult.Value.ShouldNotBeNull();
        updateCardResult.Value.Status.ShouldBe(Example.Entity.Card.StatusUpdateRequest.Status);
    }

    [Given(@"card update limit request and Id")]
    public void GivenCardUpdateLimitRequestAndId()
    {
        m_ScenarioContext[Constant.CardId]             = Example.Entity.Card.Id;
        m_ScenarioContext[Constant.LimitUpdateRequest] = Example.Entity.Card.LimitUpdateRequest;
    }

    [When(@"card limit is updated in the database")]
    public async Task WhenCardLimitIsUpdatedInTheDatabase()
    {
        var cardId = m_ScenarioContext.Get<Guid>(Constant.CardId);

        var limitUpdateRequest = m_ScenarioContext.Get<CardUpdateLimitRequest>(Constant.LimitUpdateRequest);

        var updateCardResult = await m_CardService.Update(limitUpdateRequest, cardId);

        m_ScenarioContext[Constant.LimitUpdateResult] = updateCardResult;
    }

    [Then(@"card limit should change")]
    public void ThenCardLimitShouldChange()
    {
        var updateCardResult = m_ScenarioContext.Get<Result<CardResponse>>(Constant.LimitUpdateResult);

        updateCardResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        updateCardResult.Value.ShouldNotBeNull();
        updateCardResult.Value.Limit.ShouldBe(Example.Entity.Card.LimitUpdateRequest.Limit);
        updateCardResult.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.CardId));
    }

    [When(@"all cards are fetched from the database")]
    public async Task WhenAllCardsAreFetchedFromTheDatabase()
    {
        var cards = await m_CardService.GetAll(new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetCards] = cards;
    }

    [Then(@"all cards should be returned")]
    public void ThenAllCardsShouldBeReturned()
    {
        var cards = m_ScenarioContext.Get<Result<Page<CardResponse>>>(Constant.GetCards);

        cards.ActionResult.ShouldBeOfType<OkObjectResult>();
        cards.Value.ShouldNotBeNull();
        cards.Value.Items.Count.ShouldBeGreaterThan(0);
    }

    [When(@"all cards are fetched for the account")]
    public async Task WhenAllCardsAreFetchedForTheAccount()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var cardResult = await m_CardService.GetAllForAccount(accountId, new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.CardsResult] = cardResult;
    }

    [Then(@"all cards should be returned for the account")]
    public void ThenAllCardsShouldBeReturnedForTheAccount()
    {
        var cardResult = m_ScenarioContext.Get<Result<Page<CardResponse>>>(Constant.CardsResult);

        cardResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardResult.Value.ShouldNotBeNull();

        cardResult.Value.Items.All(card => card.Account.Id == m_ScenarioContext.Get<Guid>(Constant.AccountId))
                  .ShouldBeTrue();
    }

    [Given(@"account Id so we can get cards")]
    public void GivenAccountIdSoWeCanGetCards()
    {
        m_ScenarioContext[Constant.AccountId] = Example.Entity.Account.AccountId;
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

        var getCardsResult = await m_CardService.GetAllForClient(id, new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.CardsResult] = getCardsResult;
    }

    [Then(@"all cards should be returned for that client")]
    public void ThenAllCardsShouldBeReturnedForThatClient()
    {
        var cardsResult = m_ScenarioContext.Get<Result<Page<CardResponse>>>(Constant.CardsResult);

        cardsResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardsResult.Value.ShouldNotBeNull();
        cardsResult.Value.Items.ShouldNotBeEmpty();

        cardsResult.Value.Items.All(card => card.Account.Client.Id == m_ScenarioContext.Get<Guid>(Constant.Id))
                   .ShouldBeTrue();
    }

    [Given(@"a valid card create request")]
    public void GivenAValidCardCreateRequest()
    {
        m_ScenarioContext[Constant.CardCreateRequest] = Example.Entity.Card.CreateRequest;
    }

    [When(@"a POST request is sent to the card creation endpoint")]
    public async Task WhenApostRequestIsSentToTheCardCreationEndpoint()
    {
        var cardCreateRequest = m_ScenarioContext.Get<CardCreateRequest>(Constant.CardCreateRequest);

        var createCardResult = await m_CardController.Create(cardCreateRequest);

        m_ScenarioContext[Constant.CardCreateResult] = createCardResult;
    }

    [Then(@"the response ActionResult should indicate successful card creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulCardCreation()
    {
        var createCardResult = m_ScenarioContext.Get<ActionResult<CardResponse>>(Constant.CardCreateResult);

        createCardResult.Result.ShouldBeOfType<OkObjectResult>();
        createCardResult.ShouldNotBeNull();
    }

    [Given(@"a valid card status update request and card Id")]
    public void GivenAValidCardStatusUpdateRequestAndCardId()
    {
        m_ScenarioContext[Constant.CardId]              = Example.Entity.Card.Id;
        m_ScenarioContext[Constant.StatusUpdateRequest] = Example.Entity.Card.StatusUpdateRequest;
    }

    [When(@"a PUT request is sent to the card status update endpoint")]
    public async Task WhenAputRequestIsSentToTheCardStatusUpdateEndpoint()
    {
        var cardId = m_ScenarioContext.Get<Guid>(Constant.CardId);

        var statusUpdateRequest = m_ScenarioContext.Get<CardUpdateStatusRequest>(Constant.StatusUpdateRequest);

        var updateCardResult = await m_CardController.UpdateStatus(statusUpdateRequest, cardId);

        m_ScenarioContext[Constant.StatusUpdateResult] = updateCardResult;
    }

    [Then(@"the response ActionResult should indicate successful card update status")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulCardUpdateStatus()
    {
        var updateCardResult = m_ScenarioContext.Get<ActionResult<CardResponse>>(Constant.StatusUpdateResult);

        updateCardResult.Result.ShouldBeOfType<OkObjectResult>();
        updateCardResult.ShouldNotBeNull();
    }

    [Given(@"a valid card limit update request and card Id")]
    public void GivenAValidCardLimitUpdateRequestAndCardId()
    {
        m_ScenarioContext[Constant.CardId]             = Example.Entity.Card.Id;
        m_ScenarioContext[Constant.LimitUpdateRequest] = Example.Entity.Card.LimitUpdateRequest;
    }

    [When(@"a PUT request is sent to the card limit update endpoint")]
    public async Task WhenAputRequestIsSentToTheCardLimitUpdateEndpoint()
    {
        var cardId = m_ScenarioContext.Get<Guid>(Constant.CardId);

        var limitUpdateRequest = m_ScenarioContext.Get<CardUpdateLimitRequest>(Constant.LimitUpdateRequest);

        var updateCardResult = await m_CardController.UpdateLimit(limitUpdateRequest, cardId);

        m_ScenarioContext[Constant.LimitUpdateResult] = updateCardResult;
    }

    [Then(@"the response ActionResult should indicate successful update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulUpdate()
    {
        var updateCardResult = m_ScenarioContext.Get<ActionResult<CardResponse>>(Constant.LimitUpdateResult);

        updateCardResult.Result.ShouldBeOfType<OkObjectResult>();
        updateCardResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all cards")]
    public async Task WhenAgetRequestIsSentToFetchAllCards()
    {
        var getCardsResult = await m_CardController.GetAll(new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetCards] = getCardsResult;
    }

    [Then(@"the response ActiionResult should indicate successful retrieval of all cards")]
    public void ThenTheResponseActiionResultShouldIndicateSuccessfulRetrievalOfAllCards()
    {
        var getCardsResult = m_ScenarioContext.Get<ActionResult<Page<CardResponse>>>(Constant.GetCards);

        getCardsResult.Result.ShouldBeOfType<OkObjectResult>();
        getCardsResult.ShouldNotBeNull();
    }

    [Given(@"an account Id to fetch related cards")]
    public void GivenAnAccountIdToFetchRelatedCards()
    {
        m_ScenarioContext[Constant.AccountId] = Example.Entity.Account.AccountId;
    }

    [When(@"a GET request is sent to fetch cards for the account")]
    public async Task WhenAgetRequestIsSentToFetchCardsForTheAccount()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var getCardsResult = await m_CardController.GetAllForAccount(accountId, new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.CardsResult] = getCardsResult;
    }

    [Then(@"the response should return all cards for the account")]
    public void ThenTheResponseShouldReturnAllCardsForTheAccount()
    {
        var getCardsResult = m_ScenarioContext.Get<ActionResult<Page<CardResponse>>>(Constant.CardsResult);

        getCardsResult.Result.ShouldBeOfType<OkObjectResult>();
        getCardsResult.ShouldNotBeNull();
    }

    [Given(@"a client Id to fetch related cards")]
    public void GivenAClientIdToFetchRelatedCards()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Client.Id2;
    }

    [When(@"a GET request is sent to fetch cards for the client")]
    public async Task WhenAgetRequestIsSentToFetchCardsForTheClient()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.Id);

        var getCardsResult = await m_CardController.GetAllForClient(clientId, new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.CardsResult] = getCardsResult;
    }

    [Then(@"the response should return all cards for the client")]
    public void ThenTheResponseShouldReturnAllCardsForTheClient()
    {
        var getCardsResult = m_ScenarioContext.Get<ActionResult<CardResponse>>(Constant.CardsResult);

        getCardsResult.Result.ShouldBeOfType<OkObjectResult>();
        getCardsResult.ShouldNotBeNull();
    }

    [Given(@"a card Id to fetch")]
    public void GivenACardIdToFetch()
    {
        m_ScenarioContext[Constant.CardId] = Example.Entity.Card.Id;
    }

    [When(@"a GET request is sent to fetch the card by Id")]
    public async Task WhenAgetRequestIsSentToFetchTheCardById()
    {
        var cardId = m_ScenarioContext.Get<Guid>(Constant.CardId);

        var getCardResult = await m_CardController.GetOne(cardId);

        m_ScenarioContext[Constant.CardGetResult] = getCardResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the card")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheCard()
    {
        var getCardResult = m_ScenarioContext.Get<ActionResult<CardResponse>>(Constant.CardGetResult);

        getCardResult.Result.ShouldBeOfType<OkObjectResult>();
        getCardResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string CardCreateRequest   = "CardCreateRequest";
    public const string CardCreateResult    = "CardCreateResult";
    public const string CardGetResult       = "CardGetResult";
    public const string CardId              = "CardId";
    public const string StatusUpdateRequest = "StatusUpdateRequest";
    public const string StatusUpdateResult  = "StatusUpdateResult";
    public const string LimitUpdateRequest  = "LimitUpdateRequest";
    public const string LimitUpdateResult   = "LimitUpdateResult";
    public const string GetCards            = "GetCards";
    public const string AccountId           = "AccountId";
    public const string CardsResult         = "CardsResult";
    public const string Id                  = "Id";
}
