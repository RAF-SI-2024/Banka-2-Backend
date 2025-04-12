Feature: Comapny controller testing

    Scenario: Create Company
        Given company create request
        When company is created in the database
        And company is fetched by Id
        Then company details should match the created company

    Scenario: Get All Companies
        Given company filter query
        And pageable parameters
        When all companies are fetched
        Then the result should contain a list of companies

    Scenario: Get One Company
        Given a valid company Id for fetching
        When company is fetched by Id
        Then the result should contain the company details

    Scenario: Update Company
        Given a valid company Id
        And update request
        When the company is updated
        Then the result contains the updated company response