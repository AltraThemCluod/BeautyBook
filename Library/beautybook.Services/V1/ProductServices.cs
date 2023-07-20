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
    public class ProductServices : AbstractProductServices
    {
        private AbstractProductDao abstractProductDao;

        public ProductServices(AbstractProductDao abstractProductDao)
        {
            this.abstractProductDao = abstractProductDao;
        }

        public override PagedList<AbstractProduct> Product_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, long LookUpStatusId, long VendorId, string ProductBrandIds, string ProductCategoryIds, string MinPrice, string MaxPrice,long SortBy)
        {
            return this.abstractProductDao.Product_All(pageParam, search, ProductName, ProductTypeId, ProductBrandId, LookUpStatusId, VendorId, ProductBrandIds, ProductCategoryIds, MinPrice, MaxPrice, SortBy);
        }

        public override SuccessResult<AbstractProduct> Product_ChangeStatus(long Id, long LookUpStatusId)
        {
            return this.abstractProductDao.Product_ChangeStatus(Id, LookUpStatusId);
        }

        public override SuccessResult<AbstractProduct> Product_UpdateQty(long Id, long ProductQty,long UpdatedBy)
        {
            return this.abstractProductDao.Product_UpdateQty(Id, ProductQty, UpdatedBy);
        }
        public override SuccessResult<AbstractProduct> Product_ById(long Id)
        {
            return this.abstractProductDao.Product_ById(Id);
        }

        public override SuccessResult<AbstractProduct> Product_Upsert(AbstractProduct abstractProduct)
        {
            return this.abstractProductDao.Product_Upsert(abstractProduct);
        }

        public override SuccessResult<AbstractProduct> Product_Delete(long Id, long DeletedBy)
        {
            return this.abstractProductDao.Product_Delete(Id, DeletedBy);
        }
    }

}