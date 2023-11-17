using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using StockControl.Shared.RequestModels;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace StockControl.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public static readonly string TOKEN = "StockControlToken";
        public static readonly string REFRESHTOKEN = "StockControlRefreshToken";
        public static readonly string EXPIRATION = "StockControlExpiration";

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage) 
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> IsAnonymous()
        {
            return string.IsNullOrWhiteSpace(await _localStorage.GetItemAsStringAsync(TOKEN));
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            string content = JsonSerializer.Serialize(request);
            StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
            HttpResponseMessage authResult = await _client.PostAsync("api/account/login", bodyContent);

            if (authResult.IsSuccessStatusCode)
            {
                AuthenticationResponse result = await authResult.Content.ReadFromJsonAsync<AuthenticationResponse>();
                await _localStorage.SetItemAsStringAsync(TOKEN, result!.Token);
                await _localStorage.SetItemAsStringAsync(REFRESHTOKEN, result!.RefreshToken);
                await _localStorage.SetItemAsStringAsync(EXPIRATION, result!.Expiration.ToString("yyyy-MM-dd HH:mm:ss"));

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new AuthenticationResponse { IsAuthSuccessful = true };
            }
            else
            {
                var response = new AuthenticationResponse { IsAuthSuccessful = false };
                switch (authResult.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        response.ErrorMessage = "IncorrectUsernameOrPassword";
                        break;
                    default:
                        response.ErrorMessage = "ThereWasAProblemCompletingtheRequest";
                        break;
                }
                return response;
            }
        }

        public async Task Logout()
        {
            _ = await _client.PostAsync("api/account/logout", null);
            await RemoveTokens();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task RemoveTokens()
        {
            await _localStorage.RemoveItemAsync(TOKEN);
            await _localStorage.RemoveItemAsync(REFRESHTOKEN);
            await _localStorage.RemoveItemAsync(EXPIRATION);
            _client.DefaultRequestHeaders.Remove("Authorization");
        }

        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
