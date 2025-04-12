using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class CardTypeSteps(ScenarioContext scenarioContext, ICardTypeService cardTypeService)
{
    private readonly ScenarioContext  m_ScenarioContext = scenarioContext;
    private readonly ICardTypeService m_CardTypeService = cardTypeService;

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
        m_ScenarioContext[Constant.CardTypeId] = Seeder.CardType.VisaDebitCard.Id;
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
}

file static class Constant
{
    public const string CardTypes  = "CardTypes";
    public const string CardTypeId = "CardTypeId";
    public const string CardType   = "CardType";
}
