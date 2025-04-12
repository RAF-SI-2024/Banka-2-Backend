Feature: Installment controller testing

    Scenario: Create Installment
        Given installment create request
        When installment is created in the database
        And installment is fetched by Id
        Then installment details should match the created installment

    Scenario: Update Installment
        Given installment update request
        And installment Id
        When installment is updated with request in the database
        Then installment details in request should match the updated installment

    Scenario: Get All Installments for Loan
        Given loan Id for installments
        When all installments are fetched for the account
        Then all installments should be returned for the account
