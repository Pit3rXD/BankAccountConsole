using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;

namespace WpfBankAccount.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private string? _token;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BankAccountDto> CreateAccountAsync(BankAccountDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "aplication/json");
            var response = await _httpClient.PostAsync("api/bankaccount", content);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var createResponse = JsonSerializer.Deserialize<BankAccountDto>(body);

            if (createResponse == null)
            {
                throw new Exception("Invalid response from server");
            }
            return createResponse;
        }

        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest registerRequest, int bankAccountId)
        {
            var json = JsonSerializer.Serialize(registerRequest);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "aplication/json");
            var response = await _httpClient.PostAsync($"api/bankaccount/{bankAccountId}/transaction", content);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var createResponse = JsonSerializer.Deserialize<TransactionResponse>(body);

            if (createResponse == null)
            {
                throw new Exception("Invalid response from server");
            }
            return createResponse;
        }

        public Task<IEnumerable<TransactionResponse>> GetAllByAccountIdAsync(int bankAccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<BankAccountDto?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/bankaccount/{id}");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var getByIdResponse = JsonSerializer.Deserialize<BankAccountDto?>(body);
            if (getByIdResponse == null)
            {
                throw new Exception("Invalid response from server");
            }
            return getByIdResponse;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(body);

            if (loginResponse == null)
            {
                throw new Exception("Invalid response from sever");
            }
            _token = loginResponse.Token;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);

            return loginResponse;
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var json = JsonSerializer.Serialize(registerRequest);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "aplication/json");
            var response = await _httpClient.PostAsync("api/auth/register", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<BankAccountDto> UpdateAsync(BankAccountDto dto)
        {
            //PUT
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "aplication/json");
            var response = await _httpClient.PutAsync($"api/bankaccount/{dto.Id}", content);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var updateResponse = JsonSerializer.Deserialize<BankAccountDto>(body);

            if (updateResponse == null)
            {
                throw new Exception("Invalid response from server");
            }
            return updateResponse;
        }
    }
}
