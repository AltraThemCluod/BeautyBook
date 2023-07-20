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
using System.IO;

namespace BeautyBookAdmin.Controllers
{
    public class CreateEmailTemplateController : BaseController
    {
        private readonly AbstractEmailMarketingTemplatesServices abstractEmailMarketingTemplatesServices = null;
        
        public CreateEmailTemplateController(
                AbstractEmailMarketingTemplatesServices abstractEmailMarketingTemplatesServices
            )
        {
            this.abstractEmailMarketingTemplatesServices = abstractEmailMarketingTemplatesServices;
        }

        //SMSPackages Index action
        public ActionResult Index()
        {

            return View();
        }

        //ViewAllEmailTemplate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllEmailTemplate([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Email TemplateDelete
        [HttpPost]
        public JsonResult DeleteEmailTemplate(string IdStr = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(IdStr));
            long DeletedBy = ProjectSession.AdminId;
            var result = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_Delete(Id, DeletedBy);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Email Template Create
        [HttpPost]
        public JsonResult EmailTemplateCreate(HttpPostedFileBase uploadFile, long Id = 0, string templateName = "" , string HtmlTemplatesText = "")
        {
            EmailMarketingTemplates model = new EmailMarketingTemplates();
            model.Id = Id;
            model.Name = templateName;
            model.HtmlTemplatesText = HtmlTemplatesText;

            if (uploadFile != null)
            {
                string basePath = "EmailTemplateImage/" + model.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(uploadFile.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                uploadFile.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));
                model.Image = basePath + fileName;
            }


            var result = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate Email Template
        [HttpPost]
        public JsonResult GetDataEmailTemplate(string etId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(etId));
            SuccessResult<AbstractEmailMarketingTemplates> successResult = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


    }
}