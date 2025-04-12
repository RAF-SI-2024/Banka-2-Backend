Feature: Employee controller testing

    Scenario: Get AllEmployees
        Given employee filter request
        And pageable parameters for employee list
        When employee list is fetched from the database
        Then the result should contain a list of employees

    Scenario: Create Employee
        Given employee create request
        When employee is created in the database
        Then employee details should match the created employee

    Scenario: Update Employee
        Given a valid employee Id for update
        And update request employee
        When employee is updated in the database
        Then employee details should match the updated employee

    Scenario: GetOne Employee
        Given a valid employee Id
        When employee is fetched by Id
        Then the result should contain the employee details