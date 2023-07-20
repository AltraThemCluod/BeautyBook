using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = "Data Source=rushkar-db-z.cwfpajxcr0v7.ap-south-1.rds.amazonaws.com;Initial Catalog=BeautyBook;User ID=sa;Password=*aA123123;Integrated Security=False;MultipleActiveResultSets=true";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(ConnectionString);
            while (true)
            {
                try
                {
                    string q = "SELECT Id,SMSMarketingId,ToSMS,Body,IsSMSSend,SendSMSDate from [dbo].[UserSmsMarketing] where IsSMSSend = 0";
                    SqlDataAdapter ad = new SqlDataAdapter(q, ConnectionString);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (dt.Rows.Count > 0 && Convert.ToBoolean(row["IsSMSSend"]) == false)
                            {
                                SendMsgViaTwilio(row["Body"].ToString(), row["ToSMS"].ToString(), Convert.ToInt64(row["Id"]), Convert.ToInt64(row["SMSMarketingId"]));
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

        public static string SendMsgViaTwilio(string msgBody, string mobileno,long UserSmsId,long SMSMarketingId)
        {
            string status = "FAIL";
            try
            {

                string ConnectionString = "Data Source=rushkar-db-z.cwfpajxcr0v7.ap-south-1.rds.amazonaws.com;Initial Catalog=BeautyBook;User ID=sa;Password=*aA123123;Integrated Security=False;MultipleActiveResultSets=true";
                SqlConnection conn = new SqlConnection(ConnectionString);
                

                string GetSubjectName = $"select Id from UserSmsMarketing where Id = {SMSMarketingId}";
                SqlDataAdapter ad = new SqlDataAdapter(GetSubjectName, ConnectionString);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                string accountSid = "AC2fdb796d1b54264fabd936eee253b25f";
                string authToken = "f2ed09678f4fb4b515c4adbb1d14d8c0";
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                  body: msgBody,     // replace with Body
                  to: new Twilio.Types.PhoneNumber("+966" + mobileno),   // replace with customer number 
                  from: new Twilio.Types.PhoneNumber("+17124236474") // +966540924078 //
                );

                status = "SUCCESS";
                
                if (status == "SUCCESS")
                {
                    string updateQ = $"UPDATE UserSmsMarketing set IsSMSSend = 1,SendSMSDate='{DateTime.UtcNow.Date.ToString("yyyy-MM-dd")}' where Id = {UserSmsId}";
                    SqlCommand cmd = new SqlCommand(updateQ, conn);
                    conn.Open();
                    Console.WriteLine("Openning Connection ...");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine("Connection successful!");

                    //Email send data count query
                    string DataCount = $"select COUNT(*) as CountEmailMarketing from UserSmsMarketing where SMSMarketingId = {UserSmsId} and IsSMSSend = 0";
                    ad = new SqlDataAdapter(DataCount, ConnectionString);
                    DataTable DataCountadt = new DataTable();
                    ad.Fill(DataCountadt);

                    if (Convert.ToInt32(DataCountadt.Rows[0]["CountEmailMarketing"]) == 0)
                    {
                        string UpdateEmailMarketing = $"UPDATE UserSmsMarketing set IsSMSSend = 1 where Id = {UserSmsId}";
                        SqlCommand UpdateEmailMarketingcmd = new SqlCommand(UpdateEmailMarketing, conn);
                        conn.Open();
                        Console.WriteLine("Openning Connection ...");
                        UpdateEmailMarketingcmd.ExecuteNonQuery();
                        conn.Close();
                        Console.WriteLine("Connection successful!");
                    }
                }
            }
            catch (Exception ex)
            {
                status = "FAIL" + ex.Message;
            }

            return status;
        }
    }
}

//If you lose your phone, or don't have access to your verification device, this code is your failsafe to access your account.
//rB1wKI6vXJGz_6Pqqr7dIRwlAlTpG7Iu0SEeFKgy