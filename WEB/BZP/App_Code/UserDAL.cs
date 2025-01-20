using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UserDAL
/// </summary>
public class UserDAL{

    private enum UserType {User = 1, PremiumUser, Admin};

    private Int64 _UserID;
    private Int16 _UserTypeID;
    private string _LoginID;
    private string _Password;
    private string _Email;
    private string _FName;
    private string _LName;
    private string _Phone1;
    private string _Phone2;
    private string _FAX;
    private string _Address1;
    private string _Address2;
    private string _City;
    private Int16 _StateID;
    private string _ZIP;
    private Int16 _QuestionID1;
    private string _Answer1;
    private Int16 _QuestionID2;
    private string _Answer2;
    private Int16 _QuestionID3;
    private string _Answer3;
    DateTime _Created;
    DateTime _LastModified;

    private string szErrorMessage;

	public UserDAL(){
        _UserID = 0;
        _UserTypeID = (Int16)UserType.User;
        _StateID = 0;
        _QuestionID1 = 0;
        _QuestionID2 = 0;
        _QuestionID3 = 0;
        szErrorMessage = "";
        _Created = DateTime.Now;
        _LastModified = _Created;
    }

    public UserDAL( Int64 iUserID){
        _UserID = iUserID;
        szErrorMessage = "";
        LoadUser();
    }
#region " ***   Properties   *** "
    public Int64 UserID{
        get{ return _UserID; }
        set{ _UserID = value; }
    }
    public Int16 UserTypeID{
        get{ return _UserTypeID; }
        set{ _UserTypeID = value; }
    }
    public string LoginID{
        get{ return _LoginID; }
        set{ _LoginID = value; }
    }
    public string Password{
        get{ return GenericLib.PasswordDecript(_Password); }
        set{ _Password = GenericLib.PasswordEncript(value); }
    }
    public string Email{
        get{ return _Email; }
        set{ _Email = value; }
    }
    public string FName{
        get{ return _FName; }
        set{ _FName = value; }
    }
    public string LName{
        get{ return _LName; }
        set{ _LName = value; }
    }
    public string Phone1{
        get{ return _Phone1; }
        set{ _Phone1 = value; }
    }
    public string Phone2{
        get{ return _Phone2; }
        set{ _Phone2 = value; }
    }
    public string FAX{
        get{ return _FAX; }
        set{ _FAX = value; }
    }
    public string Address1{
        get{ return _Address1; }
        set{ _Address1 = value; }
    }
    public string Address2{
        get{ return _Address2; }
        set{ _Address2 = value; }
    }
    public string City{
        get{ return _City; }
        set{ _City = value; }
    }
    public string State{
        get{ return GetStateByID(); }
    }
    public Int16 StateID{
        get{ return _StateID; }
        set{ _StateID = value; }
    }    
    public string ZIP{
        get{ return _ZIP; }
        set{ _ZIP = value; }
    }
    public Int16 QuestionID1{
        get{ return _QuestionID1; }
        set{ _QuestionID1 = value; }
    }
    public string Answer1{
        get{ return _Answer1; }
        set{ _Answer1 = value; }
    }
    public Int16 QuestionID2{
        get{ return _QuestionID2; }
        set{ _QuestionID2 = value; }
    }
    public string Answer2{
        get{ return _Answer2; }
        set{ _Answer2 = value; }
    }
    public Int16 QuestionID3{
        get{ return _QuestionID3; }
        set{ _QuestionID3 = value; }
    }
    public string Answer3{
        get{ return _Answer3; }
        set{ _Answer3 = value; }
    }
    public DateTime Created{
        get{ return _Created; }
        set{ _Created = value; }
    }
    public DateTime LastModified{
        get{ return _LastModified; }
        set{ _LastModified = value; }
    }
    public string Error{
        get{ return szErrorMessage; }
    }

#endregion

    private string GetStateByID(){
        if(StateID!=0){
            return "";
        }else{
            return "";
        }
    }

    public int Save(){
        int iRes = 0;
        if (_UserID == 0){
            if (ValidateUser()) iRes = SetNewUser();
        }else{
            if (ValidateUser()) iRes = UpdateUser();
        }
        return iRes;
    }

