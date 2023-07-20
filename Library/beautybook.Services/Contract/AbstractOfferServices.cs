using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractOfferServices
    {
        public abstract SuccessResult<AbstractOffer> Offer_Upsert(AbstractOffer abstractOffer);
        public abstract SuccessResult<AbstractOffer> Offer_ById(long Id);
        public abstract PagedList<AbstractOffer> Offer_All(PageParam pageParam, string search,long CreatedBy,long SalonId);
        public abstract SuccessResult<AbstractOffer> Offer_Delete(long Id, long DeletedBy);

    }
}
