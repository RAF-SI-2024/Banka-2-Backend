Feature: Future Contract Service testing

    Scenario: Get all future contracts
        Given a valid future contract filter query and pageable
        When all future contracts are fetched
        Then a non-empty list of future contracts should be returned

    Scenario: Get future contract by Id
        Given a valid future contract Id and filter query
        When the future contract is fetched
        Then the future contract details should be returned

    Scenario: Get daily future contract data
        Given a valid future contract Id and daily filter query
        When the daily future contract data is fetched
        Then the daily future contract candle data should be returned
