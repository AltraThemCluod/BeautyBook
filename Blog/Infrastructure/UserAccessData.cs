using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Infrastructure
{
    public class UserAccessData
    {

        public const string Home = "Home";
        public const string BlogUsers = "BlogUsers";
        public const string Category = "Category";
        public const string Profile = "Profile";
        public const string SubscribeNewslatter = "SubscribeNewslatter";
        public const string UsersBlog = "UsersBlog";

        public static string[] AdminAccess = {Home, BlogUsers, Category , Profile , SubscribeNewslatter, UsersBlog };
        public static string[] EmployeeAccess = {Home, Profile , UsersBlog };

    }
}