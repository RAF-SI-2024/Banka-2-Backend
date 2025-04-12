Feature: Account Controller testing

    Scenario: Create Account
        Given account create request
        When account is created in the database
        And account is fetched by Id
        Then account details should match the created account

    Scenario: Update Client Account
        Given account update client request
        And account Id
        When account is updated with client request in the database
        Then account details in client request should match the updated account

    Scenario: Update Employee Account
        Given account update employee request
        And account Id
        When account is updated with employee request in the database
        Then account details in employee request should match the updated account

    Scenario: Get All Cards for Account
        Given account Id
        When all cards are fetched for the account
        Then all cards should be returned for the account

    Scenario: Get All Acounts
        When all acounts are fetched
        Then all accounts should be returned
