Feature: TransactionTemplate controller testing

    Scenario: Get all transaction templates through API
        Given a pageable query for transaction templates
        When a GET request is sent to fetch all transaction templates
        Then the response ActionResult should indicate successful retrieval of transaction templates

    Scenario: Get transaction template by Id through API
        Given a valid transaction template Id
        And authorization for transaction template
        When a GET request is sent to fetch a transaction template by Id
        Then the response ActionResult should indicate successful retrieval of the transaction template

    Scenario: Create transaction template through API
        Given a valid transaction template create request
        And authorization for transaction template
        When a POST request is sent to the transaction template creation endpoint
        Then the response ActionResult should indicate successful transaction template creation

    Scenario: Update transaction template through API
        Given a valid transaction template Id and an update request
        When a PUT request is sent to the transaction template update endpoint
        Then the response ActionResult should indicate successful transaction template update