Feature: Installment controller testing

    Scenario: Create installment through API
        Given a valid installment create request
        When a POST request is sent to the installment creation endpoint
        Then the response ActionResult should indicate successful installment creation

    Scenario: Update installment through API
        Given a valid installment update request and installment Id
        When a PUT request is sent to the installment update endpoint
        Then the response ActionResult should indicate successful installment update

    Scenario: Get all installments by loan through API
        Given a valid loan Id with installments
        When a GET request is sent to fetch installments for the loan
        Then the response ActionResult should indicate successful retrieval of installments for the loan

    Scenario: Get installment by Id through API
        Given a valid installment Id
        When a GET request is sent to fetch the installment by Id
        Then the response ActionResult should indicate successful retrieval of the installment