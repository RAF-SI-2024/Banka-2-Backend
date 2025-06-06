Feature: AccountCurrency service testing

    Scenario: Create AccountCurrency
        Given account currency create request
        When account currency is created in the database
        And account currency is fetched by Id
        Then account currency details should match the created account currency

    Scenario: Update AccountCurrency
        Given account currency update request
        And account currency Id
        When account currency is updated in the database
        Then account currency details should match the updated account currency

    Scenario: Get all AccountCurrencies
        When all account currencies are fetched
        Then all account currencies should be returned