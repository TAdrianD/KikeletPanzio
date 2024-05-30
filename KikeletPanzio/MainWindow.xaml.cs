using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Ink;

namespace KikeletPanzio
{
    public partial class MainWindow : Window
    {
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

        private static void LoadFromUgyfel(string ugyfelfile)
        {
            if (!File.Exists(ugyfelfile))
            {
                MessageBox.Show($"A fájl {ugyfelfile} nem létezik.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] ugyfel = File.ReadAllLines(ugyfelfile);
            for (int i = 1; i < ugyfel.Length; i++)
            {
                ugyfelek.Add(new Ugyfel(ugyfel[i]));
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
    }
}