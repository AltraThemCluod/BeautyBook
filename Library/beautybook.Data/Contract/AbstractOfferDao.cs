using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.V1;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Data.Contract
{
    public abstract class AbstractOfferDao
    {
        public abstract SuccessResult<AbstractOffer> Offer_Upsert(AbstractOffer abstractOffer);
        public abstract SuccessResult<AbstractOffer> Offer_ById(long Id);
        public abstract PagedList<AbstractOffer> Offer_All(PageParam pageParam, string search,long CreatedBy,long SalonId);
        public abstract SuccessResult<AbstractOffer> Offer_Delete(long Id, long DeletedBy);

    }
}
