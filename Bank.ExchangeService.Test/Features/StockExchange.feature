Feature: Stock Exchange Service

    Scenario: Get all stock exchanges
        Given a valid stock exchange filter query and pageable
        When all stock exchanges are fetched
        Then a non-empty list of stock exchanges should be returned

    Scenario: Get stock exchange by Id
        Given a valid stock exchange Id
        When the stock exchange is fetched by Id
        Then the stock exchange details should be returned

    Scenario: Get stock exchange by acronym
        Given a valid stock exchange acronym
        When the stock exchange is fetched by acronym
        Then the stock exchange details should be returned with right acronym

    Scenario: Create a new stock exchange
        Given a valid stock exchange create request
        When the stock exchange is created
        Then the created stock exchange details should be returned
