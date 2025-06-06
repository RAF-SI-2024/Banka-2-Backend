Feature: Option Service

    Scenario: Get all options
        Given a valid option filter query and pageable
        When all options are fetched
        Then a non-empty list of options should be returned

    Scenario: Get option by Id
        Given a valid option Id and filter query
        When the option is fetched
        Then the option details should be returned

    Scenario: Get daily option data
        Given a valid option Id and daily filter query
        When the daily option data is fetched
        Then the daily option candle data should be returned
