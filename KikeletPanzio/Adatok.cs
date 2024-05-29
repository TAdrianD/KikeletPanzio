using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KikeletPanzio
{
    public class Szoba
    {
        public int SzobaSzama { get; set; }
        public int FerohelyekSzama { get; set; }
        public int ArFoPerEjszakara { get; set; }

        public Szoba(string sor)
        {
            var elemek = sor.Split(',');
            SzobaSzama = int.Parse(elemek[0]);
            FerohelyekSzama = int.Parse(elemek[1]);
            ArFoPerEjszakara = int.Parse(elemek[2]);
        }


        public Szoba(int szobaszama, int ferohelyekszama, int arfoperejszaka)
        {
            int SzobaSzama = szobaszama;
            int FerohelyekSzama = ferohelyekszama; 
            int ArFoPerEjszakara = arfoperejszaka;
        }

        public override string ToString()
        {
            return $"{SzobaSzama},{FerohelyekSzama},{ArFoPerEjszakara}";
        }
    }


    public class Ugyfel
    {
        public string Azonosito { get; }
        public string Nev { get; }
        public DateTime SzuletesiDatum { get; }
        public string Email { get; }
        public bool VIP { get; }

        public Ugyfel(string data)
        {
            var elemek = data.Split(',');
            Azonosito = elemek[0];
            Nev = elemek[1];
            SzuletesiDatum = DateTime.Parse(elemek[2]);
            Email = elemek[3];
            VIP = bool.Parse(elemek[4]);
        }

        public Ugyfel(string azonosito, string nev, DateTime szuletesiDatum, string email, bool vip)
        {
            Azonosito = azonosito;
            Nev = nev;
            SzuletesiDatum = szuletesiDatum;
            Email = email;
            VIP = vip;
        }

        public override string ToString()
        {
            return $"{Azonosito},{Nev},{SzuletesiDatum:yyyy-MM-dd},{Email},{VIP}";
        }
    }


    public class Foglalas
    {
        public string Azonosito { get; set; }
        public int FoglaltFo { get; set; }
        public string SzobaSzam { get; set; }
        public DateTime ErkezesiDatum { get; set; }
        public DateTime TavozasiDatum { get; set; }
        public int TeljesAr { get; set; }
        public string Allapot { get; set; }


        public Foglalas(string sor)
        {
            var elemek = sor.Split(',');
            Azonosito = elemek[0];
            FoglaltFo = int.Parse(elemek[1]);
            SzobaSzam = elemek[2];
            ErkezesiDatum = DateTime.Parse(elemek[3]);
            TavozasiDatum = DateTime.Parse(elemek[4]);
            TeljesAr = int.Parse(elemek[5]);
            Allapot = elemek[6];
        }

        public override string ToString()
        {
            return $"{Azonosito},{FoglaltFo},{SzobaSzam},{ErkezesiDatum:yyyy-MM-dd},{TavozasiDatum:yyyy-MM-dd},{TeljesAr},{Allapot}";
        }
    }


}