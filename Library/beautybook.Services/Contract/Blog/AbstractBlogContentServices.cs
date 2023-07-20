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
    public abstract class AbstractBlogContentServices
    {
        public abstract SuccessResult<AbstractBlogContent> BlogContent_Upsert(AbstractBlogContent abstractBlogContent);
        public abstract SuccessResult<AbstractBlogContent> BlogContent_Delete(int Id);

    }
}
