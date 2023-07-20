using DataTables.Mvc;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using BeautyBookAdmin.Infrastructure;
using BeautyBookAdmin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class ChangePasswordController : BaseController
    {
        #region Fields
        private readonly AbstractAdminServices abstractAdminServices;
        #endregion

        #region Ctor
        public ChangePasswordController(AbstractAdminServices abstractAdminServices)
        {
            this.abstractAdminServices = abstractAdminServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ActionName(Actions.ChangePassword)]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName(Actions.ChangePassword)]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            SuccessResult<AbstractAdmin> result1 = abstractAdminServices.Admin_ChangePassword(ProjectSession.AdminId, oldPassword, newPassword, confirmPassword);
            return Json(result1, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}