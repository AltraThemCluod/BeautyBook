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
    public abstract class AbstractProductDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractProduct> Product_ChangeStatus(long Id, long LookUpStatusId);
        public abstract SuccessResult<AbstractProduct> Product_UpdateQty(long Id, long ProductQty,long UpdatedBy);
        
        public abstract PagedList<AbstractProduct> Product_All(PageParam pageParam, string search,string ProductName, long ProductTypeId,long ProductBrandId,long LookUpStatusId,long VendorId, string ProductBrandIds,string ProductCategoryIds, string MinPrice, string MaxPrice,long SortBy);
        
        public abstract SuccessResult<AbstractProduct> Product_ById(long Id);
        
        public abstract SuccessResult<AbstractProduct> Product_Delete(long Id, long DeletedBy);
        
        public abstract SuccessResult<AbstractProduct> Product_Upsert(AbstractProduct abstractProduct);
    }
}
