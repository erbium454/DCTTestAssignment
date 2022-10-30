using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using DCTTestAssignment.Models;

namespace DCTTestAssignment.ViewModels
{
    class CoinDetailsPageVM : BaseViewModel
    {
        private string coin_id;
        public string CoinId
        {
            get { return coin_id; }
            set
            {
                coin_id = value;
                OnPropertyChanged("CoinId");
            }
        }
        private CoinDetailsVM coin;
        public CoinDetailsVM Coin
        {
            get { return coin; }
            set
            {
                coin = value;
                OnPropertyChanged("Coin");
            }
        }
        
        public RelayCommand GetCoinCmd { get; }

        public CoinDetailsPageVM() 
        {
            GetCoinCmd = new RelayCommand(arg => GetCoin());
        }

        private async Task GetCoin() 
        {
            var dto = await cryptoClient.GetAsset(CoinId);
            if (dto != null)
            {
                BitmapImage bitmap = null;
                if (!string.IsNullOrWhiteSpace(dto.image?.large))
                {
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(dto.image.large, UriKind.Absolute);
                    bitmap.EndInit();
                }

                Coin = new CoinDetailsVM
                {
                    Image = bitmap,
                    Id = dto.id,
                    Symbol = dto.symbol,
                    Name = dto.name,
                    PriceUsd = dto.market_data?.current_price?.usd?.ToString(),
                    TotalVolumeUsd = dto.market_data?.total_volume?.usd?.ToString(),
                    PriceChangeUsd24H = dto.market_data?.price_change_24h_in_currency?.usd?.ToString(),
                    Links = dto.links?.homepage?.Union(dto.links?.blockchain_site).Where(link => !string.IsNullOrWhiteSpace(link)).Aggregate((links, link) => links + ", " + link),
                    Markets = dto.tickers?.Select(t => new CoinDetailsVM.Market
                    {
                        Name = t.market?.name,
                        TargetCurrency = t.target,
                        Price = t.last?.ToString(),
                        TradeUrl = t.trade_url
                    })
                };
            }
        }
    }
}
