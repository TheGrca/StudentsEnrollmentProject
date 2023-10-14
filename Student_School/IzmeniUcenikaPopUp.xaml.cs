using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for IzmeniUcenikaPopUp.xaml
    /// </summary>
    public partial class IzmeniUcenikaPopUp : Window
    {
        MainWindow mainWindow;
        public IzmeniUcenikaPopUp(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void btnDodajUcenikaPopUp_Click(object sender, RoutedEventArgs e)
        {
            if (DodajIme.Text == "" || DodajPrezime.Text == "" || DodajJMBG.Text == "" || DodajAdresu.Text == "")
            {
                System.Windows.MessageBox.Show("Nedostaju Vam podaci za izmenu.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int.Parse(DodajJMBG.Text);
                    if (mainWindow.proveriJMBG2())
                    {
                        if (System.Windows.MessageBox.Show("Da li zelite da promenite i ikonicu?", "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpenFileDialog fajl = new OpenFileDialog();
                            fajl.ShowDialog();
                            fajl.Filter = "Image Filter(*.png)|*.png;";
                            BitmapImage Ucenici = new BitmapImage(new Uri(fajl.FileName));
                            foreach (Skola s in mainWindow.skole)
                            {
                                foreach (Ucenik u in s.Ucenici)
                                {
                                    if (s.GetUcenik(u).Jmbg == mainWindow.getTempJMBG())
                                    {
                                        s.GetUcenik(u).Ime = DodajIme.Text;
                                        s.GetUcenik(u).Prezime = DodajPrezime.Text;
                                        s.GetUcenik(u).Jmbg = DodajJMBG.Text;
                                        s.GetUcenik(u).Adresa = DodajAdresu.Text;
                                        s.GetUcenik(u).SlikaUcenika = Ucenici;
                                        break;
                                    }
                                }
                            }
                        }

                        else
                        {
                            foreach (Skola s in mainWindow.skole)
                            {
                                foreach (Ucenik u in s.Ucenici)
                                {
                                    if (s.GetUcenik(u).Jmbg == mainWindow.getTempJMBG())
                                    {
                                        s.GetUcenik(u).Ime = DodajIme.Text;
                                        s.GetUcenik(u).Prezime = DodajPrezime.Text;
                                        s.GetUcenik(u).Jmbg = DodajJMBG.Text;
                                        s.GetUcenik(u).Adresa = DodajAdresu.Text;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Učenik sa tim JMBG već postoji");
                    }
                    int index = mainWindow.list_ucenik.SelectedIndex;
                    string imeUcenika = DodajIme.Text + " " + DodajPrezime.Text;

                    mainWindow.list_ucenik.SelectedIndex = 0;
                    mainWindow.list_ucenik.Items.RemoveAt(index);
                    mainWindow.list_ucenik.Items.Add(imeUcenika);

                    this.Hide();
                }
                catch
                {
                    System.Windows.MessageBox.Show("JMBG mora da sadrži samo brojeve", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DodajIme.Text = mainWindow.Naziv_Ucenika.Text;
            DodajPrezime.Text = mainWindow.Prezime_Ucenika.Text;
            DodajJMBG.Text = mainWindow.JMBG_Ucenika.Text;
            DodajAdresu.Text = mainWindow.Adresa_Ucenika.Text;
        }
    }
}
