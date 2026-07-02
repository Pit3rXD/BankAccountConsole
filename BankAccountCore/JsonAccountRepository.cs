using System.Text.Json;

namespace BankAccountCore
{
    public class JsonAccountRepository : IAccountRepository
    {
        private static readonly string _filePath = "account.json";

        public void Save(List<BankAccountDto> accounts)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null
            };
            string json = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(_filePath, json);
        }
        public List<BankAccountDto> Load()
        {
            if (!File.Exists(_filePath))
            {
                return  new List<BankAccountDto>();
            }
            string json = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };
            try
            {
                List<BankAccountDto> accounts = JsonSerializer.Deserialize<List<BankAccountDto>>(json, options);
                if (accounts == null)
                {
                    return new List<BankAccountDto>();
                }
                return accounts;
            }
            catch (JsonException)
            {
                return new List<BankAccountDto>();
            }
        }
    }
}
