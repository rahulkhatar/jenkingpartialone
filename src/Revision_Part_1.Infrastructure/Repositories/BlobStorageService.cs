using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Revision_Part_1.Domain.Interfaces;
using Revision_Part_1.Infrastructure.Helper;

namespace Revision_Part_1.Infrastructure.Repositories;

public class BlobStorageService(IOptions<BlobStorageSettings> _blobStorageSettingsOption) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = _blobStorageSettingsOption.Value;
    public async Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionStrings);
        
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(data);

        var blobUrl = blobClient.Uri.ToString();
        return blobUrl;
    }

    public string GetBlobSasUrl(string? blobUrl)
    {
        var blobUri = new Uri(blobUrl!);
        var blobName = Uri.UnescapeDataString(blobUri.Segments.Last());

        //if (blobUrl == null) return "";

        var sasBuilder = new BlobSasBuilder()
        {
            Version = "2022-11-02",
            BlobContainerName = _blobStorageSettings.ContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
            //BlobName = GetBlobNameFromUrl(blobUrl)
            BlobName = blobName,
            
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionStrings);

        var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey)).ToString();

        return $"{blobUrl}?{sasToken}";
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var url = new Uri(blobUrl);
        return url.Segments.Last();
    }
}
