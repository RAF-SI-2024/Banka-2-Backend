Feature: Loant Type controller testing

    Scenario: Create Loan Type
        Given loan type create request
        When loan type is created in the database
        And loan type is fetched by Id
        Then loan type details should match the created loan type

    Scenario: Update Loan Type
        Given loan type update request
        And loan type Id
        When loan type is updated in the database
        Then loant type details should match the updated loan type

    Scenario: Get All Loan Types
        When all loan types are fetched from the database
        Then all loan types should be returned
