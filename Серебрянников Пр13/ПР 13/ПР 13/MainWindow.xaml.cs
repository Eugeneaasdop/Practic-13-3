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
using ПР_13.Classes;
using Microsoft.Win32;

namespace ПР_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //загрузка данных из файла
            ConnectHelper.ReadListFromFile(@"ListProduct.txt");
            DtgListProduct.ItemsSource = ConnectHelper.prices;
            txbcount.Text = ConnectHelper.prices.Count().ToString();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListProduct.ItemsSource = ConnectHelper.prices.ToList();
            DtgListProduct.SelectedIndex = -1;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgListProduct.ItemsSource = ConnectHelper.prices.Where(x =>
                x.NameProduct.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd windowAdd = new WindowAdd();
            windowAdd.ShowDialog();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DtgListProduct.ItemsSource = null;
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgListProduct.ItemsSource = ConnectHelper.prices.OrderBy(x => x.NameProduct).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgListProduct.ItemsSource = ConnectHelper.prices.OrderByDescending(x => x.NameProduct).ToList();
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CmbFiltr.SelectedIndex == 0)
            {
                DtgListProduct.ItemsSource = ConnectHelper.prices.Where(x =>
                    x.CountProduct >= 0 && x.CountProduct <= 10).ToList();
                MessageBox.Show("Недостаточное количество товара на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                if (CmbFiltr.SelectedIndex == 1)
            {
                DtgListProduct.ItemsSource = ConnectHelper.prices.Where(x =>
                    x.CountProduct >= 11 && x.CountProduct <= 50).ToList();
                MessageBox.Show("Необходимо пополнить запасы товаров в ближайшее время",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DtgListProduct.ItemsSource = ConnectHelper.prices.Where(x =>
                   x.CountProduct >= 51).ToList();
                MessageBox.Show("Достаточное количество товара на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd windowAdd = new WindowAdd((sender as Button).DataContext as Price);
            windowAdd.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                int ind = DtgListProduct.SelectedIndex;
                ConnectHelper.prices.RemoveAt(ind);
                DtgListProduct.ItemsSource = ConnectHelper.prices.ToList();
                ConnectHelper.SaveListToFile(@"ListProduct.txt");
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                // получаем выбранный файл
                ConnectHelper.fileName = openFileDialog.FileName;
                ConnectHelper.ReadListFromFile(ConnectHelper.fileName);
                //ConnectHelper.ReadListFromFile(@"ListPreparates.txt");
            }
            else
                return;


            DtgListProduct.ItemsSource = ConnectHelper.prices.ToList();
        }
        /// <summary>
        /// открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
            {
                string file = saveFileDialog.FileName;
                ConnectHelper.SaveListToFile(file);
            }
        }
    }
}
