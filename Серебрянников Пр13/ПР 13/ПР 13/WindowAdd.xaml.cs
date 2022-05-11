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
using ПР_13.Classes;

namespace ПР_13
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        int mode;
        public WindowAdd()
        {
            InitializeComponent();
            mode = 0;
        }
        public WindowAdd(Price price)
        {
            InitializeComponent();
            TxbName.Text = price.NameProduct;
            Txbshop.Text = price.NameShop;
            TxbPrice.Text = price.PriceProduct.ToString();
            TxbCount.Text = price.CountProduct.ToString();
            mode = 1;
            BtnAddProduct.Content = "Сохранить";
        }
        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)
            {
                try
                {
                    Price price = new Price()
                    {
                        NameProduct = TxbName.Text,
                        NameShop = Txbshop.Text,
                        PriceProduct = double.Parse(TxbPrice.Text),
                        CountProduct = int.Parse(TxbCount.Text)
                    };
                    ConnectHelper.prices.Add(price);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные!\n\n{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < ConnectHelper.prices.Count; i++)
                    {
                        if (ConnectHelper.prices[i].NameProduct == TxbName.Text)
                        {
                            ConnectHelper.prices[i].NameShop = TxbCount.Text;
                            ConnectHelper.prices[i].PriceProduct = double.Parse(TxbPrice.Text);
                            ConnectHelper.prices[i].CountProduct = int.Parse(TxbCount.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные!\n\n{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ConnectHelper.SaveListToFile(@"ListProduct.txt");
            this.Close();
        }
    }
}
