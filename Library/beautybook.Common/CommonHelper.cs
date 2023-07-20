using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace BeautyBook.Common
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public class CommonHelper
    {
        static object _locker = new object();
        // Instantiate random number generator.  
        private static readonly Random _random = new Random();
        private const double MinWifi_dbm = -80;
        private const double MaxWifi_dbm = -20;



        //public  IQueryable<T> OptionalWhere<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> filter)
        //{
        //    return filter == null
        //        ? queryable
        //        : queryable.Where(filter);
        //}

        public static double GetLatestWifi(double data)
        {
            try
            {
                return Math.Min((int)((1 - (MaxWifi_dbm - data) / (MaxWifi_dbm - MinWifi_dbm)) * 100), 100);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static float GetLatestBattery(double MaxBattery, double MinBattery, double data)
        {
            return Math.Max(Math.Min((int)((1 - (MaxBattery - data) /
                        (MaxBattery - MinBattery)) * 100), 100), 1);
        }

        public static AlarmState GetDeviceAlarmState(bool? HasLostConnectivity, double battery)
        {
            if (HasLostConnectivity.Value)
                return AlarmState.LostConnectivity;

            if (battery < 25)
                return AlarmState.LowBattery;

            return AlarmState.NoAlarm;
        }


        /// <summary>
        /// Generate15s the Enzeoque digits.
        /// </summary>
        /// <returns></returns>
        public static long Generate15EnzeoqueDigits()
        {
            lock (_locker)
            {
                Thread.Sleep(100);
                var number = DateTime.Now.ToString("yyyyMMddHHmmssf");
                return Convert.ToInt64(number);
            }
        }

        public static void LogError(string directory, Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = directory;//Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public static void LogError(string directory, string ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex);
            message += Environment.NewLine;                       
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = directory;//Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        /// <summary>
        /// Deletes the files and folders recursively.
        /// </summary>
        /// <param name="targetDir">The target_dir.</param>
        public static void DeleteFilesAndFoldersRecursively(string targetDir)
        {
            foreach (string file in Directory.GetFiles(targetDir))
            {
                File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }

            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            Directory.Delete(targetDir);
        }

        /// <summary>
        /// Ensures the subscriber email or throw.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new Exception("Email is not valid.");
            }

            return output;
        }

        public static object ShowAlertMessageToastr(string v, object message)
        {
            throw new NotImplementedException();
        }

        public static object ShowNotifyMessage(string v, object message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            return str;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Returns true if the input string contains only numric characters</returns>
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str =>
            {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        /// <summary>
        /// Compare two arrasy
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
        }

        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //set minimum
                _trustLevel = AspNetHostingPermissionLevel.None;

                //determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in
                        new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                    }
                }
            }
            return _trustLevel.Value;
        }

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c;
                else
                    result += c.ToString();
            return result;
        }

        /// <summary>
        /// Sets alert message and displays on page.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="IsConfirmation"></param>
        /// <param name="YesResponseMethod"></param>
        /// <param name="NoResponseMethod"></param>
        /// <returns></returns>
        public static string ShowAlertMessageToastr(string type, string message, bool IsConfirmation = false, string YesResponseMethod = "", string NoResponseMethod = "")
        {
            message = message.Replace("'", " ");
            var strString = @"<script type='text/javascript' language='javascript'>$(function() { ShowMessageToastr('" + type + "','" + message + "','" + IsConfirmation.ToString().ToLower() + "','" + YesResponseMethod + "','" + NoResponseMethod + "') ; })</script>";
            return strString;
        }

        /// <summary>
        /// Sets nofity message and displays on page.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ShowNotifyMessage(string type, string message)
        {
            message = message.Replace("'", " ");
            var strString = @"<script type='text/javascript' language='javascript'>$(function() { $.notify('" + type + "','" + message + "') ; })</script>";
            return strString;
        }

        /// <summary>
        /// Verifies the connectivity.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool VerifyConnectivity(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return true;
        }


        #region Site UrlPath

        /// <summary>
        /// Gets Url Suffix
        /// </summary>
        private static string UrlSuffix
        {
            get
            {
                var req = HttpContext.Current.Request;
                var host = req.Url.Host;
                if (req.IsLocal == true && req.Url.Port != 80)
                    host += ":" + req.Url.Port;

                if (HttpContext.Current.Request.ApplicationPath == "/")
                {
                    return host + HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return host + HttpContext.Current.Request.ApplicationPath + "/";
                }
            }
        }


        /// <summary>
        /// Gets the Root Path of the Project
        /// </summary>
        public static string ApplicationRootPath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                if (rootPath.EndsWith("\\", StringComparison.CurrentCulture))
                {
                    return rootPath;
                }
                else
                {
                    return rootPath + "\\";
                }
            }
        }

        /// <summary>
        /// Gets HostName
        /// </summary>
        public static string HostName
        {
            get { return HttpContext.Current.Request.Url.Host; }
        }

        /// <summary>
        /// Gets Secure User Base
        /// </summary>
        public static string SecureUrlBase
        {
            get
            {
                return "https://" + UrlSuffix;
            }
        }

        /// <summary>
        /// Gets Url Base
        /// </summary>
        public static string UrlBase
        {
            get
            {
                return "http://" + UrlSuffix;
            }
        }

        /// <summary>
        /// Gets Site Url Base
        /// </summary>
        public static string SiteUrlBase
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrlBase;
                }
                else
                {
                    return UrlBase;
                }
            }
        }

        #endregion Site UrlPath


        public static string OtpGenerate()
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            return new String(stringChars1);
        }

        public static string NumericOtpGenerate()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[4];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            return new String(stringChars1);
        }

        public static string CalculateRelativeTime(DateTime notificationDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - notificationDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours <= 1 ? "1 hour ago" : ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 year ago" : years + " years ago";
            }
        }

        public static bool VerifyPassword(string hashPassword, string password)
        {
            try
            {
                PasswordHasher passwordHasher = new PasswordHasher();
                PasswordVerificationResult passwordHash = passwordHasher.VerifyHashedPassword(hashPassword, password);
                if (passwordHash == (PasswordVerificationResult)1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static string HasPassword(string password)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            return passwordHasher.HashPassword(password);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string GenerateDeviceKey(string serialnumber)
        {
            if (string.IsNullOrEmpty(serialnumber))
            {
                throw new Exception("Cannot generate a key from a null or empty string");
            }

            serialnumber += '\0';
            var length = serialnumber.Length;
            byte[] snbytes = Encoding.UTF8.GetBytes(serialnumber);
            for (int i = 0; i < length - 1; i++)
            {
                snbytes[i + 1] -= 0x30;
                snbytes[i] = (byte)~snbytes[i + 1];
                snbytes[i] &= 0x07;
            }
            snbytes[length - 1] <<= 1;
            snbytes[length - 1] &= 0x07;

            var sb = new StringBuilder();
            for (int j = 0; j < length - 1; j++)
            {
                sb.Append(snbytes[j].ToString());
            }

            return sb.ToString();
        }

        // Generates a random password.
        // 4-LowerCase + 4-Digits + 2-UpperCase  
        public static string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        // Generates a random string with a given size.    
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generates a random number within a range.      
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }

    public class AdminEmailService
    {
        /// <summary>
        /// Sending An Email with master mail template
        /// </summary>
        /// <param name="mailFrom">Mail From</param>
        /// <param name="mailTo">Mail To</param>
        /// <param name="mailCC">Mail CC</param>
        /// <param name="mailBCC">Mail BCC</param>
        /// <param name="subject">Subject of mail</param>
        /// <param name="body">Body of mail</param>
        /// <param name="attachment">Attachment for the mail</param>
        /// <param name="emailType">Email Type</param>
        /// <returns>return send status</returns>
        public static bool Send(string mailTo, string mailCC, string mailBCC, string subject, string body, List<byte[]> attachmentFile = null, List<string> attachmentName = null)
        {
            Boolean issent = true;
            string mailFrom;
            mailFrom = Configurations.FromEmailAddress;

            try
            {
                if (ValidateEmail(mailFrom, mailTo) && (string.IsNullOrEmpty(mailCC) || IsEmail(mailCC)) && (string.IsNullOrEmpty(mailBCC) || IsEmail(mailBCC)))
                {
                    MailMessage mailMesg = new MailMessage();
                    SmtpClient objSMTP = new SmtpClient();

                    if (Configurations.TestMode)
                    {
                        mailFrom = Configurations.TestEmailAddress;
                        mailTo = Configurations.TestEmailAddress;
                        mailCC = string.Empty;
                        mailBCC = string.Empty;
                    }

                    mailMesg.From = new System.Net.Mail.MailAddress(mailFrom);
                    mailMesg.To.Add(mailTo);

                    if (!string.IsNullOrEmpty(mailCC))
                    {
                        string[] mailCCArray = mailCC.Split(';');
                        foreach (string email in mailCCArray)
                        {
                            mailMesg.CC.Add(email);
                        }
                    }

                    if (!string.IsNullOrEmpty(mailBCC))
                    {
                        mailBCC = mailBCC.Replace(";", ",");
                        mailMesg.Bcc.Add(mailBCC);
                    }

                    if (attachmentFile != null && attachmentName != null)
                    {
                        byte[][] Files = attachmentFile.ToArray();
                        string[] Names = attachmentName.ToArray();
                        if (Files != null)
                        {
                            for (int i = 0; i < Files.Length; i++)
                            {
                                mailMesg.Attachments.Add(new Attachment(new MemoryStream(Files[i]), Names[i]));
                            }
                        }
                    }

                    mailMesg.Subject = subject;
                    mailMesg.Body = body;
                    mailMesg.IsBodyHtml = true;

                    try
                    {
                        objSMTP.Send(mailMesg);
                        issent = true;
                        return issent;
                    }
                    catch (Exception)
                    {
                        mailMesg.Dispose();
                        mailMesg = null;
                        //CommonService.Log(e);
                        issent = false;
                        return issent;
                    }
                    finally
                    {
                        mailMesg.Dispose();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Method is used to Validate Email
        /// </summary>
        /// <param name="fromEmail">From email List</param>
        /// <param name="toEmail">To Email list</param>
        /// <returns>Returns validation result</returns>
        private static bool ValidateEmail(string fromEmail, string toEmail)
        {
            bool isValid = true;
            if (!IsEmail(fromEmail))
            {
                isValid = false;
            }

            if (!string.IsNullOrEmpty(toEmail))
            {
                toEmail = toEmail.Replace(" ", string.Empty);
                string[] emailList = null;
                try
                {
                    emailList = toEmail.Split(',');
                }
                catch
                {
                    isValid = false;
                }

                if (emailList != null && emailList.Count() > 0)
                {
                    foreach (string email in emailList)
                    {
                        if (!IsEmail(email))
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Check email string is Email or not
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>return email validation result</returns>
        private static bool IsEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SmsHelper
    {
        public static bool Send(string mobileNumber, string otp = "", string message = "", int IsResend = 0, string applicationCode = "")
        {
            bool IsSuccess = false;
            HttpClient client = new HttpClient();
            if (Configurations.OtpPopup == 1 || Configurations.OtpPopup == 2)
            {
                String SmsApiParam = string.Empty;
                if(IsResend == 1 && Configurations.UseDiffrentCredentialForResend == 1)
                {
                    SmsApiParam = Configurations.ResendSmsApiParameters.Replace(':', '&');
                    client.BaseAddress = new Uri(Configurations.ResendBaseSmsApiAddress);
                }
                else
                {
                    SmsApiParam = Configurations.SmsApiParameters.Replace(':', '&');
                    client.BaseAddress = new Uri(Configurations.BaseSmsApiAddress);
                }
                
                if (otp != "")
                {
                 
                   
                    message = otp + "%20is%20one%20time%20password%20to%20access%20KeVi%20Mart%20portal.%20Thanks%0a"+ applicationCode;
                    string urlparams = SmsApiParam.Replace("MSG", message).Replace("NUM", mobileNumber);
                    HttpResponseMessage response = client.GetAsync(urlparams).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IsSuccess = true;
                    }
                }
                else
                {
                    string urlparams = SmsApiParam.Replace("MSG", message).Replace("NUM", mobileNumber);
                    HttpResponseMessage response = client.GetAsync(urlparams).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IsSuccess = true;
                    }
                }
            }
            return IsSuccess;
        }
    }


}

