Feature: Currency controller testing

    Scenario: Get all currencies with name filter parameter through API
        Given a currency filter query with name filter
        When a GET request is sent to fetch currencies
        Then the response ActionResult should indicate successful retrieval of currencies matching the name filter

    Scenario: Get all currencies with code filter parameter through API
        Given a currency filter query with code filter
        When a GET request is sent to fetch currencies
        Then the response ActionResult should indicate successful retrieval of currencies matching the code filter

    Scenario: Get currency by Id through API
        Given a currency Id to fetch
        When a GET request is sent to fetch a currency by Id
        Then the response ActionResult should indicate successful retrieval of the currency

    Scenario: Get all simple currencies through API
        Given a currency filter query for simple currencies
        When a GET request is sent to fetch all simple currencies
        Then the response ActionResult should indicate successful retrieval of all simple currencies

    Scenario: Get simple currency by Id through API
        Given a simple currency Id to fetch
        When a GET request is sent to fetch a simple currency by Id
        Then the response ActionResult should indicate successful retrieval of the simple currency