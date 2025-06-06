﻿namespace Bank.Application.Requests;

public class ExchangeUpdateRequest
{
    public required decimal Commission { set; get; }
}

public class ExchangeMakeExchangeRequest
{
    public required Guid    CurrencyFromId { set; get; }
    public required Guid    CurrencyToId   { set; get; }
    public required decimal Amount         { set; get; }
    public required Guid    AccountId      { set; get; }
}