    private  int UpdateUser(){
        int iRes = 0;
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_UpdateUser";
                cmd.Parameters.Add(new SqlParameter("@UserID",SqlDbType.BigInt));
                cmd.Parameters["@UserID"].Value = _UserID;
                cmd.Parameters.Add(new SqlParameter("@LoginID",SqlDbType.NVarChar, 20));
                cmd.Parameters["@LoginID"].Value = _LoginID;
                cmd.Parameters.Add(new SqlParameter("@Password",SqlDbType.NVarChar, 20));
                cmd.Parameters["@Password"].Value = _Password;
                cmd.Parameters.Add(new SqlParameter("@Email",SqlDbType.NVarChar, 50));
                cmd.Parameters["@Email"].Value = _Email;
                cmd.Parameters.Add(new SqlParameter("@FName",SqlDbType.NVarChar, 25));
                cmd.Parameters["@FName"].Value = _FName;
                cmd.Parameters.Add(new SqlParameter("@QuestionID1",SqlDbType.SmallInt));
                cmd.Parameters["@QuestionID1"].Value = _QuestionID1;
                cmd.Parameters.Add(new SqlParameter("@Answer1",SqlDbType.NVarChar, 100));
                cmd.Parameters["@Answer1"].Value = _Answer1;
                cmd.Parameters.Add(new SqlParameter("@Created",SqlDbType.DateTime));
                cmd.Parameters["@Created"].Value = _Created;
                cmd.Parameters.Add(new SqlParameter("@LastModified",SqlDbType.DateTime));
                cmd.Parameters["@LastModified"].Value = _LastModified;

                if(_LName!=null){
                    cmd.Parameters.Add(new SqlParameter("@LName",SqlDbType.NVarChar, 25));
                    cmd.Parameters["@LName"].Value = _LName;
                }
                if(_Phone1!=null){
                    cmd.Parameters.Add(new SqlParameter("@Phone1",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@Phone1"].Value = _Phone1;
                }
                if(_Phone2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Phone2",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@Phone2"].Value = _Phone2;
                }
                if(_FAX!=null){
                    cmd.Parameters.Add(new SqlParameter("@FAX",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@FAX"].Value = _FAX;
                }
                if(_Address1!=null){
                    cmd.Parameters.Add(new SqlParameter("@Address1",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Address1"].Value = _Address1;
                }
                if(_Address2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Address2",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Address2"].Value = _Address2;
                }
                if(_City!=null){
                    cmd.Parameters.Add(new SqlParameter("@City",SqlDbType.NVarChar, 50));
                    cmd.Parameters["@City"].Value = _City;
                }
                if(_StateID!=0){
                    cmd.Parameters.Add(new SqlParameter("@StateID",SqlDbType.SmallInt));
                    cmd.Parameters["@StateID"].Value = _StateID;
                }
                if(_ZIP!=null){
                    cmd.Parameters.Add(new SqlParameter("@ZIP",SqlDbType.NChar, 5));
                    cmd.Parameters["@ZIP"].Value = ZIP;
                }
                if(_QuestionID2!=0){
                    cmd.Parameters.Add(new SqlParameter("@QuestionID2",SqlDbType.SmallInt));
                    cmd.Parameters["@QuestionID2"].Value = _QuestionID2;
                }
                if(_Answer2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Answer2",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Answer2"].Value = _Answer2;
                }
                if(_QuestionID3!=0){
                    cmd.Parameters.Add(new SqlParameter("@QuestionID3",SqlDbType.SmallInt));
                    cmd.Parameters["@QuestionID3"].Value = _QuestionID3;
                }
                if(_Answer3!=null){
                    cmd.Parameters.Add(new SqlParameter("@Answer3",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Answer3"].Value = _Answer3;
                }

                iRes = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
        return iRes;
    }

    private  int SetNewUser(){
        int iRes = 0;
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_SetUser";

                cmd.Parameters.Add(new SqlParameter("@UserTypeID",SqlDbType.SmallInt));
                cmd.Parameters["@UserTypeID"].Value = UserTypeID;
                cmd.Parameters.Add(new SqlParameter("@LoginID",SqlDbType.NVarChar, 20));
                cmd.Parameters["@LoginID"].Value = LoginID;
                cmd.Parameters.Add(new SqlParameter("@Password",SqlDbType.NVarChar, 20));
                cmd.Parameters["@Password"].Value = _Password;
                cmd.Parameters.Add(new SqlParameter("@Email",SqlDbType.NVarChar, 50));
                cmd.Parameters["@Email"].Value = Email;
                cmd.Parameters.Add(new SqlParameter("@FName",SqlDbType.NVarChar, 25));
                cmd.Parameters["@FName"].Value = FName;
                cmd.Parameters.Add(new SqlParameter("@QuestionID1",SqlDbType.SmallInt));
                cmd.Parameters["@QuestionID1"].Value = QuestionID1;
                cmd.Parameters.Add(new SqlParameter("@Answer1",SqlDbType.NVarChar, 100));
                cmd.Parameters["@Answer1"].Value = Answer1;
                cmd.Parameters.Add(new SqlParameter("@Created",SqlDbType.DateTime));
                cmd.Parameters["@Created"].Value = Created;
                cmd.Parameters.Add(new SqlParameter("@LastModified",SqlDbType.DateTime));
                cmd.Parameters["@LastModified"].Value = LastModified;

                if(LName!=null){
                    cmd.Parameters.Add(new SqlParameter("@LName",SqlDbType.NVarChar, 25));
                    cmd.Parameters["@LName"].Value = LName;
                }
                if(Phone1!=null){
                    cmd.Parameters.Add(new SqlParameter("@Phone1",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@Phone1"].Value = Phone1;
                }
                if(Phone2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Phone2",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@Phone2"].Value = Phone2;
                }
                if(FAX!=null){
                    cmd.Parameters.Add(new SqlParameter("@FAX",SqlDbType.NVarChar, 15));
                    cmd.Parameters["@FAX"].Value = FAX;
                }
                if(Address1!=null){
                    cmd.Parameters.Add(new SqlParameter("@Address1",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Address1"].Value = Address1;
                }
                if(Address2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Address2",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Address2"].Value = Address2;
                }
                if(City!=null){
                    cmd.Parameters.Add(new SqlParameter("@City",SqlDbType.NVarChar, 50));
                    cmd.Parameters["@City"].Value = City;
                }
                if(StateID!=0){
                    cmd.Parameters.Add(new SqlParameter("@StateID",SqlDbType.SmallInt));
                    cmd.Parameters["@StateID"].Value = StateID;
                }
                if(ZIP!=null){
                    cmd.Parameters.Add(new SqlParameter("@ZIP",SqlDbType.NChar, 5));
                    cmd.Parameters["@ZIP"].Value = ZIP;
                }
                if(QuestionID2!=0){
                    cmd.Parameters.Add(new SqlParameter("@QuestionID2",SqlDbType.SmallInt));
                    cmd.Parameters["@QuestionID2"].Value = QuestionID2;
                }
                if(Answer2!=null){
                    cmd.Parameters.Add(new SqlParameter("@Answer2",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Answer2"].Value = Answer2;
                }
                if(QuestionID3!=0){
                    cmd.Parameters.Add(new SqlParameter("@QuestionID3",SqlDbType.SmallInt));
                    cmd.Parameters["@QuestionID3"].Value = QuestionID3;
                }
                if(Answer3!=null){
                    cmd.Parameters.Add(new SqlParameter("@Answer3",SqlDbType.NVarChar, 100));
                    cmd.Parameters["@Answer3"].Value = Answer3;
                }

                iRes = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
        return iRes;
    }

    private void LoadUser(){

        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetUser";

                cmd.Parameters.Add(new SqlParameter("@UserID",SqlDbType.BigInt));
                cmd.Parameters["@UserID"].Value = _UserID;
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()){
                    _UserTypeID = (Int16)rdr["UserTypeID"];
                    _LoginID = rdr["LoginID"].ToString();
                    _Email = rdr["Email"].ToString();
                    _FName = rdr["FName"].ToString();
                    _Password = rdr["Password"].ToString();
                    _Created = (DateTime) rdr["Created"];
                    _LastModified = (DateTime) rdr["LastModified"];
                    _QuestionID1 = (Int16) rdr["QuestionID1"];
                    _Answer1 = rdr["Answer1"].ToString();
                    // nullable fields
                    _LName = rdr["LName"].ToString();
                    _Phone1 = rdr["Phone1"].ToString();
                    _Phone2 = rdr["Phone2"].ToString();
                    _FAX = rdr["FAX"].ToString();
                    _Address1 = rdr["Address1"].ToString();
                    _Address2 = rdr["Address2"].ToString();
                    _City = rdr["City"].ToString();
                    _StateID = (Int16)rdr["StateID"];
                    _ZIP = rdr["ZIP"].ToString();
                    _QuestionID2 = (Int16) rdr["QuestionID2"];
                    _Answer2 = rdr["Answer2"].ToString();
                    _QuestionID3 = (Int16) rdr["QuestionID3"];
                    _Answer3 = rdr["Answer3"].ToString();
                }

                rdr.Dispose();
                cmd.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
    }

    private bool ValidateUser(){
        if (LoginID == null){ szErrorMessage = "LoginID is null"; return false; }
        if (Password == null){ szErrorMessage = "Password is null"; return false; }
        if (Email == null){ szErrorMessage = "Email is null"; return false; }
        if (FName == null){ szErrorMessage = "FName is null"; return false; }
        if (QuestionID1 == 0){ szErrorMessage = "QuestionID1 is null"; return false; }
        if (Answer1 == null){ szErrorMessage = "Answer1 is null"; return false; }

        return true;
    }


}
