Feature: Country service testing

    Scenario: Get all countries with name filter parameter
        Given country get request with name filter parameter
        When countries are fetched from the database
        Then response should be 200
        And response should contain a list of countries matching the name parameter

    Scenario: Get all countries with currency code filter parameter
        Given country get request with currency code filter parameter
        When countries are fetched from the database
        Then response should be 200
        And response should contain a list od countries matching the currency code parameter

    Scenario: Get all countries with currency name filter parameter
        Given country get request with currency name filter parameter
        When countries are fetched from the database
        Then response should be 200
        And response should contain a list of countries matching the currency name parameter

    Scenario: Get country by Id
        Given country get request with Id
        When country is fetched by Id from the database
        Then response should contain the country with the given Id