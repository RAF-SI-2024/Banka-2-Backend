Feature: User controller testing

    Scenario: Find user by ID
        Given user with ID "123e4567-e89b-12d3-a456-426614174000" exists in database
        When I request user with ID "123e4567-e89b-12d3-a456-426614174000"
        Then the response should contain user details

    Scenario: Find user by non-existent ID
        When I request user with ID "00000000-0000-0000-0000-000000000000"
        Then the response should be Not Found

    Scenario: Fetch all users successfully
        When I request all users with no filters
        Then the response should contain at least 1 user

#Scenario: User logs in successfully
#    Given a user exists with email "user@example.com" and password "Password123"
#    When I send a login request with email "user@example.com" and password "Password123"
#    Then the response should return status 200 OK
#    And the response should contain a valid token

    Scenario: Login attempt with a non-existent email
        When I send a login request with email "invalidd@example.com" and password "Password123"
        Then the response should return status 404 Not Found
        And the response should contain the message "User with the specified email address was not found."

    Scenario: Login attempt with an incorrect password
        Given a user exists with email "user@example.com" and password "Password123"
        When I send a login request with email "user@example.com" and password "WrongPassword"
        Then the response should return status 400 Bad Request
        Then the response should contain the message: The password is incorrect.

    Scenario: User activates account successfully
        Given a valid activation token for user "user@example.com"
        And a password "SecurePass123" and confirmation password "SecurePass123"
        When I send an activation request with the token and passwords
        Then the response should return status 202 Accepted

    Scenario: Activation fails due to invalid token
        Given an invalid activation token
        And a password "SecurePass123" and confirmation password "SecurePass123"
        When I attempt to activate the account using the invalid token
        Then the activation response should return status 400 Bad Request
        And the response should contain the message: Invalid token