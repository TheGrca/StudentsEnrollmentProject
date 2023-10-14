using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projekat
{
    internal class Ucenik
    {
        private string ime;
        private string prezime;
        private string jmbg;
        private string adresa;
        private BitmapImage slikaUcenika = new BitmapImage();
        public Ucenik(string i, string p, string j, string a, BitmapImage image)
        {
            this.ime = i;
            this.prezime = p;
            this.jmbg = j;
            this.adresa = a;
            this.slikaUcenika = image;
        }

        public void SetSlikaUcenika(BitmapImage s)
        {
            slikaUcenika = s;
        }

        public string ImeiPrezime
        {
            get { return ime + " " + prezime; }
        }

        public Ucenik(BitmapImage i, string ime, string p)
        {
            this.slikaUcenika = i;
            this.ime = ime;
            this.prezime = p;
        }
        public Ucenik()
        {
            this.ime = "";
            this.prezime = "";
            this.jmbg = "";
            this.adresa = "";
        }

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }

        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        public BitmapImage SlikaUcenika{
            get { return slikaUcenika; }
            set { slikaUcenika = value; }
        }




    }





}
