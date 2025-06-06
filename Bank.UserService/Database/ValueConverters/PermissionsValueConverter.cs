using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.UserService.Database.ValueConverters;

using Permissions = Permissions.Domain.Permissions;

public class PermissionsValueConverter() : ValueConverter<Permissions, long>(permissions => permissions, value => new Permissions(value)) { }
