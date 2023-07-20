using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Newtonsoft.Json;

namespace BeautyBook.Services.V1
{
    public class EmployeePermissionServices : AbstractEmployeePermissionServices
    {
        private AbstractEmployeePermissionDao abstractEmployeePermissionDao;

        public EmployeePermissionServices(AbstractEmployeePermissionDao abstractEmployeePermissionDao)
        {
            this.abstractEmployeePermissionDao = abstractEmployeePermissionDao;
        }

        public override PagedList<AbstractEmployeeModulePermission> EmployeeModulePermission_All(PageParam pageParam, string search , long EmployeeId)
        {
            return this.abstractEmployeePermissionDao.EmployeeModulePermission_All(pageParam, search , EmployeeId);
        }
        
        public override PagedList<AbstractEmployeePermissionData> EmployeePermissionData_EmployeeId(PageParam pageParam, string search , long EmployeeId,long LookUpUserTypeId)
        {
            return this.abstractEmployeePermissionDao.EmployeePermissionData_EmployeeId(pageParam, search , EmployeeId, LookUpUserTypeId);
        }

        public override SuccessResult<AbstractEmployeePermissionData> EmployeePermissionData_Upsert(AbstractEmployeePermissionJsonData abstractEmployeePermissionJsonData)
        {
            SuccessResult<AbstractEmployeePermissionData> successEPD = new SuccessResult<AbstractEmployeePermissionData>();

            if (abstractEmployeePermissionJsonData.EmployeeModuleJsonData != null)
            {
                if (abstractEmployeePermissionJsonData.EmployeeId > 0)
                {
                    abstractEmployeePermissionDao.EmployeePermissionData_Delete(abstractEmployeePermissionJsonData.EmployeeId);

                    EmployeePermissionData employeePermissionData = new EmployeePermissionData();

                    abstractEmployeePermissionJsonData.employeePermissionJsonDataRoots = JsonConvert.DeserializeObject<List<EmployeePermissionJsonDataRoot>>(abstractEmployeePermissionJsonData.EmployeeModuleJsonData);

                    for (int i = 0; i < abstractEmployeePermissionJsonData.employeePermissionJsonDataRoots.Count; i++)
                    {
                        employeePermissionData.EmployeeId = abstractEmployeePermissionJsonData.EmployeeId;
                        employeePermissionData.EMP_Id = abstractEmployeePermissionJsonData.employeePermissionJsonDataRoots[i].Id;
                        employeePermissionData.Value = abstractEmployeePermissionJsonData.employeePermissionJsonDataRoots[i].Value;

                        successEPD = abstractEmployeePermissionDao.EmployeePermissionData_Upsert(employeePermissionData);
                    }
                }
                else
                {
                    successEPD.Code = 400;
                    successEPD.Message = "EmployeeId is required";
                }
            }
            
            return successEPD;
        }

        public override bool EmployeePermissionData_Delete(long EmployeeId)
        {
            return this.abstractEmployeePermissionDao.EmployeePermissionData_Delete(EmployeeId);
        }
    }
}