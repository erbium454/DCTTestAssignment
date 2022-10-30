using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DCTTestAssignment.ViewModels
{
    class CoinDetailsVM
    {
        public class Market
        {
            public string Name { get; set; }
            public string TargetCurrency { get; set; }
            public string Price { get; set; }
            public string TradeUrl { get; set; }
        }

        public ImageSource Image { get; set; }
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string PriceUsd { get; set; }
        public string TotalVolumeUsd { get; set; }
        public string PriceChangeUsd24H { get; set; }
        public string Links { get; set; }
        public IEnumerable<Market> Markets { get; set; }
    }
}
