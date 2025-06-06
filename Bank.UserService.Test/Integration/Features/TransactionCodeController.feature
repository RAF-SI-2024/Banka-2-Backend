Feature: TransactionCode controller testing

    Scenario: Get all transaction codes through API
        Given a transaction code filter query and pageable parameters
        When a GET request is sent to fetch all transaction codes
        Then the response ActionResult should indicate successful retrieval of transaction codes matching pageable

    Scenario: Get transaction code by Id through API
        Given a valid transaction code Id
        When a GET request is sent to fetch transaction code by Id
        Then the response ActionResult should indicate successful retrieval of the transaction code