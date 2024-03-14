using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Kulki
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenWindow2_Click(object sender, RoutedEventArgs e)
        {
            string tmp = Ilosc_kulek.Text;
            int a = int.Parse(tmp);
            Kolka k = new Kolka(a);
            this.Close();
            k.Show(); 
        }
    }
}
