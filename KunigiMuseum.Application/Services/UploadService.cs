using System.Security.Cryptography;
using System.Text;
using KunigiMuseum.Application.Data;
using Microsoft.Extensions.Configuration;

namespace KunigiMuseum.Application.Services;

public interface IUploadService
{
    void CreateFolder(string path);
}

public class UploadService : IUploadService
{
    private readonly string _mediaPath;
    private readonly DataContext _context;

    public UploadService(IConfiguration configuration, DataContext context)
    {
        _mediaPath = configuration["UploadPath"]!;
        _context = context;
    }

    public void CreateFolder(string path)
    {
        var fullPath = NormalizeAndCombinePaths(_mediaPath, path);
        Directory.CreateDirectory(fullPath);
    }

    private static string NormalizeAndCombinePaths(string basePath, string relativePath)
    {
        basePath = basePath.Replace('\\', '/');
        relativePath = relativePath.Replace('\\', '/');
        var combinedPath = Path.Combine(basePath, relativePath);

        return Path.GetFullPath(combinedPath);
    }

    private static string GetUniqueFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var uniqueHash = GenerateUniqueFileHash(fileName);
        return $"{uniqueHash}{extension}";
    }

    private static string GenerateUniqueFileHash(string fileName)
    {
        var input = $"{fileName}_{DateTime.UtcNow.Ticks}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));

        var builder = new StringBuilder();
        foreach (var t in hashBytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString()[..16];
    }
}