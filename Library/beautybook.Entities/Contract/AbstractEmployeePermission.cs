using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractEmployeeModulePermission
    {
        public long Id { get; set; }
        public string ModuleKey { get; set; }
        public string En_ModuleName { get; set; }
        public string Ar_ModuleName { get; set; }
        public long ParentId { get; set; }
        public bool Value { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    public abstract class AbstractEmployeePermissionData
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long EMP_Id { get; set; }
        public bool Value { get; set; }
        public string ModuleKey { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsShow { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }

    public abstract class AbstractEmployeePermissionJsonData
    {
        public long EmployeeId { get; set; }
        public string EmployeeModuleJsonData { get; set; }
        public List<EmployeePermissionJsonDataRoot> employeePermissionJsonDataRoots { get; set; }
    }


    public class EmployeePermissionJsonDataRoot
    {
        public int Id { get; set; }
        public string key { get; set; }
        public bool Value { get; set; }
    }


}

