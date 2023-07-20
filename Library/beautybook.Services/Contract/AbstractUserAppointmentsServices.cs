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
    public abstract class AbstractUserAppointmentsServices
    {
        public abstract PagedList<AbstractUserAppointments> UserAppointments_All(PageParam pageParam, string search, long SalonId , long CustomerId, long AssignedToUserId, string AppointmentDate, string AppointmentTime, long LookUpStatusId);
        public abstract PagedList<AbstractUserAppointments> UserAppointments_GetAssignTo(PageParam pageParam, string search, long SalonId , long CategoryId , string ServicesIds , string AppointmentDate, string AppointmentTime, long UserAppointmentId);
        public abstract SuccessResult<AbstractUserAppointments> UserAppointments_ById(long Id);
        public abstract SuccessResult<AbstractUserAppointments> UserAppointmentsInvoice_ById(long Id);
        public abstract SuccessResult<AbstractUserAppointments> AppoinmentInvoice_ById(long Id);
        public abstract SuccessResult<AbstractUserAppointments> UserAppointments_Upsert(AbstractUserAppointments abstractUserAppointments);
        public abstract SuccessResult<AbstractUserAppointments> BlankAppointment_Create(AbstractUserAppointments abstractUserAppointments);
        public abstract SuccessResult<AbstractUserAppointments> UserAppointments_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy , string getQrCodeURL);
        public abstract SuccessResult<AbstractUserAppointments> UserAppointments_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Delete(long Id);
        public abstract PagedList<AbstractAppoinmentCustomers> SalonInvoice_CustomersAll(PageParam pageParam, string search, long SalonId);
        public abstract PagedList<AbstractUserAppointments> SalonInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId);
        public abstract SuccessResult<AbstractUserAppointments> UserReturnInvoice_ById(long Id);
        public abstract SuccessResult<AbstractUserReturnInvoiceUpdate> UserReturnInvoice_Update(AbstractUserReturnInvoiceUpdate abstractUserReturnInvoiceUpdate);
        public abstract SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Upsert(AbstractAppointmentServiceDetails abstractAppointmentServiceDetails);
    }
}
