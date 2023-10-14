using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;
using Projekat;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //PRVI TAB
        internal List<Skola> skole = new List<Skola>();
        string tempId = "";
        string tempJMBG = "";
        DodajSkoluPopUp d;
        IzmeniSkoluPopUp i;
        DodajUcenikaPopUp du;
        IzmeniUcenikaPopUp iu;
        izbrisiSaMape im;
        IzbrisiUcenikaSaMape ium;
        izbrisiSkrozIzAplikacijeSkolu iss;
        izbrisiSkrozIzAplikacijeUcenika isu;
        internal List<Skola> skoleNaMapi = new List<Skola>();
        internal List<Ucenik> uceniciNaMapi = new List<Ucenik>();

        public string getTempId()
        {
            return tempId;
        }

        public string getTempJMBG()
        {
            return tempJMBG;
        }
        public void dodajSkolu(string tekst)
        {
            ComboBox_Skola.Items.Add(tekst);
        }


        public MainWindow()
        {
            InitializeComponent();
            Skola s1 = new Skola("NEUPISANI", "/", "/", null);
            skole.Add(s1);
            list_ucenik.Items.Add("Dodaj ucenika");
            ComboBox_Skola.Items.Add(s1.Naziv);
            ComboboxSkoleLevo.Items.Add(s1.Naziv);
            ComboboxSkoleDesno.Items.Add(s1.Naziv);
            d = new DodajSkoluPopUp(this);
            i = new IzmeniSkoluPopUp(this);
            du = new DodajUcenikaPopUp(this);
            iu = new IzmeniUcenikaPopUp(this);
            im = new izbrisiSaMape(this);
            ium = new IzbrisiUcenikaSaMape(this);
            iss = new izbrisiSkrozIzAplikacijeSkolu(this);
            isu = new izbrisiSkrozIzAplikacijeUcenika(this);
        }

        //SKOLA
        private void btn_dodaj_Click(object sender, RoutedEventArgs e)
        {
            d.Show();
        }

        public bool proveriID()
        {
            foreach (Skola s in skole)
            {
                if (s.Id == d.DodajId.Text)
                {
                    return false;
                }
            }
            return true;
        }

        public bool proveriID2()
        {
            tempId = ID_Skole.Text;
            foreach (Skola s in skole)
            {
                if (s.Id == i.IzmeniId.Text && s.Id != tempId)
                {
                    return false;
                }
            }
            return true;
        }



        private void btn_izmeni_Click(object sender, RoutedEventArgs e)
        {
            i.IzmeniSkolu.Text = "";
            i.IzmeniAdresu.Text = "";
            i.IzmeniId.Text = "";
            tempId = ID_Skole.Text;
            i.Show();
        }


        private void btn_brisanje_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da zelite da obrisete skolu?", "Pitanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                im.listaSkolaZaIzbrisati.ItemsSource = null;
                int index = ComboBox_Skola.SelectedIndex;
                foreach (Skola sk in skole)
                {
                    if (sk.Naziv == ComboBox_Skola.SelectedValue.ToString())
                    {
                        foreach (Skola s in skole)
                        {
                            if (s.Naziv == "NEUPISANI")
                            {
                                foreach (Ucenik u in sk.Ucenici)
                                {
                                    s.Ucenici.Add(u);
                                }
                            }
                        }
                        skole.Remove(sk);
                        break;
                    }
                }
                ComboBox_Skola.SelectedIndex = 0;
                ComboboxSkoleLevo.SelectedIndex = 0;
                ComboboxSkoleDesno.SelectedIndex = 0;
                im.listaSkolaZaIzbrisati.ItemsSource = skole;
                ComboBox_Skola.Items.RemoveAt(index);
                ComboboxSkoleLevo.Items.RemoveAt(index);
                ComboboxSkoleDesno.Items.RemoveAt(index);
            }
        }

        private void ComboBox_Skola_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Skola sk in skole)
            {
                if (sk.Naziv == ComboBox_Skola.SelectedValue.ToString())
                {
                    Naziv_Skole.Text = sk.Naziv;
                    ID_Skole.Text = sk.Id;
                    tempId = sk.Id;
                    Adresa_Skole.Text = sk.Adresa;
                    slikaSkole.Source = sk.SlikaSkole;
                    if (list_ucenik.Items.Count > 0)
                    {
                        list_ucenik.SelectedValue = null;
                        if (sk.Ucenici.Count > 0)
                        {
                            list_ucenik.Items.Clear();
                            foreach (Ucenik u in sk.Ucenici)
                            {
                                string p = u.Ime + " " + u.Prezime;
                                list_ucenik.Items.Add(p);
                            }
                            list_ucenik.Items.Insert(0, "Dodaj ucenika");
                        }
                        else
                        {
                            list_ucenik.Items.Clear();
                            list_ucenik.Items.Add("Dodaj ucenika");
                            list_ucenik.SelectedIndex = 0;
                            Naziv_Ucenika.Text = "";
                            Prezime_Ucenika.Text = "";
                            JMBG_Ucenika.Text = "";
                            Adresa_Ucenika.Text = "";
                        }
                    }
                    else if (list_ucenik.Items.Count == 0)
                    {
                        list_ucenik.Items.Add("Dodaj ucenika");
                    }
                    slikaSkole.Visibility = Visibility.Visible;
                    slikaUcenika.Visibility = Visibility.Hidden;
                    list_ucenik.SelectedIndex = 0;
                    Naziv_Ucenika.Text = "";
                    JMBG_Ucenika.Text = "";
                    Adresa_Ucenika.Text = "";
                    Prezime_Ucenika.Text = "";
                    btn_brisanje.IsEnabled = true;
                    btn_izmeni.IsEnabled = true;
                    if (ComboBox_Skola.SelectedIndex == 0)
                    {
                        btn_brisanje.IsEnabled = false;
                        btn_izmeni.IsEnabled = false;
                    }
                }
            }
        }

        //UCENIK
        private void btn_dodajUcenika_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Skola.SelectedItem == null)
            {
                MessageBox.Show("Izaberite prvo skolu pre nego sto pokusate dodati ucenika.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                du.Show();
            }

        }

        public bool proveriJMBG()
        {
            foreach (Skola s in skole)
            {
                foreach (Ucenik u in s.Ucenici)
                {
                    if (s.GetUcenik(u).Jmbg == du.DodajJMBG.Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public bool proveriJMBG2()
        {
            tempJMBG = JMBG_Ucenika.Text;
            foreach (Skola s in skole)
            {
                foreach (Ucenik u in s.Ucenici)
                {
                    if (s.GetUcenik(u).Jmbg == iu.DodajJMBG.Text && s.GetUcenik(u).Jmbg != tempJMBG)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btn_izmeniUcenika_Click(object sender, RoutedEventArgs e)
        {
            iu.DodajAdresu.Text = "";
            iu.DodajIme.Text = "";
            iu.DodajJMBG.Text = "";
            iu.DodajPrezime.Text = "";
            tempJMBG = JMBG_Ucenika.Text;
            iu.Show();
        }

        private void btn_brisanjeUcenika_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da zelite da obrisete ucenika", "Pitanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int index = list_ucenik.SelectedIndex;
                foreach (Skola s in skole)
                {
                    if (s.Naziv == ComboBox_Skola.SelectedValue.ToString())
                    {
                        foreach (Ucenik u in s.Ucenici)
                        {
                            string p = u.Ime + " " + u.Prezime;
                            if (p == list_ucenik.SelectedValue.ToString()) 
                            {
                                s.Ucenici.Remove(u);
                                list_ucenik.SelectedIndex = 0;
                                list_ucenik.Items.RemoveAt(index);
                                Naziv_Ucenika.Text = "";
                                Prezime_Ucenika.Text = "";
                                slikaUcenika.Source = null;
                                Adresa_Ucenika.Text = "";
                                JMBG_Ucenika.Text = "";
                                du.DodajIme.Text = "";
                                du.DodajPrezime.Text = "";
                                du.DodajAdresu.Text = "";
                                du.DodajJMBG.Text = "";
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }


        private void list_ucenik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list_ucenik.SelectedIndex == 0)
            {
                btn_dodajUcenika.IsEnabled = true;
                btn_brisanjeUcenika.IsEnabled = false;
                btn_izmeniUcenika.IsEnabled = false;
                Naziv_Ucenika.IsEnabled = true;
                Prezime_Ucenika.IsEnabled = true;
                JMBG_Ucenika.IsEnabled = true;
                Adresa_Ucenika.IsEnabled = true;
                Naziv_Ucenika.Text = "";
                Prezime_Ucenika.Text = "";
                JMBG_Ucenika.Text = "";
                Adresa_Ucenika.Text = "";
                slikaUcenika.Visibility = Visibility.Visible;
                slikaUcenika.Visibility = Visibility.Hidden;
                slikaUcenika.IsEnabled = false;
            }
            else
            {
                slikaUcenika.IsEnabled = true;
                btn_dodajUcenika.IsEnabled = false;
                btn_brisanjeUcenika.IsEnabled = true;
                btn_izmeniUcenika.IsEnabled = true;
                Naziv_Ucenika.IsEnabled = true;
                Prezime_Ucenika.IsEnabled = true;
                JMBG_Ucenika.IsEnabled = true;
                Adresa_Ucenika.IsEnabled = true;
                slikaUcenika.Visibility = Visibility.Visible;
            }

            if (list_ucenik.SelectedValue != null)
            {
                foreach (Skola s in skole)
                {
                    foreach (Ucenik u in s.Ucenici)
                    {
                        if (s.GetUcenik(u).Ime + " " + s.GetUcenik(u).Prezime == list_ucenik.SelectedValue.ToString())
                        {
                            Naziv_Ucenika.Text = s.GetUcenik(u).Ime;
                            Prezime_Ucenika.Text = s.GetUcenik(u).Prezime;
                            JMBG_Ucenika.Text = s.GetUcenik(u).Jmbg;
                            Adresa_Ucenika.Text = s.GetUcenik(u).Adresa;
                            tempJMBG = s.GetUcenik(u).Jmbg;
                            slikaUcenika.Source = s.GetUcenik(u).SlikaUcenika;
                        }
                    }
                }
            }

        }




























        //DRUGI TAB

        private void ComboBox_PromenaSkole_Levo(object sender, SelectionChangedEventArgs e)
        {
            foreach (Skola s in skole)
            {
                if (s.Naziv == ComboboxSkoleLevo.SelectedValue.ToString())
                {
                    if (ListLeva.Items.Count > 0)
                    {
                        ListLeva.ItemsSource = new List<Ucenik>();
                    }

                    foreach (Ucenik u in s.Ucenici)
                    {
                        if (s.Ucenici.Count == 0)
                            return;
                        else
                        {
                            ListLeva.ItemsSource = s.Ucenici;
                        }
                    }
                    break;
                }
            }

        }


        private void ComboBox_PromenaSkole_Desno(object sender, SelectionChangedEventArgs e)
        {
            foreach (Skola s in skole)
            {
                if (s.Naziv == ComboboxSkoleDesno.SelectedValue.ToString())
                {
                    if (ListDesna.Items.Count > 0)
                    {
                        ListDesna.ItemsSource = new List<Ucenik>();
                    }

                    foreach (Ucenik u in s.Ucenici)
                    {
                        if (s.Ucenici.Count == 0)
                            break;
                        else
                        {
                            ListDesna.ItemsSource = s.Ucenici;
                            break;
                        }
                    }
                    break;
                }
            }
        }



        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }


        Point startPoint = new Point();
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = (ListView)sender;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                Ucenik ucenici = (Ucenik)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                DataObject dragData = new DataObject("myFormat", ucenici);
                DragDrop.DoDragDrop(listView, dragData, DragDropEffects.Move);
            }
        }


        private void ListViewLeva_Drop(object sender, DragEventArgs e)
        {
            if (ComboboxSkoleLevo.SelectedValue.ToString() == ComboboxSkoleDesno.SelectedValue.ToString())
            {
                MessageBox.Show("Ne možete prebacivati učenike u istu školu!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (e.Data.GetDataPresent("myFormat"))
            {
                Ucenik ucenici = e.Data.GetData("myFormat") as Ucenik;

                if (ListLeva.SelectedItem != null)
                {
                    foreach (Skola s in skole)
                    {
                        if (s.Naziv == ComboboxSkoleLevo.SelectedValue.ToString())
                        {
                            foreach (Ucenik u in s.Ucenici)
                            {
                                if (u.Jmbg == ucenici.Jmbg)
                                {
                                    ListLeva.ItemsSource = new List<Ucenik>();
                                    s.Ucenici.Remove(u);
                                    ListLeva.ItemsSource = s.Ucenici;
                                    foreach (Skola sk in skole)
                                    {
                                        if (sk.Naziv == ComboboxSkoleDesno.SelectedValue.ToString())
                                        {
                                            ListDesna.ItemsSource = null;
                                            sk.Ucenici.Add(u);
                                            ListDesna.ItemsSource = sk.Ucenici;
                                            return;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }

            }
        }

        private void ListViewDesna_Drop(object sender, DragEventArgs e)
        {
            if (ComboboxSkoleLevo.SelectedValue.ToString() == ComboboxSkoleDesno.SelectedValue.ToString())
            {
                MessageBox.Show("Ne možete prebacivati učenike u istu školu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (e.Data.GetDataPresent("myFormat"))
            {
                Ucenik ucenici = e.Data.GetData("myFormat") as Ucenik;

                if (ListDesna.SelectedItem != null)
                {
                    foreach (Skola s in skole)
                    {
                        if (s.Naziv == ComboboxSkoleDesno.SelectedValue.ToString())
                        {
                            foreach (Ucenik u in s.Ucenici)
                            {
                                if (u.Jmbg == ucenici.Jmbg)
                                {
                                    ListDesna.ItemsSource = new List<Ucenik>();
                                    s.Ucenici.Remove(u);
                                    ListDesna.ItemsSource = s.Ucenici;
                                    foreach (Skola sk in skole)
                                    {
                                        if (sk.Naziv == ComboboxSkoleLevo.SelectedValue.ToString())
                                        {
                                            ListLeva.ItemsSource = null;
                                            sk.Ucenici.Add(u);
                                            ListLeva.ItemsSource = sk.Ucenici;
                                            return;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }
            }
        }


































        //TRECI TAB

        private void listSkola3_Loaded(object sender, RoutedEventArgs e)
        {
            listSkola3.ItemsSource = skole;
        }

        private void listSkola3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listSkola3.SelectedItem != null)
            {
                string Utemp = ((Skola)listSkola3.SelectedItem)?.Naziv;
                foreach (Skola s in skole)
                {
                    if (s.Naziv == Utemp)
                    {
                        if (s.Ucenici.Count == 0)
                        {
                            listUcenici3.ItemsSource = null;
                            listUcenici3.Items.Clear();
                            return;
                        }
                        else
                        {
                            listUcenici3.ItemsSource = s.Ucenici;
                        }

                    }
                }
            }
        }

        //Drag and drop za Ucenike

        private static T FindAncestor2<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private Point StartPoint3 = new Point();
        private void ListView3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPoint3 = e.GetPosition(null);
        }


        private void ListView3_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint2 - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = (ListView)sender;

                foreach (Ucenik u in uceniciNaMapi)
                {
                    if (u == listUcenici3.SelectedValue as Ucenik)
                    {
                        MessageBox.Show("Učenik je vec na mapi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ListViewItem listViewItem =
                    FindAncestor2<ListViewItem>((DependencyObject)e.OriginalSource);

                Ucenik selectedUcenik = (Ucenik)listViewItem.Content;

                DataObject dragData = new DataObject("myFormatUcenik", selectedUcenik.SlikaUcenika);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }




        // Drag and drop za skole
        private static T FindAncestor1<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private Point StartPoint2 = new Point();
        private void ListView2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPoint2 = e.GetPosition(null);
        }


        private void ListView2_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint2 - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = (ListView)sender;
                if (listSkola3.SelectedIndex == 0)
                {
                    MessageBox.Show("Ne možete da preneste 'NEUPISANE' na mapu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (Skola s in skoleNaMapi)
                {
                    if (s == listSkola3.SelectedValue as Skola)
                    {
                        MessageBox.Show("Škola je već na mapi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ListViewItem listViewItem =
                    FindAncestor1<ListViewItem>((DependencyObject)e.OriginalSource);

                Skola selectedSkola = (Skola)listViewItem.Content;

                DataObject dragData = new DataObject("myFormat", selectedSkola.SlikaSkole);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void ListView2_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                ImageSource skolaSource = e.Data.GetData("myFormat") as ImageSource;
                Point dropPosition = e.GetPosition(ContainerCanvas);
                if (skolaSource != null)
                {
                    Image newImage = new Image();
                    newImage.Source = skolaSource;
                    newImage.Width = 25;
                    newImage.Height = 25;

                    Canvas.SetLeft(newImage, dropPosition.X);
                    Canvas.SetTop(newImage, dropPosition.Y);
                    Panel.SetZIndex(newImage, 1);

                    ContainerCanvas.Children.Add(newImage);
                    skoleNaMapi.Add(listSkola3.SelectedValue as Skola);
                    im.listaSkolaZaIzbrisati.ItemsSource = null;
                    im.listaSkolaZaIzbrisati.ItemsSource = skoleNaMapi;
                    iss.listaSkolaZaIzbrisati.ItemsSource = null;
                    iss.listaSkolaZaIzbrisati.ItemsSource = skoleNaMapi;
                }
            }
            else if (e.Data.GetDataPresent("myFormatUcenik"))
            {
                ImageSource ucenikSource = e.Data.GetData("myFormatUcenik") as ImageSource;
                Point dropPosition = e.GetPosition(ContainerCanvas);
                if (ucenikSource != null)
                {
                    Image newImage = new Image();
                    newImage.Source = ucenikSource;
                    newImage.Width = 25;
                    newImage.Height = 25;

                    Canvas.SetLeft(newImage, dropPosition.X);
                    Canvas.SetTop(newImage, dropPosition.Y);
                    Panel.SetZIndex(newImage, 1);

                    ContainerCanvas.Children.Add(newImage);
                    uceniciNaMapi.Add(listUcenici3.SelectedValue as Ucenik);
                    ium.listaUcenikaZaIzbrisati.ItemsSource = null;
                    ium.listaUcenikaZaIzbrisati.ItemsSource = uceniciNaMapi;
                    isu.listaUcenikaZaIzbrisati.ItemsSource = null;
                    isu.listaUcenikaZaIzbrisati.ItemsSource = uceniciNaMapi;
                }
            }
        }

        private void izbrisiSkolu(object sender, RoutedEventArgs e)
        {
            if (skoleNaMapi.Count == 0)
            {
                MessageBox.Show("Ne možete izbrisati kada ne postoji ni jedna škola na mapi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                iss.Show();
            }


        }

        private void izbrisiUcenika(object sender, RoutedEventArgs e)
        {
            if (uceniciNaMapi.Count == 0)
            {
                MessageBox.Show("Ne možete izbrisati kada ne postoji ni jedan učenik na mapi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                isu.Show();
            }
        }
        private void izbrisiSkoluIkonicu(object sender, RoutedEventArgs e)
        {
            if (skoleNaMapi.Count == 0)
            {
                MessageBox.Show("Ne možete izbrisati kada ne postoji ni jedna ikonica škole na mapi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                im.Show();
            }

        }

        private void izbrisiUcenikaIkonicu(object sender, RoutedEventArgs e)
        {
            if (uceniciNaMapi.Count == 0)
            {
                MessageBox.Show("Ne možete izbrisati kada ne postoji ni jedna ikonica učenika na mapi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ium.Show();
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filePath = "skole.txt";
            int brojacUcenika = 1;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Skola sk in skole)
                {
                    writer.WriteLine("Naziv skole: " + sk.Naziv + " Adresa: " + sk.Adresa + " ID: " + sk.Id);
                    brojacUcenika = 1;
                    writer.WriteLine("Ucenici:");
                    foreach (Ucenik uc in sk.Ucenici)
                    {
                        writer.WriteLine(brojacUcenika + ". " + "Ime: " + uc.Ime + " Prezime: " + uc.Prezime + " JMBG: " + uc.Jmbg + " Adresa: " + uc.Adresa);
                        brojacUcenika++;
                    }
                    writer.WriteLine("**********************************");
                }
                writer.Close();
            }
        }
    }
}
