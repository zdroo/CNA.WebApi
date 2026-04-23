namespace CNA.Application.Services;

public record GoogleUserInfo(string GoogleId, string Email, string FirstName, string LastName);

public interface IGoogleAuthService
{
    Task<GoogleUserInfo> ValidateTokenAsync(string idToken);
}
