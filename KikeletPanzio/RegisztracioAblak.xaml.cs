using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace KikeletPanzio
{
    /// <summary>
    /// Interaction logic for RegisztracioAblak.xaml
    /// </summary>
    public partial class RegisztracioAblak : Window, INotifyPropertyChanged
    {
        internal static List<Ugyfel> ugyfelek = new List<Ugyfel>();
        public List<Ugyfel> Ugyfelek
        {
            get { return ugyfelek; }
            set
            {
                ugyfelek = value;
                OnPropertyChanged(nameof(Ugyfelek));
            }
        }

        public RegisztracioAblak()
        {
            InitializeComponent();
            DataContext = this;
            LoadFromUgyfel("ugyfel.txt");
            DgrUgyfelek.ItemsSource = ugyfelek;
        }

        private static void LoadFromUgyfel(string ugyfelfile)
        {
            if (!File.Exists(ugyfelfile))
            {
                MessageBox.Show($"The file {ugyfelfile} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] ugyfel = File.ReadAllLines(ugyfelfile);
            for (int i = 1; i < ugyfel.Length; i++)
            {
                ugyfelek.Add(new Ugyfel(ugyfel[i]));
            }
        }

        private void BtnMegse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbxNev.Text) && !string.IsNullOrWhiteSpace(TbxEmail.Text))
            {
                string azonosito = TbxNev.Text.ToLower().Replace(" ", "") + "_" + DateTime.Now.ToString("yyyyMMdd");
                TbxAzonosito.Text = azonosito;

                DateTime szuletesiDatum;
                if (!DateTime.TryParse(DtpSzuletesiDatum.Text, out szuletesiDatum))
                {
                    MessageBox.Show("Érvénytelen dátum. Kérem vállaszon ki érvényes dátumot.", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool isVIP = CbxVIPUgyfel.IsChecked.HasValue && CbxVIPUgyfel.IsChecked.Value;

                ugyfelek.Add(new Ugyfel(TbxAzonosito.Text, TbxNev.Text, szuletesiDatum, TbxEmail.Text, isVIP));
                SaveToUgyfel("ugyfel.txt");

                MessageBox.Show("Az ön azonosítója: " + azonosito, "Sikeres regisztráció!", MessageBoxButton.OK, MessageBoxImage.Information);
                OnPropertyChanged(nameof(Ugyfelek));
            }
            else
            {
                MessageBox.Show("Nincsen kitöltve az összes mező, kérem próbálja meg újra!", "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveToUgyfel(string ugyfelfile)
        {
            var lines = new List<string> { "Azonosito,Nev,SzuletesiDatum,Email,VIP" };
            lines.AddRange(ugyfelek.Select(u => u.ToString()));
            File.WriteAllLines(ugyfelfile, lines);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
