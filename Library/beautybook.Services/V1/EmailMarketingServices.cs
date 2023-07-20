using System.Web;
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
    public class EmailMarketingServices : AbstractEmailMarketingServices
    {
        private AbstractEmailMarketingDao abstractEmailMarketingDao;
        private AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao;
        private AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao;


        public EmailMarketingServices(AbstractEmailMarketingDao abstractEmailMarketingDao, 
            AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao,
            AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao
            )
        {
            this.abstractEmailMarketingDao = abstractEmailMarketingDao;
            this.abstractSendEmailMarketingTemplatesDao = abstractSendEmailMarketingTemplatesDao;
            this.abstractSelectPlan_SMSPackagesDao = abstractSelectPlan_SMSPackagesDao;
        }

        public override PagedList<AbstractEmailMarketing> EmailMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            return this.abstractEmailMarketingDao.EmailMarketing_All(pageParam, search, CreatedBy, SalonId);
        }
                
        public override SuccessResult<AbstractEmailMarketing> EmailMarketing_ById(long Id)
        {
            return this.abstractEmailMarketingDao.EmailMarketing_ById(Id);
        }
        public override SuccessResult<AbstractEmailMarketing> EmailMarketing_Upsert(AbstractEmailMarketing abstractEmailMarketing , long NoOfEmailCount)
        {

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 0;

            var CustomerSinceStart = "";
            if (abstractEmailMarketing.CustomerSinceStart != "")
            {
                var StartDateSplit = abstractEmailMarketing.CustomerSinceStart.Split('/');
                CustomerSinceStart = StartDateSplit[2] + '-' + StartDateSplit[0] + '-' + StartDateSplit[1];
            }

            var CustomerSinceEnd = "";
            if (abstractEmailMarketing.CustomerSinceEnd != "")
            {
                var EndDateSplit = abstractEmailMarketing.CustomerSinceEnd.Split('/');
                CustomerSinceEnd = EndDateSplit[2] + '-' + EndDateSplit[0] + '-' + EndDateSplit[1];
            }

            var LastVisitStartDate = "";
            if (abstractEmailMarketing.LastVisitStart != "")
            {
                var LastVisitStartSplit = abstractEmailMarketing.LastVisitStart.Split('/');
                LastVisitStartDate = LastVisitStartSplit[2] + '-' + LastVisitStartSplit[0] + '-' + LastVisitStartSplit[1];
            }

            var LastVisitEndDate = "";
            if (abstractEmailMarketing.LastVisitEnd != "")
            {
                var LastVisitEndDateSplit = abstractEmailMarketing.LastVisitEnd.Split('/');
                LastVisitEndDate = LastVisitEndDateSplit[2] + '-' + LastVisitEndDateSplit[0] + '-' + LastVisitEndDateSplit[1];
            }

            var responseAll = this.abstractSendEmailMarketingTemplatesDao.EmailUser_All(pageParam, "", CustomerSinceStart, CustomerSinceEnd, abstractEmailMarketing.MinAppoinment, abstractEmailMarketing.MaxAppoinment, abstractEmailMarketing.MinYear, abstractEmailMarketing.MaxYear, LastVisitStartDate, LastVisitEndDate, abstractEmailMarketing.ServicesId, abstractEmailMarketing.Gender, abstractEmailMarketing.SalonId);

            var response =  this.abstractEmailMarketingDao.EmailMarketing_Upsert(abstractEmailMarketing , responseAll.Values.Count);

            //response.Code == 200 this code run
            if (response.Code == 200)
            {
                if (abstractEmailMarketing.SaveStatus == true)
                {
                    this.abstractSelectPlan_SMSPackagesDao.SelectPlan_CompleteNoOfMsg(responseAll.Values.Count, abstractEmailMarketing.SalonId, 2);
                }

                var HtmlEmailTemplate = HttpUtility.UrlDecode(abstractEmailMarketing.EmailTemplate);

                //UserEmailMarketing table stor data
                if (responseAll.Values.Count > 0)
                {
                    var userIds = new List<int>();

                    for (int i = 0; i < responseAll.Values.Count; i++)
                    {
                        userIds.Add(Convert.ToInt32(responseAll.Values[i].Id));
                    }

                    string UserStr = "";

                    foreach (var item in userIds)
                    {
                        UserStr += item+",";
                    }

                    this.abstractEmailMarketingDao.UserEmailMarketing_Upsert(0, response.Item.Id, UserStr, HtmlEmailTemplate);
                }

            }
            return response;
        }
    }

}