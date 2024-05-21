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
using System.Windows.Shapes;

namespace KikeletPanzio
{
    /// <summary>
    /// Interaction logic for RegisztracioAblak.xaml
    /// </summary>
    public partial class RegisztracioAblak : Window
    {
        public RegisztracioAblak()
        {
            InitializeComponent();
        }

        private void BtnMegse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            string azonosito = TbxNev.Text + DateTime.Now;
            MessageBox.Show("Az ön azonosítója:" + azonosito, "Sikeres regisztráció!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
