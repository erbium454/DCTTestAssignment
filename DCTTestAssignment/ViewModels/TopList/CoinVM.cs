using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels
{
    class CoinVM
    {
        public int? Rank { get; set; }
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string PriceUsd { get; set; }
    }
}
