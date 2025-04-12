Feature: Transaction template controller testing

    Scenario: Get all Transaction templates
        Given transaction template get request with query pageable
        And authorization for transaction template
        When transactions template are fetched from the database
        Then transaction template response should be 200
        And response should contain the list of transactions template matching the filter parameters

    Scenario: Get Transaction template by Id
        Given transaction template get request with Id
        And authorization for transaction template
        When transaction template is fetched by Id from the database
        Then transaction template response should be 200
        And response should contain the transaction template with the given Id

    Scenario: Create Transaction template
        Given transaction template create request
        And authorization for transaction template
        When transaction template is created in the database
        Then transaction template response should be 200
        And transaction template details should match the created transaction template

    Scenario: Update Transaction
        Given a valid transaction template Id for update
        And update request transaction template
        When transaction template is updated in the database
        Then transaction template response should be 200
        And transaction template details should match the updated transaction template