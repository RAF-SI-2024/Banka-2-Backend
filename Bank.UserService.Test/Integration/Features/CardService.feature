Feature: Card service testing

    Scenario: Create Card
        Given card create request
        When card is created in the database
        And card is fetched by Id
        Then card details should match the created card

    Scenario: Update Status Card
        Given card update status request and Id
        When card status is updated in the database
        Then card status should change

    Scenario: Update Limit Card
        Given card update limit request and Id
        When card limit is updated in the database
        Then card limit should change

    Scenario: Get All Cards
        When all cards are fetched from the database
        Then all cards should be returned

    Scenario: Get All Cards for Account
        Given account Id so we can get cards
        When all cards are fetched for the account
        Then all cards should be returned for the account

    Scenario: Get all Cards from Client
        Given client Id which has cards
        When all cards are fetched from the database for the client
        Then all cards should be returned for that client