using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace beautybook.Controllers
{
    public class SetAuthCookieController : Controller
    {
        //EmployeePermissionServices

        #region Fields
        private readonly AbstractEmployeePermissionServices abstractEmployeePermissionServices;
        #endregion

        #region Ctor
        public SetAuthCookieController(AbstractEmployeePermissionServices abstractEmployeePermissionServices)
        {
            this.abstractEmployeePermissionServices = abstractEmployeePermissionServices;
        }
        #endregion

        public JsonResult SetAuthTokenCookie()
        {
            HttpCookie loginDetails = Request.Cookies["DeviceToken"];
            //HttpCookie EmployeePermissionData = Request.Cookies["EmployeePermissionData"];            
            HttpCookie UserId = Request.Cookies["UserId"];
            HttpCookie UserTypeId = Request.Cookies["UserTypeId"];
            ProjectSession.UserTypeId = Convert.ToInt32(UserTypeId.Value);

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 0;

            var response = abstractEmployeePermissionServices.EmployeePermissionData_EmployeeId(pageParam, "", Convert.ToInt64(ConvertTo.Base64Decode(UserId.Value)), Convert.ToInt64(UserTypeId.Value));

            ProjectSession.PagedData<ProjectSession.EPRoot> list = new ProjectSession.PagedData<ProjectSession.EPRoot>();
            list.Item = response.Values.Select(x => new ProjectSession.EPRoot()
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EMP_Id = x.EMP_Id,
                Value = x.Value,
                ModuleKey = x.ModuleKey,
                IsShow = x.IsShow,
                Controller = x.Controller,
                Action = x.Action,
            }).ToList();

            //list.Item = JsonConvert.DeserializeObject<List<ProjectSession.EPRoot>>(response.Values.ToString());
            ProjectSession.EmployeePermission = list;

            ProjectSession.PagedData<ProjectSession.EmployeePermissionCont> listController = new ProjectSession.PagedData<ProjectSession.EmployeePermissionCont>();

            listController.Item = response.Values.Where(x => x.Controller != null && x.Value == true).Select(x => new ProjectSession.EmployeePermissionCont()
            {
                Controller = x.Controller,
                Action = x.Action,
            }).ToList();

            List<ProjectSession.EmployeePermissionCont> AddHomeController = new List<ProjectSession.EmployeePermissionCont>();
            AddHomeController.Add(new ProjectSession.EmployeePermissionCont { Controller = "Home", Action = "Index" });
            AddHomeController.Add(new ProjectSession.EmployeePermissionCont { Controller = "Home", Action = "Change" });
            AddHomeController.Add(new ProjectSession.EmployeePermissionCont { Controller = "MyProfile", Action = "EditProfile" });
            AddHomeController.Add(new ProjectSession.EmployeePermissionCont { Controller = "Authentication", Action = "ChangePassword" });

            listController.Item.AddRange(AddHomeController);

            ProjectSession.EmployeePermissionController = listController;

            HttpCookie cookie = new HttpCookie("LoginDetails");
            cookie.Values.Add("IsAuthtokenValid", loginDetails.Value.ToString());
            //cookie.Values.Add("EmployeePermissionData", response.Values.ToString());
            //cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Expires = DateTime.Now.AddYears(100);
            Response.Cookies.Add(cookie);
            return Json(true);
        }

        public JsonResult GetPermissionData()
        {
            var response = SetPermissionData();

            return Json(response.Item, JsonRequestBehavior.AllowGet); ;
        }

        public ProjectSession.PagedData<ProjectSession.EPRoot> SetPermissionData()
        {
            var response = ProjectSession.EmployeePermission;

            return response;
        }
    }
}