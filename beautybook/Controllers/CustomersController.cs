using BeautyBook.Common;
using BeautyBook.Data.V1;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Infrastructure;
using ClosedXML.Excel;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BeautyBook.Controllers
{
    public class CustomersController : BaseController
    {
        readonly UsersDao usersDao = new UsersDao();

        public ActionResult CustomerDetails()
        {
            return View();
        }

        public ActionResult ManageCustomers()
        {
            return View();
        }

        public ActionResult UploadExcel()
        {
            return View();
        }

        /// <summary>
        /// Download customer data excel formate
        /// </summary>
        public void DownloadExcel()
        {
            //DataSet ds = getDataSetExportToExcel();
            DataTable dt = new DataTable();
            DataTable dt_r = new DataTable();
            
            dt.TableName = "Customers";
            dt_r.TableName = "Reference";

            //Customers
            dt.Columns.AddRange(new DataColumn[9] {
                                new DataColumn("First Name",typeof(string)),
                                new DataColumn("Second Name",typeof(string)),
                                new DataColumn("Gender",typeof(int)),
                                new DataColumn("Dob",typeof(string)),
                                new DataColumn("Email",typeof(string)),
                                new DataColumn("Primary Phone",typeof(string)),
                                new DataColumn("Alternate Phone",typeof(string)),
                                new DataColumn("Refered By Email",typeof(string)),
                                new DataColumn("Tags",typeof(string)),
            });

            //Field Descriptions
            dt_r.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Reference",typeof(string)),
                new DataColumn("Description",typeof(string)),
                new DataColumn("Example",typeof(string))
            });

            dt_r.Rows.Add(
                "Dob", "Please use the date format as follows", "MM/DD/YYYY"
            );
            dt_r.Rows.Add(
                "Email", "Please use the proper email format", "Example@gmail.com"
            );
            dt_r.Rows.Add(
                "PrimaryPhone", "PrimaryPhone Number not be accepted more than 9 and less than 9", "xxxxxxxxx"
            );
            dt_r.Rows.Add(
                "AlternatePhone", "AlternatePhone Number not be accepted more than 9 and less than 9", "xxxxxxxxx"
            );

            var fileName = "CustomerData" + DateTime.Now.ToString("ddMMyyyyhhmmss");

            using (XLWorkbook wb = new XLWorkbook())
            {

                var dt_column = wb.Worksheets.Add(dt);
                var dt_r_column = wb.Worksheets.Add(dt_r);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                //dt_r_column.Columns().AdjustToContents();
                //dt_r_column.Columns().AdjustToContents();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename="+ fileName + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);

                    Response.Flush();
                    Response.End();
                }
            }
        }


        /// <summary>
        /// Import customer data using excel file
        /// </summary>
        /// <param name="ExcelUrl"></param>
        /// <returns></returns>
        public async Task<ActionResult> ImportCustomerData(HttpPostedFileBase ExcelUrl)
        {
            try
            {
                //provide file path

                HttpCookie loginDetails = Request.Cookies["SalonId"];
                int DecodeSalonId = Convert.ToInt32(ConvertTo.Base64Decode(loginDetails.Value));

                ///Upload excel file upload project folder and get URL
                string basePath = "SalonCustomerExcel/" + DecodeSalonId + "/";
                string fileName = DateTime.Now.ToString("ss") + "_" + Path.GetFileName(ExcelUrl.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                ExcelUrl.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));


                FileInfo existingFile = new FileInfo(path + fileName);
                //use EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int colCount = worksheet.Dimension.End.Column; //get Column Count
                    int rowCount = worksheet.Dimension.End.Row; //get row count

                    for (int row = 2; row <= rowCount; row++)
                    {

                        Users users = new Users();
                        users.SalonId = DecodeSalonId;
                        users.LookUpUserTypeId = 4;
                        users.FirstName = Convert.ToString(worksheet.Cells[row, 1].Value).Trim();
                        users.SecondName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        users.Gender = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        users.Dob = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        users.Email = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        users.PrimaryPhone = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        users.AlternatePhone = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        users.ReferedByEmail = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                        users.Tags = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();

                        var response = usersDao.Users_Upsert(users);

                        if(response.Code != 200)
                        {
                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (rowCount == 1)
                    {
                        return Json(404, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }

            return Json(200, JsonRequestBehavior.AllowGet);
        }
    }
}