using DCTTestAssignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DCTTestAssignment.ViewModels
{
    abstract class PageBaseVM : INotifyPropertyChanged
    {
        private CryptoClient crypto_client = null;
        protected CryptoClient cryptoClient => crypto_client ?? (crypto_client = (CryptoClient)Application.Current.FindResource("CryptoClient"));


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
