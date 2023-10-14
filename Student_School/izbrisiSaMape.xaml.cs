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
    /// Interaction logic for izbrisiSaMape.xaml
    /// </summary>
    public partial class izbrisiSaMape : Window
    {
        private MainWindow mainWindow;
        public izbrisiSaMape(MainWindow main)
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

            for (int i = mainWindow.skoleNaMapi.Count - 1; i >= 0; i--)
            {
                Skola s = mainWindow.skoleNaMapi[i];
                if (sIzbrisati == s)
                {
                    mainWindow.skoleNaMapi.Remove(s);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listaSkolaZaIzbrisati.SelectedItem == null)
            {
               if(MessageBox.Show("Niste izabrali ni jednu školu, da li želite da izadjete iz prozora?","Izlazak?",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Hide();
                }
            }
            else
            {
                UkloniSkolu();
            }
            Hide();
        }
    }
}
