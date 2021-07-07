using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Elektrodistribucija_baza
{
    class Potrosaci
    {
        private string ime, prezime, adresa, zona, popust, podrucje, naziv,jmbg;
        private double kilovati;
        private int dani, cena, umanjena_cena;
        private DateTime datum_izdavanja, datum_uplate;
        public double Kilovati
        {
            get
            {
                return kilovati;
            }
            set
            {
                kilovati = value;
            }
        }
        public int Cena
        {
            get
            {
                return cena;
            }
            set
            {
                cena = value;
            }
        }
        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
            }
        }
        public int Umanjena_cena
        {
            get
            {
                return umanjena_cena;
            }
            set
            {
                umanjena_cena = value;
            }
        }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
            }
        }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                naziv = value;
            }
        }
        public string Podrucje
        {
            get
            {
                return podrucje;
            }
            set
            {
                podrucje = value;
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
            }
        }
        public string Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
            }
        }
        public Potrosaci()
        {
            ime = "";
            prezime = "";
            adresa = "";
            kilovati = 0;
            cena = 0;
            podrucje = "";

        }
        public DateTime Datum_izdavanja
        {
            get
            {
                return datum_izdavanja;
            }
            set
            {
                datum_izdavanja = value;
            }
        }
        public DateTime Datum_uplate
        {
            get
            {
                return datum_uplate;
            }
            set
            {
                datum_uplate = value;
            }
        }
        public Potrosaci(string ime, string prezime, string jmbg, string podrucje, string adresa, double kilovati, int cena, int umanjena_cena, DateTime datum_izdavanja,
       DateTime datum_uplate)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
            this.datum_izdavanja = datum_izdavanja;
            this.datum_uplate = datum_uplate;
            this.kilovati = kilovati;
            this.cena = cena;
            this.umanjena_cena = umanjena_cena;
            this.podrucje = podrucje;
            this.jmbg = jmbg;
        }
        public void Pisi(StreamWriter sw)
        {
            sw.WriteLine("Ime i prezime: " + ime + " " + prezime);
            sw.WriteLine("Vas jmbg je: " + jmbg);
            sw.WriteLine("Nalazite se u: " + podrucje);
            sw.WriteLine("Vasa adresa je: " + adresa);
            sw.WriteLine("Broj potrosenih kilovata: " + kilovati + " kWh");
            sw.WriteLine("Datum izdavanja racuna: " + datum_izdavanja.ToShortDateString());
            sw.WriteLine("Datum uplate racuna: " + datum_uplate.ToShortDateString());
            sw.WriteLine("Razlika izmedju dana izdavanja i dana uplate: " + Dani());
            sw.WriteLine(Zona());
            sw.WriteLine("Vasa cena za utrosenu elektricnu energiju je " + cena + " rsd");
            sw.WriteLine(Popust());

            sw.WriteLine("------------------------------------------------------------------------");
        }
        public string Popust()
        {
            if (Dani() <= 15)
            {
                popust = "Ostvarili ste popust od 10% i vasa cena sa popustom iznosi " + umanjena_cena + " rsd";
            }
            else
                popust = "Niste ostvarili popust";
            return popust;
        }

        public int Dani()
        {
            if (datum_uplate > datum_izdavanja)
            {
                dani = (datum_uplate - datum_izdavanja).Days;
            }
            else
                dani = 0;

            return dani;
        }
        public string Zona()
        {
            if (kilovati <= 350)
            {
                zona = "U zelenoj ste zoni i cena po kilovatu je 5 dinara";

            }
            else if (kilovati > 350 && kilovati <= 1600)
            {
                zona = "U plavoj ste zoni i cena po kilovatu je 7 dinara";

            }
            else if (kilovati > 1600)
            {
                zona = "U crvenoj ste zoni i cena po kilovatu je 15 dinara";

            }
            return zona;
        }
    }
}
