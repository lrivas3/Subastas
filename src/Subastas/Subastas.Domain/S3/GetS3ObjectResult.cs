namespace Subastas.Domain.S3;

public class GetS3ObjectResult
{
    public byte[] Data { get; set; }
    public string ContentType { get; set; }
}