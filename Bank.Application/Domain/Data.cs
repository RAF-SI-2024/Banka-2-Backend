namespace Bank.Application.Domain;

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
    Failed
}

public enum Permission : long
{
    Invalid  = 1 << 0,
    Admin    = 1 << 1,
    Employee = 1 << 2,
    Client   = 1 << 3
}
