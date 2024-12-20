using Microsoft.Win32;
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

namespace Shigapov_glazki
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;
            }

            DataContext = _currentAgent;

        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (_currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(" ", "").Replace(")", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgent.ID == 0)
                ShigapovEntitiess.GetContext().Agent.Add(_currentAgent);

            _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

            try
            {
                ShigapovEntitiess.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;
            var currentDell = ShigapovEntitiess.GetContext().ProductSale.ToList();
            currentDell = currentDell.Where(p => p.Agent.ID == currentAgent.ID).ToList();
            if (currentDell.Count != 0)
            {
                MessageBox.Show("Невозможно удалить агента");
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ShigapovEntitiess.GetContext().Agent.Remove(currentAgent);
                        ShigapovEntitiess.GetContext().SaveChanges();
                        Manager.MainFrame.Navigate(new AgentPage());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }
            }
        }

        private void ChangePictureBtn_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Prodazi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            SaleWindow myWindow = new SaleWindow();
            myWindow.ShowDialog();
            ProductSale newSale = new ProductSale();
            newSale.SaleDate = Convert.ToDateTime(myWindow.SaleDate.Text);
            var currentProducts = ShigapovEntitiess.GetContext().Product.ToList();
            //var currentProduct = currentProducts.Where(p => p.Title == myWindow.ComboProducts.SelectedItem).ToList()[0];
            newSale.ProductID = myWindow.ComboProducts.SelectedIndex + 1;
            newSale.ProductCount = Convert.ToInt32(myWindow.TBSaleCount.Text.ToString());
            newSale.AgentID = _currentAgent.ID;
            ShigapovEntitiess.GetContext().ProductSale.Add(newSale);
            try
            {
                ShigapovEntitiess.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                var currentSales = ShigapovEntitiess.GetContext().ProductSale.ToList();
                var currentSale = currentSales.Where(p => p.AgentID == _currentAgent.ID).ToList()[0];
                //HakimovGlaskiSaveEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ShigapovEntitiess.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SaleListBox.ItemsSource = ShigapovEntitiess.GetContext().ProductSale.ToList().Where(p => p.AgentID == _currentAgent.ID).ToList();
                //Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SaleListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выбери продажу");
                return;
            }
            if (SaleListBox.SelectedItems.Count == 1)
            {
                var currentSales = ShigapovEntitiess.GetContext().ProductSale.ToList();
                var currentSale = currentSales.Where(p => p.AgentID == _currentAgent.ID).ToList()[SaleListBox.SelectedIndex];
                ShigapovEntitiess.GetContext().ProductSale.Remove(currentSale);
                ShigapovEntitiess.GetContext().SaveChanges();
                MessageBox.Show("Продажа удалена");
                ShigapovEntitiess.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SaleListBox.ItemsSource = ShigapovEntitiess.GetContext().ProductSale.ToList().Where(p => p.AgentID == _currentAgent.ID).ToList();
                return;
            }
            else
            {
                MessageBox.Show("Выбери 1 продажу");
                return;
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ShigapovEntitiess.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SaleListBox.ItemsSource = ShigapovEntitiess.GetContext().ProductSale.ToList().Where(p => p.AgentID == _currentAgent.ID).ToList();
            }
        }
    }
}
