Feature: Card Type controller testing

    Scenario: Get all card types through API
        When a GET request is sent to fetch all card types
        Then the response ActionResult should indicate successful retrieval of all card types

    Scenario: Get card type by Id through API
        Given a card type Id to fetch
        When a GET request is sent to fetch a card type by Id
        Then the response ActionResult should indicate successful retrieval of the card type