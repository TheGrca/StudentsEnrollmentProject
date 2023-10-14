using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for izbrisiSkrozIzAplikacijeSkolu.xaml
    /// </summary>
    public partial class izbrisiSkrozIzAplikacijeSkolu : Window
    {
        private MainWindow mainWindow;
        private izbrisiSaMape im;
        public izbrisiSkrozIzAplikacijeSkolu(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        public void UkloniSkolu()
        {
            Skola sIzbrisati = listaSkolaZaIzbrisati.SelectedItem as Skola;
            List<Image> zaIzbaciti = new List<Image>();
            foreach (var i in mainWindow.ContainerCanvas.Children)
            {
                if (i is Image image)
                {
                    if (image.Source is BitmapImage bi)
                    {
                        if (bi.UriSource == sIzbrisati.SlikaSkole.UriSource)
                        {
                            zaIzbaciti.Add(image);
                        }
                    }
                }
            }

            foreach (var izbaci in zaIzbaciti)
            {
                mainWindow.ContainerCanvas.Children.Remove(izbaci);
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UkloniSkolu();
            List<Skola> zaIzbaciti = new List<Skola>();
            mainWindow.listSkola3.ItemsSource = new List<Skola>();

            foreach (Skola s in mainWindow.skole)
            {
                if (mainWindow.skoleNaMapi.Count > 0)
                {
                    foreach (Skola sk in mainWindow.skoleNaMapi)
                    {
                        if (sk.Id == s.Id)
                        {
                            zaIzbaciti.Add(s);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Niste izabrali ni jednu školu, da li želite da izadjete iz prozora?", "Izlazak?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Hide();
                    }
                }
            }

          foreach(Skola s in zaIzbaciti)
            {
                mainWindow.skoleNaMapi.Remove(s);
            }

            foreach (Skola s in zaIzbaciti)
            {
                foreach (Skola sk in mainWindow.skole)
                {
                    if (sk.Naziv == s.Naziv)
                    {
                        foreach (Skola skole in mainWindow.skole)
                        {
                            if (skole.Naziv == "NEUPISANI")
                            {
                                foreach (Ucenik u in sk.Ucenici)
                                {
                                    skole.Ucenici.Add(u);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
                mainWindow.skole.Remove(s);
                mainWindow.ComboboxSkoleDesno.SelectedIndex = 0;
                mainWindow.ComboboxSkoleLevo.SelectedIndex = 0;
                mainWindow.ComboBox_Skola.SelectedIndex = 0;
                mainWindow.ComboboxSkoleDesno.Items.Remove(s.Naziv);
                mainWindow.ComboboxSkoleLevo.Items.Remove(s.Naziv);
                mainWindow.ComboBox_Skola.Items.Remove(s.Naziv);
            }
            mainWindow.listSkola3.ItemsSource = mainWindow.skole;
            mainWindow.listSkola3.SelectedIndex = 0;
            mainWindow.skoleNaMapi.Clear();
            mainWindow.listSkola3.ItemsSource = mainWindow.skole;

            Hide();
        }

    }
}
