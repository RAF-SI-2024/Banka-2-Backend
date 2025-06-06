Feature: Order Service

    Scenario: Get all orders
        Given a valid order filter query and pageable
        When all orders are fetched
        Then a non-empty list of orders should be returned

    Scenario: Get order by Id
        Given a valid order Id
        When the order is fetched
        Then the order details should be returned

    Scenario: Create a new order
        Given a valid order create request
        When the order is created
        Then the created order details should be returned

    Scenario: Update an existing order
        Given a valid order update request and order Id
        When the order is updated
        Then the updated order details should be returned
