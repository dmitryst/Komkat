using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPDataLayer.Repository
{
    public class ModelRepository : GenericRepository<Model>
    {
        public ModelRepository(HEPedmDatabaseEntities context)
            : base(context)
        {
        }

        public IEnumerable<Model> GetAllModels()
        {
            return _context.Models.AsEnumerable();
        }

        public IEnumerable<Model> GetAllModelsWithInclude()
        {
            return _context.Models.Include("Brand").AsEnumerable();
        }

        public IEnumerable<Model> GetModelListByBrand(int brandId)
        {
            return _context.Models.Where(x => x.BrandId == brandId).OrderBy(x => x.ModelName).AsEnumerable();
        }

        public IEnumerable<Model> GetModelListByTypeAndBrand(MachineTypeEnum type, int brandId)
        {
            return _context.Models.Where(x => x.BrandId == brandId && x.MachineTypeId == type).OrderBy(x => x.ModelName).AsEnumerable();
        }
    }
}
