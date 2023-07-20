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
    public class UsersDao : AbstractUsersDao
    {
        public override SuccessResult<AbstractUsers> Users_ActInact(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_ActInact, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }
        public override PagedList<AbstractUsers> Users_All(PageParam pageParam, string search , AbstractUsers abstractUsers)
        {
            PagedList<AbstractUsers> Users = new PagedList<AbstractUsers>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", abstractUsers.LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", abstractUsers.LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Name", abstractUsers.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeTypeId", abstractUsers.LookUpEmployeeTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeRolesId", abstractUsers.LookUpEmployeeRolesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PrimaryPhone", abstractUsers.PrimaryPhone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Gender", abstractUsers.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUsers.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsLanguage", abstractUsers.IsLanguage, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_All, param, commandType: CommandType.StoredProcedure);
                Users.Values.AddRange(task.Read<Users>());
                Users.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Users;
        }
        public override SuccessResult<AbstractUsers> Users_ById(long Id , string IsLanguage)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsLanguage", IsLanguage, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_ById, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();

                if(Users.Item != null)
                {
                    Users.Item.UserWorkingHours = new List<UserWorkingHours>();
                    Users.Item.UserWorkingHours.AddRange(task.Read<UserWorkingHours>());

                    Users.Item.UserServices = new List<UserServices>();
                    Users.Item.UserServices.AddRange(task.Read<UserServices>());

                    Users.Item.UserAppointments = new List<UserAppointments>();
                    Users.Item.UserAppointments.AddRange(task.Read<UserAppointments>());

                    if (Users.Item.UserAppointments.Count > 0)
                    {
                        for (int i = 0; i < Users.Item.UserAppointments.Count; i++)
                        {
                            Users.Item.UserAppointments[i].AppointmentServicesList = JsonConvert.DeserializeObject<List<AppointmentServicesRoot>>(Users.Item.UserAppointments[i].AppointmentServices);
                        }
                    }

                    Users.Item.AssignToUserAppointments = new List<UserAppointments>();
                    Users.Item.AssignToUserAppointments.AddRange(task.Read<UserAppointments>());

                }


            }

            return Users;
        }
        public override SuccessResult<AbstractUsers> Users_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_Delete, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }
            return Users;
        }

        public override SuccessResult<AbstractUsers> Employee_IsCreate(long SalonId)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Employee_IsCreate, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }
            return Users;
        }

        public override SuccessResult<AbstractUsers> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType , long LookUpUserTypeId)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PasswordHash", PasswordHash, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DeviceToken", DeviceToken, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LoginType", LoginType, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_Login, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }
        public override bool Users_Logout(long Id)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.Users_Logout, param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }
            return result;

        }
        public override bool Users_MobileLogout(long Id)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.Users_MobileLogout, param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }

            return result;
        }
        public override SuccessResult<AbstractUsers> Users_ResetPassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword, long Type)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OldPassword", OldPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@NewPassword", NewPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ConfirmPassword", ConfirmPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_ResetPassword, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

        public override SuccessResult<AbstractUsers> Users_Upsert(AbstractUsers abstractUsers)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUsers.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUsers.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", abstractUsers.LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FirstName", abstractUsers.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SecondName", abstractUsers.SecondName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Gender", abstractUsers.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Email", abstractUsers.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PasswordHash", abstractUsers.PasswordHash, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PrimaryPhone", abstractUsers.PrimaryPhone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AlternatePhone", abstractUsers.AlternatePhone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProfileUrl", abstractUsers.ProfileUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Tags", abstractUsers.Tags, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Dob", abstractUsers.Dob, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AddressLine1", abstractUsers.AddressLine1, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Latitude", abstractUsers.Latitude, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Longitude", abstractUsers.Longitude, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AddressLine2", abstractUsers.AddressLine2, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpCountryId", abstractUsers.LookUpCountryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStateId", abstractUsers.LookUpStateId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@JoiningDate", abstractUsers.JoiningDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeRolesId", abstractUsers.LookUpEmployeeRolesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeTypeId", abstractUsers.LookUpEmployeeTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@City", abstractUsers.City, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ZipCode", abstractUsers.ZipCode, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUsers.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractUsers.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ReferedByEmail", abstractUsers.ReferedByEmail, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ReferedByUsername", abstractUsers.ReferedByUsername, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@WorkingStartTime", abstractUsers.WorkingStartTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@WorkingEndTime", abstractUsers.WorkingEndTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@WorkingHoursId", abstractUsers.WorkingHoursId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsAvailable", abstractUsers.IsAvailable, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MessageVendor", abstractUsers.MessageVendor, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@BankName", abstractUsers.BankName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IBANNumber", abstractUsers.IBANNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VATNumber", abstractUsers.VATNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CommercialRegisterNumber", abstractUsers.CommercialRegisterNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorCompanyNumber", abstractUsers.VendorCompanyNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_Upsert, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

        public override SuccessResult<AbstractUsers> BlankUsers_Create(AbstractUsers abstractUsers)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUsers.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUsers.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", abstractUsers.LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUsers.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlankUsers_Create, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }
            return Users;
        }

        public override SuccessResult<AbstractUsers> Users_VerifyEmail(long Id, string EmailVerificationCode)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmailVerificationCode", EmailVerificationCode, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_VerifyEmail, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

        public override SuccessResult<AbstractUsers> Users_VerifyCode(long Id)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_VerifyCode, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

        public override SuccessResult<AbstractUsers> User_ByEmail(string Email , long IsSeller)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsSeller", IsSeller, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.User_ByEmail, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

    }
}
