using eCommerce.Data.Base;
using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public class CitiesService : EntityBaseReponsitory<City>, ICitiesService
    {
        public CitiesService(AppDbContext context) : base(context) { }
    }
}
