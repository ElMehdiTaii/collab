namespace Collaboration.Application.Contracts.HashingPassword;

public interface IPasswordHasherService
{
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
}
