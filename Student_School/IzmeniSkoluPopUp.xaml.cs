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
    /// Interaction logic for IzmeniSkoluPopUp.xaml
    /// </summary>
    public partial class IzmeniSkoluPopUp : Window
    {
        MainWindow mainWindow;
        public IzmeniSkoluPopUp(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void btnDodajSkoluPopUp_Click(object sender, RoutedEventArgs e)
        {
            if (IzmeniSkolu.Text == "" || IzmeniId.Text == "" || IzmeniAdresu.Text == "")
            {
                System.Windows.MessageBox.Show("Nedostaju Vam podaci za izmenu.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (mainWindow.proveriID2())
                {
                    if (System.Windows.MessageBox.Show("Da li zelite da promenite ikonicu?", "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpenFileDialog fajl = new OpenFileDialog();
                        fajl.ShowDialog();
                        fajl.Filter = "Image Filter(*.png)|*.png;";

                        BitmapImage Skole = new BitmapImage(new Uri(fajl.FileName));
                    
                                
                                foreach (Skola s in mainWindow.skole)
                                {
                                    if (s.Id == mainWindow.getTempId())
                                    {
                                        s.Naziv = IzmeniSkolu.Text;
                                        s.Id = IzmeniId.Text;
                                        s.Adresa = IzmeniAdresu.Text;
                                        s.SlikaSkole = Skole;
                                    break;
                                    }
                                }
                    }
                    else
                    {
                        foreach (Skola s in mainWindow.skole)
                        {
                            if (s.Id == mainWindow.getTempId())
                            {
                                s.Naziv = IzmeniSkolu.Text;
                                s.Id = IzmeniId.Text;
                                s.Adresa = IzmeniAdresu.Text;
                                break;
                            }
                        }
                    }
                            int index = mainWindow.ComboBox_Skola.SelectedIndex;
                            string imeSkole = IzmeniSkolu.Text;
                            mainWindow.ComboBox_Skola.SelectedIndex = 0;
                            mainWindow.ComboboxSkoleLevo.SelectedIndex = 0;
                            mainWindow.ComboboxSkoleDesno.SelectedIndex = 0;
                            mainWindow.ComboBox_Skola.Items.RemoveAt(index);
                            mainWindow.ComboboxSkoleLevo.Items.RemoveAt(index);
                            mainWindow.ComboboxSkoleDesno.Items.RemoveAt(index);
                            mainWindow.ComboBox_Skola.Items.Add(imeSkole);
                            mainWindow.ComboboxSkoleLevo.Items.Add(imeSkole);
                            mainWindow.ComboboxSkoleDesno.Items.Add(imeSkole);
                            mainWindow.listSkola3.ItemsSource = mainWindow.skole;
                            this.Hide();
                }
                else
                {
                    System.Windows.MessageBox.Show("Skola vec postoji sa tim ID");
                }

            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            IzmeniSkolu.Text = mainWindow.Naziv_Skole.Text;
            IzmeniId.Text = mainWindow.ID_Skole.Text;
            IzmeniAdresu.Text = mainWindow.Adresa_Skole.Text;
        }
    }
    
}
