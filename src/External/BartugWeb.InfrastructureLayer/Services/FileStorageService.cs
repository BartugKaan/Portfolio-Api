using Amazon.S3;
using Amazon.S3.Model;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using Microsoft.Extensions.Configuration;

namespace BartugWeb.InfrastructureLayer.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _publicBaseUrl;


    public FileStorageService(IConfiguration configuration)
    {
        var endpoint = configuration["Storage:Endpoint"];
        var accessKeyId = configuration["Storage:AccessKeyId"];
        var secretAccessKey = configuration["Storage:SecretAccessKey"];
        _bucketName = configuration["Storage:BucketName"];
        _publicBaseUrl = configuration["Storage:PublicBaseUrl"];

        var s3Config = new AmazonS3Config()
        {
            ServiceURL = endpoint,
            AuthenticationRegion = "auto"
        };

        _s3Client = new AmazonS3Client(accessKeyId, secretAccessKey, s3Config);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            ContentType = contentType,
            InputStream = fileStream,
            CannedACL = S3CannedACL.PublicRead
        };
        await _s3Client.PutObjectAsync(putObjectRequest);
        return $"{_publicBaseUrl}/{fileName}"; 
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };
        await _s3Client.DeleteObjectAsync(deleteObjectRequest); 
    }
    
}