Feature: Bank service testing

    Scenario: Get all Banks
        When all banks are fetched
        Then all banks should be returned

    Scenario: Get Bank by Id
        Given bank Id
        When bank is fetched by Id
        Then bank details should match the expected bank