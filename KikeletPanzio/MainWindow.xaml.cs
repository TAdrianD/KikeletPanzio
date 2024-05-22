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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static List<Szoba> szobak = new List<Szoba>();
        

        public MainWindow()
        {
            InitializeComponent();
            LoadFromSzoba("szoba.txt");
        }

        private static void LoadFromSzoba(string szobafile)
        {
            string[] szoba = File.ReadAllLines(szobafile);
            for (int i = 1; i < szoba.Length; i++)
            {
                szobak.Add(new Szoba(szoba[i]));
            }
        }

        private void MenuRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegisztracioAblak regisztracio = new RegisztracioAblak();
            regisztracio.ShowDialog();
        }

        private void MenuFoglalas_Click(object sender, RoutedEventArgs e)
        {
            FoglalasAblak foglalas = new FoglalasAblak();
            foglalas.ShowDialog();
        }
    }
}