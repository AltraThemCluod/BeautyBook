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
    public class MasterProductCategoryServices : AbstractMasterProductCategoryServices
    {
        private AbstractMasterProductCategoryDao abstractMasterProductCategoryDao;

        public MasterProductCategoryServices(AbstractMasterProductCategoryDao abstractMasterProductCategoryDao)
        {
            this.abstractMasterProductCategoryDao = abstractMasterProductCategoryDao;
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Upsert(AbstractMasterProductCategory abstractMasterProductCategory)
        {
            return this.abstractMasterProductCategoryDao.MasterProductCategory_Upsert(abstractMasterProductCategory);
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ById(long Id)
        {
            return this.abstractMasterProductCategoryDao.MasterProductCategory_ById(Id);
        }

        public override PagedList<AbstractMasterProductCategory> MasterProductCategory_All(PageParam pageParam, string search, long ProductTypeId)
        {
            return this.abstractMasterProductCategoryDao.MasterProductCategory_All(pageParam, search, ProductTypeId);
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ActInact(long Id)
        {
            return this.abstractMasterProductCategoryDao.MasterProductCategory_ActInact(Id);
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Delete(long Id, long DeletedBy)
        {
            return this.abstractMasterProductCategoryDao.MasterProductCategory_Delete(Id, DeletedBy);
        }

    }
}
