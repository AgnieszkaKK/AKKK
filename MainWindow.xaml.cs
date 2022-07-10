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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AKKK
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            refreshGrid();
        }

        public void refreshGrid()
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            var wizyty = from w in db.Widok_Wizyty
                          select w;
            /*
            select new
            {
                ID = l.Id,
                NazwiskoLekarza = l.Imie_Nazwisko,
                Specjalizacja = l.Specjalizacja,
                Kwalifikacje = l.Kwalifikacje
            };


foreach (var item in lekarze)
{
  Console.WriteLine(item.NazwiskoLekarza);
  Console.WriteLine(item.Kwalifikacje);
  Console.WriteLine(item.Specjalizacja);
}
*/
            this.gridLekarze.ItemsSource = wizyty.ToList();
        }

        private void btnDodajLekarza_Click(object sender, RoutedEventArgs e)
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            Wizyta WizytyObj = new Wizyta()
            {
                Data = (DateTime)txtData.SelectedDate,
                Pacjent = PacjentCB.SelectedIndex+1,
                Sala = SalaCB.SelectedIndex + 1,
                Lekarz = LekarzCB.SelectedIndex + 1
            };
          
            db.Wizytas.Add(WizytyObj);
            db.SaveChanges();
            refreshGrid();
        }
        private void btnDodajPacjenta_Click(object sender, RoutedEventArgs e)
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            Pacjent pacjentObj = new Pacjent()
            {
                Imie = txtImiePacjenta.Text,
                Nazwisko = txtNazwiskoPacjenta.Text,
                Telefon = txtTelefonPacjenta.Text

            };

            db.Pacjents.Add(pacjentObj);
            db.SaveChanges();
            refreshGrid();
            refreshComboBoxPacjenci();

            txtImiePacjenta.Text = "";
            txtNazwiskoPacjenta.Text = "";
            txtTelefonPacjenta.Text = "";
        }
        /*
        private void btnZaladujLekarzy_Click(object sender, RoutedEventArgs e)
        {
            refreshGrid();
        }
        private int aktualizacjaIDWizyty = 0;
        private void gridLekarze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.gridLekarze.SelectedItems.Count >= 0 && this.gridLekarze.SelectedIndex >= 0)
            {
                if (this.gridLekarze.SelectedItems[0].GetType() == typeof(Wizyta))
                {
                    Wizyta w = (Wizyta)this.gridLekarze.SelectedItems[0];
                     this.txtData = (DateTime)ww.Data;
                    this.txtImiePacjenta = ww.Imie;
                    this.txtLekarzZmiana.Text = l.Imie_Nazwisko;
                    this.txtSpecjalizacjaZmiana.Text = l.Specjalizacja;
                    this.txtKwalifikacjeZmiana.Text = l.Kwalifikacje;
                    
                    this.aktualizacjaIDWizyty = w.Id;
                }
            }
        }/*
        /*
        private void btnZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();

            var r = from l in db.Lekarzs
                    where l.Id == this.aktualizacjaIDWizyty
                    select l;
            Lekarz obiekt = r.SingleOrDefault();

            if (obiekt != null)
            {
                obiekt.Imie_Nazwisko = this.txtLekarzZmiana.Text;
                obiekt.Specjalizacja = this.txtSpecjalizacjaZmiana.Text;
                obiekt.Kwalifikacje = this.txtKwalifikacjeZmiana.Text;
                db.SaveChanges();
                refreshGrid();
            }
        }*/

        private void btnUsunZaznaczonaWizyte_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MsgBoxRezultat = MessageBox.Show("Czy na pewno chcesz usunąć wizytę z listy?",
                "Usuń wizytę",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (MsgBoxRezultat == MessageBoxResult.Yes)
            {

                DataGrid x = (DataGrid)this.FindName("gridLekarze");
                var index = x.SelectedIndex;

                SzpitalMedDBEntities db = new SzpitalMedDBEntities();
                var delW = from w in db.Wizytas
                        where w.Id == index + 1
                        select w;
                Wizyta obiekt = delW.SingleOrDefault();

                if (obiekt != null)
                {
                    db.Wizytas.Remove(obiekt);
                    db.SaveChanges();
                    
                }
            }
            refreshGrid();
        }

        private void SalaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SalaCB_Loaded(object sender, RoutedEventArgs e)
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            var Sale = from s in db.Salas
                         select s.NumerSali;

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Sale.ToList();
            comboBox.SelectedIndex = 0;
        }
        private void LekarzCB_Loaded(object sender, RoutedEventArgs e)
        {
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            var Lekarze = from l in db.Lekarzs
                       select l.Imie_Nazwisko;

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Lekarze.ToList();
            comboBox.SelectedIndex = 0;
        }
        private void LekarzCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void PacjentCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void PacjentCB_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            var Pacjenci = (from p in db.Pacjents select p.Nazwisko);

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Pacjenci.ToList();
            comboBox.SelectedIndex = 0;
            */
            refreshComboBoxPacjenci();
        }
        public void refreshComboBoxPacjenci()
        {
            PacjentCB.ItemsSource = "";
            SzpitalMedDBEntities db = new SzpitalMedDBEntities();
            var Pacjenci = (from p in db.Pacjents select p.Nazwisko);
            PacjentCB.ItemsSource = Pacjenci.ToList();
            PacjentCB.SelectedIndex = 0;
        }
    }
}
