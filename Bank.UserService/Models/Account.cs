﻿namespace Bank.UserService.Models;

public class Account
{
    public required Guid                  Id                { set;  get; }
    public          User?                 Client            { set;  get; }
    public required Guid                  ClientId          { set;  get; }
    public required string                Name              { set;  get; }
    public required string                Number            { set;  get; }
    public required decimal               Balance           { set;  get; }
    public required decimal               AvailableBalance  { set;  get; }
    public          User?                 Employee          { set;  get; }
    public required Guid                  EmployeeId        { set;  get; }
    public          Currency?             Currency          { set;  get; }
    public required Guid                  CurrencyId        { set;  get; }
    public          AccountType?          Type              { set;  get; }
    public required Guid                  AccountTypeId     { set;  get; }
    public          List<AccountCurrency> AccountCurrencies { init; get; } = [];
    public required decimal               DailyLimit        { set;  get; }
    public required decimal               MonthlyLimit      { set;  get; }
    public required DateOnly              CreationDate      { set;  get; }
    public required DateOnly              ExpirationDate    { set;  get; }
    public required bool                  Status            { set;  get; }
    public required DateTime              CreatedAt         { set;  get; }
    public required DateTime              ModifiedAt        { set;  get; }
}
