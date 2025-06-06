using Bank.Application.Domain;

using MessagePack;

namespace Bank.ExchangeService.Models;

public class Order
{
    public required Guid        Id            { set; get; }
    public required Guid        ActuaryId     { set; get; }
    public          Guid?       SupervisorId  { set; get; }
    public required Guid        AccountId     { set; get; }
    public required Guid        SecurityId    { set; get; }
    public          Security?   Security      { set; get; }
    public required OrderType   OrderType     { set; get; }
    public required bool        AllOrNone     { set; get; }
    public required int         Quantity      { set; get; }
    public required int         ContractCount { set; get; }
    public required decimal     LimitPrice    { set; get; }
    public required decimal     StopPrice     { set; get; }
    public required Direction   Direction     { set; get; }
    public required OrderStatus Status        { set; get; }
    public required DateTime    CreatedAt     { set; get; }
    public required DateTime    ModifiedAt    { set; get; }
}

[MessagePackObject]
public class RedisOrder
{
    [IgnoreMember]
    public Guid Id { set; get; }

    [IgnoreMember]
    public SecurityType SecurityType { set; get; }

    [Key(0)]
    public required Guid SecurityId { set; get; }

    [Key(1)]
    public required string Ticker { set; get; }

    [Key(2)]
    public required OrderType Type { set; get; }

    [Key(3)]
    public required Guid AccountId { get; set; }

    [Key(4)]
    public required int RemainingPortions { set; get; }

    [Key(5)]
    public required decimal LimitPrice { set; get; }

    [Key(6)]
    public required decimal StopPrice { set; get; }

    [Key(7)]
    public required Direction Direction { set; get; }

    [Key(8)]
    public required bool AllOrNone { set; get; }
}
