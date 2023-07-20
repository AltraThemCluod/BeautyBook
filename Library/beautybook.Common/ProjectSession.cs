using System.Web;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using BeautyBook.Common.Paging;

namespace BeautyBook.Common
{
    public class ProjectSession
    {
        public class EPRoot
        {
            public long Id { get; set; }
            public long EmployeeId { get; set; }
            public long EMP_Id { get; set; }
            public bool Value { get; set; }
            public string ModuleKey { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public bool IsShow { get; set; }
        }

        public class EmployeePermissionCont
        {
            public string Controller { get; set; }
            public string Action { get; set; }
            public bool Value { get; set; }
        }

        public class PagedData<T>
        {
            public List<T> Item { get; set; }
        }

        public static PagedData<EPRoot> EmployeePermission
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeePermission"] == null)
                {
                    return null;
                }
                else
                {
                    return (PagedData<EPRoot>)System.Web.HttpContext.Current.Session["EmployeePermission"];
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["EmployeePermission"] = value;
            }
        }

        public static PagedData<EmployeePermissionCont> EmployeePermissionController
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["EmployeePermissionController"] == null)
                {
                    return null;
                }
                else
                {
                    return (PagedData<EmployeePermissionCont>)System.Web.HttpContext.Current.Session["EmployeePermissionController"];
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["EmployeePermissionController"] = value;
            }
        }

        public static long AdminId
        {
            get
            {
                if (HttpContext.Current.Session["AdminId"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertTo.Integer(HttpContext.Current.Session["AdminId"]);
                }
            }

            set
            {
                HttpContext.Current.Session["AdminId"] = value;
            }
        }

        public static string AdminName
        {
            get
            {
                if (HttpContext.Current.Session["AdminName"] == "")
                {
                    return "-";
                }
                else
                {
                    return ConvertTo.String(HttpContext.Current.Session["AdminName"]);
                }
            }

            set
            {
                HttpContext.Current.Session["AdminName"] = value;
            }
        }

        public static string AdminProfile
        {
            get
            {
                if (HttpContext.Current.Session["AdminProfile"] == "")
                {
                    return "-";
                }
                else
                {
                    return ConvertTo.String(HttpContext.Current.Session["AdminProfile"]);
                }
            }

            set
            {
                HttpContext.Current.Session["AdminProfile"] = value;
            }
        }

        public static int UserTypeId
        {
            get
            {
                if (HttpContext.Current.Session["UserTypeId"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertTo.Integer(HttpContext.Current.Session["UserTypeId"]);
                }
            }

            set
            {
                HttpContext.Current.Session["UserTypeId"] = value;
            }
        }

    }
}