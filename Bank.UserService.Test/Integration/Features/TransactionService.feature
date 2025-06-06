Feature: Transaction service testing

    Scenario: Get all transaction with filter parameters
        Given transaction get request with query filter parameters
        And transaction get request with query pageable
        When transactions are fetched from the database
        Then transaction response should be 200
        And response should contain the list of transactions matching the filter parameters

    Scenario: Get transaction by Id
        Given transaction get request with Id
        When transaction is fetched by Id from the database
        Then transaction response should be 200
        And response should contain the transaction with the given Id

    Scenario: Get all transaction by account Id with filter parameters
        Given transaction get request with account Id
        And transaction get request with query filter parameters
        And transaction get request with query pageable
        When transactions are fetched by account Id from the database
        Then transaction response should be 200
        And response should contain the list of transactions matching the filter parameters and account Id

#    Scenario: Create Transaction
#        Given transaction create request
#        When transaction is created in the database
#        Then transaction response should be 200
#        And transaction details should match the created transaction

    Scenario: Update Transaction
        Given a valid transaction Id for update
        And update request transaction
        When transaction is updated in the database
        Then transaction response should be 200
        And transaction details should match the updated transaction