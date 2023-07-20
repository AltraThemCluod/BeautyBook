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
    public abstract class AbstractLookUpStatusServices
    {
        public abstract PagedList<AbstractLookUpStatus> LookUpStatus_All(PageParam pageParam, string search);
    }
}
