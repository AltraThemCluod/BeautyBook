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
    public abstract class AbstractInventoryProductServices
    {
        public abstract PagedList<AbstractInventoryProduct> InventoryProduct_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, decimal Weight, long WeightTypeId, long SalonId);
        public abstract PagedList<AbstractInventoryProduct> InventoryProduct_TopOne(PageParam pageParam, string search, string ProductName, long ProductTypeId,long ProductBrandId,long SalonId);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_ById(long Id);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_Upsert(AbstractInventoryProduct abstractInventoryProduct);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateQty(AbstractInventoryProduct abstractInventoryProduct);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_LowQtyAlert(AbstractInventoryProduct abstractInventoryProduct);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateInventory(AbstractInventoryProduct abstractInventoryProduct);
        public abstract SuccessResult<AbstractInventoryProduct> InventoryProduct_Delete(string Ids, long DeletedBy);
    }
}
