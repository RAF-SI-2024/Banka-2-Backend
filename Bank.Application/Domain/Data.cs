namespace Bank.Application.Domain;

public enum Role
{
    Invalid,
    Admin,
    Employee,
    Client,
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
    Completed,
    Failed
}
