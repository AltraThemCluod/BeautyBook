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
    public class SalonsServices : AbstractSalonsServices
    {
        private AbstractSalonsDao abstractSalonsDao;
        private AbstractUsersDao abstractUsersDao;

        public SalonsServices(AbstractSalonsDao abstractSalonsDao , AbstractUsersDao abstractUsersDao)
        {
            this.abstractSalonsDao = abstractSalonsDao;
            this.abstractUsersDao = abstractUsersDao; 
        }
        public override PagedList<AbstractSalons> Salons_All(PageParam pageParam, string search, long LookUpStatusId)
        {
            return this.abstractSalonsDao.Salons_All(pageParam, search, LookUpStatusId);
        }
        public override PagedList<AbstractSalons> Salons_ByUserId(PageParam pageParam, string search, long UserId , long LookUpStatusId)
        {
            return this.abstractSalonsDao.Salons_ByUserId(pageParam, search, UserId , LookUpStatusId);
        }
        public override SuccessResult<AbstractSalons> Salons_ActInact(long Id,long LookUpStatusId, long LookUpStatusChangedBy)
        {
            return this.abstractSalonsDao.Salons_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractSalons> Salons_ApprovedUnApproved(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            return this.abstractSalonsDao.Salons_ApprovedUnApproved(Id, LookUpStatusId,LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractSalons> Salons_ById(long Id)
        {
            return this.abstractSalonsDao.Salons_ById(Id);
        }
        public override SuccessResult<AbstractSalons> Salons_Upsert(AbstractSalons abstractSalons)
        {

            var UserListById = this.abstractUsersDao.Users_ById(abstractSalons.UserId , "");

            if (abstractSalons.Id == 0)
            {
                EmailHelper.CreateSalon(
                    abstractSalons.SalonName, 
                    abstractSalons.AddressLine1, 
                    abstractSalons.PrimaryPhone, 
                    abstractSalons.Latitude, 
                    abstractSalons.Longitude, 
                    UserListById.Item.FirstName, 
                    UserListById.Item.SecondName, 
                    UserListById.Item.Email, 
                    UserListById.Item.PrimaryPhone,
                    UserListById.Item.AddressLine1, 
                    UserListById.Item.Latitude, 
                    UserListById.Item.Longitude
                );
            }

            return this.abstractSalonsDao.Salons_Upsert(abstractSalons);
        }
        public override SuccessResult<AbstractSalons> Salons_Delete(long Id, long DeletedBy)
        {
            return this.abstractSalonsDao.Salons_Delete(Id, DeletedBy);
        }
    }

}