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
    public class OfferServices : AbstractOfferServices
    {
        private AbstractOfferDao abstractOfferDao;

        public OfferServices(AbstractOfferDao abstractOfferDao)
        {
            this.abstractOfferDao = abstractOfferDao;
        }

        public override SuccessResult<AbstractOffer> Offer_Upsert(AbstractOffer abstractOffer)
        {
            return this.abstractOfferDao.Offer_Upsert(abstractOffer);
        }

        public override SuccessResult<AbstractOffer> Offer_ById(long Id)
        {
            return this.abstractOfferDao.Offer_ById(Id);
        }

        public override PagedList<AbstractOffer> Offer_All(PageParam pageParam, string search, long CreatedBy, long SalonId)
        {
            return this.abstractOfferDao.Offer_All(pageParam, search, CreatedBy, SalonId);

        }


        public override SuccessResult<AbstractOffer> Offer_Delete(long Id, long DeletedBy)
        {
            return this.abstractOfferDao.Offer_Delete(Id, DeletedBy);
        }

    }
}
