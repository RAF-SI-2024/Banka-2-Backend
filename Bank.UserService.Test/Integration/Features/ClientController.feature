Feature: Client controller testing

    Scenario: Create Client
        Given client create request
        When client is created in the database
        And client is fetched by Id
        Then client details should match the created client

    Scenario: Update Client
        Given client update request and Id
        When client is updated in the database
        Then client details should match the updated client

    Scenario: Get All Clients
        When all clients are fetched from the database
        Then all clients and only clients should be returned

    Scenario: Get all Account from Client
        Given client Id
        When all accounts are fetched from the database
        Then all accounts  should be returned

    Scenario: Get all Cards from Client
        Given client Id which has cards
        When all cards are fetched from the database for the client
        Then all cards  should be returned
