﻿namespace Bank.Application.Domain;

public enum Role
{
    Invalid,
    Admin,
    Employee,
    Client
}

public enum Gender
{
    Invalid,
    Male,
    Female
}

public enum TransactionStatus
{
    Invalid,
    Pending,
    Canceled,
    Affirm,
    Completed,
    Failed
}

public enum LoanStatus
{
    Pending,
    Active,
    Rejected,
    Closed,
    DefaultWarning,
    Default
}

public enum InterestType
{
    Fixed,
    Variable,
    Mixed
}

public enum InstallmentStatus
{
    Pending,
    Paid,
    Overdue,
    Cancelled
}

public enum Profile
{
    Development,
    Testing,
    Staging,
    Production
}

public enum OrderType
{
    Invalid,
    Market,
    Limit,
    Stop,
    StopLimit
}

public enum Direction
{
    Invalid,
    Buy,
    Sell
}

public enum OrderStatus
{
    Invalid,
    Pending,
    Approved,
    Declined,
    Completed,
    Canceled,
    Failed
}

public enum PermissionType
{
    Invalid,
    Set,
    Clear,
}

public enum Permission : long
{
    Invalid      = 0,
    Client       = 1 << 0,
    Employee     = 1 << 1,
    Admin        = 1 << 2,
    Trade        = 1 << 3,
    ApproveTrade = 1 << 4,

    Agent      = Employee | Trade,
    Supervisor = Employee | Trade | ApproveTrade,
}

public enum Liquidity
{
    High,
    Medium,
    Low
}

public enum OptionType
{
    Call,
    Put
}

public enum ContractUnit
{
    Kilogram,
    Liter,
    Barrel,
    Bushel,
    Pound,
    CubicMeter
}

public enum SecurityType
{
    Stock,
    Option,
    FutureContract,
    ForexPair
}

public enum QuoteIntervalType
{
    Week,
    Day,
    Month,
    ThreeMonths,
    Year,
    Max
}
