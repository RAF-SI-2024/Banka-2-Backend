Feature: Account controller testing

    Scenario: Create Account through API
        Given a valid account create request
        When a POST request is sent to the account creation endpoint
        Then the response ActionResult should indicate successful account creation

    Scenario: Update Client Account through API
        Given a valid account client update request and account Id
        When a PUT request is sent to the account client update endpoint
        Then the response ActionResult should indicate successful account client update

    Scenario: Update Employee Account through API
        Given a valid account employee update request and account Id
        When a PUT request is sent to the account employee update endpoint
        Then the response ActionResult should indicate successful account employee update

    Scenario: Get All Accounts through API
        When a GET request is sent to fetch all accounts
        Then the response ActionResult should indicate successful retrieval of all accounts

    Scenario: Get All Accounts for Client through API
        Given a client Id to fetch related accounts
        When a GET request is sent to fetch accounts for the client
        Then the response should return all accounts for the client

    Scenario: Get one Account by Id through API
        Given an account Id to fetch
        When a GET request is sent to fetch the account by Id
        Then the response ActionResult should indicate successful retrieval of the account