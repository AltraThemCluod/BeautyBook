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
    public class WriteaReviewServices : AbstractWriteaReviewServices
    {
        private AbstractWriteaReviewDao abstractWriteaReviewDao;

        public WriteaReviewServices(AbstractWriteaReviewDao abstractWriteaReviewDao)
        {
            this.abstractWriteaReviewDao = abstractWriteaReviewDao;
        }

        public override SuccessResult<AbstractWriteaReview> WriteaReview_Upsert(AbstractWriteaReview abstractWriteaReview)
        {
            return this.abstractWriteaReviewDao.WriteaReview_Upsert(abstractWriteaReview);
        }

        public override PagedList<AbstractWriteaReview> WriteaReview_All(PageParam pageParam, string search, long ProductId,long VendorId)
        {
            return this.abstractWriteaReviewDao.WriteaReview_All(pageParam, search , ProductId, VendorId);
        }
    }
}
