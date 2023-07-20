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
    public abstract class AbstractEmployeePermissionDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractEmployeeModulePermission> EmployeeModulePermission_All(PageParam pageParam, string search , long EmployeeId);
        public abstract PagedList<AbstractEmployeePermissionData> EmployeePermissionData_EmployeeId(PageParam pageParam, string search , long EmployeeId,long LookUpUserTypeId);
        public abstract SuccessResult<AbstractEmployeePermissionData> EmployeePermissionData_Upsert(AbstractEmployeePermissionData abstractEmployeePermissionData);
        public abstract bool EmployeePermissionData_Delete(long EmployeeId);
    }
}
