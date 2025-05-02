﻿using Bank.Application.Responses;

namespace Bank.Link.Core;

public class DefaultData(List<CurrencyResponse>? currencies = null, List<TransactionCodeResponse>? transactionCodes = null)
{
    public List<CurrencyResponse>        Currencies       { set; get; } = currencies       ?? [];
    public List<TransactionCodeResponse> TransactionCodes { set; get; } = transactionCodes ?? [];
}
