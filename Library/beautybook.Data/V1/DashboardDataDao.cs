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
    public class DashboardDataDao : AbstractDashboardDataDao
    {
       
        public override DashboardData DashboardData_All(PageParam pageParam, string search , long LookUpCountry , long LookUpState)
        {
            DashboardData Dashboard = new DashboardData();
            PagedList<AbstractDashboardData> DashboardData = new PagedList<AbstractDashboardData>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpCountry", LookUpCountry, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpState", LookUpState, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DashboardData_All, param, commandType: CommandType.StoredProcedure);

                Dashboard.TotalSalonOwner = task.Read<long>().SingleOrDefault();
                Dashboard.UsersSalonOwner.AddRange(task.Read<Users>());

                Dashboard.TotalSeller = task.Read<long>().SingleOrDefault();
                Dashboard.UsersSallers.AddRange(task.Read<Users>());

                Dashboard.TotalSalon = task.Read<long>().SingleOrDefault();
                Dashboard.SalonBranch.AddRange(task.Read<Salons>());

                DashboardData.Values.AddRange(task.Read<DashboardData>());
                //DashboardData.TotalRecords = task.Read<long>().SingleOrDefault();

                if (DashboardData.Values != null)
                {
                    //DashboardData.Values.UsersSalonOwner = new List<Users>();
                    //DashboardData.Values.UsersSalonOwner.AddRange(task.Read<Users>());
                }

            }
            return Dashboard;
        }
               
    }
}
