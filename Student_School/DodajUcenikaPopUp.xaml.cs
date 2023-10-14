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
    /// Interaction logic for DodajUcenikaPopUp.xaml
    /// </summary>
    public partial class DodajUcenikaPopUp : Window
    {
        private MainWindow mainWindow;
        public DodajUcenikaPopUp(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void btnDodajUcenikaPopUp_Click(object sender, RoutedEventArgs e)
        {
            if (DodajIme.Text == "" || DodajPrezime.Text == "" || DodajJMBG.Text == "" || DodajAdresu.Text == "")
            {
                System.Windows.MessageBox.Show("Unesite sve podatke pre nego sto dodate ucenika u skolu.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int.Parse(DodajJMBG.Text);

                    if (mainWindow.proveriJMBG())
                    {

                        try
                        {
                            OpenFileDialog fajl = new OpenFileDialog();
                            fajl.ShowDialog();
                            fajl.Filter = "Image Filter(*.png)|*.png|(*.jpg)|*.jpg;";
                            BitmapImage Ucenici = new BitmapImage(new Uri(fajl.FileName));


                            string p = DodajIme.Text + " " + DodajPrezime.Text;
                            mainWindow.list_ucenik.Items.Add(p);
                            foreach (Skola s in mainWindow.skole)
                            {
                                if (mainWindow.ID_Skole.Text == s.Id)
                                {
                                    s.Ucenici.Add(new Ucenik(DodajIme.Text, DodajPrezime.Text, DodajJMBG.Text, DodajAdresu.Text, Ucenici));
                                }
                            }
                            DodajIme.Text = "";
                            DodajPrezime.Text = "";
                            DodajAdresu.Text = "";
                            DodajJMBG.Text = "";
                        }
                        catch (Exception)
                        {
                            System.Windows.MessageBox.Show("Greska pri dodavanju slike.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Ucenik sa tim JMBG vec postoji.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    this.Hide();
                }
                catch
                {
                    System.Windows.MessageBox.Show("JMBG mora da sadrži samo brojeve", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
