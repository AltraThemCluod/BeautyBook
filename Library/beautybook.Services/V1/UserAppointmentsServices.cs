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
    public class UserAppointmentsServices : AbstractUserAppointmentsServices
    {
        private AbstractUserAppointmentsDao abstractUserAppointmentsDao;

        public UserAppointmentsServices(AbstractUserAppointmentsDao abstractUserAppointmentsDao)
        {
            this.abstractUserAppointmentsDao = abstractUserAppointmentsDao;
        }
        public override PagedList<AbstractUserAppointments> UserAppointments_All(PageParam pageParam, string search, long SalonId, long CustomerId, long AssignedToUserId, string AppointmentDate, string AppointmentTime, long LookUpStatusId)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_All(pageParam, search, SalonId , CustomerId, AssignedToUserId, AppointmentDate, AppointmentTime, LookUpStatusId);
        }

        public override PagedList<AbstractUserAppointments> UserAppointments_GetAssignTo(PageParam pageParam, string search, long SalonId , long CategoryId , string ServicesIds , string AppointmentDate, string AppointmentTime, long UserAppointmentId)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_GetAssignTo(pageParam, search, SalonId , CategoryId , ServicesIds, AppointmentDate, AppointmentTime, UserAppointmentId);
        }
        public override SuccessResult<AbstractUserAppointments> UserAppointments_ChangeStatus(long Id,long LookUpStatusId,long LookUpStatusChangedBy , string getQrCodeURL)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_ChangeStatus(Id, LookUpStatusId, LookUpStatusChangedBy , getQrCodeURL);
        }
        public override SuccessResult<AbstractUserAppointments> UserAppointments_ById(long Id)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_ById(Id);
        }
        public override SuccessResult<AbstractUserAppointments> UserAppointmentsInvoice_ById(long Id)
        {
            return this.abstractUserAppointmentsDao.UserAppointmentsInvoice_ById(Id);
        }
        public override SuccessResult<AbstractUserAppointments> AppoinmentInvoice_ById(long Id)
        {
            return this.abstractUserAppointmentsDao.AppoinmentInvoice_ById(Id);
        }
        public override SuccessResult<AbstractUserAppointments> UserAppointments_Upsert(AbstractUserAppointments abstractUserAppointments)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_Upsert(abstractUserAppointments);
        }

        public override SuccessResult<AbstractUserAppointments> BlankAppointment_Create(AbstractUserAppointments abstractUserAppointments)
        {
            return this.abstractUserAppointmentsDao.BlankAppointment_Create(abstractUserAppointments);
        }

        public override SuccessResult<AbstractUserAppointments> UserAppointments_Delete(long Id, long DeletedBy)
        {
            return this.abstractUserAppointmentsDao.UserAppointments_Delete(Id, DeletedBy);
        }

        public override SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Delete(long Id)
        {
            return this.abstractUserAppointmentsDao.AppointmentServiceDetails_Delete(Id);
        }

        public override PagedList<AbstractAppoinmentCustomers> SalonInvoice_CustomersAll(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractUserAppointmentsDao.SalonInvoice_CustomersAll(pageParam, search, SalonId);
        }
        public override PagedList<AbstractUserAppointments> SalonInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            return this.abstractUserAppointmentsDao.SalonInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);
        }
        public override SuccessResult<AbstractUserReturnInvoiceUpdate> UserReturnInvoice_Update(AbstractUserReturnInvoiceUpdate abstractUserReturnInvoiceUpdate)
        {
            return this.abstractUserAppointmentsDao.UserReturnInvoice_Update(abstractUserReturnInvoiceUpdate);
        }
        public override SuccessResult<AbstractUserAppointments> UserReturnInvoice_ById(long Id)
        {
            return this.abstractUserAppointmentsDao.UserReturnInvoice_ById(Id);
        }
        public override SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Upsert(AbstractAppointmentServiceDetails abstractAppointmentServiceDetails)
        {
            return this.abstractUserAppointmentsDao.AppointmentServiceDetails_Upsert(abstractAppointmentServiceDetails);
        }
    }
}