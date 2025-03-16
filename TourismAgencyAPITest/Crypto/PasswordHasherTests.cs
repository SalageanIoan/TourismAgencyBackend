using TourismAgencyAPI.Services;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TourismAgencyAPITest.Crypto;

public class HashServiceTests
{
    private readonly HashService _hashService;

    public HashServiceTests()
    {
        _hashService = new HashService();
    }

    [Fact]
    public void Hash_ShouldGenerateDifferentHashesForSameInput()
    {
        string password = "TestPassword123";

        string hash1 = _hashService.Hash(password);
        string hash2 = _hashService.Hash(password);

        Assert.False(hash1 == hash2, "Hashes should not be equal due to salt.");    }

    [Fact]
    public void Matches_ShouldReturnTrueForValidPassword()
    {
        string password = "TestPassword123";
        string hashedValue = _hashService.Hash(password);

        bool result = _hashService.Matches(password, hashedValue);

        Assert.True(result);
    }

    [Fact]
    public void Matches_ShouldReturnFalseForInvalidPassword()
    {
        string correctPassword = "TestPassword123";
        string wrongPassword = "WrongPassword456";
        string hashedValue = _hashService.Hash(correctPassword);

        bool result = _hashService.Matches(wrongPassword, hashedValue);

        Assert.False(result);
    }

    [Fact]
    public void Matches_ShouldReturnFalseForInvalidHashFormat()
    {
        string password = "TestPassword123";
        string invalidHashedValue = "Invalid$HashFormat";

        bool result = _hashService.Matches(password, invalidHashedValue);

        Assert.False(result);
    }
}