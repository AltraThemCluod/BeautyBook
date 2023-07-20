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
    public class PackageCustomPriceServices : AbstractPackageCustomPriceServices
    {
        private AbstractPackageCustomPriceDao abstractPackageCustomPriceDao;

        public PackageCustomPriceServices(AbstractPackageCustomPriceDao abstractPackageCustomPriceDao)
        {
            this.abstractPackageCustomPriceDao = abstractPackageCustomPriceDao;
        }
        public override SuccessResult<AbstractPackageCustomPrice> PackageCustomPrice_Create(AbstractPackageCustomPrice abstractPackageCustomPrice)
        {
            return this.abstractPackageCustomPriceDao.PackageCustomPrice_Create(abstractPackageCustomPrice);
        }
    }

}