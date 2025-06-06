using System.Net.Http.Json;

using Bank.Application.Requests;
using Bank.Link.Requests;

namespace Bank.Link.Mapper.B3.Content;

internal static class TransactionMapper
{
    internal static Request.B3.TransactionCreateRequest ToB3(this TransactionCreateRequest request, string paymentCode)
    {
        return new Request.B3.TransactionCreateRequest
               {
                   SenderAccountNumber   = request.FromAccountNumber!,
                   ReceiverAccountNumber = request.ToAccountNumber!,
                   Amount                = request.Amount,
                   PaymentCode           = paymentCode,
                   ReferenceNumber       = request.ReferenceNumber,
                   PurposeOfPayment      = request.Purpose,
                   CallbackId            = 0,
                   ExternalTransactionId = (request.ExternalTransactionId is Guid id ? id : Guid.Empty).ToString(),
               };
    }

    internal static Request.B3.TransactionNotifyStatusRequest ToB3(this TransactionNotifyStatusRequest request)
    {
        return new Request.B3.TransactionNotifyStatusRequest
               {
                   Success = request.TransferSucceeded,
               };
    }

    internal static HttpContent ToContent(this Request.B3.TransactionCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    internal static HttpContent ToContent(this Request.B3.TransactionNotifyStatusRequest request)
    {
        return JsonContent.Create(request);
    }
}
