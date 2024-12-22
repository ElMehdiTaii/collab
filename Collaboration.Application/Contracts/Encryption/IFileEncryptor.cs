namespace Collaboration.Application.Contracts.Encryption;

public interface IFileEncryptor
{
    byte[] EncryptFile(string filePath, byte[] key, out byte[] iv, out byte[] tag);
    void DecryptFile(byte[] encryptedData, byte[] key, byte[] iv, byte[] tag, string outputFilePath);
}
