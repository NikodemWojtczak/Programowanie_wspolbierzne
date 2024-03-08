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
            k.Show(); // Use this to show Window2 and keep Window1 open

            // Alternatively, to open Window2 and close Window1:
            // window2.Show();
            // this.Close();
        }
    }
}
