using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class LookUpStatusServices : AbstractLookUpStatusServices
    {
        private AbstractLookUpStatusDao abstractLookUpStatusDao;

        public LookUpStatusServices(AbstractLookUpStatusDao abstractLookUpStatusDao)
        {
            this.abstractLookUpStatusDao = abstractLookUpStatusDao;
        }
        public override PagedList<AbstractLookUpStatus> LookUpStatus_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpStatusDao.LookUpStatus_All(pageParam, search);
        }
    }

}