using System.Security.Cryptography;

namespace TourismAgencyAPI.Services;

public class HashService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000; 

    public string Hash(string value)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            value, salt, Iterations, HashAlgorithmName.SHA512, HashSize
        );

        return $"{Convert.ToHexString(salt)}${Convert.ToHexString(hash)}";
    }

    public bool Matches(string value, string hashedValue)
    {
        var hashParts = hashedValue.Split('$');
        if (hashParts.Length != 2) return false;

        if (hashParts[0].Length % 2 != 0 || hashParts[1].Length % 2 != 0)
        {
            return false; 
        }

        try
        {
            var salt = Convert.FromHexString(hashParts[0]);
            var storedHash = Convert.FromHexString(hashParts[1]);

            var computedHash = Rfc2898DeriveBytes.Pbkdf2(
                value, salt, Iterations, HashAlgorithmName.SHA512, HashSize
            );

            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
        catch (FormatException)
        {
            return false; 
        }
    }
}