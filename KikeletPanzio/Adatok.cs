using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KikeletPanzio
{
    public class Szoba
    {
        public Szoba(string inputString)
        {
            string[] szobadata = inputString.Split(';');
            SzobaSzama = int.Parse(szobadata[0]);
            FerohelyekSzama = int.Parse(szobadata[1]);
            ArFoEjszakara = decimal.Parse(szobadata[2]);
        }

        public int SzobaSzama { get; set; }
        public int FerohelyekSzama { get; set; }
        public decimal ArFoEjszakara { get; set; }
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
            var parts = data.Split(',');
            Azonosito = parts[0];
            Nev = parts[1];
            SzuletesiDatum = DateTime.Parse(parts[2]);
            Email = parts[3];
            VIP = bool.Parse(parts[4]);
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
        public Foglalas(string inputString)
        {
            string[] foglalasdata = inputString.Split(";");
            SzobaSzam = int.Parse(foglalasdata[0]);
            UgyfelAzonosito = foglalasdata[1];
            ErkezesDatuma = DateTime.Parse(foglalasdata[2]);
            TavozasDatuma = DateTime.Parse(foglalasdata[3]);
            FoSzam = int.Parse(foglalasdata[4]);
            TeljesAr = decimal.Parse(foglalasdata[5]);
            Allapot = foglalasdata[6];
        }

        public int SzobaSzam { get; set; }
        public string UgyfelAzonosito { get; set; }
        public DateTime ErkezesDatuma { get; set; }
        public DateTime TavozasDatuma { get; set; }
        public int FoSzam { get; set; }
        public decimal TeljesAr { get; set; }
        public string Allapot { get; set; }
    }

}