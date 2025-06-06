using Bank.Application.Domain;

using MessagePack;

namespace Bank.ExchangeService.Models;

public class Quote
{
    public required Guid      Id                { get; set; }
    public required Guid      SecurityId        { get; set; }
    public          Security? Security          { get; set; }
    public required decimal   AskPrice          { get; set; }
    public required decimal   BidPrice          { get; set; }
    public required int       AskSize           { get; set; }
    public required int       BidSize           { get; set; }
    public required decimal   HighPrice         { get; set; }
    public required decimal   LowPrice          { get; set; }
    public required decimal   ClosePrice        { get; set; }
    public required decimal   OpeningPrice      { get; set; }
    public          decimal   ImpliedVolatility { get; set; }
    public required long      Volume            { get; set; }
    public required int       ContractCount     { get; set; }

    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}

public class DailyQuote
{
    public required decimal  HighPrice    { get; set; }
    public required decimal  LowPrice     { get; set; }
    public required decimal  ClosePrice   { get; set; }
    public required decimal  OpeningPrice { get; set; }
    public required long     Volume       { get; set; }
    public required DateTime Date         { get; set; }
}

[MessagePackObject]
public class RedisQuote
{
    [IgnoreMember]
    public SecurityType SecurityType { get; set; }

    [IgnoreMember]
    public string Ticker { get; set; } = string.Empty;

    [IgnoreMember]
    public DateTime Time { get; set; }

    [Key(0)]
    public required Guid Id { get; set; }

    [Key(1)]
    public required decimal AskPrice { get; set; }

    [Key(2)]
    public required decimal BidPrice { get; set; }

    [Key(3)]
    public required int AskSize { get; set; }

    [Key(4)]
    public required int BidSize { get; set; }

    [Key(5)]
    public required decimal HighPrice { get; set; }

    [Key(6)]
    public required decimal LowPrice { get; set; }

    [Key(7)]
    public required decimal ClosePrice { get; set; }

    [Key(8)]
    public required decimal OpeningPrice { get; set; }

    [Key(9)]
    public required decimal ImpliedVolatility { get; set; }

    [Key(10)]
    public required long Volume { get; set; }

    [Key(11)]
    public required int ContractCount { get; set; }
}
