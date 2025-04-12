Feature: Loan controller testing

    Scenario: Create Loan
        Given loan create request
        When loan is created in the database
        And loan is fetched by Id
        Then loan details should match the created loan

    Scenario: Update Loan
        Given loan update request
        And loan Id
        When loan is updated in the database
        Then loant details should match the updated loan

    Scenario: Get all installemtns for Loan
        Given loan Id which has installemtns
        When all installemtns are fetched for the loan
        Then all installemtns should be returned for the loan

    Scenario: Get all Loans
        When all loans are fetched
        Then all loans should be returned
