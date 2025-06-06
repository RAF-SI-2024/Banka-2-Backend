Feature: Stock Service

    Scenario: Get all stocks
        Given a valid stock filter query and pageable
        When all stocks are fetched
        Then a non-empty list of stocks should be returned

    Scenario: Get stock by Id
        Given a valid stock Id and filter query
        When the stock is fetched
        Then the stock details should be returned

    Scenario: Get daily stock data
        Given a valid stock Id and daily filter query
        When the daily stock data is fetched
        Then the daily stock candle data should be returned
