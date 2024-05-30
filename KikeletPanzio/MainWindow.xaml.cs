using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KikeletPanzio
{
    public partial class MainWindow : Window
    {
        private List<Foglalas> foglalasok = new List<Foglalas>();
        internal static List<Szoba> szobak = new List<Szoba>();
        internal static List<Ugyfel> ugyfelek = new List<Ugyfel>();

        public MainWindow()
        {
            InitializeComponent();
            LoadFromSzoba("szoba.txt");
            LoadFromUgyfel("ugyfel.txt");
            DataContext = this;
            DgrSzobak.ItemsSource = szobak;
            DgrUgyfelek.ItemsSource = ugyfelek;
            DgrUgyfelek.Items.Refresh();
            BtnShowStatistics_Click(null, null);
        }

        private void LoadData()
        {
            LoadFromFoglalas("foglalas.txt");
            LoadFromUgyfel("ugyfel.txt");
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

        private static void LoadFromSzoba(string szobafile)
        {
            if (!File.Exists(szobafile))
            {
                MessageBox.Show($"A fájl {szobafile} nem létezik.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] szoba = File.ReadAllLines(szobafile);
            for (int i = 1; i < szoba.Length; i++)
            {
                szobak.Add(new Szoba(szoba[i]));
            }
        }

        private void LoadFromFoglalas(string foglalasfile)
        {
            if (!File.Exists(foglalasfile))
            {
                MessageBox.Show($"A fájl {foglalasfile} nem létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] foglalasLines = File.ReadAllLines(foglalasfile);
            for (int i = 1; i < foglalasLines.Length; i++)
            {
                foglalasok.Add(new Foglalas(foglalasLines[i]));
            }
        }

        private void MenuRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegisztracioAblak regisztracio = new RegisztracioAblak();
            regisztracio.ShowDialog();
            DgrUgyfelek.Items.Refresh();
        }

        private void MenuFoglalas_Click(object sender, RoutedEventArgs e)
        {
            FoglalasAblak foglalas = new FoglalasAblak();
            foglalas.ShowDialog();
            DgrUgyfelek.Items.Refresh();
        }

        private void BtnCalculateRevenue_Click(object sender, RoutedEventArgs e)
        {
            if (DtpStartDatum.SelectedDate == null || DtpEndDatum.SelectedDate == null)
            {
                MessageBox.Show("Kérjük, válassza ki a kezdő és befejező dátumot.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime startDatum = DtpStartDatum.SelectedDate.Value;
            DateTime endDatum = DtpEndDatum.SelectedDate.Value;

            int osszesBevetel = foglalasok
                .Where(f => f.ErkezesiDatum >= startDatum && f.TavozasiDatum <= endDatum)
                .Sum(f => f.TeljesAr);

            TbkOsszesBevetel.Text = $"Összes bevétel: {osszesBevetel} Ft";
        }

        private void BtnShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            var legtobbetKiadottSzoba = foglalasok
                .GroupBy(f => f.SzobaSzam)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;

            TbkLegtobbetKiadottSzoba.Text = legtobbetKiadottSzoba != null
                ? $"Legtöbbet kiadott szoba: {legtobbetKiadottSzoba}"
                : "Nincs adat a legtöbbet kiadott szobára.";

            var visszajaroVendegek = foglalasok
                .GroupBy(f => f.Azonosito)
                .Select(g => new
                {
                    Ugyfel = ugyfelek.First(u => u.Azonosito == g.Key),
                    FizetettOsszeg = g.Sum(f => f.TeljesAr)
                })
                .OrderByDescending(x => x.FizetettOsszeg)
                .Select(x => new
                {
                    x.Ugyfel.Nev,
                    x.FizetettOsszeg
                })
                .ToList();

            LvwVisszajaroVendegek.ItemsSource = visszajaroVendegek;
        }
    }
}
