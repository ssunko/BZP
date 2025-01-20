using System;
using System.Data;
using System.Configuration;
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
    /// Summary description for ClassifiedLib
    /// </summary>
    public static class ClassifiedLib {
     
        public static string GetAdTypes(){
            string szXML = "";
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetAdTypes_XML";
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read()){
                    szXML += rdr[0].ToString();
                }
                cmd.Dispose();
                rdr.Dispose();
            }
            return szXML;
        }

    public static Int64 AddClassified(string szTitle,string szSDescription,string szZIP,string szPhone,string szEmail,string szCategoryID, Int64 iUserID, Int16 iPicNo){
        Int64 iClassifiedID = 0;
        szTitle = szTitle.Replace("'", "''");
        szSDescription = szSDescription.Replace("'", "''");
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Classified_SET";
                cmd.Parameters.Add(new SqlParameter("@Action",SqlDbType.Char,1));
                cmd.Parameters["@Action"].Value = "I";  // Insert
                cmd.Parameters.Add(new SqlParameter("@Title",SqlDbType.NVarChar,60));
                cmd.Parameters["@Title"].Value = szTitle;
                cmd.Parameters.Add(new SqlParameter("@ShortDescription",SqlDbType.NVarChar,300));
                cmd.Parameters["@ShortDescription"].Value = szSDescription;
                cmd.Parameters.Add(new SqlParameter("@ZipCode",SqlDbType.NChar, 5));
                cmd.Parameters["@ZipCode"].Value = szZIP;
                
                if(szPhone!=""){
                    cmd.Parameters.Add(new SqlParameter("@Phone",SqlDbType.NVarChar,20));
                    cmd.Parameters["@Phone"].Value = szPhone;
                }
                if(szEmail!=""){
                    cmd.Parameters.Add(new SqlParameter("@Email",SqlDbType.NVarChar,50));
                    cmd.Parameters["@Email"].Value = szEmail;
                }                
                cmd.Parameters.Add(new SqlParameter("@CategoryID",SqlDbType.SmallInt));
                cmd.Parameters["@CategoryID"].Value = Int16.Parse(szCategoryID);
                cmd.Parameters.Add(new SqlParameter("@Stands",SqlDbType.SmallInt));
                cmd.Parameters["@Stands"].Value = GenericLib.StandsFor;
                cmd.Parameters.Add(new SqlParameter("@Active",SqlDbType.Bit));
                cmd.Parameters["@Active"].Value = 0;
                cmd.Parameters.Add(new SqlParameter("@UserID",SqlDbType.BigInt));
                cmd.Parameters["@UserID"].Value = iUserID;
                cmd.Parameters.Add(new SqlParameter("@PicNo",SqlDbType.TinyInt));
                cmd.Parameters["@PicNo"].Value = iPicNo;

                iClassifiedID = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
        return iClassifiedID;
    }

    public static int UpdateClassified(Int64 ClassifiedID, string szTitle,string szSDescription, string szText, string szZIP,string szPhone,string szEmail,string szCategoryID, string szStands, string szActive, string szPicNo){
        int iRes = 0;
        szTitle = szTitle.Replace("'", "''");
        szSDescription = szSDescription.Replace("'", "''");
        szText = szText.Replace("'", "''");
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Classified_SET";
                cmd.Parameters.Add(new SqlParameter("@Action",SqlDbType.Char,1));
                cmd.Parameters["@Action"].Value = "U";  // Update
                cmd.Parameters.Add(new SqlParameter("@ClassifiedID",SqlDbType.BigInt));
                cmd.Parameters["@ClassifiedID"].Value = ClassifiedID;

                if(szTitle!=""){
                    cmd.Parameters.Add(new SqlParameter("@Title",SqlDbType.NVarChar,60));
                    cmd.Parameters["@Title"].Value = szTitle;
                }
                if(szSDescription!=""){
                    cmd.Parameters.Add(new SqlParameter("@ShortDescription",SqlDbType.NVarChar,300));
                    cmd.Parameters["@ShortDescription"].Value = szSDescription;
                }                
                if(szText!=""){
                    cmd.Parameters.Add(new SqlParameter("@Text",SqlDbType.NVarChar,3600));
                    cmd.Parameters["@Text"].Value = szText;
                }
                if(szZIP!=""){
                    cmd.Parameters.Add(new SqlParameter("@ZipCode",SqlDbType.NChar, 5));
                    cmd.Parameters["@ZipCode"].Value = szZIP;
                }
                if(szPhone!=""){
                    cmd.Parameters.Add(new SqlParameter("@Phone",SqlDbType.NVarChar,20));
                    cmd.Parameters["@Phone"].Value = szPhone;
                }
                if(szEmail!=""){
                    cmd.Parameters.Add(new SqlParameter("@Email",SqlDbType.NVarChar,50));
                    cmd.Parameters["@Email"].Value = szEmail;
                }   
                if(szCategoryID!=""){
                    cmd.Parameters.Add(new SqlParameter("@CategoryID",SqlDbType.SmallInt));
                    cmd.Parameters["@CategoryID"].Value = Int16.Parse(szCategoryID);
                }
                if(szStands!=""){
                    cmd.Parameters.Add(new SqlParameter("@Stands",SqlDbType.SmallInt));
                    cmd.Parameters["@Stands"].Value = szStands;
                }
                if(szActive!=""){
                    cmd.Parameters.Add(new SqlParameter("@Active",SqlDbType.Bit));
                    cmd.Parameters["@Active"].Value = (bool.Parse(szActive))?1:0;
                }
                if(szPicNo!="" && szPicNo!="0"){
                    cmd.Parameters.Add(new SqlParameter("@PicNo",SqlDbType.TinyInt));
                    cmd.Parameters["@PicNo"].Value = Int16.Parse(szPicNo);
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

    public static int UpdateLongDescription(Int64 ClassifiedID, string szLongDescription, string szPicNo){
        return UpdateClassified(ClassifiedID, "", "", szLongDescription, "", "", "", "", "", "", szPicNo);
    }



    } // class ClassifiedLib