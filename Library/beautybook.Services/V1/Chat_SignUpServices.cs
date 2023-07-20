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
    public class Chat_SignUpServices : AbstractChat_SignUpServices
    {
        private AbstractChat_SignUpDao abstractChat_SignUpDao;

        public Chat_SignUpServices(AbstractChat_SignUpDao abstractChat_SignUpDao)
        {
            this.abstractChat_SignUpDao = abstractChat_SignUpDao;
        }

        public override SuccessResult<AbstractChat_SignUp> Chat_SignUp_Upsert(AbstractChat_SignUp abstractChat_SignUp)
        {
            return this.abstractChat_SignUpDao.Chat_SignUp_Upsert(abstractChat_SignUp); ;
        }
        
    }

}