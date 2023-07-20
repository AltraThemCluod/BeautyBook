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

namespace BeautyBook.Data.V1
{
    public class SalonsDao : AbstractSalonsDao
    {
        public override SuccessResult<AbstractSalons> Salons_ById(long Id)
        {
            SuccessResult<AbstractSalons> Salons = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_ById, param, commandType: CommandType.StoredProcedure);
                Salons = task.Read<SuccessResult<AbstractSalons>>().SingleOrDefault();
                Salons.Item = task.Read<Salons>().SingleOrDefault();
                if (Salons.Item != null)
                {
                    Salons.Item.OrderProducts = new List<OrderProducts>();
                    Salons.Item.OrderProducts.AddRange(task.Read<OrderProducts>());
                }
            }

            return Salons;
        }
        public override SuccessResult<AbstractSalons> Salons_ActInact(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractSalons> Salons = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_ActInact, param, commandType: CommandType.StoredProcedure);
                Salons = task.Read<SuccessResult<AbstractSalons>>().SingleOrDefault();
                Salons.Item = task.Read<Salons>().SingleOrDefault();
            }

            return Salons;
        }
        public override SuccessResult<AbstractSalons> Salons_ApprovedUnApproved(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractSalons> Salons = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_ApprovedUnApproved, param, commandType: CommandType.StoredProcedure);
                Salons = task.Read<SuccessResult<AbstractSalons>>().SingleOrDefault();
                Salons.Item = task.Read<Salons>().SingleOrDefault();
            }

            return Salons;
        }
        public override PagedList<AbstractSalons> Salons_All(PageParam pageParam, string search, long LookUpStatusId)
        {
            PagedList<AbstractSalons> Salons = new PagedList<AbstractSalons>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_All, param, commandType: CommandType.StoredProcedure);
                Salons.Values.AddRange(task.Read<Salons>());
                Salons.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Salons;
        }

        public override PagedList<AbstractSalons> Salons_ByUserId(PageParam pageParam, string search, long UserId, long LookUpStatusId)
        {
            PagedList<AbstractSalons> Salons = new PagedList<AbstractSalons>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_ByUserId, param, commandType: CommandType.StoredProcedure);
                Salons.Values.AddRange(task.Read<Salons>());
                Salons.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Salons;
        }

        public override SuccessResult<AbstractSalons> Salons_Upsert(AbstractSalons abstractSalons)
        {
            SuccessResult<AbstractSalons> Salons = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSalons.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractSalons.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonName", abstractSalons.SalonName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LogoUrl", abstractSalons.LogoUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PrimaryPhone", abstractSalons.PrimaryPhone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AlternatePhone", abstractSalons.AlternatePhone, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AddressLine1", abstractSalons.AddressLine1, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Latitude", abstractSalons.Latitude, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Longitude", abstractSalons.Longitude, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AddressLine2", abstractSalons.AddressLine2, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpCountryId", abstractSalons.LookUpCountryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStateId", abstractSalons.LookUpStateId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@City", abstractSalons.City, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ZipCode", abstractSalons.ZipCode, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@BankName", abstractSalons.BankName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IBANNumber", abstractSalons.IBANNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@TaxNumber", abstractSalons.TaxNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VATNumber", abstractSalons.VATNumber, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_Upsert, param, commandType: CommandType.StoredProcedure);
                Salons = task.Read<SuccessResult<AbstractSalons>>().SingleOrDefault();
                Salons.Item = task.Read<Salons>().SingleOrDefault();
            }

            return Salons;
        }
        public override SuccessResult<AbstractSalons> Salons_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractSalons> Salons = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Salons_Delete, param, commandType: CommandType.StoredProcedure);
                Salons = task.Read<SuccessResult<AbstractSalons>>().SingleOrDefault();
                Salons.Item = task.Read<Salons>().SingleOrDefault();
            }
            return Salons;
        }
    }
}
