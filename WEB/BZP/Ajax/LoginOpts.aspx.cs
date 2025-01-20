using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class Ajax_LoginOpts : System.Web.UI.Page{
    string szOpt = "";
    int iOpt;
    const int   LOGIN = 1,
                REGISTER = 2,
                RECOVER_PASSWORD = 3,
                EDIT_PROFILE = 21,
                LOGIN_REGISTER_FAQ = 99,
                DO_LOGIN = 100,
                DO_LOGOFF = 102,
                DO_RECOVER_GET_QUESTION = 104,
                DO_RECOVER_PASSWORD = 106,
                DO_REGISTER = 108,
                DO_LOAD_PROFILE = 110,
                DO_UPDATE_PROFILE = 112;

    private const string szPleaseSelect = " *** Please Select Security Question ***";

    protected void Page_Load(object sender, EventArgs e){
        System.Uri uriReferrer = System.Web.HttpContext.Current.Request.UrlReferrer;
        if (uriReferrer == null)
            return;
        if (uriReferrer.Host != System.Web.HttpContext.Current.Request.Url.Host)
            Response.Redirect(uriReferrer.ToString());

        string szHTMLPath = Request.PhysicalApplicationPath + "HTML\\";
        string szUserName = "";
        string szPassword = "";
        string szFName = "";
        SqlDataReader rdr = null;
        UserDAL oUser;
        string sSeparator = "|!~j";
        Int64 UserID;

        szOpt = Request.QueryString["Opt"];
        if (szOpt == null || szOpt == "")
            szOpt = "1";
        iOpt = int.Parse(szOpt);
        string szHTML = "";
        switch (iOpt){
            case LOGIN:
                szHTMLPath += "Login.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case REGISTER:
                szHTMLPath += "Register.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                szHTML = GetDDs(szHTML);
                break;
            case RECOVER_PASSWORD:
                szHTMLPath += "Recover.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;            
            case EDIT_PROFILE:
                szHTMLPath += "EditProfile.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                szHTML = GetDDs(szHTML);
                break;            
            case LOGIN_REGISTER_FAQ:
                szHTMLPath += "LoginRegFAQ.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case DO_LOGIN:                
                szUserName = Request.QueryString["UN"];
                szPassword = Request.QueryString["PW"];
                Int64 iUserID = 0;
                Int16 iUserTypeID = 0;
                string sUEmail = "";
                string sUPhone = "";
                string sUZIP = "";
                szHTML = "0";
                try{
                    szPassword = GenericLib.PasswordEncript(szPassword);
                    using (SqlConnection DbConnection = new SqlConnection()){
                        DbConnection.ConnectionString = GenericLib.BZPConStr();
                        DbConnection.Open();

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = DbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "P_CheckLogin";
                        cmd.Parameters.Add(new SqlParameter("@LoginID",SqlDbType.NVarChar,50));
                        cmd.Parameters["@LoginID"].Value = GenericLib.SQLReady(szUserName);
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 20));
                        cmd.Parameters["@Password"].Value = GenericLib.SQLReady(szPassword);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read()) {
                            szFName = rdr["FName"].ToString();
                            iUserID = (Int64) rdr["UserID"];
                            iUserTypeID = (Int16)rdr["UserTypeID"];
                            sUEmail = rdr["Email"].ToString();
                            sUPhone = rdr["Phone"].ToString();
                            sUZIP = rdr["ZIP"].ToString();
                        }
                        cmd.Dispose();
                        rdr.Dispose();
                    }
                }
                catch(Exception ex){
                    GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
                }                
                if (iUserID != 0){
                    Session["UName"] = szUserName;
                    Session["FName"] = szFName;
                    Session["UserTypeID"] = iUserTypeID;
                    Session["UserID"] = iUserID;
                    Session["Email"] = sUEmail;
                    Session["Phone"] = sUPhone;
                    Session["ZIP"] = sUZIP;       
                    szHTML = "1";
                }
                break;
            case DO_LOGOFF:
                Session.Remove("UName");
                Session.Remove("FName");
                Session.Remove("UserTypeID");
                Session.Remove("UserID");
                Session.Remove("Email");
                Session.Remove("Phone");
                Session.Remove("ZIP");
                szHTML = "1";
                break;
            case DO_RECOVER_GET_QUESTION:
                szUserName = Request.QueryString["UN"];
                string szRecTryNum = Request.QueryString["TN"];
                szHTML = "0";
                try{
                    using (SqlConnection DbConnection = new SqlConnection()){
                        DbConnection.ConnectionString = GenericLib.BZPConStr();
                        DbConnection.Open();

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = DbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "P_PassRecoverQuestions_GET";
                        cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.NVarChar, 50));
                        cmd.Parameters["@LoginID"].Value = GenericLib.SQLReady(szUserName);
                        cmd.Parameters.Add(new SqlParameter("@RecTryNum", SqlDbType.TinyInt));
                        cmd.Parameters["@RecTryNum"].Value = szRecTryNum;
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read()){
                            szHTML = rdr["Question"].ToString() + "<Q|A>" + rdr["Answer"].ToString();
                        }
                        
                        cmd.Dispose();
                        rdr.Dispose();
                    }
                }
                catch (Exception ex) {
                    GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
                }               
                break;
            case DO_RECOVER_PASSWORD:
                szUserName = Request.QueryString["UN"];
                string szEmail = "";
                bool bRes = true;
                try{
                    using (SqlConnection DbConnection = new SqlConnection()){
                        DbConnection.ConnectionString = GenericLib.BZPConStr();
                        DbConnection.Open();

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = DbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "P_GetEmailByLoginID";
                        cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.NVarChar, 50));
                        cmd.Parameters["@LoginID"].Value = GenericLib.SQLReady(szUserName);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read()){
                            szEmail = rdr["Email"].ToString();
                            szPassword = rdr["Password"].ToString();
                            szFName = rdr["FName"].ToString();
                        }
                        cmd.Dispose();
                        rdr.Dispose();
                        szPassword = GenericLib.PasswordDecript(szPassword);
                    }
                }
                catch (Exception ex){
                    GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
                    bRes = false;
                }
                if(bRes) bRes = GenericLib.SendPassRecoverEMail(szEmail, szPassword, szFName, Request.PhysicalApplicationPath + "TEXT\\");
                szHTML =(bRes)? "1": "0";
                break;

            case DO_REGISTER:
                oUser = new UserDAL();
                oUser.LoginID = Request.QueryString["LoginID"].ToString();
                oUser.Password= Request.QueryString["Password"].ToString();
                oUser.Email = Request.QueryString["Email"].ToString();
                oUser.FName = Request.QueryString["FName"].ToString();
                oUser.QuestionID1 = Int16.Parse(Request.QueryString["QuestionID1"]);
                oUser.Answer1 = Request.QueryString["Answer1"].ToString();               
                
                if(Request.QueryString["LName"]!=null)
                     oUser.LName = Request.QueryString["LName"];

                if(Request.QueryString["Phone1"]!=null)
                    oUser.Phone1 = Request.QueryString["Phone1"];

                if (Request.QueryString["Phone2"] != null)
                    oUser.Phone2 = Request.QueryString["Phone2"];

                if (Request.QueryString["FAX"] != null)
                    oUser.FAX = Request.QueryString["FAX"];

                if (Request.QueryString["Address1"] != null)
                    oUser.Address1 = Request.QueryString["Address1"];

                if (Request.QueryString["Address2"] != null)
                    oUser.Address2 = Request.QueryString["Address2"];

                if (Request.QueryString["City"] != null)
                    oUser.City = Request.QueryString["City"];

                if (Request.QueryString["StateID"] != null)
                    oUser.StateID = Int16.Parse(Request.QueryString["StateID"]);

                if (Request.QueryString["ZIP"] != null)
                    oUser.ZIP = Request.QueryString["ZIP"];

                if (Request.QueryString["QuestionID2"] != null)
                    oUser.QuestionID2 = Int16.Parse(Request.QueryString["QuestionID2"]);

                if (Request.QueryString["Answer2"] != null)
                    oUser.Answer2 = Request.QueryString["Answer2"];

                if (Request.QueryString["QuestionID3"] != null)
                    oUser.QuestionID3 = Int16.Parse(Request.QueryString["QuestionID3"]);

                if (Request.QueryString["Answer3"] != null)
                    oUser.Answer3 = Request.QueryString["Answer3"];

                szHTML = oUser.Save().ToString();
                if (szHTML == "1"){
                    bRes = GenericLib.SendActivateAccountEMail(oUser, Request.PhysicalApplicationPath + "TEXT\\");
                    if (!bRes) szHTML = "37";
                }
                break;
            case DO_LOAD_PROFILE:
                UserID = 0;
                szHTML = "";
                if(Session["UserID"]!=null){
                    UserID = (Int64)Session["UserID"];
                    oUser = new UserDAL(UserID);
                    szHTML += oUser.LoginID.ToString(); 
                    szHTML += sSeparator + oUser.Email.ToString(); 
                    szHTML += sSeparator + oUser.FName.ToString(); 
                    szHTML += sSeparator + oUser.Password.ToString();
                    szHTML += sSeparator + oUser.QuestionID1.ToString();
                    szHTML += sSeparator + oUser.Answer1.ToString();
                    // nullable fields
                    szHTML += sSeparator;
                    if(oUser.LName!="") szHTML += oUser.LName.ToString();
                    szHTML += sSeparator;
                    if (oUser.Phone1 != "") szHTML += oUser.Phone1.ToString();
                    szHTML += sSeparator;
                    if(oUser.Phone2!="") szHTML += oUser.Phone2.ToString();
                    szHTML += sSeparator;
                    if(oUser.FAX!="") szHTML += oUser.FAX.ToString();
                    szHTML += sSeparator;
                    if(oUser.Address1!="") szHTML += oUser.Address1.ToString();
                    szHTML += sSeparator;
                    if(oUser.Address2!="") szHTML += oUser.Address2.ToString();
                    szHTML += sSeparator;
                    if(oUser.City!="") szHTML += oUser.City.ToString();
                    szHTML += sSeparator;
                    if(oUser.StateID!=0) szHTML += oUser.StateID.ToString();
                    szHTML += sSeparator;
                    if(oUser.ZIP!="") szHTML += oUser.ZIP.ToString();
                    szHTML += sSeparator;
                    if(oUser.QuestionID2!=0) szHTML += oUser.QuestionID2.ToString();
                    szHTML += sSeparator;                     
                    if(oUser.Answer2!="") szHTML += oUser.Answer2.ToString();
                    szHTML += sSeparator;
                    if(oUser.QuestionID3!=0) szHTML += oUser.QuestionID3.ToString();
                    szHTML += sSeparator;
                    if(oUser.Answer3!="") szHTML += oUser.Answer3.ToString();
                }
                break;
            case DO_UPDATE_PROFILE:
                UserID = 0;
                szHTML = "";
                if(Session["UserID"]!=null){
                    UserID = (Int64)Session["UserID"];
                    oUser = new UserDAL(UserID);
                    oUser.LoginID = Request.QueryString["LoginID"].ToString();
                    oUser.Password= Request.QueryString["Password"].ToString();
                    oUser.Email = Request.QueryString["Email"].ToString();
                    oUser.FName = Request.QueryString["FName"].ToString();
                    oUser.QuestionID1 = Int16.Parse(Request.QueryString["QuestionID1"]);
                    oUser.Answer1 = Request.QueryString["Answer1"].ToString();               
                    
                    if(Request.QueryString["LName"]!=null)
                        oUser.LName = Request.QueryString["LName"];

                    if(Request.QueryString["Phone1"]!=null)
                        oUser.Phone1 = Request.QueryString["Phone1"];

                    if (Request.QueryString["Phone2"] != null)
                        oUser.Phone2 = Request.QueryString["Phone2"];

                    if (Request.QueryString["FAX"] != null)
                        oUser.FAX = Request.QueryString["FAX"];

                    if (Request.QueryString["Address1"] != null)
                        oUser.Address1 = Request.QueryString["Address1"];

                    if (Request.QueryString["Address2"] != null)
                        oUser.Address2 = Request.QueryString["Address2"];

                    if (Request.QueryString["City"] != null)
                        oUser.City = Request.QueryString["City"];

                    if (Request.QueryString["StateID"] != null)
                        oUser.StateID = Int16.Parse(Request.QueryString["StateID"]);

                    if (Request.QueryString["ZIP"] != null)
                        oUser.ZIP = Request.QueryString["ZIP"];

                    if (Request.QueryString["QuestionID2"] != null)
                        oUser.QuestionID2 = Int16.Parse(Request.QueryString["QuestionID2"]);

                    if (Request.QueryString["Answer2"] != null)
                        oUser.Answer2 = Request.QueryString["Answer2"];

                    if (Request.QueryString["QuestionID3"] != null)
                        oUser.QuestionID3 = Int16.Parse(Request.QueryString["QuestionID3"]);

                    if (Request.QueryString["Answer3"] != null)
                        oUser.Answer3 = Request.QueryString["Answer3"];

                    szHTML = oUser.Save().ToString();
                    if(szHTML=="1") Session["uad"] = 11;
                }else{
                    Session["uad"] = 10;
                    szHTML = "1";
                }
                break;
        }
        Response.Write(szHTML);
    }

    private string GetDDs(string szHTML){
        szHTML = szHTML.Replace("DDSTATES", HTMLLib.GetDDStates("cboStates", "cssDropDown", true, "", ""));
        string szDDQuestions = HTMLLib.GetDDRecoverQuestions("RecQues_01", "cssDropDownLong", true, szPleaseSelect);
        szHTML = szHTML.Replace("RecQues_01", szDDQuestions);
        szDDQuestions = szDDQuestions.Replace("RecQues_01", "RecQues_02");
        szDDQuestions = szDDQuestions.Replace(szPleaseSelect, "");
        szHTML = szHTML.Replace("RecQues_02", szDDQuestions);
        szDDQuestions = szDDQuestions.Replace("RecQues_02", "RecQues_03");
        szHTML = szHTML.Replace("RecQues_03", szDDQuestions);
        return szHTML;
    }
}
