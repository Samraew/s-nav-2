using Microsoft.EntityFrameworkCore;
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

namespace WPF_NTP_DERS_SINAV2.Views
{
    /// <summary>
    /// PersonelPage.xaml etkileşim mantığı
    /// </summary>
    public partial class PersonelPage : Page
    {
        VT2022_sinav2Context veriler = new VT2022_sinav2Context();  
        Kisitur secilen_kisitur = new Kisitur(); 
        private void yenikayit_alan_temizle()
        {
            TextBox_guncelle_ad.Text = "";
            TextBox_guncelle_adres.Text = "";
            TextBox_guncelle_mail.Text = "";
            TextBox_guncelle_soyad.Text = "";
            TextBox_guncelle_telefon.Text = "";
            if (Datagrid_Personel.SelectedItem != null)
                Datagrid_Personel.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
        }
        public PersonelPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Kisiturs.Load();
            Datagrid_Personel.ItemsSource = veriler.Kisiturs.Local.ToObservableCollection();

        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;

        }

        private void Datagrid_Personel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Personel.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Kisiturs.Local.Remove(secilen_kisitur);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Personel.SelectedItem = null;
        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility=Visibility.Hidden;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Kisitur yeni_kisitur = new Kisitur();
            yeni_kisitur.Tur = TextBox_yeni_telefon.Text;
            veriler.SaveChanges();
            veriler.Kisiturs.Local.Add(yeni_kisitur);
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;

        }
    }
}
