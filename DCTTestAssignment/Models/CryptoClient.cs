using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

using DCTTestAssignment.Models.DataModels.CoinCap;
using DCTTestAssignment.Models.DataModels.CoinGecko;

namespace DCTTestAssignment.Models
{
    class CryptoClient
    {
        const string coincap_url = "https://api.coincap.io/v2";
        const string coingecko_url = "https://api.coingecko.com/api/v3";

        readonly RestClient client;

        public CryptoClient()
        {
            client = new RestClient();
            client.AddDefaultHeader("Accept-Encoding", "gzip");
        }

        public async Task<Data<CoinInf[]>?> GetAssets(uint? limit = null) => await client.GetJsonAsync<Data<CoinInf[]>>($"{coincap_url}/assets?limit={limit?.ToString() ?? ""}");
        public async Task<CoinDetailedInf?> GetAsset(string id) => await client.GetJsonAsync<CoinDetailedInf>($"{coingecko_url}/coins/{id}");
    }
}
