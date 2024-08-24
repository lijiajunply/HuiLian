using System.Security.Claims;
using HuiLianMedical.Share.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace HuilianMedical.Backend.Models;

public class Provider(ProtectedSessionStorage sessionStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var storageResult = await sessionStorage.GetAsync<UserModel>("Permission");
        var permission = storageResult.Success ? storageResult.Value : null;
        if (permission == null)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new(ClaimTypes.Name, permission.UserName),
            new(ClaimTypes.Role, permission.Identity),
            new (ClaimTypes.NameIdentifier,permission.Id)
        }, "Auth"));
            
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthState(UserModel? permission)
    {
        ClaimsPrincipal claimsPrincipal;
        if (permission is not null)
        {
            await sessionStorage.SetAsync("Permission", permission);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, permission.UserName),
                new(ClaimTypes.Role, permission.Identity),
                new (ClaimTypes.NameIdentifier,permission.Id)
            }));
        }
        else
        {
            await sessionStorage.DeleteAsync("Permission");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
    
}

public static class ProviderExtensions
{
    public static async Task Logout(this Provider provider)
    {
        await provider.UpdateAuthState(null);
    }
}