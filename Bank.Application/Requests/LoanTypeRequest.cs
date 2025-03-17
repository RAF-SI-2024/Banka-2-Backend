﻿namespace Bank.Application.Requests;

public class LoanTypeRequest
{
    public required string  Name   { get; set; }
    public required decimal Margin { get; set; }
}

public class LoanTypeUpdateRequest
{
    public string?  Name   { get; set; }
    public decimal? Margin { get; set; }
}
