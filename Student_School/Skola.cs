using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projekat
{
    class Skola
    {
        private string id;
        private string naziv;
        private string adresa;
        private List<Ucenik> ucenici;
        private BitmapImage image;

        public List<Ucenik> Ucenici
        {
            get { return ucenici; }
            set { ucenici = value; }
        }


        public Skola()
        {
            this.naziv = "";
            this.id = "";
            this.adresa = "";
            this.ucenici = new List<Ucenik>();
        }
        public Skola(string Naziv, string Id, string Adresa, BitmapImage image)
        {
            this.naziv = Naziv;
            this.id = Id;
            this.adresa = Adresa;
            this.ucenici = new List<Ucenik>();
            this.image = image;
        }


        public string Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set { this.naziv = value; }
        }

        public string Adresa
        {
            get { return adresa; }
            set { this.adresa = value; }
        }

        public BitmapImage Bi
        {
            get { return Bi; }
        }

        public Ucenik GetUcenik(Ucenik u)
        {
            return u;
        }

        public BitmapImage SlikaSkole
        {
            get { return image; }
            set { image = value; }
        }
    }
}
