using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Models.DataModels.CoinCap
{
    class Data<T>
    {
        public T data { get; set; }
    }
    class CoinInf
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string priceUsd { get; set; }
    }
    class CandleInf
    {
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string close { get; set; }
        public string volume { get; set; }
        public long period { get; set; }
    }
    class RateInf
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string currencySymbol { get; set; }
        public string type { get; set; }
        public string rateUsd { get; set; }
    }
}
