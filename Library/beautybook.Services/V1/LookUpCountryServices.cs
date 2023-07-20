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
    public class LookUpCountryServices : AbstractLookUpCountryServices
    {
        private AbstractLookUpCountryDao abstractLookUpCountryDao;

        public LookUpCountryServices(AbstractLookUpCountryDao abstractLookUpCountryDao)
        {
            this.abstractLookUpCountryDao = abstractLookUpCountryDao;
        }
        public override PagedList<AbstractLookUpCountry> LookUpCountry_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpCountryDao.LookUpCountry_All(pageParam, search);
        }
    }

}