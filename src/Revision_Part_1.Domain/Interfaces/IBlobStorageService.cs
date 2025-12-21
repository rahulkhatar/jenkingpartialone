namespace Revision_Part_1.Domain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream data, string fileName);
    string? GetBlobSasUrl(string? blobUrl);
}
