using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

file static class Values
{
    public static readonly Guid   Id   = Guid.Parse("7f3e7c12-a8b4-47f9-b5a7-123456789abc");
    public const           string Name = "United States of America";
}

public static partial class Example
{
    public static class Country
    {
        public static readonly CountryResponse Response = new()
                                                          {
                                                              Id         = Values.Id,
                                                              Name       = Values.Name,
                                                              Currency   = null!,
                                                              CreatedAt  = DateTime.Now,
                                                              ModifiedAt = DateTime.Now
                                                          };

        public static readonly CountrySimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id         = Values.Id,
                                                                          Name       = Values.Name,
                                                                          CreatedAt  = DateTime.Now,
                                                                          ModifiedAt = DateTime.Now
                                                                      };
    }
}
