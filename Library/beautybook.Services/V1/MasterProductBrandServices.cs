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
    public class MasterProductBrandServices : AbstractMasterProductBrandServices
    {
        private AbstractMasterProductBrandDao abstractMasterProductBrandDao;

        public MasterProductBrandServices(AbstractMasterProductBrandDao abstractMasterProductBrandDao)
        {
            this.abstractMasterProductBrandDao = abstractMasterProductBrandDao;
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Upsert(AbstractMasterProductBrand abstractMasterProductBrand)
        {
            return this.abstractMasterProductBrandDao.MasterProductBrand_Upsert(abstractMasterProductBrand);
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ById(long Id)
        {
            return this.abstractMasterProductBrandDao.MasterProductBrand_ById(Id);
        }

        public override PagedList<AbstractMasterProductBrand> MasterProductBrand_All(PageParam pageParam, string search,long SalonId)
        {
            return this.abstractMasterProductBrandDao.MasterProductBrand_All(pageParam, search, SalonId);

        }
        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ActInAct(long Id)
        {
            return this.abstractMasterProductBrandDao.MasterProductBrand_ActInAct(Id);
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Delete(long Id, long DeletedBy)
        {
            return this.abstractMasterProductBrandDao.MasterProductBrand_Delete(Id, DeletedBy);
        }

    }
}
