
namespace Blog.Infrastructure
{
    public class Enums
    {
        public enum MessageType
        {
            /// <summary>
            /// for Success message Class
            /// </summary>
            success,

            /// <summary>
            /// for error message Class
            /// </summary>
            danger,

            /// <summary>
            /// for Warning message Class
            /// </summary>
            warning,

            /// <summary>
            /// for info message Class
            /// </summary>
            info,

            /// <summary>
            /// for error message Class
            /// </summary>
            error,
        }

        public enum TagStatus
        {
            /// <summary>
            /// for Success message Class
            /// </summary>
            insert,

            /// <summary>
            /// for error message Class
            /// </summary>
            update
        }

        /// <summary>
        /// Gender type male and female
        /// </summary>
        public enum Gender
        {
            Male = 1,
            FeMale = 2
        }

        public enum ResetPasswordType
        {
            /// <summary>
            /// for set Password Type
            /// </summary>
            ResetPassword = 1,

            /// <summary>
            /// for forgot Password Type
            /// </summary>
            ForgotPassword = 2,

            /// <summary>
            /// for set password
            /// </summary>
            SetPassword = 3,

        }

    }
}