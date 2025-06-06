Feature: Client controller testing

    Scenario: Create Client through API
        Given a valid client create request
        When a POST request is sent to the client creation endpoint
        Then the response ActionResult should indicate successful client creation

    Scenario: Update Client through API
        Given a valid client update request and client Id
        When a PUT request is sent to the client update endpoint
        Then the response ActionResult should indicate successful client update

    Scenario: Get All Clients through API
        When a GET request is sent to fetch all clients
        Then the response ActionResult should indicate successful retrieval of all clients

    Scenario: Get Client by Id through API
        Given a client Id to fetch
        When a GET request is sent to fetch a client by Id
        Then the response ActionResult should indicate successful retrieval of the client