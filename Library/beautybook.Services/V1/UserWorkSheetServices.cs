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
    public class UserWorkSheetServices : AbstractUserWorkSheetServices
    {
        private AbstractUserWorkSheetDao abstractUserWorkSheetDao;

        public UserWorkSheetServices(AbstractUserWorkSheetDao abstractUserWorkSheetDao)
        {
            this.abstractUserWorkSheetDao = abstractUserWorkSheetDao;
        }
        public override PagedList<AbstractUserWorkSheet> UserWorkSheet_All(PageParam pageParam, string search, AbstractUserWorkSheet abstractUserWorkSheet)
        {
            return this.abstractUserWorkSheetDao.UserWorkSheet_All(pageParam, search, abstractUserWorkSheet);
        }
        public override SuccessResult<AbstractUserWorkSheet> UserWorkSheet_ById(long Id)
        {
            return this.abstractUserWorkSheetDao.UserWorkSheet_ById(Id);
        }
        public override SuccessResult<AbstractUserWorkSheet> UserWorkSheet_Upsert(AbstractUserWorkSheet abstractUserWorkSheet)
        {
            return this.abstractUserWorkSheetDao.UserWorkSheet_Upsert(abstractUserWorkSheet);
        }       
    }

}