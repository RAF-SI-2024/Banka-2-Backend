Feature: Country controller testing

    Scenario: Get all countries with name filter parameter through API
        Given a country filter query with name filter
        When a GET request is sent to fetch countries
        Then the response ActionResult should indicate successful retrieval of countries matching the name filter

    Scenario: Get all countries with currency code filter parameter through API
        Given a country filter query with currency code filter
        When a GET request is sent to fetch countries
        Then the response ActionResult should indicate successful retrieval of countries matching the currency code filter

    Scenario: Get all countries with currency name filter parameter through API
        Given a country filter query with currency name filter
        When a GET request is sent to fetch countries
        Then the response ActionResult should indicate successful retrieval of countries matching the currency name filter

    Scenario: Get country by Id through API
        Given a country Id to fetch
        When a GET request is sent to fetch a country by Id
        Then the response ActionResult should indicate successful retrieval of the country