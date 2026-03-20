using System.Text.Json;

namespace BankAccountCore
{
    public class AccountDataService
    {
        private static readonly string _seedPath = "accountSeed.txt";
        public void SaveAccountNumberSeed(int seed)
        {
            File.WriteAllText(_seedPath, seed.ToString());
        }
        public int LoadAccountNumberSeed()
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
    }
}
