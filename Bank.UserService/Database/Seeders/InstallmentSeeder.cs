﻿using System.Collections.Immutable;

using Bank.Application.Domain;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using InstallmentModel = Installment;

public static partial class Seeder
{
    public static class Installment
    {
        public static readonly InstallmentModel PersonalLoanInstallment1 = new()
                                                                           {
                                                                               Id              = Guid.Parse("3a0a1c1c-9f8b-4f1a-8c4d-f8c3a1e5b4c7"),
                                                                               LoanId          = Guid.Parse("f5a74113-8f10-42a3-b130-54c5c691ba8e"), // PersonalLoan1.Id
                                                                               InterestRate    = 5.75m,
                                                                               ExpectedDueDate = DateTime.UtcNow.AddDays(-1), // Yesterday - overdue
                                                                               ActualDueDate   = DateTime.UtcNow.AddDays(-1), // Set to past date
                                                                               Status          = InstallmentStatus.Pending,
                                                                               CreatedAt       = DateTime.UtcNow.AddDays(-30),
                                                                               ModifiedAt      = DateTime.UtcNow.AddDays(-30)
                                                                           };

        public static readonly InstallmentModel MortgageLoanInstallment1 = new()
                                                                           {
                                                                               Id              = Guid.Parse("2b5e4f7d-6c8a-4b9d-8e5f-1a2b3c4d5e6f"),
                                                                               LoanId          = Guid.Parse("47782a0e-e03c-4b91-8228-d6fd5a6906f1"), // MortgageLoan1.Id
                                                                               InterestRate    = 3.25m,
                                                                               ExpectedDueDate = DateTime.UtcNow, // Due today
                                                                               ActualDueDate   = DateTime.UtcNow, // Set to today
                                                                               Status          = InstallmentStatus.Pending,
                                                                               CreatedAt       = DateTime.UtcNow.AddDays(-30),
                                                                               ModifiedAt      = DateTime.UtcNow.AddDays(-30)
                                                                           };

        public static readonly InstallmentModel AutoLoanInstallment1 = new()
                                                                       {
                                                                           Id              = Guid.Parse("7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d"),
                                                                           LoanId          = Guid.Parse("b99e8f17-2b07-4742-906f-df62092652d3"), // AutoLoan1.Id
                                                                           InterestRate    = 4.50m,
                                                                           ExpectedDueDate = DateTime.UtcNow.AddDays(-2), // 2 days ago - overdue
                                                                           ActualDueDate   = DateTime.UtcNow.AddDays(-2), // Set to past date
                                                                           Status          = InstallmentStatus.Pending,
                                                                           CreatedAt       = DateTime.UtcNow.AddDays(-32),
                                                                           ModifiedAt      = DateTime.UtcNow.AddDays(-32)
                                                                       };

        public static readonly InstallmentModel BusinessLoanInstallment1 = new()
                                                                           {
                                                                               Id              = Guid.Parse("9c8d7e6f-5a4b-3c2d-1e0f-9a8b7c6d5e4f"),
                                                                               LoanId          = Guid.Parse("3e37c493-2efb-4312-b76a-c160636b3dc8"), // BusinessLoan1.Id
                                                                               InterestRate    = 6.25m,
                                                                               ExpectedDueDate = DateTime.UtcNow, // Due today
                                                                               ActualDueDate   = DateTime.UtcNow, // Set to today
                                                                               Status          = InstallmentStatus.Pending,
                                                                               CreatedAt       = DateTime.UtcNow.AddDays(-30),
                                                                               ModifiedAt      = DateTime.UtcNow.AddDays(-30)
                                                                           };

        public static readonly InstallmentModel StudentLoanInstallment1 = new()
                                                                          {
                                                                              Id              = Guid.Parse("1d2e3f4a-5b6c-7d8e-9f0a-1b2c3d4e5f6a"),
                                                                              LoanId          = Guid.Parse("c3e68735-957f-4e06-ac74-fd8f2fac466b"), // StudentLoan1.Id
                                                                              InterestRate    = 2.75m,
                                                                              ExpectedDueDate = DateTime.UtcNow.AddDays(-3), // 3 days ago - overdue
                                                                              ActualDueDate   = DateTime.UtcNow.AddDays(-3), // Set to past date
                                                                              Status          = InstallmentStatus.Pending,
                                                                              CreatedAt       = DateTime.UtcNow.AddDays(-33),
                                                                              ModifiedAt      = DateTime.UtcNow.AddDays(-33)
                                                                          };

        public static readonly ImmutableArray<InstallmentModel> All =
        [
            PersonalLoanInstallment1, MortgageLoanInstallment1, AutoLoanInstallment1, BusinessLoanInstallment1,
            StudentLoanInstallment1
        ];
    }
}