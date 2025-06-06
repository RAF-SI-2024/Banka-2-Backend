Feature: User controller testing

    Scenario: Get all users through API
        Given a user filter query and pageable parameters
        When a GET request is sent to fetch all users
        Then the response should contain the list of users

    Scenario: Get user by Id through API
        Given a valid user Id
        When a GET request is sent to fetch a user by Id
        Then the response should contain the user

    Scenario: Login user through API
        Given a valid user login request
        When a POST request is sent to the login endpoint
        Then the login response should be successfully returned

    Scenario: Activate user account through API
        Given a valid activation request and token
        When a POST request is sent to the activation endpoint
        Then the response should indicate successful activation

    Scenario: Request password reset through API
        Given a valid password reset request
        When a POST request is sent to request password reset
        Then the response should indicate reset email was sent

    Scenario: Reset password through API
        Given a valid new password and reset token
        When a POST request is sent to reset password
        Then the password should be reset successfully

    Scenario: Update user permissions through API
        Given a user Id and permission update request
        When a PUT request is sent to update user permission
        Then the user permission should be updated successfully