namespace Product.Api.Dtos;

public class CustomResponse
{
    public bool Success { get; set; }

    public int Status { get; set; }

    public DateTimeOffset DateUtc { get; private set; }

    public object Data { get; set; }

    public string[] Messages { get; set; }

    public CustomResponse()
    {
        DateUtc = DateTimeOffset.UtcNow;
    }
}