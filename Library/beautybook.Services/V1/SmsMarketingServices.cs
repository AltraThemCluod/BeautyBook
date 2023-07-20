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
    public class SmsMarketingServices : AbstractSmsMarketingServices
    {
        private AbstractSmsMarketingDao abstractSmsMarketingDao;
        private AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao;
        private AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao;

        public SmsMarketingServices(AbstractSmsMarketingDao abstractSmsMarketingDao,
            AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao,
            AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao
        )
        {
            this.abstractSmsMarketingDao = abstractSmsMarketingDao;
            this.abstractSendEmailMarketingTemplatesDao = abstractSendEmailMarketingTemplatesDao;
            this.abstractSelectPlan_SMSPackagesDao = abstractSelectPlan_SMSPackagesDao;
        }

        public override PagedList<AbstractSmsMarketing> SmsMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            return this.abstractSmsMarketingDao.SmsMarketing_All(pageParam, search, CreatedBy, SalonId);
        }

        public override SuccessResult<AbstractSmsMarketing> SmsMarketing_ById(long Id)
        {
            return this.abstractSmsMarketingDao.SmsMarketing_ById(Id);
        }
        public override SuccessResult<AbstractSmsMarketing> SmsMarketing_Upsert(AbstractSmsMarketing abstractSmsMarketing,long NoOfSMSCount)
        {

            //var responseUpsert = this.abstractSendSmsMarketingTemplatesDao.SendSmsMarketingTemplates_Upsert(abstractSendSmsMarketingTemplates);

            //response.Code == 200 this code run
                
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;
                
                var CustomerSinceStart = "";
                if (abstractSmsMarketing.CustomerSinceStart != "")
                {
                    var StartDateSplit = abstractSmsMarketing.CustomerSinceStart.Split('/');
                    CustomerSinceStart = StartDateSplit[2] + '-' + StartDateSplit[0] + '-' + StartDateSplit[1];
                }

                var CustomerSinceEnd = "";
                if (abstractSmsMarketing.CustomerSinceEnd != "")
                {
                    var EndDateSplit = abstractSmsMarketing.CustomerSinceEnd.Split('/');
                    CustomerSinceEnd = EndDateSplit[2] + '-' + EndDateSplit[0] + '-' + EndDateSplit[1];
                }

                var LastVisitStartDate = "";
                if (abstractSmsMarketing.LastVisitStart != "")
                {
                    var LastVisitStartSplit = abstractSmsMarketing.LastVisitStart.Split('/');
                    LastVisitStartDate = LastVisitStartSplit[2] + '-' + LastVisitStartSplit[0] + '-' + LastVisitStartSplit[1];
                }

                var LastVisitEndDate = "";
                if (abstractSmsMarketing.LastVisitEnd != "")
                {
                    var LastVisitEndDateSplit = abstractSmsMarketing.LastVisitEnd.Split('/');
                    LastVisitEndDate = LastVisitEndDateSplit[2] + '-' + LastVisitEndDateSplit[0] + '-' + LastVisitEndDateSplit[1];
                }

                var HtmlSmsTemplate = HttpUtility.UrlDecode(abstractSmsMarketing.SmsTemplate);

            var responseAll = this.abstractSendEmailMarketingTemplatesDao.EmailUser_All(pageParam, "", CustomerSinceStart, CustomerSinceEnd, abstractSmsMarketing.MinAppoinment, abstractSmsMarketing.MaxAppoinment, abstractSmsMarketing.MinYear, abstractSmsMarketing.MaxYear, LastVisitStartDate, LastVisitEndDate, abstractSmsMarketing.ServicesId, abstractSmsMarketing.Gender, abstractSmsMarketing.SalonId);

            var response = this.abstractSmsMarketingDao.SmsMarketing_Upsert(abstractSmsMarketing, responseAll.Values.Count);
                
            if (response.Code == 200)
            {
                this.abstractSelectPlan_SMSPackagesDao.SelectPlan_CompleteNoOfMsg(responseAll.Values.Count , abstractSmsMarketing.SalonId , 1);


                //UserSmsMarketing table stor data
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
                        UserStr += item + ",";
                    }

                    this.abstractSmsMarketingDao.UserSMSMarketing_Upsert(0, response.Item.Id, UserStr, HtmlSmsTemplate);
                }

            }
            return response;
        }
    }

}