using Bank.Application.Endpoints;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

using Example = Bank.UserService.Test.Examples.Entities.Example;

namespace Bank.UserService.Test.Integration.Steps;

[Binding]
public class EmailSteps(ScenarioContext scenarioContext, IEmailService emailService)
{
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;
    private readonly IEmailService   m_EmailService    = emailService;

    [Given(@"email type")]
    public void GivenEmailType()
    {
        m_ScenarioContext[Constant.EmailType] = Example.Entity.Email.EmailType;
    }

    [Given(@"user exists")]
    public void GivenUserExists()
    {
        m_ScenarioContext[Constant.User] = Example.Entity.Email.Client;
    }

    [When(@"email is sent")]
    public async Task WhenEmailIsSent()
    {
        var emailType = m_ScenarioContext.Get<EmailType>(Constant.EmailType);

        var client = m_ScenarioContext.Get<Client>(Constant.User);

        var user = UserMapper.ToUser(client);

        var emailResult = await m_EmailService.Send(emailType, user);

        m_ScenarioContext[Constant.EmailResult] = emailResult;
    }

    [Then(@"email should be sent successfully")]
    public void ThenEmailShouldBeSentSuccessfully()
    {
        var emailResult = m_ScenarioContext.Get<Result>(Constant.EmailResult);

        emailResult.ActionResult.ShouldBeOfType<OkResult>();
    }

    [Given(@"parameters exist")]
    public void GivenParametersExist()
    {
        m_ScenarioContext[Constant.InstallmentAmount] = Example.Entity.Email.InstallmentAmount;

        m_ScenarioContext[Constant.Code] = Example.Entity.Email.Code;

        m_ScenarioContext[Constant.RemainingAmount] = Example.Entity.Email.RemainingAmount;
    }

    [When(@"email is sent with parameters")]
    public async Task WhenEmailIsSentWithParameters()
    {
        var emailType = m_ScenarioContext.Get<EmailType>(Constant.EmailType);

        var client = m_ScenarioContext.Get<Client>(Constant.User);

        var user = UserMapper.ToUser(client);

        var installmentAmount = m_ScenarioContext.Get<decimal>(Constant.InstallmentAmount);

        var code = m_ScenarioContext.Get<string>(Constant.Code);

        var remainingAmount = m_ScenarioContext.Get<decimal>(Constant.RemainingAmount);

        var emailResult = await m_EmailService.Send(emailType, user, user.FirstName, installmentAmount, code, remainingAmount);

        m_ScenarioContext[Constant.EmailResult] = emailResult;
    }
}

file static class Constant
{
    public const string EmailType         = "EmailType";
    public const string User              = "User";
    public const string EmailResult       = "EmailResult";
    public const string InstallmentAmount = "InstallmentAmount";
    public const string Code              = "Code";
    public const string RemainingAmount   = "RemainingAmount";
}
