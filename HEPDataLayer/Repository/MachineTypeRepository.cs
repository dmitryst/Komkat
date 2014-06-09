using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPDataLayer.Repository
{
    public class MachineTypeRepository : GenericRepository<MachineType>
    {
        public MachineTypeRepository(HEPedmDatabaseEntities context)
            : base(context)
        {
        }

        public IEnumerable<MachineType> GetAllMachineTypeByLang(int lang)
        {
            return _context.MachineTypes.Where(x => x.LangId == lang).AsEnumerable();
        }
    }
}
