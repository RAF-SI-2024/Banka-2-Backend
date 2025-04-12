Feature: Card controller testing

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
