using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPDataLayer.Repository
{
    public class ItemRepository: GenericRepository<Item>
    {
        public ItemRepository(HEPedmDatabaseEntities context)
            : base(context)
        {
        }

        public IEnumerable<GetItemListProcedure_Result> GetItemList(int langId)
        {
            return _context.GetItemListProcedure(langId).AsEnumerable();
        }
    }
}
