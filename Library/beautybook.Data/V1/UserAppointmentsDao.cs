using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using Dapper;
using Newtonsoft.Json;

namespace BeautyBook.Data.V1
{
    public class UserAppointmentsDao : AbstractUserAppointmentsDao
    {
        public override SuccessResult<AbstractUserAppointments> UserAppointments_ById(long Id)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_ById, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();

                if (UserAppointments.Item.ServiceDetailsStr != null)
                {
                    UserAppointments.Item.ServiceDetailsObj = JsonConvert.DeserializeObject<List<ServiceDetailsRoot>>(Convert.ToString(UserAppointments.Item.ServiceDetailsStr));
                }
            }

            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> UserAppointmentsInvoice_ById(long Id)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointmentsInvoice_ById, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }

            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> AppoinmentInvoice_ById(long Id)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AppoinmentInvoice_ById, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();

                if(UserAppointments.Item.ServicesObject != null)
                {
                    UserAppointments.Item.AppoinmentServices = JsonConvert.DeserializeObject<List<AppoinmentServices>>(Convert.ToString(UserAppointments.Item.ServicesObject));
                }
            }

            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> UserAppointments_ChangeStatus(long Id, long LookUpStatusId,long LookUpStatusChangedBy , string getQrCodeURL)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@getQrCodeURL", getQrCodeURL, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_ChangeStatus, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }

            return UserAppointments;
        }
        public override PagedList<AbstractUserAppointments> UserAppointments_All(PageParam pageParam, string search, long SalonId , long CustomerId, long AssignedToUserId, string AppointmentDate, string AppointmentTime, long LookUpStatusId)
        {
            PagedList<AbstractUserAppointments> UserAppointments = new PagedList<AbstractUserAppointments>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerId ", CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AssignedToUserId ", AssignedToUserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AppointmentDate ", AppointmentDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AppointmentTime ", AppointmentTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId ", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_All, param, commandType: CommandType.StoredProcedure);
                UserAppointments.Values.AddRange(task.Read<UserAppointments>());
                UserAppointments.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override PagedList<AbstractUserAppointments> UserAppointments_GetAssignTo(PageParam pageParam, string search, long SalonId , long CategoryId, string ServicesIds, string AppointmentDate, string AppointmentTime, long UserAppointmentId)
        {
            PagedList<AbstractUserAppointments> UserAppointments = new PagedList<AbstractUserAppointments>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CategoryId", CategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServicesIds ", ServicesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AppointmentDate ", AppointmentDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AppointmentTime ", AppointmentTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserAppointmentId ", UserAppointmentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_GetAssignTo, param, commandType: CommandType.StoredProcedure);
                UserAppointments.Values.AddRange(task.Read<UserAppointments>());
                UserAppointments.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> UserAppointments_Upsert(AbstractUserAppointments abstractUserAppointments)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUserAppointments.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractUserAppointments.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserAppointments.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AssignedToUserId", abstractUserAppointments.AssignedToUserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AppointmentDate", abstractUserAppointments.AppointmentDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AppointmentTime", abstractUserAppointments.AppointmentTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Price", abstractUserAppointments.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@TotalPrice", abstractUserAppointments.TotalPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Duration", abstractUserAppointments.Duration, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Comment", abstractUserAppointments.Comment, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServicesIds", abstractUserAppointments.ServicesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CategoryId", abstractUserAppointments.CategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUserAppointments.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractUserAppointments.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_Upsert, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> BlankAppointment_Create(AbstractUserAppointments abstractUserAppointments)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUserAppointments.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUserAppointments.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlankAppointment_Create, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override SuccessResult<AbstractUserAppointments> UserAppointments_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserAppointments_Delete, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Delete(long Id)
        {
            SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AppointmentServiceDetails_Delete, param, commandType: CommandType.StoredProcedure);
                AppointmentServiceDetails = task.Read<SuccessResult<AbstractAppointmentServiceDetails>>().SingleOrDefault();
                AppointmentServiceDetails.Item = task.Read<AppointmentServiceDetails>().SingleOrDefault();
            }
            return AppointmentServiceDetails;
        }

        public override PagedList<AbstractAppoinmentCustomers> SalonInvoice_CustomersAll(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractAppoinmentCustomers> AppoinmentCustomers = new PagedList<AbstractAppoinmentCustomers>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonInvoice_CustomersAll, param, commandType: CommandType.StoredProcedure);
                AppoinmentCustomers.Values.AddRange(task.Read<AppoinmentCustomers>());
                AppoinmentCustomers.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return AppoinmentCustomers;
        }

        public override PagedList<AbstractUserAppointments> SalonInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            PagedList<AbstractUserAppointments> UserAppointments = new PagedList<AbstractUserAppointments>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerId ", CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@InvoiceDate ", InvoiceDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@InvoiceNumber ", InvoiceNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonInvoice_All, param, commandType: CommandType.StoredProcedure);
                UserAppointments.Values.AddRange(task.Read<UserAppointments>());
                UserAppointments.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserAppointments;
        }

        
        public override SuccessResult<AbstractUserReturnInvoiceUpdate> UserReturnInvoice_Update(AbstractUserReturnInvoiceUpdate abstractUserReturnInvoiceUpdate)
        {
            SuccessResult<AbstractUserReturnInvoiceUpdate> UserReturnInvoiceUpdate = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUserReturnInvoiceUpdate.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServicesObject", abstractUserReturnInvoiceUpdate.ServicesObject, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Tax", abstractUserReturnInvoiceUpdate.Tax, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@ServicesTotalPrice", abstractUserReturnInvoiceUpdate.ServicesTotalPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserReturnInvoice_Update, param, commandType: CommandType.StoredProcedure);
                UserReturnInvoiceUpdate = task.Read<SuccessResult<AbstractUserReturnInvoiceUpdate>>().SingleOrDefault();
                UserReturnInvoiceUpdate.Item = task.Read<UserReturnInvoiceUpdate>().SingleOrDefault();
            }
            return UserReturnInvoiceUpdate;
        }

        public override SuccessResult<AbstractUserAppointments> UserReturnInvoice_ById(long Id)
        {
            SuccessResult<AbstractUserAppointments> UserAppointments = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserReturnInvoice_ById, param, commandType: CommandType.StoredProcedure);
                UserAppointments = task.Read<SuccessResult<AbstractUserAppointments>>().SingleOrDefault();
                UserAppointments.Item = task.Read<UserAppointments>().SingleOrDefault();
            }
            return UserAppointments;
        }

        public override SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails_Upsert(AbstractAppointmentServiceDetails abstractAppointmentServiceDetails)
        {
            SuccessResult<AbstractAppointmentServiceDetails> AppointmentServiceDetails = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractAppointmentServiceDetails.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AppointmentId", abstractAppointmentServiceDetails.AppointmentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CategoryId", abstractAppointmentServiceDetails.CategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServiceId", abstractAppointmentServiceDetails.ServiceId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AssignedToUserId", abstractAppointmentServiceDetails.AssignedToUserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Price", abstractAppointmentServiceDetails.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Duration", abstractAppointmentServiceDetails.Duration, dbType: DbType.Decimal, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AppointmentServiceDetails_Upsert, param, commandType: CommandType.StoredProcedure);
                AppointmentServiceDetails = task.Read<SuccessResult<AbstractAppointmentServiceDetails>>().SingleOrDefault();
                AppointmentServiceDetails.Item = task.Read<AppointmentServiceDetails>().SingleOrDefault();
            }
            return AppointmentServiceDetails;
        }
    }
}
