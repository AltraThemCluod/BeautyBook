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
    public class OfferDao : AbstractOfferDao
    {

        public override SuccessResult<AbstractOffer> Offer_Upsert(AbstractOffer abstractOffer)
        {
            SuccessResult<AbstractOffer> Offer = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOffer.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractOffer.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OfferName", abstractOffer.OfferName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ApplyOn", abstractOffer.ApplyOn, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OfferPeriodStart", abstractOffer.OfferPeriodStart, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OfferPeriodEnd", abstractOffer.OfferPeriodEnd, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AgeBetweenMinYear", abstractOffer.AgeBetweenMinYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AgeBetweenMaxYear", abstractOffer.AgeBetweenMaxYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractOffer.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractOffer.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpServicesIdStr ", abstractOffer.LookUpServicesIdStr, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OfferPriceStr", abstractOffer.OfferPriceStr, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PackagesIds", abstractOffer.PackagesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PackagesOfferPrices", abstractOffer.PackagesOfferPrices, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsType", abstractOffer.IsType, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Offer_Upsert, param, commandType: CommandType.StoredProcedure);
                Offer = task.Read<SuccessResult<AbstractOffer>>().SingleOrDefault();
                Offer.Item = task.Read<Offer>().SingleOrDefault();
            }

            return Offer;
        }

        public override SuccessResult<AbstractOffer> Offer_ById(long Id)
        {
            SuccessResult<AbstractOffer> Offer = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Offer_ById, param, commandType: CommandType.StoredProcedure);
                Offer = task.Read<SuccessResult<AbstractOffer>>().SingleOrDefault();
                Offer.Item = task.Read<Offer>().SingleOrDefault();
                if (Offer.Item != null)
                {
                    Offer.Item.MasterOfferPrice = new List<MasterOfferPrice>();
                    Offer.Item.MasterOfferPrice.AddRange(task.Read<MasterOfferPrice>());

                    Offer.Item.PackageOfferPrice = new List<PackageOfferPrice>();
                    Offer.Item.PackageOfferPrice.AddRange(task.Read<PackageOfferPrice>());

                }
            }
            return Offer;
        }

        public override PagedList<AbstractOffer> Offer_All(PageParam pageParam, string search,long CreatedBy,long SalonId)
        {
            PagedList<AbstractOffer> Offer = new PagedList<AbstractOffer>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Offer_All, param, commandType: CommandType.StoredProcedure);
                Offer.Values.AddRange(task.Read<Offer>());
                Offer.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Offer;
        }

        public override SuccessResult<AbstractOffer> Offer_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractOffer> Offer = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Offer_Delete, param, commandType: CommandType.StoredProcedure);
                Offer = task.Read<SuccessResult<AbstractOffer>>().SingleOrDefault();
                Offer.Item = task.Read<Offer>().SingleOrDefault();
            }

            return Offer;
        }



    }
}
