using BankAccountCore;
using System.Globalization;
using System.Windows.Data;

namespace WpfBankAccount.Converters
{
    public class TransactionDirectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionType type = (TransactionType)values[0];
            string counterpartyAccountNumber = (string)values[1];

            if(type == TransactionType.TransferIn)
            {
                return $"From: {counterpartyAccountNumber}";
            }
            if(type == TransactionType.TransferOut)
            {
                return $"To: {counterpartyAccountNumber}";
            }
            else
            {
                return "-";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
