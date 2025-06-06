using System.Collections.Immutable;

using Bank.Application.Domain;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using ClientModel = Client;
using Permissions = Permissions.Domain.Permissions;

public static partial class Seeder
{
    public static class Client
    {
        public static readonly ClientModel Bank = new()
                                                  {
                                                      Id                         = Guid.Parse("45ddacb5-eedf-4e18-b23e-850a1f1e2e8a"),
                                                      FirstName                  = "Uwubank",
                                                      LastName                   = "Bankster",
                                                      Role                       = Role.Client,
                                                      BankId                     = Seeder.Bank.Bank02.Id,
                                                      DateOfBirth                = new DateOnly(2000, 1, 1),
                                                      Gender                     = Gender.Male,
                                                      UniqueIdentificationNumber = "0101000710017",
                                                      Email                      = "uwubankster@gmail.com",
                                                      PhoneNumber                = "+38164123123",
                                                      Address                    = "Boulevard of Broken Dreams 20",
                                                      Password                   = "uwubank",
                                                      Salt                       = Guid.NewGuid(),
                                                      CreatedAt                  = DateTime.UtcNow,
                                                      ModifiedAt                 = DateTime.UtcNow,
                                                      Activated                  = true,
                                                      Permissions                = new Permissions(Permission.Client)
                                                  };

