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
        private List<Foglalas> foglalasok = new List<Foglalas>();
        private List<Ugyfel> ugyfelek = new List<Ugyfel>();
        private List<Szoba> szobak = new List<Szoba>();

        public FoglalasAblak()
        {
            InitializeComponent();
            LoadData();
            DgrFoglalasok.ItemsSource = foglalasok;
            CobxUgyfel.ItemsSource = ugyfelek;
            CobxHanyFo.Items.Add("2");
            CobxHanyFo.Items.Add("3");
            CobxHanyFo.Items.Add("4");
            CobxSzoba.ItemsSource = szobak;
            CobxSzoba.SelectedIndex = 0;
            CobxSzoba.DisplayMemberPath = "SzobaSzama";
        }

        private void LoadData()
        {
            LoadFromFoglalas("foglalas.txt");
            LoadFromUgyfel("ugyfel.txt");
            LoadFromSzoba("szoba.txt");
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

        private void LoadFromSzoba(string szobafile)
        {
            if (!File.Exists(szobafile))
            {
                MessageBox.Show($"The file {szobafile} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] szobaLines = File.ReadAllLines(szobafile);
            for (int i = 1; i < szobaLines.Length; i++)
            {
                Szoba szoba = new Szoba(szobaLines[i]);
                if (szoba.SzobaSzama >= 1 && szoba.SzobaSzama <= 6)
                {
                    szobak.Add(szoba);
                }
            }
        }

        private void LoadFromFoglalas(string foglalasfile)
        {
            if (!File.Exists(foglalasfile))
            {
                MessageBox.Show($"The file {foglalasfile} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] foglalasLines = File.ReadAllLines(foglalasfile);
            for (int i = 1; i < foglalasLines.Length; i++)
            {
                foglalasok.Add(new Foglalas(foglalasLines[i]));
            }
        }

        private void BtnMegse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnFoglalas_Click(object sender, RoutedEventArgs e)
        {
            if (CobxSzoba.SelectedItem != null && CobxUgyfel.SelectedItem != null && CobxHanyFo.SelectedItem != null)
            {
                int fizetendo = 0;
                Szoba kivalasztottSzoba = (Szoba)CobxSzoba.SelectedItem;
                Ugyfel kivalasztottUgyfel = (Ugyfel)CobxUgyfel.SelectedItem;
                int hanyFo = Convert.ToInt32((CobxHanyFo.SelectedItem as ComboBoxItem)?.Content);

                if ((hanyFo == 2 && (kivalasztottSzoba.SzobaSzama == 1 || kivalasztottSzoba.SzobaSzama == 2)) ||
                    (hanyFo == 3 && (kivalasztottSzoba.SzobaSzama == 3 || kivalasztottSzoba.SzobaSzama == 4)) ||
                    (hanyFo == 4 && (kivalasztottSzoba.SzobaSzama == 5 || kivalasztottSzoba.SzobaSzama == 6)))
                {
                    fizetendo = kivalasztottSzoba.ArFoPerEjszakara * hanyFo;

                    if (kivalasztottUgyfel.VIP)
                    {
                        fizetendo = (int)(fizetendo * 0.97);
                    }

                    TbxTeljesAr.Text = fizetendo.ToString();
                    TbxAllapot.Text = "Lefoglalva";

                    foglalasok.Add(new Foglalas($"{kivalasztottUgyfel.Azonosito},{hanyFo},{kivalasztottSzoba.SzobaSzama},{DateTime.Now:yyyy-MM-dd},{DateTime.Now:yyyy-MM-dd},{fizetendo},{TbxAllapot.Text}"));
                    SaveFoglalasok("foglalas.txt");

                    DgrFoglalasok.Items.Refresh();
                    CobxSzoba.SelectedIndex = -1;
                    CobxUgyfel.SelectedIndex = -1;
                    CobxHanyFo.SelectedIndex = -1;

                    MessageBox.Show("Szoba lefoglalva", "Sikeres foglalás!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nem megfelelő szoba lett kiválasztva a férőhelyek számához!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nincsen kiválasztva szoba, ügyfél vagy férőhelyek száma!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SaveFoglalasok(string foglalasfile)
        {
            var foglalasokLines = new List<string> { "Azonosito,SzobaSzam,ErkezesiDatum,TavozasiDatum,HanyFo,TeljesAr,Allapot" };
            foglalasokLines.AddRange(foglalasok.Select(f => f.ToString()));
            File.WriteAllLines(foglalasfile, foglalasokLines);
        }
    }
}
