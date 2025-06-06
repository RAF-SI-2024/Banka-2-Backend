namespace Bank.Link.Requests;

internal static partial class Request
{
    internal static class B3
    {
        internal class TransactionCreateRequest
        {
            public required string  SenderAccountNumber   { set; get; }
            public required string  ReceiverAccountNumber { set; get; }
            public required decimal Amount                { set; get; }
            public required string  PaymentCode           { set; get; }
            public          string? ReferenceNumber       { set; get; }
            public required string  PurposeOfPayment      { set; get; }
            public          long    CallbackId            { set; get; }
            public          string? ExternalTransactionId { set; get; }
        }

        internal class TransactionNotifyStatusRequest
        {
            public required bool Success { set; get; }
        }
    }
}
