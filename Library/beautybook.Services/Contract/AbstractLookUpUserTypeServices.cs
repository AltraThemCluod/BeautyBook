using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractLookUpUserTypeServices
    {
        public abstract PagedList<AbstractLookUpUserType> LookUpUserType_All(PageParam pageParam, string search);
    }
}
