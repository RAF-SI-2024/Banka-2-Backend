Feature: Transaction Code service testing

    Scenario: Get all Transaction codes
        Given transaction code get request with query pageable
        When transaction codes are fecthed from the database
        Then the transaction code response code should be 200
        And response should contain list of transaction codes matching pageable

    Scenario: Get transaction code by id
        Given given valid transaction code id
        When transaction code is fecthed from the database
        Then the transaction code response code should be 200
        And response should contain transaction code with given id