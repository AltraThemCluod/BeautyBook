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
    public class Create_EmailMsg_PackagesServices : AbstractCreate_EmailMsg_PackagesServices
    {
        private AbstractCreate_EmailMsg_PackagesDao abstractCreate_EmailMsg_PackagesDao;

        public Create_EmailMsg_PackagesServices(AbstractCreate_EmailMsg_PackagesDao abstractCreate_EmailMsg_PackagesDao)
        {
            this.abstractCreate_EmailMsg_PackagesDao = abstractCreate_EmailMsg_PackagesDao;
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Upsert(AbstractCreate_EmailMsg_Packages abstractCreate_EmailMsg_Packages)
        {
            return this.abstractCreate_EmailMsg_PackagesDao.Create_EmailMsg_Packages_Upsert(abstractCreate_EmailMsg_Packages);
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ById(long Id)
        {
            return this.abstractCreate_EmailMsg_PackagesDao.Create_EmailMsg_Packages_ById(Id);
        }

        public override PagedList<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_All(PageParam pageParam, string search,long LookUpDurationId,long SalonId)
        {
            return this.abstractCreate_EmailMsg_PackagesDao.Create_EmailMsg_Packages_All(pageParam, search, LookUpDurationId, SalonId);

        }
        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ActInAct(long Id)
        {
            return this.abstractCreate_EmailMsg_PackagesDao.Create_EmailMsg_Packages_ActInAct(Id);
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Delete(long Id, long DeletedBy)
        {
            return this.abstractCreate_EmailMsg_PackagesDao.Create_EmailMsg_Packages_Delete(Id, DeletedBy);
        }

       
    }
}
