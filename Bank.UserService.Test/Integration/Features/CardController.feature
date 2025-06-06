Feature: Card controller testing

    Scenario: Create Card through API
        Given a valid card create request
        When a POST request is sent to the card creation endpoint
        Then the response ActionResult should indicate successful card creation

    Scenario: Update Status Card through API
        Given a valid card status update request and card Id
        When a PUT request is sent to the card status update endpoint
        Then the response ActionResult should indicate successful card update status

    Scenario: Update Limit Card through API
        Given a valid card limit update request and card Id
        When a PUT request is sent to the card limit update endpoint
        Then the response ActionResult should indicate successful update

    Scenario: Get All Cards through API
        When a GET request is sent to fetch all cards
        Then the response ActiionResult should indicate successful retrieval of all cards

    Scenario: Get All Cards for Account through API
        Given an account Id to fetch related cards
        When a GET request is sent to fetch cards for the account
        Then the response should return all cards for the account

    Scenario: Get All Cards for Client through API
        Given a client Id to fetch related cards
        When a GET request is sent to fetch cards for the client
        Then the response should return all cards for the client

    Scenario: Get Card by Id through API
        Given a card Id to fetch
        When a GET request is sent to fetch the card by Id
        Then the response ActionResult should indicate successful retrieval of the card