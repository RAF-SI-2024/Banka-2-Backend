Feature: Exchange controller testing

    Scenario: Get all exchanges through API
        Given a valid exchange filter query
        When a GET request is sent to the exchange list endpoint
        Then the response ActionResult should indicate successful retrieval of the exchange list

    Scenario: Get exchange by Id through API
        Given a valid exchange Id
        When a GET request is sent to fetch the exchange by Id
        Then the response ActionResult should indicate successful retrieval of the exchange

    Scenario: Get exchange by currency pair through API
        Given a valid exchange between query
        When a GET request is sent to fetch the exchange by currencies
        Then the response ActionResult should indicate successful retrieval of the exchange for the given currencies

#    Scenario: Make exchange through API
#        Given a valid exchange make request
#        When a POST request is sent to the exchange creation endpoint
#        Then the response ActionResult should indicate successful exchange creation

    Scenario: Update exchange through API
        Given a valid exchange update request and exchange Id
        When a PUT request is sent to the exchange update endpoint
        Then the response ActionResult should indicate successful exchange update