        public static readonly ClientModel Client01 = new()
                                                      {
                                                          Id                         = Guid.Parse("b5d36c22-3b6c-4de0-845b-a1a74e7b9856"),
                                                          FirstName                  = "Marko",
                                                          LastName                   = "Petrović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(2000, 11, 11),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "1111000710024",
                                                          Email                      = "client1@gmail.com",
                                                          PhoneNumber                = "+38166567890",
                                                          Address                    = "Knez Mihailova 15, Beograd",
                                                          Password                   = "client1",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client02 = new()
                                                      {
                                                          Id                         = Guid.Parse("e6073905-9bee-46ab-b3c1-e25346288add"),
                                                          FirstName                  = "Andrija",
                                                          LastName                   = "Petrović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(2000, 10, 10),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "1010000720037",
                                                          Email                      = "client2@gmail.com",
                                                          PhoneNumber                = "+38167678901",
                                                          Address                    = "Manhattan Avenue 30",
                                                          Password                   = "client2",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client03 = new()
                                                      {
                                                          Id                         = Guid.Parse("36bfba9b-2810-4957-8cf5-c9cc40adb7d6"),
                                                          FirstName                  = "Stefan",
                                                          LastName                   = "Nikolić",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1989, 9, 25),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "2509989300007",
                                                          Email                      = "client3@gmail.com",
                                                          PhoneNumber                = "+38168789012",
                                                          Address                    = "Cara Dušana 55",
                                                          Password                   = "client3",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client04 = new()
                                                      {
                                                          Id                         = Guid.Parse("2eecd1a1-d558-4c88-83e3-517768fca1fe"),
                                                          FirstName                  = "Ivana",
                                                          LastName                   = "Popović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1990, 11, 12),
                                                          Gender                     = Gender.Female,
                                                          UniqueIdentificationNumber = "1211990785019",
                                                          Email                      = "client4@gmail.com",
                                                          PhoneNumber                = "+38169890123",
                                                          Address                    = "Bulevar Oslobođenja 71",
                                                          Password                   = "client4",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client05 = new()
                                                      {
                                                          Id                         = Guid.Parse("28423419-f892-4c65-beeb-d3f3f364f8fd"),
                                                          FirstName                  = "Ivan",
                                                          LastName                   = "Marić",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1985, 7, 7),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "0707985120008",
                                                          Email                      = "client5@gmail.com",
                                                          PhoneNumber                = "+38161901234",
                                                          Address                    = "Vojvode Stepe 122",
                                                          Password                   = "client5",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client06 = new()
                                                      {
                                                          Id                         = Guid.Parse("0be15ed6-db15-4605-898c-6a843fbc604b"),
                                                          FirstName                  = "Jelena",
                                                          LastName                   = "Kovačević",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1994, 3, 18),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "1803994430003",
                                                          Email                      = "client6@gmail.com",
                                                          PhoneNumber                = "+38162012345",
                                                          Address                    = "Nemanjina 8",
                                                          Password                   = "client6",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client07 = new()
                                                      {
                                                          Id                         = Guid.Parse("83b0d1eb-653f-4666-b300-9b16b0f99dbe"),
                                                          FirstName                  = "Jovana",
                                                          LastName                   = "Ilić",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1995, 6, 22),
                                                          Gender                     = Gender.Female,
                                                          UniqueIdentificationNumber = "2206995705014",
                                                          Email                      = "client7c@gmail.com",
                                                          PhoneNumber                = "+38163123456",
                                                          Address                    = "Takovska 7",
                                                          Password                   = "client7",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client08 = new()
                                                      {
                                                          Id                         = Guid.Parse("5e71e865-d504-4d43-a790-a5a8339fec5d"),
                                                          FirstName                  = "Aleksandar",
                                                          LastName                   = "Todorović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1987, 9, 3),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "0309987350002",
                                                          Email                      = "client8@gmail.com",
                                                          PhoneNumber                = "+38164234567",
                                                          Address                    = "Kralja Milana 31",
                                                          Password                   = "client8",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client09 = new()
                                                      {
                                                          Id                         = Guid.Parse("6e01bdcb-12bb-4579-b359-e7d2f7d0c2b8"),
                                                          FirstName                  = "Milica",
                                                          LastName                   = "Pavlović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1996, 2, 28),
                                                          Gender                     = Gender.Female,
                                                          UniqueIdentificationNumber = "2802996735018",
                                                          Email                      = "client9@email.com",
                                                          PhoneNumber                = "+38165345678",
                                                          Address                    = "Makedonska 15",
                                                          Password                   = "client9",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };

        public static readonly ClientModel Client10 = new()
                                                      {
                                                          Id                         = Guid.Parse("98ba23a3-9b47-40e0-86df-c95bdda371c1"),
                                                          FirstName                  = "Dragan",
                                                          LastName                   = "Simić",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1983, 12, 5),
                                                          Gender                     = Gender.Male,
                                                          UniqueIdentificationNumber = "0512983290007",
                                                          Email                      = "client10@gmail.com",
                                                          PhoneNumber                = "+38166456789",
                                                          Address                    = "Terazije 23",
                                                          Password                   = "client10",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client)
                                                      };
        
        public static readonly ClientModel Actuary01 = new()
                                                      {
                                                          Id                         = Guid.Parse("6d14d187-b610-49d8-af7b-7656ef23edd5"),
                                                          FirstName                  = "Milica",
                                                          LastName                   = "Petrović",
                                                          Role                       = Role.Client,
                                                          BankId                     = Seeder.Bank.Bank02.Id,
                                                          DateOfBirth                = new DateOnly(1912, 9, 9),
                                                          Gender                     = Gender.Female,
                                                          UniqueIdentificationNumber = "0512983290007",
                                                          Email                      = "actuary01@gmail.com",
                                                          PhoneNumber                = "+38166456789",
                                                          Address                    = "Terazije 23",
                                                          Password                   = "actuary01",
                                                          Salt                       = Guid.NewGuid(),
                                                          CreatedAt                  = DateTime.UtcNow,
                                                          ModifiedAt                 = DateTime.UtcNow,
                                                          Activated                  = true,
                                                          Permissions                = new Permissions(Permission.Client, Permission.Trade)
                                                      };
        
        public static readonly ClientModel Actuary02 = new()
                                                       {
                                                           Id                         = Guid.Parse("42c78897-ecb9-4817-af50-4ad3423c8264"),
                                                           FirstName                  = "Nikola",
                                                           LastName                   = "Jovanović",
                                                           Role                       = Role.Client,
                                                           BankId                     = Seeder.Bank.Bank02.Id,
                                                           DateOfBirth                = new DateOnly(1973, 10, 5),
                                                           Gender                     = Gender.Male,
                                                           UniqueIdentificationNumber = "0512983290007",
                                                           Email                      = "actuary02@gmail.com",
                                                           PhoneNumber                = "+38166456789",
                                                           Address                    = "Terazije 23",
                                                           Password                   = "actuary02",
                                                           Salt                       = Guid.NewGuid(),
                                                           CreatedAt                  = DateTime.UtcNow,
                                                           ModifiedAt                 = DateTime.UtcNow,
                                                           Activated                  = true,
                                                           Permissions                = new Permissions(Permission.Client, Permission.Trade)
                                                       };

        public static readonly ImmutableArray<ClientModel> All =
        [
            Bank, Client01, Client02, Client03, Client04, Client05, Client06, Client07, Client08, Client09, Client10, Actuary01, Actuary02
        ];
    }
}
