using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KikeletPanzio
{
    public partial class FoglalasAblak : Window
    {
        internal static List<Foglalas> foglalasok = new List<Foglalas>();
        internal static List<Ugyfel> ugyfelek = new List<Ugyfel>();

        public FoglalasAblak()
        {
            InitializeComponent();
            LoadFromFoglalas("foglalas.txt");
            LoadFromUgyfel("ugyfel.txt");
            DgrFoglalasok.ItemsSource = foglalasok;
            CobxUgyfel.ItemsSource = ugyfelek; // Az ügyfelek hozzáadása a ComboBox-hoz
        }

        private void LoadFromUgyfel(string ugyfelfile)
        {
            if (!File.Exists(ugyfelfile))
            {
                MessageBox.Show($"The file {ugyfelfile} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] ugyfelLines = File.ReadAllLines(ugyfelfile);
            for (int i = 1; i < ugyfelLines.Length; i++)
            {
                ugyfelek.Add(new Ugyfel(ugyfelLines[i]));
            }
        }

        private void BtnMegse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnFoglalas_Click(object sender, RoutedEventArgs e)
        {
            if (CobxSzoba.SelectedItem != null && CobxUgyfel.SelectedItem != null)
            {
                int fizetendo = 0;
                Szoba kivalasztottSzoba = (Szoba)CobxSzoba.SelectedItem;
                Ugyfel kivalasztottUgyfel = (Ugyfel)CobxUgyfel.SelectedItem;

                if (CobxHanyFo.SelectedItem != null)
                {
                    int hanyFo = Convert.ToInt32(((ComboBoxItem)CobxHanyFo.SelectedItem).Content);

                    if (hanyFo == 2 && (kivalasztottSzoba.SzobaSzama == 1 || kivalasztottSzoba.SzobaSzama == 2))
                    {
                        fizetendo = (int)(kivalasztottSzoba.ArFoEjszakara * hanyFo);
                    }
                    else if ((hanyFo == 3 || hanyFo == 4) && (kivalasztottSzoba.SzobaSzama == 3 || kivalasztottSzoba.SzobaSzama == 4))
                    {
                        fizetendo = (int)(kivalasztottSzoba.ArFoEjszakara * hanyFo);
                    }
                    else if (hanyFo == 4 && (kivalasztottSzoba.SzobaSzama == 5 || kivalasztottSzoba.SzobaSzama == 6))
                    {
                        fizetendo = (int)(kivalasztottSzoba.ArFoEjszakara * hanyFo);
                    }
                    else
                    {
                        MessageBox.Show("Nem megfelelő szoba lett kiválasztva a férőhelyek számához!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (kivalasztottUgyfel.VIP)
                    {
                        fizetendo = (int)(fizetendo * 0.97);
                    }

                    TbxTeljesAr.Text = fizetendo.ToString();
                    MessageBox.Show("Szoba lefoglalva", "Sikeres foglalás!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nincsen kiválasztva a férőhelyek száma!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nincsen kiválasztva szoba vagy ügyfél, kérem válasszon egyet!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private static void LoadFromFoglalas(string foglalasfile)
        {
            string[] foglalas = File.ReadAllLines(foglalasfile);
            for (int i = 1; i < foglalas.Length; i++)
            {
                foglalasok.Add(new Foglalas(foglalas[i]));
            }
        }
    }
}
