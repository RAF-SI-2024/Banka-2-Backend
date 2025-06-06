Feature: ForexPair service testing

    Scenario: Get All Forex Pairs
        Given a valid quote filter query and pageable
        When all forex pairs are fetched
        Then a non-empty list of forex pairs should be returned

    Scenario: Get Forex Pair by Id
        Given a valid forex pair Id and filter query
        When the forex pair is fetched
        Then the forex pair details should be returned

    Scenario: Get Daily Data for Forex Pair
        Given a valid forex pair Id and daily filter query
        When the daily forex pair data is fetched
        Then the daily forex pair candle data should be returned
