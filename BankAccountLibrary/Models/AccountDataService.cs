using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BankAccountLibrary.Models
{
    public class AccountDataService
    {
        private static readonly string _filePath = "account.json";
        private static readonly string _seedPath = "accountSeed.txt";

        public static int LoadAccountNumberSeed()
        {
            if (File.Exists(_seedPath))
            {
                string content = File.ReadAllText(_seedPath);
                if (int.TryParse(content, out int seed))
                {
                    return seed;
                }
            }
            return 10000000;
        }

        public static List<BankAccount> LoadAccounts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<BankAccount>();
            }
            string json = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };
            try
            {
                List<BankAccount> accounts = JsonSerializer.Deserialize<List<BankAccount>>(json, options) ?? new List<BankAccount>();
                if (accounts == null)
                {
                    return new List<BankAccount>();
                }
                return accounts;
            }
            catch (JsonException)
            {
                return new List<BankAccount>();
            }
        }

        public static void SaveAccountNumberSeed(int seed)
        {
            File.WriteAllText(_seedPath, seed.ToString());
        }

        public static void SaveAccounts(List<BankAccount> accounts)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null
            };
            string json = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(_filePath, json);
        }
    }
}