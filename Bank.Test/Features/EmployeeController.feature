Feature: Employee controller testing

    Scenario: Successfully fetch employees
        Given the database contains multiple employees
        When I request all employees with no filters
        Then the API should respond with status 200 OK
        And the response should contain a list of employees
        And all employees should have the role Employee

    Scenario: Successfully retrieve an employee by ID
        Given an employee exists with ID "123e4567-e89b-12d3-a456-426614174000"
        When I request the employee with ID "123e4567-e89b-12d3-a456-426614174000"
        Then the request should be successful with status 200 OK
        And the response should contain the employee details

    Scenario: User exists but is not an Employee
        Given a user exists with ID "123e4567-e89b-12d3-a456-426614174000" and has role "Admin"
        When I request the employee with ID "123e4567-e89b-12d3-a456-426614174000"
        Then the request should return status 404 Not Found
        And the response message should indicate that no employee was found with the given ID