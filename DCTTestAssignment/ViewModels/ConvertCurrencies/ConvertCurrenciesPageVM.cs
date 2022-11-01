using DCTTestAssignment.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels
{
    class ConvertCurrenciesPageVM : PageBaseVM
    {
        private IEnumerable<CurrencyVM>? currencies;
        public IEnumerable<CurrencyVM>? Currencies
        {
            get { return currencies; }
            set
            {
                currencies = value;
                OnPropertyChanged("Currencies");
            }
        }
        public CurrencyVM FromCurrency { get; set; }
        public CurrencyVM ToCurrency { get; set; }
        public float FromPrice { get; set; }
        private string to_price;
        public string ToPrice
        {
            get { return to_price; }
            set
            {
                to_price = value;
                OnPropertyChanged("ToPrice");
            }
        }

        public RelayCommand ConvertCurrencyCmd { get; }

        public ConvertCurrenciesPageVM() 
        {
            FromPrice = 1;
            ConvertCurrencyCmd = new RelayCommand(arg => ConvertCurrency());

            InitCurrencies();
        }

        private async Task InitCurrencies() 
        {
            Currencies = (await cryptoClient.GetRates())?.data?.Select(rate => new CurrencyVM
            {
                Id = rate.id,
                Symbol = rate.symbol
            }).OrderBy(cur => cur.Symbol);
        }
        private async Task ConvertCurrency()
        {
            if (FromCurrency == null || ToCurrency == null)
            {
                ToPrice = "First you need to choose currencies";
                return;
            }

            var from_pirce_task = cryptoClient.GetRate(FromCurrency.Id);
            var to_pirce_task = cryptoClient.GetRate(ToCurrency.Id);
            var from_pirce_str = (await from_pirce_task).data?.rateUsd;
            var to_pirce_str = (await to_pirce_task).data?.rateUsd;

            double from_price, to_price;
            if (!double.TryParse(from_pirce_str, NumberStyles.Any, CultureInfo.InvariantCulture, out from_price) || 
                !double.TryParse(to_pirce_str, NumberStyles.Any, CultureInfo.InvariantCulture, out to_price))
            {
                ToPrice = "Something went wrong while calculating";
                return;
            }

            ToPrice = Math.Round(((from_price / to_price) * FromPrice), 2, MidpointRounding.AwayFromZero).ToString();
        }
    }
}
