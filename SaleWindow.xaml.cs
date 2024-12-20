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

namespace Shigapov_glazki
{
    /// <summary>
    /// Логика взаимодействия для SaleWindow.xaml
    /// </summary>
    public partial class SaleWindow : Window
    {
        public SaleWindow()
        {
            InitializeComponent();
            var currentProducts = ShigapovEntitiess.GetContext().Product.ToList();
            ComboProducts.ItemsSource = currentProducts;
        }

        private void AddSalewindawBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBSaleCount.Text))
            {
                MessageBox.Show("Укажите количество продукции");
                return;
            }
            if (Convert.ToInt32(TBSaleCount.Text) < 0)
            {
                MessageBox.Show("Количество продукции должно быть положительное");
                return;
            }
            if (ComboProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукцию");
                return;
            }
            if (SaleDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату продажи");
                return;
            }
            this.Close();
        }

        private void TBSaleCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
