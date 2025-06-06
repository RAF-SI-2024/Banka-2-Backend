Feature: Transaction controller testing

    Scenario: Get all transactions with filter parameters through API
        Given a transaction filter query and pageable parameters
        When a GET request is sent to fetch all transactions
        Then the response ActionResult should indicate successful retrieval of transactions matching the filter parameters

    Scenario: Get all transactions by account Id and filter parameters through API
        Given a valid account Id and a transaction filter query and pageable parameters
        When a GET request is sent to fetch transactions by account Id
        Then the response ActionResult should indicate successful retrieval of transactions for the account

    Scenario: Get transaction by Id through API
        Given a valid transaction Id
        When a GET request is sent to fetch a transaction by Id
        Then the response ActionResult should indicate successful retrieval of the transaction

#    Scenario: Create transaction through API
#        Given a valid transaction create request
#        When a POST request is sent to the transaction creation endpoint
#        Then the response ActionResult should indicate successful transaction creation

    Scenario: Update transaction through API
        Given a valid transaction Id and update request
        When a PUT request is sent to the transaction update endpoint
        Then the response ActionResult should indicate successful transaction update