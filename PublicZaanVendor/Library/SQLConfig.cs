namespace PublicZaanVendor.Data
{
    /// <summary>
    /// SQL configuration class which holds stored procedure name.
    /// </summary>
    internal sealed class SQLConfig
    {
        #region BlogCategory
        public const string BlogCategory_ActInAct = "BlogCategory_ActInAct";
        public const string BlogCategory_Upsert = "BlogCategory_Upsert";
        public const string BlogCategory_All = "BlogCategory_All";
        public const string BlogCategory_ById = "BlogCategory_ById";
        public const string BlogCategory_Delete = "BlogCategory_Delete";
        #endregion

        #region BlogUsers
        public const string BlogUsers_ActInAct = "BlogUsers_ActInAct";
        public const string BlogUsers_Upsert = "BlogUsers_Upsert";
        public const string BlogUsers_All = "BlogUsers_All";
        public const string BlogUsers_ById = "BlogUsers_ById";
        public const string BlogUser_Login = "BlogUser_Login";
        public const string BlogUsers_ChangePassword = "BlogUsers_ChangePassword";
        public const string BlogUsers_Delete = "BlogUsers_Delete";
        #endregion

        #region Blog
        public const string Blog_ActInAct = "Blog_ActInAct";
        public const string Blog_Upsert = "Blog_Upsert";
        public const string Blog_All = "Blog_All";
        public const string BlogTopArticles_All = "BlogTopArticles_All";
        public const string BlogTopPost_All = "BlogTopPost_All";
        public const string Blog_ById = "Blog_ById";
        public const string BlogSharedCount = "BlogSharedCount";
        public const string Blog_Delete = "Blog_Delete";
        public const string BlogImages_All = "BlogImages_All";
        public const string BlogImages_Delete = "BlogImages_Delete";
        public const string BlogImages_Upsert = "BlogImages_Upsert";
        public const string SocialShare_Insert = "SocialShare_Insert";
        public const string BlogLike_Insert = "BlogLike_Insert";
        #endregion

        #region BlogContent
        public const string BlogContent_Upsert = "BlogContent_Upsert";
        public const string BlogContent_Delete = "BlogContent_Delete";
        #endregion

        #region BlogSubscribeNewslatter
        public const string BlogSubscribeNewslatter_Insert = "BlogSubscribeNewslatter_Insert";
        public const string BlogSubscribeNewslatter_All = "BlogSubscribeNewslatter_All";
        #endregion
    }
}
