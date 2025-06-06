Feature: Exchange service testing

    Scenario: Get all Exchanges with query
        Given exchange filter query <currencyId>, <currencyCode>, <date>
        When exchanges are fetched from the database
        Then the response code should be 200
        And response should contain exchanges that pass filter

    Examples:
      | currencyId | currencyCode | date  |
      |            |              |       |
      |            |              | TODAY |
      |            | EUR_CODE     |       |
      |            | RSD_CODE     |       |
      | RSD_ID     |              |       |
      | EUR_ID     |              |       |

    Scenario: Get One Exchange with id
        Given exchange id <exchangeId>
        When exchange is fetched from the database with id
        Then the response code should be <statusCode>
        And exchange response should <return>

    Examples:
      | exchangeId          | statusCode | return     |
      | VALID_EXCHANGE_ID   | 200        | DO_RETURN  |
      | INVALID_EXCHANGE_ID | 404        | NOT_RETURN |

    Scenario: Get Exchange by currencies
        Given currency from <currencyFromCode>
        And currency to <currencyToCode>
        When exchange is fetched from the database with currencies
        Then the response code should be <statusCode>
        And exchange response should <return>

    Examples:
      | currencyFromCode | currencyToCode | statusCode | return     |
      | RSD_CODE         | EUR_CODE       | 200        | DO_RETURN  |
      |                  |                | 404        | NOT_RETURN |
      | RSD_CODE         |                | 404        | NOT_RETURN |
      |                  | EUR_CODE       | 404        | NOT_RETURN |

    Scenario: Update exchange with valid id
        Given updated exchange request
        And exchange id <exchangeId>
        When updated exchange is put into database
        Then the response code should be <statusCode>
        And exchange update response should <return>

    Examples:
      | exchangeId               | statusCode | return     |
      | UPDATE_VALID_EXCHANGE_ID | 200        | DO_RETURN  |
      | INVALID_EXCHANGE_ID      | 404        | NOT_RETURN |