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
/// Summary description for HTMLLib
/// </summary>
public static class HTMLLib{


    public static string GetDDStates(string DD_ID, string DD_class, bool bShowPleaseSelect, string szPleaseSelectTEXT, string szOnChange) {
        string szDDHTML = "";
        SqlDataReader rdr = null;
        string szStateID;
        string szStateName;
        string szDDOptions = "";
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetStates";                
                rdr = cmd.ExecuteReader();
                while (rdr.Read()){
                    szStateID = rdr["StateID"].ToString();
                    szStateName = rdr["StateLN"].ToString() + "   (" + rdr["StateSN"].ToString() + ")";
                    szDDOptions += "<option value='" + szStateID + "'>" + szStateName + "</option>";
                }
                cmd.Dispose();
                rdr.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
        szDDHTML = "<select id='" + DD_ID + "'";
        if (szOnChange != "") szDDHTML += " onchange='" + szOnChange + "'";
        szDDHTML = (DD_class != "") ? szDDHTML + " class='" + DD_class + "'>" : ">";
        if (bShowPleaseSelect)
            szDDHTML = szDDHTML + "<option value='-1'>" + szPleaseSelectTEXT + "</option>";
        szDDHTML += szDDOptions + "</select>";
        return szDDHTML;
    }



    public static string GetDDRecoverQuestions(string DD_ID, string DD_class, bool bShowPleaseSelect, string szPleaseSelectTEXT) {
        string szDDHTML = "";
        SqlDataReader rdr = null;
        string szQuestionID;
        string szQuestion;
        string szDDOptions = "";
        
        try{
            using (SqlConnection DbConnection = new SqlConnection()){
                DbConnection.ConnectionString = GenericLib.BZPConStr();
                DbConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetRecoverQuestions";
                rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    szQuestionID = rdr["QuestionID"].ToString();
                    szQuestion = rdr["Question"].ToString();
                    szDDOptions += "<option value='" + szQuestionID + "'>" + szQuestion + "</option>";
                }
                cmd.Dispose();
                rdr.Dispose();
            }
        }
        catch (Exception ex){
            GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
        }
        szDDHTML = "<select id='" + DD_ID + "'";
        szDDHTML = (DD_class != "") ? szDDHTML + " class='" + DD_class + "'>" : ">";
        if (bShowPleaseSelect)
            szDDHTML = szDDHTML + "<option value='-1'>" + szPleaseSelectTEXT + "</option>";
        szDDHTML += szDDOptions + "</select>";
        return szDDHTML;
    }
}
