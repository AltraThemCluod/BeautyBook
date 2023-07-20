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
    public abstract class AbstractWriteaReviewDao
    {
        public abstract SuccessResult<AbstractWriteaReview> WriteaReview_Upsert(AbstractWriteaReview abstractWriteaReview);
        public abstract PagedList<AbstractWriteaReview> WriteaReview_All(PageParam pageParam, string search, long ProductId,long VendorId);
    }
}
