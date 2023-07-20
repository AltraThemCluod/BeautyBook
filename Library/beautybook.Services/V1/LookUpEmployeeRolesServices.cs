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
    public class LookUpEmployeeRolesServices : AbstractLookUpEmployeeRolesServices
    {
        private AbstractLookUpEmployeeRolesDao abstractLookUpEmployeeRolesDao;

        public LookUpEmployeeRolesServices(AbstractLookUpEmployeeRolesDao abstractLookUpEmployeeRolesDao)
        {
            this.abstractLookUpEmployeeRolesDao = abstractLookUpEmployeeRolesDao;
        }
        public override PagedList<AbstractLookUpEmployeeRoles> LookUpEmployeeRoles_All(PageParam pageParam, string search,string IsLanguage)
        {
            return this.abstractLookUpEmployeeRolesDao.LookUpEmployeeRoles_All(pageParam, search, IsLanguage);
        }
    }

}