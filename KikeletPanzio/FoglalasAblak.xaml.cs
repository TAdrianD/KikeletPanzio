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
using System.IO;

namespace KikeletPanzio
{
    /// <summary>
    /// Interaction logic for FoglalasAblak.xaml
    /// </summary>
    public partial class FoglalasAblak : Window
    {
        internal static List<Foglalas> foglalasok = new List<Foglalas>();
        public FoglalasAblak()
        {
            InitializeComponent();
            LoadFromFoglalas("foglalas.txt");
            DgrFoglalasok.ItemsSource = foglalasok;
        }


        private static void LoadFromFoglalas(string foglalasfile)
        {
            string[] foglalas = File.ReadAllLines(foglalasfile);
            for (int i = 1; i < foglalas.Length; i++)
            {
                foglalasok.Add(new Foglalas(foglalas[i]));
            }
        }

        private void BtnMegse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnFoglalas_Click(object sender, RoutedEventArgs e)
        {
            if (CobxSzoba.SelectedItem != null)
            {
                int fizetendo = 0; 

                //if (CobxSzoba.SelectedIndex == 0  && CobxHanyFo.SelectedIndex == 0 || CobxSzoba.SelectedIndex == 1 && CobxHanyFo.SelectedIndex == 0)
                //{
                //    fizetendo = 0;
                //}
                MessageBox.Show("Szoba lefoglalva", "Sikeres foglalás!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nincsen kiválasztva szoba, kérem válasszon egyet!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
