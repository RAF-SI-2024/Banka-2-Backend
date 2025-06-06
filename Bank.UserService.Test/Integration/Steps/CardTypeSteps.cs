using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class CardTypeSteps(ScenarioContext scenarioContext, ICardTypeService cardTypeService, CardTypeController cardTypeController)
{
    private readonly ScenarioContext    m_ScenarioContext    = scenarioContext;
    private readonly ICardTypeService   m_CardTypeService    = cardTypeService;
    private readonly CardTypeController m_CardTypeController = cardTypeController;

    [When(@"all card types are fetched from the database")]
    public void WhenAllCardTypesAreFetchedFromTheDatabase()
    {
        var cardTypes = m_CardTypeService.FindAll(new CardTypeFilterQuery(), new Pageable())
                                         .Result;

        m_ScenarioContext[Constant.CardTypes] = cardTypes;
    }

    [Then(@"all card types should be returned")]
    public void ThenAllCardTypesShouldBeReturned()
    {
        var cardTypes = m_ScenarioContext.Get<Result<Page<CardTypeResponse>>>(Constant.CardTypes);

        cardTypes.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardTypes.Value.ShouldNotBeNull();
        cardTypes.Value.Items.ShouldNotBeEmpty();
    }

    [Given(@"card type Id")]
    public void GivenCardTypeId()
    {
        m_ScenarioContext[Constant.CardTypeId] = Example.Entity.CardType.Id;
    }

    [When(@"card type is fetched from the database")]
    public void WhenCardTypeIsFetchedFromTheDatabase()
    {
        var cardTypeId = m_ScenarioContext.Get<Guid>(Constant.CardTypeId);

        var cardType = m_CardTypeService.GetCardTypeById(cardTypeId)
                                        .Result;

        m_ScenarioContext[Constant.CardType] = cardType;
    }

    [Then(@"card type should be returned")]
    public void ThenCardTypeShouldBeReturned()
    {
        var cardType = m_ScenarioContext.Get<Result<CardTypeResponse>>(Constant.CardType);

        cardType.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardType.Value.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all card types")]
    public async Task WhenAGetRequestIsSentToFetchAllCardTypes()
    {
        var getCardTypesResult = await m_CardTypeController.GetAll(new CardTypeFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetCardTypes] = getCardTypesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all card types")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllCardTypes()
    {
        var getCardTypesResult = m_ScenarioContext.Get<IActionResult>(Constant.GetCardTypes);

        getCardTypesResult.ShouldBeOfType<OkObjectResult>();
        getCardTypesResult.ShouldNotBeNull();
    }

    [Given(@"a card type Id to fetch")]
    public void GivenACardTypeIdToFetch()
    {
        m_ScenarioContext[Constant.CardTypeId] = Example.Entity.CardType.Id;
    }

    [When(@"a GET request is sent to fetch a card type by Id")]
    public async Task WhenAGetRequestIsSentToFetchACardTypeById()
    {
        var cardTypeId = m_ScenarioContext.Get<Guid>(Constant.CardTypeId);

        var getCardTypeResult = await m_CardTypeController.GetOne(cardTypeId);

        m_ScenarioContext[Constant.GetCardType] = getCardTypeResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the card type")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheCardType()
    {
        var getCardTypeResult = m_ScenarioContext.Get<IActionResult>(Constant.GetCardType);

        getCardTypeResult.ShouldBeOfType<OkObjectResult>();
        getCardTypeResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string CardTypes    = "CardTypes";
    public const string CardTypeId   = "CardTypeId";
    public const string CardType     = "CardType";
    public const string GetCardTypes = "GetCardTypes";
    public const string GetCardType  = "GetCardType";
}
