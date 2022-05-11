using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ПР_13.Classes
{
    class ConnectHelper
    {
        public static List<Price> prices = new List<Price>();
        public static string fileName;
        public static void ReadListFromFile(string filename)
        {
            try
            {
                StreamReader streamReader = new StreamReader(filename, Encoding.UTF8);
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] items = line.Split(';');
                    Price prise = new Price()
                    {
                        NameProduct = items[0].Trim(),
                        NameShop = items[1].Trim(),
                        PriceProduct = double.Parse(items[2].Trim()),
                        CountProduct = int.Parse(items[3].Trim())
                    };
                    prices.Add(prise);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void SaveListToFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            foreach (Price ph in prices)
            {
                streamWriter.WriteLine($"{ph.NameProduct};{ph.NameShop};{ph.PriceProduct};{ph.CountProduct}");
            }
            streamWriter.Close();
        }
    }
}
