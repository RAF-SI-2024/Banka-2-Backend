Feature: AccountCurrency controller testing

    Scenario: Create AccountCurrency through API
        Given a valid account currency create request
        When a POST request is sent to the account currency creation endpoint
        Then the response ActionResult should indicate successful account currency creation

    Scenario: Update AccountCurrency through API
        Given a valid account currency update request and account currency Id
        When a PUT request is sent to the account currency update endpoint
        Then the response ActionResult should indicate successful account currency update

    Scenario: Get all AccountCurrencies through API
        When a GET request is sent to fetch all account currencies
        Then the response ActionResult should indicate successful retrieval of all account currencies

    Scenario: Get AccountCurrency by Id through API
        Given an account currency Id to fetch
        When a GET request is sent to fetch the account currency by Id
        Then the response ActionResult should indicate successful retrieval of the account currency