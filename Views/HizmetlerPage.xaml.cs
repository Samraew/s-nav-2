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
    /// HizmetlerPage.xaml etkileşim mantığı
    /// </summary>
    public partial class HizmetlerPage : Page
    {
        VT2022_sinav2Context veriler = new VT2022_sinav2Context();
        Hizmet secilen_hzm = new Hizmet();
        private void yenikayit_alan_temizle()
        {
            TextBox_yeni_hizmetAd.Text = "";
            TextBox_yeni_fiyat.Text = "";
            
            if (Datagrid_Hizmetler.SelectedItem != null)
                Datagrid_Hizmetler.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
        }
        public HizmetlerPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Hizmets.Load();
            Datagrid_Hizmetler.ItemsSource = veriler.Hizmets.Local.ToObservableCollection();
        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Hizmetler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Hizmets.Local.Remove(secilen_hzm);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_hzm.Fiyat = Int32.Parse(TextBox_guncelle_fiyat.Text);
            secilen_hzm.Hizmetad = TextBox_guncelle_hizmetAd.Text;
            
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Hizmetler.SelectedItem = null;
            Datagrid_Hizmetler.Items.Refresh();
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Hizmetler.SelectedItem = null;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Hizmet yeni_hizmet = new Hizmet();
            yeni_hizmet.Fiyat = Int32.Parse(TextBox_yeni_fiyat.Text);
            yeni_hizmet.Hizmetad = TextBox_yeni_hizmetAd.Text;
           
            veriler.Hizmets.Local.Add(yeni_hizmet);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;
        }

        private void Datagrid_Hizmetler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid_Hizmetler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_hzm = (Hizmet)Datagrid_Hizmetler.SelectedItem;
                TextBox_guncelle_fiyat.Text = secilen_hzm.Fiyat.ToString();
                TextBox_guncelle_hizmetAd.Text = secilen_hzm.Hizmetad;
                
                Button_yenikayit.IsEnabled = true;
            }
        }
    }
}
