using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.V1;
using BeautyBook.Entities.V1;
using BeautyBook.Infrastructure;
using ClosedXML.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook_Vendor.Controllers
{
    public class InventoryProductController : BaseController
    {
        readonly InventoryProductDao inventoryProductDao = new InventoryProductDao();
        readonly MasterProductTypeDao masterProductTypeDao = new MasterProductTypeDao();
        readonly MasterProductBrandDao masterProductBrandDao = new MasterProductBrandDao();
        readonly MasterProductWeightDao masterProductWeightDao = new MasterProductWeightDao();
        readonly OfflineVendorDao offlineVendorDao = new OfflineVendorDao();


        public ActionResult ManageInventory()
        {
            return View();
        }

        public ActionResult ProductDetails(string InventoryPrdIds = "")
        {
            ViewBag.InventoryPrdId = ConvertTo.Integer(ConvertTo.Base64Decode(InventoryPrdIds));
            return View();
        }

        public ActionResult InventoryProduct()
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
            HttpCookie loginDetails = Request.Cookies["SalonId"];
            int DecodeSalonId = Convert.ToInt32(ConvertTo.Base64Decode(loginDetails.Value));

            //DataSet ds = getDataSetExportToExcel();
            DataTable dt = new DataTable();
            DataTable dt_r = new DataTable();
            DataTable dt_productType = new DataTable();
            DataTable dt_productBrand = new DataTable();
            DataTable dt_productWeightType = new DataTable();
            DataTable dt_productOfflineVendor = new DataTable();

            dt.TableName = "Product Details";
            dt_r.TableName = "Reference";
            dt_productType.TableName = "Product Type";
            dt_productBrand.TableName = "Product Brand";
            dt_productWeightType.TableName = "Product Weight Type";
            dt_productOfflineVendor.TableName = "Offline Vendor";

            //Customers
            dt.Columns.AddRange(new DataColumn[11] {
                                new DataColumn("Product Name",typeof(string)),
                                new DataColumn("Product Type Id",typeof(int)),
                                new DataColumn("Product Brand Id",typeof(int)),
                                new DataColumn("Purchase Date",typeof(string)),
                                new DataColumn("Product Qty",typeof(int)),
                                new DataColumn("Price",typeof(decimal)),
                                new DataColumn("Weight",typeof(decimal)),
                                new DataColumn("Weight Type Id",typeof(int)),
                                new DataColumn("Bill Number",typeof(string)),
                                new DataColumn("Low Qty Alert",typeof(int)),
                                new DataColumn("Offline Vendor Id",typeof(int)),
            });


            //Field Descriptions
            dt_r.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Reference",typeof(string)),
                new DataColumn("Description",typeof(string)),
                new DataColumn("Example",typeof(string))
            });

            dt_r.Rows.Add(
                "Product Type Id", "You can see the product type id from the product type sheet", "1  -  Product  Type Id"
            );
            dt_r.Rows.Add(
                "Product Brand Id", "You can see the product brand id from the product brand sheet" , "2  -  Product  Brand Id"
            );
            dt_r.Rows.Add(
                "Purchase Date", "Please follow this format for date", "MM/DD/YYYY"
            );
            dt_r.Rows.Add(
                "Weight Type Id", "You can see the weight type id from the product weight type sheet", "3  -  Weight  Type Id"
            );


            //Product Type dropdown data [Start]
            dt_productType.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Product Type Id",typeof(int)),
                new DataColumn("Product Type Name",typeof(string))
            });

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 99999;

            var response = masterProductTypeDao.MasterProductType_All(pageParam, "", false);

            if(response.Values.Count > 0)
            {
                for (int i = 0; i < response.Values.Count; i++)
                {
                    dt_productType.Rows.Add(
                       "" + response.Values[i].Id + "", "" + response.Values[i].Name + ""
                    );
                }
            }
            //Product Type dropdown data [End]


            //Product Brand dropdown data [Start]
            dt_productBrand.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Product Brand Id",typeof(int)),
                new DataColumn("Product Brand Name",typeof(string))
            });

           
            var responseBrand = masterProductBrandDao.MasterProductBrand_All(pageParam , "" , DecodeSalonId);

            if(responseBrand.Values.Count > 0)
            {
                for (int i = 0; i < responseBrand.Values.Count; i++)
                {
                    dt_productBrand.Rows.Add(
                        "" + responseBrand.Values[i].Id + "", "" + responseBrand.Values[i].Name + ""
                    );
                }
            }
            //Product Brand dropdown data [End]

            //Product Weight Type dropdown data [Start]
            dt_productWeightType.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Product Weight Type Id",typeof(int)),
                new DataColumn("Product Weight Type Name",typeof(string))
            });

            var responseWeightType = masterProductWeightDao.MasterProductWeight_All(pageParam, "");

            if(responseWeightType.Values.Count > 0)
            {
                for (int i = 0; i < responseWeightType.Values.Count; i++)
                {
                    dt_productWeightType.Rows.Add(
                        "" + responseWeightType.Values[i].Id + "", "" + responseWeightType.Values[i].Name + ""
                    );
                }
            }
            //Product Weight Type dropdown data [End]

            //Offline Data [Start]
            dt_productOfflineVendor.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Vendor Id",typeof(int)),
                new DataColumn("Vendor Name",typeof(string)),
                new DataColumn("Vendor Phone Number",typeof(string)),
                new DataColumn("Vendor Email",typeof(string))
            });

            var responseOfflineVendor = offlineVendorDao.OfflineVendor_All(pageParam, "", DecodeSalonId);

            if (responseOfflineVendor.Values.Count > 0)
            {
                for (int i = 0; i < responseOfflineVendor.Values.Count; i++)
                {
                    dt_productOfflineVendor.Rows.Add(
                        "" + responseOfflineVendor.Values[i].Id + "", 
                        "" + responseOfflineVendor.Values[i].Name + "",
                        "" + responseOfflineVendor.Values[i].PhoneNumber + "",
                        "" + responseOfflineVendor.Values[i].Email + ""
                    );
                }
            }
            //Product Weight Type dropdown data [End]

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Worksheets.Add(dt_r);
                wb.Worksheets.Add(dt_productType);
                wb.Worksheets.Add(dt_productBrand);
                wb.Worksheets.Add(dt_productWeightType);
                wb.Worksheets.Add(dt_productOfflineVendor);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                //dt_r_column.Columns().AdjustToContents();
                //dt_r_column.Columns().AdjustToContents();


                var fileName = "InventoryData" + DateTime.Now.ToString("ddMMyyyyhhmmss");
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
        /// 400 Something went wrong please check your data
        /// 404 Excel sheet data not found please enter a data
        public async Task<ActionResult> ImportInventoryData(HttpPostedFileBase ExcelUrl)
        {
            try
            {
                HttpCookie loginDetails = Request.Cookies["SalonId"];
                int DecodeSalonId = Convert.ToInt32(ConvertTo.Base64Decode(loginDetails.Value));

                ///Upload excel file upload project folder and get URL
                string basePath = "SalonInventoryExcel/" + DecodeSalonId + "/";
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
                        InventoryProduct inventoryProduct = new InventoryProduct();
                        inventoryProduct.SalonId = DecodeSalonId;
                        inventoryProduct.PurchaseVia = 25;
                        inventoryProduct.ProductName = Convert.ToString(worksheet.Cells[row, 1].Value).Trim();
                        inventoryProduct.ProductTypeId = Convert.ToInt64(worksheet.Cells[row, 2].Value);
                        inventoryProduct.ProductBrandId = Convert.ToInt64(worksheet.Cells[row, 3].Value);
                        inventoryProduct.PurchaseDate = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        inventoryProduct.ProductQty = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                        inventoryProduct.Price = Convert.ToDecimal(worksheet.Cells[row, 6].Value);
                        inventoryProduct.Weight = Convert.ToDecimal(worksheet.Cells[row, 7].Value);
                        inventoryProduct.WeightTypeId = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                        inventoryProduct.BillNumber = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();
                        inventoryProduct.LowQtyAlert = Convert.ToInt64(worksheet.Cells[row, 10].Value);
                        inventoryProduct.OfflineVendorId = Convert.ToInt64(worksheet.Cells[row, 11].Value);

                        var response = inventoryProductDao.InventoryProduct_Upsert(inventoryProduct);

                        if (response.Code != 200)
                        {
                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if(rowCount == 1)
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