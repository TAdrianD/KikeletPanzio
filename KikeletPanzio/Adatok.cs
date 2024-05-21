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
        public decimal ArFoEjszakara { get; set; }
    }

    public class Ugyfelek
    {
        public string Azonosito { get; set; }
        public string Nev { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public string Email { get; set; }
        public bool VIPUgyfel { get; set; }
    }

    public class Foglalas
    {
        public int SzobaSzam { get; set; }
        public string UgyfelAzonosito { get; set; }
        public DateTime ErkezesDatuma { get; set; }
        public DateTime TavozasDatuma { get; set; }
        public int FoSzam { get; set; }
        public decimal TeljesAr { get; set; }
        public string Allapot { get; set; }
    }

}