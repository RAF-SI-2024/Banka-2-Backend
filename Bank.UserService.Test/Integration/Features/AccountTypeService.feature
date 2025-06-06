Feature: Account Type service testing

    Scenario: Get all account types
        When I get all account types
        Then I should get all account types

    Scenario: Get one account type
        Given I have an account type
        When I get one account type
        Then I should get one account type