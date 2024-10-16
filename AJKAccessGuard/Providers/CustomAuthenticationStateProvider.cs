using AJKAccessGuard.Services;
using System.Security.Claims;

namespace AJKAccessGuard.Providers;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IUserStorageService _userStorageService;

    public CustomAuthenticationStateProvider(IUserStorageService userStorageService)
    {
        _userStorageService = userStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _userStorageService.GetTokenAsync();
        var storedUser = await _userStorageService.GetUserAsync();
        ClaimsIdentity identity;

        if (!string.IsNullOrEmpty(token))
        {
            // Extract claims from the JWT token
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims;

            identity = new ClaimsIdentity(claims, "apiauth_type");
            identity.AddClaim(new Claim(ClaimTypes.Name, storedUser.UserName ?? string.Empty));
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        var user = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(user));
    }

    public async Task NotifyUserAuthenticationAsync(string token)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var storedUser = await _userStorageService.GetUserAsync();
        var claims = jwtToken.Claims;
        claims = claims.Append(new Claim(ClaimTypes.Name, storedUser.UserName ?? string.Empty));

        var identity = new ClaimsIdentity(claims, "apiauth_type");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogoutAsync()
    {
        _userStorageService.ClearStorage();
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}