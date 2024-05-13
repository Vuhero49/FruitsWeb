using eCommerce.Data.Base;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Services
{
    public class CompaniesService : EntityBaseReponsitory<Company>, ICompaniesService
    {
        public CompaniesService(AppDbContext context) : base(context) { }
    }
}
