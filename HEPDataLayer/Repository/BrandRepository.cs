using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPDataLayer.Repository
{
    public class BrandRepository : GenericRepository<Brand>
    {
        public BrandRepository(HEPedmDatabaseEntities context)
            : base(context)
        {
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brands.AsEnumerable();
        }

        public IEnumerable<Brand> GetBrandByType(MachineTypeEnum type)
        {
            return _context.Brands.Where(b => b.Models.Any(m => m.MachineTypeId == type)).AsEnumerable();
        }

        

    }
}
