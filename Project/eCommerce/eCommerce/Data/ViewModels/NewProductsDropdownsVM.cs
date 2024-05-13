using eCommerce.Models;
using System.Collections.Generic;

namespace eCommerce.Data.ViewModels
{
    public class NewProductsDropdownsVM
    {
        public NewProductsDropdownsVM() 
        {
            Companies = new List<Company>();
            Stores = new List<Store>();
            Cities = new List<City>();
        }

        public List<Company> Companies { get; set; }
        public List<Store> Stores { get; set; }
        public List<City> Cities { get; set;  }
    }
}
