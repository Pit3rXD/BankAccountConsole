using System.Net.Http;
using System.Text.Json;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;
using System.Net.Http.Headers;

namespace WpfBankAccount.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private string? _token;
        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BankAccountDto> CreateAccountAsync(BankAccountDto dto)
        {
            var createResponse = await RequestAsync<BankAccountDto>(HttpMethod.Post, "api/bankaccount", dto);
            return createResponse;
        }

        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request, int bankAccountId)
        {
            var transactionRequest = await RequestAsync<TransactionResponse>(HttpMethod.Post,
                $"api/bankaccount/{bankAccountId}/transactions", request);
            return transactionRequest;
        }

        public async Task<TransferResponse> CreateTransferAsync(TransferRequest request, int bankAccountId)
        {
            var transferRequest = await RequestAsync<TransferResponse>(HttpMethod.Post,
                $"api/bankaccount/{bankAccountId}/transactions/transfer", request);
            return transferRequest;
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllByAccountIdAsync(int bankAccountId)
        {
            var getAll = await RequestAsync<IEnumerable<TransactionResponse>>(HttpMethod.Get, $"api/bankaccount/{bankAccountId}/transactions");
            return getAll;
        }

        public async Task<BankAccountDto?> GetByIdAsync(int id)
        {

            var getByIdResponse = await RequestAsync<BankAccountDto>(HttpMethod.Get, $"api/bankaccount/{id}");
            return getByIdResponse;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var loginResponse = await RequestAsync<LoginResponse>(HttpMethod.Post, "api/auth/login", loginRequest);
            _token = loginResponse.Token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return loginResponse;
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var json = JsonSerializer.Serialize(registerRequest, _jsonOptions);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseBody);
            }
        }

        public async Task<BankAccountDto> UpdateAsync(BankAccountDto dto)
        {
            var updateResponse = await RequestAsync<BankAccountDto>(HttpMethod.Put, $"api/bankaccount/{dto.Id}", dto);
            return updateResponse;
        }

        private async Task<T> RequestAsync<T>(HttpMethod method, string url, object? body = null)
        {
            var json = JsonSerializer.Serialize(body, _jsonOptions);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(method, url);
            if (body != null)
            {
                request.Content = content;
            }

            
            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseBody);
            }
            var updateResponse = JsonSerializer.Deserialize<T>(responseBody, _jsonOptions);
            if (updateResponse == null)
            {
                throw new Exception("Invalid response from server");
            }
            return updateResponse;
        }
    }
}
