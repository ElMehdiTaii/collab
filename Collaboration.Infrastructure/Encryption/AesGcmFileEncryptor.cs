using Collaboration.Application.Contracts.Encryption;
using System.Security.Cryptography;

namespace Collaboration.Infrastructure.Encryption;

public class AesGcmFileEncryptor : IFileEncryptor
{
    public byte[] EncryptFile(string filePath, byte[] key, out byte[] iv, out byte[] tag)
    {
        ArgumentNullException.ThrowIfNull(filePath, nameof(filePath));
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        iv = GenerateRandomBytes(12);
        var fileContent = File.ReadAllBytes(filePath);
        var cipherText = new byte[fileContent.Length];
        tag = new byte[16];

        using var aesGcm = new AesGcm(key, tag.Length * 8);
        aesGcm.Encrypt(iv, fileContent, cipherText, tag);

        return cipherText;
    }

    public void DecryptFile(byte[] encryptedData, byte[] key, byte[] iv, byte[] tag, string outputFilePath)
    {
        ArgumentNullException.ThrowIfNull(encryptedData, nameof(encryptedData));
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        ArgumentNullException.ThrowIfNull(iv, nameof(iv));
        ArgumentNullException.ThrowIfNull(tag, nameof(tag));
        ArgumentNullException.ThrowIfNull(outputFilePath, nameof(outputFilePath));

        var plainText = new byte[encryptedData.Length];

        using var aesGcm = new AesGcm(key, tag.Length * 8);
        aesGcm.Decrypt(iv, encryptedData, tag, plainText);

        File.WriteAllBytes(outputFilePath, plainText);
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        var randomBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}
