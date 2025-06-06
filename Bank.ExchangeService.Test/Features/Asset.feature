Feature: Asset Service

    Scenario: Get all assets
        Given a valid asset filter query and pageable
        When all assets are fetched
        Then a non-empty list of assets should be returned

    Scenario: Get asset by Id
        Given a valid asset Id
        When the asset is fetched
        Then the asset details should be returned
