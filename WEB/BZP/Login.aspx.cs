using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Login : System.Web.UI.Page{
    protected void Page_Load(object sender, EventArgs e){
        this.Title = "Login";
    }
    protected void Page_PreRender(object sender, EventArgs e){
        PlaceHolder objPH = (PlaceHolder) Master.FindControl("mpScriptPH");
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("function DoPageEvents() {");
        sbScript.Append("SetLeftMargin();");
        //
        sbScript.Append("fGetJS(1);");        
        if (Session["UName"] == null){
            sbScript.Append("fMCH(12);");
        }else{
            sbScript.Append("fMCH(16);");
        }
        sbScript.Append("fSetTopMnuActv('mnuLogin');");
        //
        sbScript.Append("ShowBody();");
        sbScript.Append("}");
        sbScript.Append("$(document).ready(function(){DoPageEvents();});");
        HtmlGenericControl objScript = new HtmlGenericControl("script");
        objScript.Attributes.Add("type", "text/javascript");
        objScript.InnerHtml = sbScript.ToString();
        objPH.Controls.Add(objScript);
    }
}
