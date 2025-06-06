Feature: Card Type service testing

    Scenario: Get All Card Types
        When all card types are fetched from the database
        Then all card types should be returned

    Scenario: Get Card Type by Id
        Given card type Id
        When card type is fetched from the database
        Then card type should be returned