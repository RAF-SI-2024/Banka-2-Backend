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
public class CardSteps(ScenarioContext scenarioContext, ICardService cardService)
{
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;
    private readonly ICardService    m_CardService     = cardService;

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
        var Cards = m_ScenarioContext.Get<Result<Page<CardResponse>>>(Constant.GetCards);

        Cards.ActionResult.ShouldBeOfType<OkObjectResult>();
        Cards.Value.ShouldNotBeNull();
        Cards.Value.Items.Count.ShouldBeGreaterThan(0);
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
}
