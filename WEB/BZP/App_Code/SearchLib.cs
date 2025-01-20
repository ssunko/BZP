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
/// Summary description for SearchLib
/// </summary>
public static class SearchLib{

    public static string GetCities(Int16 StateID){
        string szCities = "";
        using (SqlConnection DbConnection = new SqlConnection()){
            DbConnection.ConnectionString = GenericLib.BZPConStr();
            DbConnection.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = null;
            cmd.Connection = DbConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "P_GetCitiesByState";
            cmd.Parameters.Add(new SqlParameter("@StateID",SqlDbType.SmallInt));
            cmd.Parameters["@StateID"].Value = StateID;
            rdr = cmd.ExecuteReader();
            while (rdr.Read()){
                szCities += rdr[0].ToString();
            }               
            cmd.Dispose();
            rdr.Dispose();
            try{
                szCities = szCities.Substring(0, szCities.Length - 1);
            }catch{}
        }
        return szCities;
    }


}
