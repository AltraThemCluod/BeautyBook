using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace SendEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = "Data Source=rushkar-db-z.cwfpajxcr0v7.ap-south-1.rds.amazonaws.com;Initial Catalog=BeautyBook;User ID=sa;Password=*aA123123;Integrated Security=False;MultipleActiveResultSets=true";

            Console.WriteLine("Getting Connection ...");

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(ConnectionString);
            while (true)
            {
                try
                {
                    string q = "SELECT Id,EmailMarketingId,ToEmail,Body,IsEmailSend,SendEmailDate from [dbo].[UserEmailMarketing] where IsEmailSend = 0";
                    SqlDataAdapter ad = new SqlDataAdapter(q, ConnectionString);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string CheckSaveStatus = $"select count(*) as Count from [dbo].[EmailMarketing] where Id = {Convert.ToInt64(row["EmailMarketingId"])} and SaveStatus = 1";
                            ad = new SqlDataAdapter(CheckSaveStatus, ConnectionString);
                            DataTable CheckSaveStatusdt = new DataTable();
                            ad.Fill(CheckSaveStatusdt);

                            if (CheckSaveStatusdt.Rows.Count > 0 && Convert.ToBoolean(row["IsEmailSend"]) == false && Convert.ToInt32(CheckSaveStatusdt.Rows[0]["Count"]) > 0)
                            {
                                Email(row["Body"].ToString(), row["ToEmail"].ToString(), Convert.ToInt64(row["Id"]), Convert.ToInt64(row["EmailMarketingId"]));
                            }

                        }
                    }
                 
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            Console.Read();
        }

        public static void Email(string htmlString, string ToMailAddress, long UserEmailId, long EmailMarketingId)
        {
            try
            {
            string ConnectionString = "Data Source=rushkar-db-z.cwfpajxcr0v7.ap-south-1.rds.amazonaws.com;Initial Catalog=BeautyBook;User ID=sa;Password=*aA123123;Integrated Security=False;MultipleActiveResultSets=true";
                SqlConnection conn = new SqlConnection(ConnectionString);
                string GetSubjectName = $"select EmailSubject from EmailMarketing where Id = {EmailMarketingId}";
                SqlDataAdapter ad = new SqlDataAdapter(GetSubjectName, ConnectionString);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                var EmailSubjectName = "";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        EmailSubjectName = row["EmailSubject"].ToString();
                    }
                }

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("noreply.beautybook@gmail.com");
                message.To.Add(new MailAddress(ToMailAddress));
                message.Subject = EmailSubjectName;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                //25 or 587(some providers block port 25)
                smtp.Port = 587;
                //in-v3.mailjet.com
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("noreply.beautybook@gmail.com", "tildiixgzjbqorif");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                if (message.To.Count > 0)
                {

                    Console.WriteLine(ToMailAddress);

                    string updateQ = $"UPDATE UserEmailMarketing set IsEmailSend = 1,SendEmailDate='{DateTime.UtcNow.Date.ToString("yyyy-MM-dd")}' where Id = {UserEmailId}";
                    SqlCommand cmd = new SqlCommand(updateQ, conn);
                    conn.Open();
                    Console.WriteLine("Openning Connection ...");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine("Connection successful!");

                    //Email send data count query
                    string DataCount = $"select COUNT(*) as CountEmailMarketing from UserEmailMarketing where EmailMarketingId = {UserEmailId} and IsEmailSend = 0";
                    ad = new SqlDataAdapter(DataCount, ConnectionString);
                    DataTable DataCountadt = new DataTable();
                    ad.Fill(DataCountadt);

                    if (Convert.ToInt32(DataCountadt.Rows[0]["CountEmailMarketing"]) == 0)
                    {
                        string UpdateEmailMarketing = $"UPDATE EmailMarketing set IsSendEmail = 1 where Id = {EmailMarketingId}";
                        SqlCommand UpdateEmailMarketingcmd = new SqlCommand(UpdateEmailMarketing, conn);
                        conn.Open();
                        Console.WriteLine("Openning Connection ...");
                        UpdateEmailMarketingcmd.ExecuteNonQuery();
                        conn.Close();
                        Console.WriteLine("Connection successful!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
