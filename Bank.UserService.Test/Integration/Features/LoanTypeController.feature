Feature: LoanType controller testing

    Scenario: Create loan type through API
        Given a valid loan type create request
        When a POST request is sent to the loan type creation endpoint
        Then the response ActionResult should indicate successful loan type creation

    Scenario: Update loan type through API
        Given a valid loan type update request and loan type Id
        When a PUT request is sent to the loan type update endpoint
        Then the response ActionResult should indicate successful loan type update

    Scenario: Get all loan types through API
        When a GET request is sent to fetch all loan types
        Then the response ActionResult should indicate successful retrieval of all loan types

    Scenario: Get loan type by Id through API
        Given a valid loan type Id
        When a GET request is sent to fetch a loan type by Id
        Then the response ActionResult should indicate successful retrieval of the loan type