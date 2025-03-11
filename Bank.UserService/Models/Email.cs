namespace Bank.UserService.Models;

public class Email
{
    public required string Subject { set; get; }
    public required string Body    { set; get; }

    public string FormatBody(params object[] values)
    {
        var body = Body;

        for (int index = 0; index < values.Length; index++)
            body = body.Replace("{{" + index + "}}", values[index]
                                .ToString());

        return body;
    }
}
