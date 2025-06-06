Feature: Client service testing

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