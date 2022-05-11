using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР_13.Classes
{
    public class Price
    {
        string nameProduct;
        string nameShop;
        double priceProduct;
        int countProduct;

        //свойства
        public string NameProduct
        {
            get { return nameProduct; }
            set { nameProduct = value; }
        }
        public string NameShop
        {
            get { return nameShop; }
            set { nameShop = value; }
        }
        public double PriceProduct
        {
            get { return priceProduct; }
            set { priceProduct = value; }
        }
        public int CountProduct
        {
            get { return countProduct; }
            set { countProduct = value; }
        }
        //конструкторы
        public Price()
        {
            nameProduct = "";
            nameShop = "";
            priceProduct = 0.0;
            countProduct = 0;
        }
        public Price(string name, string nameSh, double price, int count)
        {
            nameProduct = name;
            nameShop = nameSh;
            priceProduct = price;
            countProduct = count;
        }
    }
}
