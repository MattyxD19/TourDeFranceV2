using System;
using System.Collections.Generic;
using System.Linq;
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
using TourDeFranceApp.Model;

namespace TourDeFranceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var cyclist in Parser.CyclistListMaker())
        	{
                listViewName.Items.Add(cyclist);
	        }

        }

        private void OrderRank_Selected(object sender, RoutedEventArgs e)
        {
            listViewName.Items.Clear();
            List<Cyclist> SortedList = Parser.CyclistListMaker().OrderBy(o => o.EndPosition).ToList();
            var top10 = SortedList.Take(10);

            foreach (var top in top10)
            {
                listViewName.Items.Add(top);
            }
        }

        private void OrderName_Selected(object sender, RoutedEventArgs e)
        {
            listViewName.Items.Clear();
            List<Cyclist> sortedList = Parser.CyclistListMaker().OrderBy(o => o.Name).ToList();

            foreach (var name in sortedList)
            {
                listViewName.Items.Add(name);
            }
        }

        private void OrderCountry_Selected(object sender, RoutedEventArgs e)
        {
            listViewName.Items.Clear();
            List<Cyclist> SortedList = Parser.CyclistListMaker().OrderBy(o => o.Country).ToList();
            var top10 = SortedList.Take(10);

            foreach (var top in top10)
            {
                listViewName.Items.Add(top);
            }
        }

        private void OrderResultTime_Selected(object sender, RoutedEventArgs e)
        {

            listViewName.Items.Clear();
            List<Cyclist> SortedList = Parser.CyclistListMaker().OrderBy(o => o.ResultTime).ToList();
            var top10 = SortedList.Take(10);

            foreach (var top in top10)
            {
                listViewName.Items.Add(top);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Cyclist findCyclist = new Cyclist();
            //string searchText = SearchBar.Text;
            //Convert.ChangeType(searchText, typeof(Object));
            //listViewName.Items.Clear();
            //listViewName.Items.Add(Parser.CyclistListMaker().Contains(searchText));
        }
    }
}
