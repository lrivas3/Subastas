﻿using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Subastas.Domain.S3;
using Subastas.Interfaces.Services;
using S3Object = Subastas.Domain.S3.S3Object;

namespace Subastas.Services;
public class S3StorageService : IS3StorageService
{
    private readonly AmazonS3Config _config;
    
    public S3StorageService()
    {
        // Specify region
        _config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1
        };
    }

    public async Task<S3ResponseDto> UploadObjectAsync(S3Object s3Object, AwsCredentials awsCredentials)
    {
        using var client = CreateAmazonS3Client(awsCredentials);

        var response = new S3ResponseDto();
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = s3Object.InputStream,
                Key = s3Object.Name,
                BucketName = s3Object.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            var transferUtility = new TransferUtility(client);
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Message = $"File {s3Object.Name} uploaded successfully.";
        }
        catch (AmazonS3Exception e)
        {
            response.StatusCode = (int)e.StatusCode;
            response.Message = e.Message;
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<S3ResponseDto> DeleteObjectAsync(AwsCredentials awsCredentials, S3File s3File)
    {
        using var client = CreateAmazonS3Client(awsCredentials);
        return await DeleteObjectAsync(client, s3File);
    }

    private AmazonS3Client CreateAmazonS3Client(AwsCredentials awsCredentials)
    {
        var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);
        return new AmazonS3Client(credentials, _config);
    }

    private static async Task<S3ResponseDto> DeleteObjectAsync(IAmazonS3 client, S3File s3File)
    {
        var result = new S3ResponseDto();
        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = s3File.BucketName,
                Key = s3File.KeyName
            };

            await client.DeleteObjectAsync(deleteObjectRequest);
            result.Message = $"Successfully deleted {s3File.KeyName}";
            result.StatusCode = 200;
        }
        catch (AmazonS3Exception ex)
        {
            result.Message = $"Error encountered on server. Message:'{ex.Message}' when deleting an object.";
            result.StatusCode = (int)ex.StatusCode;
        }
        catch (Exception e)
        {
            result.Message = $"Error encountered on server. Message:'{e.Message}' when deleting an object.";
            result.StatusCode = 500;
        }

        return result;
    }
    
    /// <summary>
    /// Retrieves an object from an Amazon S3 bucket.
    /// </summary>
    /// <param name="awsCredentials">An initialized Amazon S3 credentials object.</param>
    /// <param name="s3File">The name and bucket of the object to retrieve.</param>
    /// <returns>A stream containing the object's data.</returns>
    public async Task<GetS3ObjectResult> GetObjectAsync(AwsCredentials awsCredentials, S3File s3File)
        {
            try
            {
                using var client = CreateAmazonS3Client(awsCredentials);
                var request = new GetObjectRequest
                {
                    BucketName = s3File.BucketName,
                    Key = s3File.KeyName,
                };

                // Issue request and get the response
                using var response = await client.GetObjectAsync(request);
                using var responseStream = new MemoryStream();
                await response.ResponseStream.CopyToAsync(responseStream);
                
                var result = new GetS3ObjectResult()
                {
                    Data = responseStream.ToArray(),
                    ContentType = response.Headers["Content-Type"] 
                };

                return result;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Amazon S3 Exception: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; 
            }
        }
}

