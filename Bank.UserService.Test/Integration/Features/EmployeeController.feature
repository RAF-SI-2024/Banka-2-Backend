Feature: Employee controller testing

    Scenario: Get all employees through API
        Given a valid employee filter request and pageable parameters
        When a GET request is sent to fetch all employees
        Then the response ActionResult should indicate successful retrieval of all employees

    Scenario: Create employee through API
        Given a valid employee create request
        When a POST request is sent to the employee creation endpoint
        Then the response ActionResult should indicate successful employee creation

    Scenario: Update employee through API
        Given a valid employee Id and update request
        When a PUT request is sent to the employee update endpoint
        Then the response ActionResult should indicate successful employee update

    Scenario: Get employee by Id through API
        Given a valid employee Id that exists
        When a GET request is sent to fetch the employee by Id
        Then the response ActionResult should indicate successful retrieval of the employee