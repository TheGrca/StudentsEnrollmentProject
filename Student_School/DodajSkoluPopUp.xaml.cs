using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DodajSkoluPopUp.xaml
    /// </summary>
    public partial class DodajSkoluPopUp : Window
    {

        private MainWindow prozorce;
        public DodajSkoluPopUp(MainWindow main)
        {
            InitializeComponent();
            prozorce = main;
            
        }

        private void btnDodajSkoluPopUp_Click(object sender, RoutedEventArgs e)
        {
            if (DodajSkolu.Text == "" || DodajId.Text == "" || DodajAdresu.Text == "")
            {
                System.Windows.MessageBox.Show("Unesite sve podatke pre nego sto dodate skolu.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                if (prozorce.proveriID())
                {
                    try
                    {
                        OpenFileDialog fajl = new OpenFileDialog();
                        fajl.ShowDialog();
                        fajl.Filter = "Image Filter(*.png)|*.png;";

                        BitmapImage Skole = new BitmapImage(new Uri(fajl.FileName));
                        int br = 1;
                        prozorce.listSkola3.ItemsSource = null;
                        prozorce.ComboBox_Skola.Items.Add(DodajSkolu.Text);
                        prozorce.ComboboxSkoleLevo.Items.Add(DodajSkolu.Text);
                        prozorce.ComboboxSkoleDesno.Items.Add(DodajSkolu.Text);
                        prozorce.skole.Add(new Skola(DodajSkolu.Text, DodajId.Text, DodajAdresu.Text, Skole));
                        prozorce.listSkola3.ItemsSource = prozorce.skole;

                        string p = br + ". " + DodajSkolu.Text + ", " + DodajId.Text + ", " + DodajAdresu.Text;
                        DodajSkolu.Text = "";
                        DodajId.Text = "";
                        DodajAdresu.Text = "";
                        br++;
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Greska pri dodavanju slike.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Skola sa tim ID-em vec postoji.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Hide();
            }
        }
    }
}
