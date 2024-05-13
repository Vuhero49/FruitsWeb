using eCommerce.Data.Base;
using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public class StoresService : EntityBaseReponsitory<Store>, IStoresService
    {
        public StoresService(AppDbContext context) : base(context) 
        {

        }
    }
}
