using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;

public interface ICloudService
{
    Task<string> UploadProfileImageAsync(IFormFile file);
}
