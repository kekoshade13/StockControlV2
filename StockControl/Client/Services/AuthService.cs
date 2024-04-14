using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using StockControl.Client.Authentication;
using StockControl.Shared.ModelsDto.RequestModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StockControl.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResponse> Register(RegisterRequest registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/register", registerModel);
            if (!result.IsSuccessStatusCode)
            {
                return new RegisterResponse { Successful = false, Errors = new List<string> { "Ocurrió un error" } };
            }
            else
            {
                return new RegisterResponse { Successful = true };
            }
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", loginModel);
            var loginResult = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            if (response != null && loginResult != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsStringAsync("AuthToken", loginResult.Token);
                    await _localStorage.SetItemAsStringAsync("AuthRefreshToken", loginResult.RefreshToken);
                    await _localStorage.SetItemAsStringAsync("AuthExpiration", loginResult.Expiration.ToString());
                    ((AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
                    return loginResult;
                }
            }
            return loginResult ?? new() { IsAuthSuccessful = false };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("AuthToken");
            await _localStorage.RemoveItemAsync("AuthRefreshToken");
            await _localStorage.RemoveItemAsync("AuthExpiration");
            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
