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


public partial class FAQ : System.Web.UI.Page{
    protected void Page_Load(object sender, EventArgs e){
        this.Title = "FAQ";
    }
    protected void Page_PreRender(object sender, EventArgs e){
        PlaceHolder objPH = (PlaceHolder) Master.FindControl("mpScriptPH");
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("function DoPageEvents() {");
        sbScript.Append("SetLeftMargin();");
        //
        sbScript.Append("fMCH(18);"); 
        sbScript.Append("fSetTopMnuActv('mnuFAQ');");
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
