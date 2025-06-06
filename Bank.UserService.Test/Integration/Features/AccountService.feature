Feature: Account service testing

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

    Scenario: Get All Acounts
        When all accounts are fetched
        Then all accounts should be returned

    Scenario: Get all Account from Client
        Given client Id
        When all accounts are fetched from the database
        Then all accounts should be returned for that client