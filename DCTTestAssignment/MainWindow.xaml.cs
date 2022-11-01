using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DCTTestAssignment.ViewModels;
using System.Collections;

namespace DCTTestAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] langs = new string[] { "EN", "UA", "RU" };

        public MainWindow()
        {
            InitializeComponent();

            langBox.SelectionChanged += LangChange;
            langBox.ItemsSource = langs;
            langBox.SelectedItem = "EN";
        }

        private void LangChange(object sender, SelectionChangedEventArgs e)
        {
            string lang = ((string)langBox.SelectedItem).ToLower();
            var uri = new Uri($"Resources/lang.{lang}.xaml", UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            var oldDict = (from d in Application.Current.Resources.MergedDictionaries
                           where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                           select d).FirstOrDefault();
            if (oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, resourceDict);
            }
            else Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        }
    }

    class CandleChartData
    {
        public string rectangle_bottom { get; set; }
        public string rectangle_height { get; set; }
        public string rectangle_top { get; set; }
        public string line_bottom { get; set; }
        public string line_height { get; set; }
        public string line_top { get; set; }
        public Brush color { get; set; }
        public CandleChartVM hint_data { get; set; }
    }
    class CandleChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var chart = value as IEnumerable<CandleChartVM>;
            if (value == null) throw new ArgumentException("Wrong type of converter values param");

            double min_value = chart.Min(c => c.Low);
            double max_value = chart.Max(c => c.High);
            double range = max_value - min_value;

            return chart.Select(chart_el => new CandleChartData
            {
                rectangle_bottom = Math.Round((Math.Min(chart_el.Open, chart_el.Close) - min_value) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                rectangle_height = Math.Round((Math.Max(chart_el.Open, chart_el.Close) - Math.Min(chart_el.Open, chart_el.Close)) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                rectangle_top = Math.Round((max_value - Math.Max(chart_el.Open, chart_el.Close)) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                line_bottom = Math.Round((chart_el.Low - min_value) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                line_height = Math.Round((chart_el.High - chart_el.Low) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                line_top = Math.Round((max_value - chart_el.High) / range, 8, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + "*",
                color = new SolidColorBrush(chart_el.Close > chart_el.Open ? Colors.Green : Colors.Red),
                hint_data = chart_el
            });
        } 
        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
