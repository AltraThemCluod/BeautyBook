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
    public abstract class AbstractMasterProductWeightDao
    {
        public abstract SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Upsert(AbstractMasterProductWeight abstractMasterProductWeight);
        public abstract SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ById(long Id);
        public abstract PagedList<AbstractMasterProductWeight> MasterProductWeight_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ActInAct(long Id);

    }
}
