Feature: User controller testing

    Scenario: Successful user login
        Given user login request
        When user sends valid login request
        Then login should be successful
        And user should get valid jwt

    Scenario: Retrieves all users
        Given user get request with query filter parameters
        And user get request with query pageable
        When they request to get all users
        Then user response should be 200
        And the system returns a paginated list of users

    Scenario: Get user by Id
        Given user get request with Id
        When user is fetched by Id from the database
        Then user response should be 200
        And response should contain the user with the given Id

    Scenario: User activates their account successfully
        Given a user has received a valid activation token
        And user activation request
        When they submit a matching password and confirm password
        Then user response should be 202

    Scenario: User requests a password reset succesfully
        Given user request password reset request with data
        When they request a password reset
        Then user response should be 202