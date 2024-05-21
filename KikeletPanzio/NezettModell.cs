//using KikeletPanzio;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KikeletPanzio
//{
//    public class SzobaViewModel
//    {
//        public ObservableCollection<Szoba> Szobak { get; set; }

//        public SzobaViewModel()
//        {
//            Szobak = new ObservableCollection<Szoba>
//        {
//            new Szoba { SzobaSzam = 1, FerohelyekSzama = 2, ArFoEjszakara = 6000 },
//            new Szoba { SzobaSzam = 2, FerohelyekSzama = 2, ArFoEjszakara = 6000 },
//            new Szoba { SzobaSzam = 3, FerohelyekSzama = 3, ArFoEjszakara = 9000 },
//            new Szoba { SzobaSzam = 4, FerohelyekSzama = 3, ArFoEjszakara = 9000 },
//            new Szoba { SzobaSzam = 5, FerohelyekSzama = 4, ArFoEjszakara = 12000 },
//            new Szoba { SzobaSzam = 6, FerohelyekSzama = 5, ArFoEjszakara = 12000 }
//        };
//        }
//    }

//    public class UgyfelViewModel
//    {
//        public ObservableCollection<Ugyfel> Ugyfelek { get; set; }

//        public UgyfelViewModel()
//        {
//            Ugyfelek = new ObservableCollection<Ugyfel>();
//        }

//        public void UgyfelHozzaadasa(string nev, DateTime szuletesiDatum, string email, bool vipUgyfel)
//        {
//            string azonosito = $"{nev}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
//            Ugyfelek.Add(new Ugyfel
//            {
//                Azonosito = azonosito,
//                Nev = nev,
//                SzuletesiDatum = szuletesiDatum,
//                Email = email,
//                VIPUgyfel = vipUgyfel
//            });
//        }
//    }

//    public class FoglalasViewModel
//    {
//        public ObservableCollection<Foglalas> Foglalasok { get; set; }

//        public FoglalasViewModel()
//        {
//            Foglalasok = new ObservableCollection<Foglalas>();
//        }

//        public void FoglalasHozzaadasa(int szobaSzam, string ugyfelAzonosito, DateTime erkezesDatuma, DateTime tavozasDatuma, int foSzam, decimal arFoEjszakara)
//        {
//            decimal teljesAr = foSzam * arFoEjszakara * (decimal)(tavozasDatuma - erkezesDatuma).TotalDays;
//        //if (ugyfelAzonosito.VIPUgyfel)
//        //    {
//        //        teljesAr *= 0.97m;
//        //    }

//            Foglalasok.Add(new Foglalas
//            {
//                SzobaSzam = szobaSzam,
//                UgyfelAzonosito = ugyfelAzonosito,
//                ErkezesDatuma = erkezesDatuma,
//                TavozasDatuma = tavozasDatuma,
//                FoSzam = foSzam,
//                TeljesAr = teljesAr,
//                Allapot = "Foglalt"
//            });
//        }
//    }

//}