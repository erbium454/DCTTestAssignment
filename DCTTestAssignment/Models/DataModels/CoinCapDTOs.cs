using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.Models.DataModels.CoinCap
{
    public class Data<T>
    {
        public T data { get; set; }
    }
    public class CoinInf
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string priceUsd { get; set; }
    }
}
