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
            CobxHanyFo.SelectedIndex = 0;
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
                MessageBox.Show($"A fájl {ugyfelfile} nem létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"A fájl {szobafile} nem létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"A fájl {foglalasfile}  nem létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
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
                int foglaltFo = int.Parse(CobxHanyFo.SelectedItem.ToString());
                Szoba kivalasztottSzoba = (Szoba)CobxSzoba.SelectedItem;
                Ugyfel kivalasztottUgyfel = (Ugyfel)CobxUgyfel.SelectedItem;

                if (foglaltFo > kivalasztottSzoba.FerohelyekSzama)
                {
                    MessageBox.Show("Nincs elegendő hely a foglaláshoz!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                fizetendo = kivalasztottSzoba.ArFoPerEjszakara * foglaltFo;

                if (kivalasztottUgyfel.VIP)
                {
                    fizetendo = (int)(fizetendo * 0.97);
                }

                TbxTeljesAr.Text = fizetendo.ToString();
                TbxAllapot.Text = "Lefoglalva";

                // Új Foglalas objektum létrehozása a kiválasztott adatok alapján
                DateTime erkezesDatum = DtpErkezesDatum.SelectedDate ?? DateTime.Today;
                DateTime tavozasDatum = DtpTavozasDatum.SelectedDate ?? DateTime.Today;
                foglalasok.Add(new Foglalas($"{kivalasztottUgyfel.Azonosito},{foglaltFo},{kivalasztottSzoba.SzobaSzama},{erkezesDatum:yyyy-MM-dd},{tavozasDatum:yyyy-MM-dd},{fizetendo},{TbxAllapot.Text}"));

                SaveFoglalasok("foglalas.txt");

                DgrFoglalasok.Items.Refresh();
                CobxSzoba.SelectedIndex = -1;
                CobxUgyfel.SelectedIndex = -1;
                CobxHanyFo.SelectedIndex = -1;

                MessageBox.Show("Szoba lefoglalva", "Sikeres foglalás!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nincsen kiválasztva szoba, ügyfél vagy a férőhelyek száma!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFoglalasok(string foglalasfile)
        {
            var foglalasokLines = new List<string> { "Azonosito,FoglaltFo,SzobaSzam,ErkezesiDatum,TavozasiDatum,TeljesAr,Allapot" };
            foglalasokLines.AddRange(foglalasok.Select(f => f.ToString()));
            File.WriteAllLines(foglalasfile, foglalasokLines);
        }
    }
}