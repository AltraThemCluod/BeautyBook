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
    public class SellerProductsFeedbackServices : AbstractSellerProductsFeedbackServices
    {
        private AbstractSellerProductsFeedbackDao abstractSellerProductsFeedbackDao;

        public SellerProductsFeedbackServices(AbstractSellerProductsFeedbackDao abstractSellerProductsFeedbackDao)
        {
            this.abstractSellerProductsFeedbackDao = abstractSellerProductsFeedbackDao;
        }
        public override PagedList<AbstractSellerProductsFeedback> SellerProductsFeedback_All(PageParam pageParam, string search, AbstractSellerProductsFeedback abstractSellerProductsFeedback)
        {
            return this.abstractSellerProductsFeedbackDao.SellerProductsFeedback_All(pageParam, search, abstractSellerProductsFeedback);
        }
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            return this.abstractSellerProductsFeedbackDao.SellerProductsFeedback_ChangeStatus(Id, LookUpStatusId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ById(long Id)
        {
            return this.abstractSellerProductsFeedbackDao.SellerProductsFeedback_ById(Id);
        }
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_Upsert(AbstractSellerProductsFeedback abstractSellerProductsFeedback)
        {
            return this.abstractSellerProductsFeedbackDao.SellerProductsFeedback_Upsert(abstractSellerProductsFeedback);
        }
    }

}