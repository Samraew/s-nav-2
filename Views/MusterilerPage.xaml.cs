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
    /// MusterilerPage.xaml etkileşim mantığı
    /// </summary>
    public partial class MusterilerPage : Page
    {
        VT2022_sinav2Context veriler = new VT2022_sinav2Context();
        Kisi secilen_kisi = new Kisi();
        private void yenikayit_alan_temizle()
        {
            TextBox_guncelle_ad.Text = "";
            TextBox_guncelle_adres.Text = "";
            TextBox_guncelle_mail.Text = "";
            TextBox_guncelle_soyad.Text = "";
            TextBox_guncelle_telefon.Text = "";
            if (Datagrid_Musteriler.SelectedItem != null)
                Datagrid_Musteriler.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
        }

        public MusterilerPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Kisis.Load();
            Datagrid_Musteriler.ItemsSource = veriler.Kisis.Local.ToObservableCollection();        
        }

        private void Datagrid_Musteriler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid_Musteriler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_kisi = (Kisi)Datagrid_Musteriler.SelectedItem;
                TextBox_guncelle_ad.Text = secilen_kisi.Ad;
                TextBox_guncelle_adres.Text = secilen_kisi.Adres;
                TextBox_guncelle_mail.Text = secilen_kisi.Mail;
                TextBox_guncelle_soyad.Text = secilen_kisi.Soyad;
                TextBox_guncelle_telefon.Text = secilen_kisi.Tel.ToString(); 
                
                Button_yenikayit.IsEnabled = true;
            }
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Kisi yeni_mst = new Kisi();
            yeni_mst.Tel = TextBox_yeni_telefon.Text.ToString();
            yeni_mst.Ad = TextBox_yeni_ad.Text;
            yeni_mst.Soyad = TextBox_yeni_soyad.Text;
            yeni_mst.Adres = TextBox_yeni_adres.Text;
            yeni_mst.Mail = TextBox_yeni_mail.Text;

            veriler.Kisis.Local.Add(yeni_mst);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Musteriler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Kisis.Local.Remove(secilen_kisi);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Musteriler.SelectedItem = null;

        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_kisi.Tel = TextBox_guncelle_telefon.Text.ToString();
            secilen_kisi.Ad = TextBox_guncelle_ad.Text;
            secilen_kisi.Soyad = TextBox_guncelle_soyad.Text;
            secilen_kisi.Mail = TextBox_guncelle_mail.Text;
            secilen_kisi.Adres = TextBox_guncelle_adres.Text;
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Musteriler.SelectedItem = null;
            Datagrid_Musteriler.Items.Refresh();
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;
        }
    }
}
