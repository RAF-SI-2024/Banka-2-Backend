Feature: Loan controller testing

    Scenario: Create loan through API
        Given a valid loan create request
        When a POST request is sent to the loan creation endpoint
        Then the response ActionResult should indicate successful loan creation

    Scenario: Update loan through API
        Given a valid loan Id and loan update request
        When a PUT request is sent to the loan update endpoint
        Then the response ActionResult should indicate successful loan update

    Scenario: Get all loans through API
        Given a valid loan filter query and pageable parameters
        When a GET request is sent to fetch all loans
        Then the response ActionResult should indicate successful retrieval of all loans

    Scenario: Get loan by Id through API
        Given a valid loan Id
        When a GET request is sent to fetch the loan by Id
        Then the response ActionResult should indicate successful retrieval of the loan

    Scenario: Get loans by client Id through API
        Given a valid client Id
        When a GET request is sent to fetch loans by client Id
        Then the response ActionResult should indicate successful retrieval of all loans for the client