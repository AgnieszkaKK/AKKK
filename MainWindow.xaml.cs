﻿using System;
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
            SzpitalDBEntities db = new SzpitalDBEntities();
            var lekarze = from l in db.Lekarzs
                          select l;
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
            refreshGrid();
        }

        private void btnZaladujLekarzy_Click(object sender, RoutedEventArgs e)
        {
            refreshGrid();
        }
        private int aktualizacjaIDLekarza = 0;
        private void gridLekarze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.gridLekarze.SelectedItems.Count >= 0 && this.gridLekarze.SelectedIndex >= 0)
            {
                if (this.gridLekarze.SelectedItems[0].GetType() == typeof(Lekarz))
                {
                    Lekarz l = (Lekarz)this.gridLekarze.SelectedItems[0];
                    this.txtLekarzZmiana.Text = l.Imie_Nazwisko;
                    this.txtSpecjalizacjaZmiana.Text = l.Specjalizacja;
                    this.txtKwalifikacjeZmiana.Text = l.Kwalifikacje;
                    this.aktualizacjaIDLekarza = l.Id;
                }
            }
        }

        private void btnZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            SzpitalDBEntities db = new SzpitalDBEntities();

            var r = from l in db.Lekarzs
                    where l.Id == this.aktualizacjaIDLekarza
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
        }

        private void btnUsunZaznaczonegoLekarz(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MsgBoxRezultat = MessageBox.Show("Czy na pewno chcesz usunąć lekarza z listy?",
                "Usuń lekarza",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            if (MsgBoxRezultat == MessageBoxResult.Yes)
            {
                SzpitalDBEntities db = new SzpitalDBEntities();
                var r = from l in db.Lekarzs
                        where l.Id == this.aktualizacjaIDLekarza
                        select l;
                Lekarz obiekt = r.SingleOrDefault();

                if (obiekt != null)
                {
                    db.Lekarzs.Remove(obiekt);
                    db.SaveChanges();
                    refreshGrid();
                }
            }

        }
    }
}
