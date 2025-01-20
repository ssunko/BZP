using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;
using System.Net.Mail;
using System.Data.OleDb;
using System.Data.SqlClient;

    /// <summary>
    /// Summary description for GenericLib
    /// </summary>
    public static class GenericLib {
        private const string szPassSolt = "po!6@BZp|trei9#)$4[j"; //po!6@BZp|trei9#)$4[jVgD!s(e_r+g'e>y|#~@S<U;N}K*O/.
        public const string ERROR_LOG = "ERRORS";
        public const string UploadImgFolder = "Pictures";
        public const string UploadTmbFolder = "Thumb";
        public const int StandsFor = 7;
        public const int MaxSize = 3 * 1048576; // 3MB

        public static string LoadHTML(string szPage) {
            string s = "";
            try{
                s = System.IO.File.ReadAllText(szPage);
            }
            catch (Exception e) { s = e.Message; }
            return s;
        }
        public static string BZPConStr(){
            return ConfigurationManager.ConnectionStrings["BZP_main"].ToString();
        }
        public static void CreateLog(string szLogSource, string szLogName, string szLogEntry) {
            //if (!EventLog.SourceExists(szLogSource)){
            //    EventLog.CreateEventSource(szLogSource, szLogName);
            //    return;
            //}
            EventLog myLog = new EventLog();
            myLog.Source = szLogSource;
            myLog.WriteEntry(szLogEntry);
        }
        public static string PasswordEncript(string szPassword) {
            string PasswordEncripted = "";
            for (int i = 0; i < szPassword.Length; i++){
                PasswordEncripted += ((char)((szPassword[i] + szPassSolt[i]) % 255)).ToString();
            }
            return PasswordEncripted;
        }
        public static string PasswordDecript(string szPassword) {
            string PasswordDecripted = "";
            for (int i = 0; i < szPassword.Length; i++){
                PasswordDecripted += ((char)((szPassword[i] - szPassSolt[i]) % 255)).ToString();
            }
            return PasswordDecripted;
        }
        public static string SQLReady(string szStr){
            return szStr.Replace("'","''");
        }
        public static string SysEmail(){
            return ConfigurationManager.AppSettings["SysEmail"].ToString();
        }
        public static string EmailUser(){
            return ConfigurationManager.AppSettings["EmailUser"].ToString();
        }
        public static string EmailPass(){
            return ConfigurationManager.AppSettings["EmailPass"].ToString();
        }
        public static string sSmtpServer(){
            return ConfigurationManager.AppSettings["SmtpServer"].ToString();
        }
        public static bool SendEMail(string mailTo, string mailSubject, string mailBody){
            return SendEMail(SysEmail(), mailTo, mailSubject, mailBody, EmailUser(), EmailPass());        
        }

        public static bool SendEMail(string mailFrom, string mailTo, string mailSubject, string mailBody, string username, string password) {
            bool bRes = true;
            try{
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(sSmtpServer());
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = mailSubject;
                mail.Body = mailBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex){
                bRes = false;
                CreateLog(ERROR_LOG, ERROR_LOG, ex.Message);
            }
            return bRes;
        }

        public static bool SendPassRecoverEMail(string szEmail, string szPassword, string FName, string szFileName){
            bool bRes = true;
            szFileName = szFileName + "PassRecoveryEmail.htm";
            string mailSubject = "BZPage: Forgotten password.";
            string szBody = LoadHTML(szFileName);
            szBody = szBody.Replace("<USER>", " " + FName);
            szBody = szBody.Replace("<PASSWORD>", szPassword);
            //bRes = SendEMail(szEmail, mailSubject, szBody);
            return bRes;
        }
        public static bool SendActivateAccountEMail(UserDAL oUser,  string szFileName ){
            bool bRes = true;
            szFileName = szFileName + "ActivateAccount.htm";
            string szEncrUN = HttpUtility.UrlEncode(PasswordEncript(oUser.LoginID));
            string mailSubject = "BZPage: Account created.";
            string szBody = LoadHTML(szFileName);
            szBody = szBody.Replace("<USER>", " " + oUser.FName);
            szBody = szBody.Replace("<LOGINID>", oUser.LoginID);
            szBody = szBody.Replace("<EncrUN>", szEncrUN);
     
            //bRes = SendEMail(szEmail, mailSubject, szBody);
            return bRes;
        }
     
        public static bool CheckZIP(string szZIP){
            bool bZipValid = false;
            int iRes = 0;
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_CheckZIP";
                cmd.Parameters.Add(new SqlParameter("@ZIP",SqlDbType.NChar, 5));
                cmd.Parameters["@ZIP"].Value = szZIP;
               
                iRes = Convert.ToInt32(cmd.ExecuteScalar());
                bZipValid = (iRes == 1) ? true : false;
                cmd.Dispose();
            }
            return bZipValid;
        }

    } // class GenericLib