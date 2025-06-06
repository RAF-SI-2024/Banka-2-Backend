Feature: Company controller testing

    Scenario: Create Company through API
        Given a valid company create request
        When a POST request is sent to the company creation endpoint
        Then the response ActionResult should indicate successful company creation

    Scenario: Get All Companies through API
        Given a valid company filter query and pageable parameters
        When a GET request is sent to fetch all companies
        Then the response ActionResult should indicate successful retrieval of all companies

    Scenario: Get Company by Id through API
        Given a valid company Id to fetch
        When a GET request is sent to fetch a company by Id
        Then the response ActionResult should indicate successful retrieval of the company

    Scenario: Update Company through API
        Given a valid company Id and company update request
        When a PUT request is sent to update the company
        Then the response ActionResult should indicate successful company update