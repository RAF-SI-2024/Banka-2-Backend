using Bank.Application.Domain;
using Bank.Application.Extensions;

namespace Bank.Permissions.Domain;

public struct Permissions(long permissions = (int)Permission.Invalid) : IEquatable<Permissions>
{
    public static readonly string Identifier = nameof(Permissions)
    .ToCamelCase();

    private long m_Permissions = permissions;

    public Permissions(params Permission[] permissions) : this(0L)
    {
        AddPermissions(permissions);
    }

    public static bool TryParse(string? value, out Permissions permissions)
    {
        permissions = default;

        if (long.TryParse(value, out var permissionsValue) is false)
            return false;

        permissions.m_Permissions = permissionsValue;

        return true;
    }

    public Permissions AddPermission(Permission permission)
    {
        m_Permissions |= (long)permission;

        return this;
    }

    public Permissions RemovePermission(Permission permission)
    {
        m_Permissions &= ~(long)permission;

        return this;
    }

    public bool HasPermission(Permission permission)
    {
        return (m_Permissions & (long)permission) == (long)permission;
    }

    public Permissions AddPermissions(params Permission[] permissions)
    {
        foreach (var permission in permissions)
            AddPermission(permission);

        return this;
    }

    public Permissions RemovePermissions(params Permission[] permissions)
    {
        foreach (var permission in permissions)
            RemovePermission(permission);

        return this;
    }

    public bool HasPermissions(Permissions permissions)
    {
        return m_Permissions == permissions.m_Permissions;
    }

    public bool HasPermissions(params Permission[] permissions)
    {
        var permissionsInstance = this;

        return permissions.Select(permission => permissionsInstance.HasPermission(permission))
                          .All(identity => identity);
    }

    public static Permissions operator+(Permissions permissions, Permission permission) => permissions.AddPermission(permission);

    public static Permissions operator-(Permissions permissions, Permission permission) => permissions.RemovePermission(permission);

    public static bool operator==(Permissions permissions, Permission permission) => permissions.HasPermission(permission);

    public static bool operator!=(Permissions permissions, Permission permission) => !permissions.HasPermission(permission);

    public static bool operator==(Permissions permissions, Permissions comparePermissions) => permissions.HasPermissions(comparePermissions);

    public static bool operator!=(Permissions permissions, Permissions comparePermissions) => !permissions.HasPermissions(comparePermissions);

    public static implicit operator long(Permissions permissions) => permissions.m_Permissions;

    public bool Equals(Permissions other) => HasPermissions(other);

    public override bool Equals(object? @object) => @object is Permissions other && Equals(other);

    public override int GetHashCode() => m_Permissions.GetHashCode();

    public override string ToString() => m_Permissions.ToString();
}
