using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using DCTTestAssignment.Models;

namespace DCTTestAssignment.ViewModels
{
    class TopListPageVM : BaseViewModel
    {
        private uint? top_amount;
        public uint? TopAmount
        {
            get { return top_amount; }
            set
            {
                top_amount = value;
                OnPropertyChanged("TopAmount");
            }
        }

        public IEnumerable<CoinVM>? coins;
        public IEnumerable<CoinVM>? Coins
        {
            get { return coins; }
            set
            {
                coins = value;
                OnPropertyChanged("Coins");
            }
        }

        public RelayCommand RefreshCmd { get; }

        public TopListPageVM()
        {
            TopAmount = 10;
            RefreshCmd = new RelayCommand(arg => Refresh());

            Refresh();
        }

        private async Task Refresh()
        {
            var coins_dto = (await cryptoClient.GetAssets(TopAmount))?.data;
            if (coins_dto != null)
                Coins = coins_dto?.Select(dto => new CoinVM
                {
                    Rank = Int32.TryParse(dto?.rank, out int temp_rank) ? temp_rank : null,
                    Id = dto.id,
                    Symbol = dto.symbol,
                    Name = dto.name,
                    PriceUsd = dto.priceUsd
                }).OrderBy(dto => dto.Rank);
        }
    }
}
