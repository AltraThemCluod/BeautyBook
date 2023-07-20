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
    public abstract class AbstractSellerProductsFeedbackDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractSellerProductsFeedback> SellerProductsFeedback_All(PageParam pageParam, string search, AbstractSellerProductsFeedback abstractSellerProductsFeedback);
        public abstract SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ById(long Id);
        public abstract SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_Upsert(AbstractSellerProductsFeedback abstractSellerProductsFeedback);
        public abstract SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy);
    }
}
