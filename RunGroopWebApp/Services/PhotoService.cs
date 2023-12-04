using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloundinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var cloudinarySettings = config.Value;
        var account = new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret);

        _cloundinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };

            uploadResult = await _cloundinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhonoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloundinary.DestroyAsync(deleteParams);

        return result;
    }
}
