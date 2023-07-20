using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Data.Contract
{
    public abstract class AbstractLookUpCountryDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractLookUpCountry> LookUpCountry_All(PageParam pageParam, string search);
    }
}
