Feature: Currency controller testing

    Scenario: Get all currencies with name filter parameter
        Given currency get request with name filter parameter
        When currencies are fetched from the database
        Then response should contain a list of currencies matching the name filter

    Scenario: Get all currencies with code filter parameter
        Given currency get request with code filter parameter
        When currencies are fetched from the database
        Then response should contain a list of currencies matching the code filter

    Scenario: Get currency by Id
        Given currency get request with Id
        When currency is fetched by Id from the database
        Then response should contain the currency with the given Id