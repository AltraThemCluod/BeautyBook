using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class MasterProductWeightServices : AbstractMasterProductWeightServices
    {
        private AbstractMasterProductWeightDao abstractMasterProductWeightDao;

        public MasterProductWeightServices(AbstractMasterProductWeightDao abstractMasterProductWeightDao)
        {
            this.abstractMasterProductWeightDao = abstractMasterProductWeightDao;
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Upsert(AbstractMasterProductWeight abstractMasterProductWeight)
        {
            return this.abstractMasterProductWeightDao.MasterProductWeight_Upsert(abstractMasterProductWeight);
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ById(long Id)
        {
            return this.abstractMasterProductWeightDao.MasterProductWeight_ById(Id);
        }

        public override PagedList<AbstractMasterProductWeight> MasterProductWeight_All(PageParam pageParam, string search)
        {
            return this.abstractMasterProductWeightDao.MasterProductWeight_All(pageParam, search);

        }
        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ActInAct(long Id)
        {
            return this.abstractMasterProductWeightDao.MasterProductWeight_ActInAct(Id);
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Delete(long Id, long DeletedBy)
        {
            return this.abstractMasterProductWeightDao.MasterProductWeight_Delete(Id, DeletedBy);
        }

    }
}
