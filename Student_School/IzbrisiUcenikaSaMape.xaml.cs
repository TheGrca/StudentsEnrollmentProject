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
    /// Interaction logic for IzbrisiUcenikaSaMape.xaml
    /// </summary>
    public partial class IzbrisiUcenikaSaMape : Window
    {
        private MainWindow mainWindow;
        public IzbrisiUcenikaSaMape(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }


        private void UkloniUcenika()
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

            for (int i = mainWindow.uceniciNaMapi.Count - 1; i >= 0; i--)
            {
                Ucenik u = mainWindow.uceniciNaMapi[i];
                if (sIzbrisati == u)
                {
                    mainWindow.uceniciNaMapi.Remove(u);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaUcenikaZaIzbrisati.SelectedItem == null)
            {
                if (MessageBox.Show("Niste izabrali ni jednog učenika, da li želite da izadjete iz prozora?", "Izlazak?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Hide();
                }
            }
            else
            {
                UkloniUcenika();
            }
            Hide();
        }
    }
}
