Feature: Email service testing

    Scenario: Sending email
        Given email type
        And user exists
        When email is sent
        Then email should be sent successfully

    Scenario: Sending email with parameters
        Given email type
        And user exists
        And parameters exist
        When email is sent with parameters
        Then email should be sent successfully