using System.IO;
using System.Threading.Tasks;

namespace BartugWeb.ApplicationLayer.Abstracts.IServices;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteFileAsync(string fileName);
}