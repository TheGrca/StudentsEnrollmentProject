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
    /// Interaction logic for izbrisiSkrozIzAplikacijeUcenika.xaml
    /// </summary>
    public partial class izbrisiSkrozIzAplikacijeUcenika : Window
    {
        private MainWindow mainWindow;
       // private IzbrisiUcenikaSaMape ium;
        public izbrisiSkrozIzAplikacijeUcenika(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        public void UkloniUcenika()
        {
            Ucenik sIzbrisati = listaUcenikaZaIzbrisati.SelectedItem as Ucenik;
            List<Image> zaIzbaciti = new List<Image>();
            foreach (var i in mainWindow.ContainerCanvas.Children)
            {
                if (i is Image image)
                {
                    if (image.Source is BitmapImage bi)
                    {
                        if (bi.UriSource == sIzbrisati.SlikaUcenika.UriSource)
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
            UkloniUcenika();
            List<Ucenik> zaIzbacitiUcenika = new List<Ucenik>();
            mainWindow.listUcenici3.ItemsSource = new List<Ucenik>();
            foreach (Ucenik u in mainWindow.uceniciNaMapi)
            {
                if (mainWindow.uceniciNaMapi.Count > 0)
                {
                    foreach (Ucenik uk in mainWindow.uceniciNaMapi)
                    {
                        if (uk.Jmbg == u.Jmbg)
                        {
                            zaIzbacitiUcenika.Add(u);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Niste izabrali ni jednog učenika, da li želite da izadjete iz prozora?", "Izlazak?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Hide();
                    }
                }
            }
            Skola skola = mainWindow.listSkola3.SelectedValue as Skola;
            List<Ucenik> uK = new List<Ucenik>();
            foreach (Skola s in mainWindow.skole)
            {
                if (s == mainWindow.listSkola3.SelectedValue as Skola)
                {
                    foreach (Ucenik u in zaIzbacitiUcenika)
                    {
                        foreach (Ucenik uc in s.Ucenici)
                        {
                            if (u.Jmbg == uc.Jmbg)
                            {
                                uK.Add(u);
                            }
                        }
                    }
                    break;
                }
               
                mainWindow.list_ucenik.SelectedIndex = 0;
                mainWindow.ComboboxSkoleDesno.SelectedIndex = 0;
                mainWindow.ComboboxSkoleLevo.SelectedIndex = 0;
            }
            foreach(Ucenik u in uK)
            {
                skola.Ucenici.Remove(u);
            }


            mainWindow.listSkola3.SelectedIndex = 0;
            mainWindow.uceniciNaMapi.Clear();
            Hide();
        }
    }
}
