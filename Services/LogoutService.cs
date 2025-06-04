using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorServerApp.Services
{
    public class LogoutService
    {
        private readonly NavigationManager _navigation;
        private readonly AuthenticationStateProvider _authStateProvider;

        public LogoutService(NavigationManager navigation, AuthenticationStateProvider authStateProvider)
        {
            _navigation = navigation;
            _authStateProvider = authStateProvider;
        }

        public async Task RedirectIfNotAuthenticatedAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                _navigation.NavigateTo("/", forceLoad: true);
            }
        }
    }
}
