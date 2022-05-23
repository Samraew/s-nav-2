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
    /// IslemlerPage.xaml etkileşim mantığı
    /// </summary>
    public partial class IslemlerPage : Page
    {
        VT2022_sinav2Context veriler = new VT2022_sinav2Context();
        Islem secilen_ısl = new Islem();
        private void yenikayit_alan_temizle()
        {
           TextBox_yeni_tarih.Text = "";          
            if (Datagrid_Islemler.SelectedItem != null)
                Datagrid_Islemler.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
            ComboBox_FilterHizmet.SelectedIndex = -1;
            ComboBox_FilterMusteri.SelectedIndex = -1;
            ComboBox_FilterOdeme.SelectedIndex = -1;
            ComboBox_FilterPersonel.SelectedIndex = -1;
        }
        public IslemlerPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Islems.Load();                                 
            Datagrid_Islemler.ItemsSource = veriler.Islems.Local.ToObservableCollection();
           
           
        }

        private void Datagrid_Islemler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid_Islemler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_ısl = (Islem)Datagrid_Islemler.SelectedItem;
                TextBox_guncelle_tarih.Text = secilen_ısl.Tarih.ToString();
                
                ComboBox_guncelle_musteri.SelectedItem = secilen_ısl.MusteriNavigation;
                ComboBox_guncelle_personel.SelectedItem = secilen_ısl.PersonelNavigation;
                ComboBox_guncelle_hizmet.SelectedItem = secilen_ısl.HizmetNavigation;
                
                Button_yenikayit.IsEnabled = true;
            }
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;
        }

        private void ComboBox_FilterMusteri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_FilterPersonel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_TumMusteriGoster_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_TumPersonelGoster_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_FilterHizmet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBox_FilterMusteri.SelectedIndex == -1)
            {
                CheckBox_TumHizmetGoster.IsEnabled = true ;
                Int32 secilen = (Int32)ComboBox_FilterHizmet.SelectedValue;
                Datagrid_Islemler.ItemsSource = veriler.Hizmets.Local.ToObservableCollection().Where(x => x.Fiyat == secilen);
            }
        }

        private void ComboBox_FilterOdeme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_TumHizmetGoster_Checked(object sender, RoutedEventArgs e)
        {
         
        }

        private void CheckBox_TumOdemeGoster_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Islemler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Islems.Local.Remove(secilen_ısl);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }


        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_ısl.Tarih = TextBox_guncelle_tarih.Text;
            
           
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Islemler.SelectedItem = null;
            Datagrid_Islemler.Items.Refresh();
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Islemler.SelectedItem = null;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Islem yeni_ısm = new Islem();
            yeni_ısm.Tarih = TextBox_yeni_tarih.Text;
            
            veriler.Islems.Local.Add(yeni_ısm);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }
    }
}
