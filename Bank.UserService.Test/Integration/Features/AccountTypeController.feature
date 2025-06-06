Feature: AccountType controller testing

    Scenario: Get all account types through API
        When a GET request is sent to fetch all account types
        Then the response ActionResult should indicate successful retrieval of all account types

    Scenario: Get one account type through API
        Given an account type Id to fetch
        When a GET request is sent to fetch one account type
        Then the response ActionResult should indicate successful retrieval of the account type