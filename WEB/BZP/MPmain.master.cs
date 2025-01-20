using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage{
    protected void Page_Load(object sender, EventArgs e){

        string szFname = "";
        if (Session["UserID"] != null && Session["UserID"].ToString() != ""){
            szFname = Session["FName"].ToString().Trim();
            szFname = (szFname == "") ? szFname : szFname + ",";
            lblUser.Text = szFname;
            liLogOff.Visible = true;
            spnLogOffSep.Visible = true;
        }else{
            liLogOff.Visible = false;
            spnLogOffSep.Visible = false;
        }
        lblDate.Text = "<b>Welcome to BZPage</b><br />" + DateTime.Now.ToLongDateString();
        lblYear.Text = DateTime.Now.Year.ToString(); 
    }
}
