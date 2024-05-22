using Subastas.Dto.S3;

namespace Subastas.Interfaces.Services;

public interface IS3StorageService
{
    string GetUrlForObject(S3File s3File, AwsCredentials awsCredentials);
    Task<S3ResponseDto> UploadObjectAsync(S3Object s3Object, AwsCredentials awsCredentials);
    Task<S3ResponseDto> DeleteObjectAsync(AwsCredentials awsCredentials, S3File s3File);
    Task<GetS3ObjectResult> GetObjectAsync(AwsCredentials awsCredentials, S3File s3File);
}