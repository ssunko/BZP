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

public partial class Activate : System.Web.UI.Page{
    public int iRes;
    protected void Page_Load(object sender, EventArgs e){
        this.Title = "Activate";
        //string aun = HttpUtility.UrlDecode(Request.QueryString["aun"]);
        string LoginID = GenericLib.PasswordDecript( HttpUtility.UrlDecode(Request.QueryString["aun"]));
        

        try{
                using (SqlConnection DbConnection = new SqlConnection()){
                    DbConnection.ConnectionString = GenericLib.BZPConStr();
                    DbConnection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "P_ActivateAccount";
                    cmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.NVarChar, 20));
                    cmd.Parameters["@LoginID"].Value = GenericLib.SQLReady(LoginID);
                    
                    iRes = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                       
                    if (iRes == 1)
                        Session["uad"]= "1";
                    else{
                        GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, "Could not activate user: " + LoginID);
                        Session["uad"]= "0";
                    }
                    Response.Redirect("Home.aspx");
                }
            }
            catch (Exception ex) {
                GenericLib.CreateLog(GenericLib.ERROR_LOG, GenericLib.ERROR_LOG, ex.Message);
            }  
        }
}
