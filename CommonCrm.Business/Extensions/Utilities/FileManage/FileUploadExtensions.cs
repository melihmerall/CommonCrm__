using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CommonCrm.Business.Extensions.Utilities.FileManage;

public static class FileUploadExtensions
{
    public static async Task<string> SaveToWwwRootAsync(this IFormFile file, IWebHostEnvironment webHostEnvironment, string productName, string subPath = null)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentNullException(nameof(file), "Dosya boş olamaz.");

        if (webHostEnvironment == null)
            throw new ArgumentNullException(nameof(webHostEnvironment), "WebHostEnvironment null olamaz.");

        var wwwRootPath = webHostEnvironment.WebRootPath;
        var uploadPath = string.IsNullOrEmpty(subPath) ? Path.Combine(wwwRootPath, "Images") : Path.Combine(wwwRootPath, "Images", subPath);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = $"{productName}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(subPath ?? "", fileName).Replace("\\", "/");
    }
}