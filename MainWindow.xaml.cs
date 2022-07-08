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

            SzpitalDBEntities db = new SzpitalDBEntities();
            var lekarze = from l in db.Lekarzs
                          select new
                          {
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
            this.gridLekarze.ItemsSource = lekarze.ToList();
        }

        private void btnDodajLekarza_Click(object sender, RoutedEventArgs e)
        {
            SzpitalDBEntities db = new SzpitalDBEntities();
            Lekarz lekarzObiekt = new Lekarz()
            {
                Imie_Nazwisko = txtLekarz.Text,
                Kwalifikacje = txtKwalifikacje.Text,
                Specjalizacja = txtSpecjalizacja.Text
            };
            db.Lekarzs.Add(lekarzObiekt);
            db.SaveChanges();
        }

        private void btnZaladujLekarzy_Click(object sender, RoutedEventArgs e)
        {
            SzpitalDBEntities db = new SzpitalDBEntities();

            this.gridLekarze.ItemsSource = db.Lekarzs.ToList();
        }
    }
}
