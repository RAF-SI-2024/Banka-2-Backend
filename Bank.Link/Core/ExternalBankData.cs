namespace Bank.Link.Core;

public class BankData(string code = "", string baseUrl = "")
{
    public string Code    { set; get; } = code;
    public string BaseUrl { set; get; } = baseUrl;
}
