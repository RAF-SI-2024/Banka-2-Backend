using System.Collections.Immutable;

using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using TransactionCodeModel = TransactionCode;

public static partial class Seeder
{
    public static class TransactionCode
    {
        public static readonly TransactionCodeModel TransactionCode220 = new()
                                                                         {
                                                                             Id         = Guid.Parse("840bbb64-c94c-4e78-95b0-d7842e736629"),
                                                                             Code       = "220",
                                                                             Name       = "INTERMEDIATE CONSUMPTION PAYMENTS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode221 = new()
                                                                         {
                                                                             Id         = Guid.Parse("2c42c332-4995-4e25-8318-d8d9fab56e7b"),
                                                                             Code       = "221",
                                                                             Name       = "FINAL CONSUMPTION PAYMENTS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode222 = new()
                                                                         {
                                                                             Id         = Guid.Parse("1a89c117-7a3c-4936-b351-f4aa066677b6"),
                                                                             Code       = "222",
                                                                             Name       = "PUBLIC ENTERPRISE SERVICES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode223 = new()
                                                                         {
                                                                             Id         = Guid.Parse("c081cea1-50df-46ba-bf3a-9eafad8d8c9c"),
                                                                             Code       = "223",
                                                                             Name       = "INVESTMENTS IN BUILDINGS AND EQUIPMENT",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode224 = new()
                                                                         {
                                                                             Id         = Guid.Parse("bb27c75f-4280-48d2-b111-e0dae9a430d5"),
                                                                             Code       = "224",
                                                                             Name       = "OTHER INVESTMENTS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode225 = new()
                                                                         {
                                                                             Id         = Guid.Parse("e763f051-16b0-47eb-9bfc-83d053a215f5"),
                                                                             Code       = "225",
                                                                             Name       = "LEASE PAYMENTS (STATE PROPERTY)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode226 = new()
                                                                         {
                                                                             Id         = Guid.Parse("27544ada-096d-48b2-a2f5-103ddca97ae4"),
                                                                             Code       = "226",
                                                                             Name       = "LEASE PAYMENTS (TAXABLE)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode227 = new()
                                                                         {
                                                                             Id         = Guid.Parse("e4f9dfd3-cf70-41ce-a6c3-d398dd4132f5"),
                                                                             Code       = "227",
                                                                             Name       = "SUBSIDIES, REFUNDS, AND BONUSES (CONSOLIDATED ACCOUNT)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode228 = new()
                                                                         {
                                                                             Id         = Guid.Parse("6106f6bc-41be-4d51-bd60-614bb7e7f16a"),
                                                                             Code       = "228",
                                                                             Name       = "SUBSIDIES, REFUNDS, AND BONUSES (OTHER ACCOUNTS)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode231 = new()
                                                                         {
                                                                             Id         = Guid.Parse("590a23bb-ea5f-46d1-a44c-e9dda8200afd"),
                                                                             Code       = "231",
                                                                             Name       = "CUSTOMS AND OTHER IMPORT DUTIES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode240 = new()
                                                                         {
                                                                             Id         = Guid.Parse("81c69e4e-ad32-46ca-a1dc-9f6c3f68b5f0"),
                                                                             Code       = "240",
                                                                             Name       = "WAGES AND OTHER EMPLOYEE INCOME",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode241 = new()
                                                                         {
                                                                             Id         = Guid.Parse("29540ec0-5ec9-4467-84f0-047003046e85"),
                                                                             Code       = "241",
                                                                             Name       = "TAX-FREE EMPLOYEE INCOME",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode242 = new()
                                                                         {
                                                                             Id         = Guid.Parse("481b39e1-0db7-4db1-9e6e-f9661d7fa338"),
                                                                             Code       = "242",
                                                                             Name       = "WAGE COMPENSATION AT EMPLOYER’S EXPENSE",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode244 = new()
                                                                         {
                                                                             Id         = Guid.Parse("19122f51-734d-4beb-82e0-d1913d7dce16"),
                                                                             Code       = "244",
                                                                             Name       = "PAYMENTS THROUGH YOUTH AND STUDENT COOPERATIVES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode245 = new()
                                                                         {
                                                                             Id         = Guid.Parse("e13168f7-401d-41bc-8b78-a2948924521c"),
                                                                             Code       = "245",
                                                                             Name       = "PENSIONS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode246 = new()
                                                                         {
                                                                             Id         = Guid.Parse("a195202f-6774-4f0e-b3ff-0bf3f3cc16a5"),
                                                                             Code       = "246",
                                                                             Name       = "DEDUCTIONS FROM PENSIONS AND WAGES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode247 = new()
                                                                         {
                                                                             Id         = Guid.Parse("097560e7-c1e2-4827-a3c7-8a79aa74fe9d"),
                                                                             Code       = "247",
                                                                             Name       = "WAGE COMPENSATION AT OTHER PAYERS’ EXPENSE",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode248 = new()
                                                                         {
                                                                             Id         = Guid.Parse("7a04b6fb-dbaf-4410-8217-3b32047404c2"),
                                                                             Code       = "248",
                                                                             Name       = "INCOME OF INDIVIDUALS FROM CAPITAL",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode249 = new()
                                                                         {
                                                                             Id         = Guid.Parse("fcecc233-f2b5-4880-aef4-acb986f6f754"),
                                                                             Code       = "249",
                                                                             Name       = "OTHER INCOME OF INDIVIDUALS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode253 = new()
                                                                         {
                                                                             Id         = Guid.Parse("d8dbd742-7c24-44d2-8d54-0cb1f238699a"),
                                                                             Code       = "253",
                                                                             Name       = "PUBLIC REVENUES BY DEDUCTION (EXCLUDING TAXES AND CONTRIBUTIONS)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode254 = new()
                                                                         {
                                                                             Id         = Guid.Parse("3ef864cb-53b1-43ac-9b54-6d451753d1f9"),
                                                                             Code       = "254",
                                                                             Name       = "PUBLIC REVENUES BY DEDUCTION (TAXES AND CONTRIBUTIONS)",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode257 = new()
                                                                         {
                                                                             Id         = Guid.Parse("564a6630-cb9c-48f2-9e59-20ec7373580b"),
                                                                             Code       = "257",
                                                                             Name       = "REFUND OF OVERPAID OR INCORRECTLY COLLECTED CURRENT REVENUES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode258 = new()
                                                                         {
                                                                             Id         = Guid.Parse("3ecbe85a-f242-4d37-8995-8d37c418c008"),
                                                                             Code       = "258",
                                                                             Name       = "RECLASSIFICATION OF OVERPAID OR INCORRECTLY PAID CURRENT REVENUE",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode260 = new()
                                                                         {
                                                                             Id         = Guid.Parse("470d6ddb-97a1-4756-bbe4-711dafcc4833"),
                                                                             Code       = "260",
                                                                             Name       = "INSURANCE PREMIUMS AND COMPENSATION FOR DAMAGES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode261 = new()
                                                                         {
                                                                             Id         = Guid.Parse("82311288-6052-4bec-ab12-c3c1c3756983"),
                                                                             Code       = "261",
                                                                             Name       = "ALLOCATION OF CURRENT REVENUES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode262 = new()
                                                                         {
                                                                             Id         = Guid.Parse("58d5fe22-8b79-45a2-86cd-2b41bf2124c8"),
                                                                             Code       = "262",
                                                                             Name       = "TRANSFERS WITHIN STATE BODIES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode263 = new()
                                                                         {
                                                                             Id         = Guid.Parse("8844fb4b-32a1-4ac2-bdf4-56304eeccee1"),
                                                                             Code       = "263",
                                                                             Name       = "OTHER TRANSFERS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode264 = new()
                                                                         {
                                                                             Id         = Guid.Parse("014256af-f470-483d-a0a2-17219459d5c3"),
                                                                             Code       = "264",
                                                                             Name       = "BUDGET TRANSFERS FOR REFUNDS OF OVERPAID REVENUES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode265 = new()
                                                                         {
                                                                             Id         = Guid.Parse("e6b716ad-a74a-488e-84ee-b00611034801"),
                                                                             Code       = "265",
                                                                             Name       = "DAILY SALES DEPOSITS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode266 = new()
                                                                         {
                                                                             Id         = Guid.Parse("9d13614f-0173-449c-8f7f-1d04fdb9b08e"),
                                                                             Code       = "266",
                                                                             Name       = "CASH WITHDRAWALS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode270 = new()
                                                                         {
                                                                             Id         = Guid.Parse("6ee9a34c-e9fb-4f5a-bbfb-c55c0eb70947"),
                                                                             Code       = "270",
                                                                             Name       = "SHORT-TERM LOANS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode271 = new()
                                                                         {
                                                                             Id         = Guid.Parse("38259d40-8fc1-4f3d-bc4d-02b8a0283400"),
                                                                             Code       = "271",
                                                                             Name       = "LONG-TERM LOANS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode272 = new()
                                                                         {
                                                                             Id         = Guid.Parse("a8bdc718-8a1f-4956-8a35-73178cbe604c"),
                                                                             Code       = "272",
                                                                             Name       = "ACTIVE INTEREST",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode273 = new()
                                                                         {
                                                                             Id         = Guid.Parse("16b16ff3-b299-41d1-a107-342c5aa2a303"),
                                                                             Code       = "273",
                                                                             Name       = "TERM DEPOSITS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode278 = new()
                                                                         {
                                                                             Id         = Guid.Parse("a742265c-ea57-4dde-9ee1-f79339b5286a"),
                                                                             Code       = "278",
                                                                             Name       = "REFUND OF TERM DEPOSITS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode275 = new()
                                                                         {
                                                                             Id         = Guid.Parse("b17e15de-2550-4468-acd5-4ea4d9041edd"),
                                                                             Code       = "275",
                                                                             Name       = "OTHER PLACEMENTS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode276 = new()
                                                                         {
                                                                             Id         = Guid.Parse("596adadb-81a8-4b2c-b76d-af788a83a039"),
                                                                             Code       = "276",
                                                                             Name       = "REPAYMENT OF SHORT-TERM LOANS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode277 = new()
                                                                         {
                                                                             Id         = Guid.Parse("be65a39f-ba05-4a8a-88b6-850f83a3942f"),
                                                                             Code       = "277",
                                                                             Name       = "REPAYMENT OF LONG-TERM LOANS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode279 = new()
                                                                         {
                                                                             Id         = Guid.Parse("4fc68604-f47e-45cb-b443-0e0014913f32"),
                                                                             Code       = "279",
                                                                             Name       = "PASSIVE INTEREST",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode280 = new()
                                                                         {
                                                                             Id         = Guid.Parse("46e21e62-08bc-4e9d-9279-d5817602a232"),
                                                                             Code       = "280",
                                                                             Name       = "SECURITIES",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode281 = new()
                                                                         {
                                                                             Id         = Guid.Parse("bb73698c-c8ed-4044-b9c4-3a61716ea6f0"),
                                                                             Code       = "281",
                                                                             Name       = "FOUNDER LOANS FOR LIQUIDITY",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode282 = new()
                                                                         {
                                                                             Id         = Guid.Parse("13b01076-ce3e-49ae-b694-ab5bd975f783"),
                                                                             Code       = "282",
                                                                             Name       = "REFUND OF FOUNDER LOANS FOR LIQUIDITY",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode283 = new()
                                                                         {
                                                                             Id         = Guid.Parse("aa2197be-88c9-4d84-84f8-c7e1c7750977"),
                                                                             Code       = "283",
                                                                             Name       = "CITIZENS' CHECKS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode284 = new()
                                                                         {
                                                                             Id         = Guid.Parse("bdf63630-360a-46bf-87b8-d5731cb03349"),
                                                                             Code       = "284",
                                                                             Name       = "PAYMENT CARDS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode285 = new()
                                                                         {
                                                                             Id         = Guid.Parse("2bc19a27-9737-4e27-80b0-d42200e0bb8b"),
                                                                             Code       = "285",
                                                                             Name       = "CURRENCY EXCHANGE TRANSACTIONS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode286 = new()
                                                                         {
                                                                             Id         = Guid.Parse("58157119-5a70-43de-b242-0b6a5fc9c490"),
                                                                             Code       = "286",
                                                                             Name       = "FOREIGN EXCHANGE TRANSACTIONS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode287 = new()
                                                                         {
                                                                             Id         = Guid.Parse("f26250c3-130f-4b5c-8156-9f150b0b1765"),
                                                                             Code       = "287",
                                                                             Name       = "DONATIONS / SPONSORSHIPS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode288 = new()
                                                                         {
                                                                             Id         = Guid.Parse("c3da3111-db6e-4677-bf8e-99db757e4157"),
                                                                             Code       = "288",
                                                                             Name       = "DONATIONS FROM INTERNATIONAL AGREEMENTS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode289 = new()
                                                                         {
                                                                             Id         = Guid.Parse("2b170aad-9572-498d-a92a-63cb58ea59c3"),
                                                                             Code       = "289",
                                                                             Name       = "TRANSACTIONS AT CITIZENS' REQUEST",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly TransactionCodeModel TransactionCode290 = new()
                                                                         {
                                                                             Id         = Guid.Parse("ce80851d-9af6-4124-a5d1-d6ed587d8f78"),
                                                                             Code       = "290",
                                                                             Name       = "OTHER TRANSACTIONS",
                                                                             CreatedAt  = DateTime.UtcNow,
                                                                             ModifiedAt = DateTime.UtcNow
                                                                         };

        public static readonly ImmutableArray<TransactionCodeModel> All =
        [
            TransactionCode220, TransactionCode221, TransactionCode222, TransactionCode223, TransactionCode224,
            TransactionCode225, TransactionCode226, TransactionCode227, TransactionCode228, TransactionCode231,
            TransactionCode240, TransactionCode241, TransactionCode242, TransactionCode244, TransactionCode245,
            TransactionCode246, TransactionCode247, TransactionCode248, TransactionCode249, TransactionCode253,
            TransactionCode254, TransactionCode257, TransactionCode258, TransactionCode260, TransactionCode261,
            TransactionCode262, TransactionCode263, TransactionCode264, TransactionCode265, TransactionCode266,
            TransactionCode270, TransactionCode271, TransactionCode272, TransactionCode273, TransactionCode275,
            TransactionCode276, TransactionCode277, TransactionCode278, TransactionCode279, TransactionCode280,
            TransactionCode281, TransactionCode282, TransactionCode283, TransactionCode284, TransactionCode285,
            TransactionCode286, TransactionCode287, TransactionCode288, TransactionCode289, TransactionCode290
        ];
    }
}
