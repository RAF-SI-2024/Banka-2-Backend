namespace Bank.Application.Domain;

public struct AuthPermission
{
    public Permission Value { get; private set; }

    public AuthPermission(long value)
    {
        Value = (Permission)value;
    }

    public AuthPermission(Permission permission)
    {
        Value = permission;
    }

    public void AddPermission(Permission permission)
    {
        Value = (Permission)((long)Value | (long)permission);
    }

    public void RemovePermission(Permission permission)
    {
        Value = (Permission)((long)Value & ~(long)permission);
    }

    public bool HasPermission(Permission permission)
    {
        return ((long)Value & (long)permission) == (long)permission;
    }

    public static implicit operator Permission(AuthPermission permissions)
    {
        return permissions.Value;
    }

    public static implicit operator AuthPermission(long value)
    {
        return new AuthPermission(value);
    }

    public static implicit operator AuthPermission(Permission permission)
    {
        return new AuthPermission(permission);
    }
}
