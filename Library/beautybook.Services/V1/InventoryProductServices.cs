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
    public class InventoryProductServices : AbstractInventoryProductServices
    {
        private AbstractInventoryProductDao abstractInventoryProductDao;

        public InventoryProductServices(AbstractInventoryProductDao abstractInventoryProductDao)
        {
            this.abstractInventoryProductDao = abstractInventoryProductDao;
        }
        public override PagedList<AbstractInventoryProduct> InventoryProduct_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, decimal Weight, long WeightTypeId, long SalonId)
        {
            return this.abstractInventoryProductDao.InventoryProduct_All(pageParam, search, ProductName, ProductTypeId, ProductBrandId, Weight, WeightTypeId,SalonId);
        }
        public override PagedList<AbstractInventoryProduct> InventoryProduct_TopOne(PageParam pageParam, string search, string ProductName,long ProductTypeId,long ProductBrandId,long SalonId)
        {
            return this.abstractInventoryProductDao.InventoryProduct_TopOne(pageParam, search, ProductName, ProductTypeId, ProductBrandId, SalonId);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_ById(long Id)
        {
            return this.abstractInventoryProductDao.InventoryProduct_ById(Id);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_Upsert(AbstractInventoryProduct abstractInventoryProduct)
        {
            return this.abstractInventoryProductDao.InventoryProduct_Upsert(abstractInventoryProduct);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateInventory(AbstractInventoryProduct abstractInventoryProduct)
        {
            return this.abstractInventoryProductDao.InventoryProduct_UpdateInventory(abstractInventoryProduct);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateQty(AbstractInventoryProduct abstractInventoryProduct)
        {
            return this.abstractInventoryProductDao.InventoryProduct_UpdateQty(abstractInventoryProduct);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_LowQtyAlert(AbstractInventoryProduct abstractInventoryProduct)
        {
            return this.abstractInventoryProductDao.InventoryProduct_LowQtyAlert(abstractInventoryProduct);
        }
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_Delete(string Ids, long DeletedBy)
        {
            return this.abstractInventoryProductDao.InventoryProduct_Delete(Ids, DeletedBy);
        }
    }

}