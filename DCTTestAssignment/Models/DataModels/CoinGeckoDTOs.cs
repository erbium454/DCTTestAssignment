using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Models.DataModels.CoinGecko
{
    public class CoinDetailedInf
    {
        public class Image
        {
            public string large { get; set; }
        }
        public class Links
        {
            public string[] homepage { get; set; }
            public string[] blockchain_site { get; set; }
        }
        public class Market_Data
        {
            public Current_Price current_price { get; set; }
            public Total_Volume total_volume { get; set; }
            public Price_Change_24H_In_Currency price_change_24h_in_currency { get; set; }
        }
        public class Current_Price
        {
            public float? usd { get; set; }
        }
        public class Total_Volume
        {
            public double? usd { get; set; }
        }
        public class Price_Change_24H_In_Currency
        {
            public float? usd { get; set; }
        }
        public class Ticker
        {
            public string target { get; set; }
            public Market market { get; set; }
            public float? last { get; set; }
            public string trade_url { get; set; }
        }
        public class Market
        {
            public string name { get; set; }
        }

        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public Links links { get; set; }
        public Image image { get; set; }
        public Market_Data market_data { get; set; }
        public Ticker[] tickers { get; set; }
    }
}
