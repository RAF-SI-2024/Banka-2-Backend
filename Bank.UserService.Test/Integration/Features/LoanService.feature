Feature: Loan service testing

    Scenario: Create Loan
        Given loan create request
        When loan is created in the database
        And loan is fetched by Id
        Then loan details should match the created loan

    Scenario: Update Loan
        Given loan update request
        And loan Id
        When loan is updated in the database
        Then loan details should match the updated loan

    Scenario: Get all Loans
        When all loans are fetched
        Then all loans should be returned

    Scenario: Get Loan by Id
        Given loan Id
        When loan is provided by Id
        Then loan details should be returned

    Scenario: Get Loans by Client Id
        Given client Id which has loans
        When loans are fetched by client Id
        Then all loans for the client should be returned