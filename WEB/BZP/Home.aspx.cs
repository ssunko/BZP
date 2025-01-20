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

public partial class Home : System.Web.UI.Page{
    private string szActivated = "Your Account has been successfully activated.<br />You can now log in using the username and password you chose during the registration.";
    private string szActivationError = "Your Account has NOT been activated due to system error.<br />Please try to re-activate it.";
    private string szUpdated = "Your Profile has been successfully updated.";
    private string szSessionExpired = "Your session has been expired.<br />Please re-log in.";
    
    protected void Page_Load(object sender, EventArgs e){
        this.Title = "Home";
        string sUad = "";
        string szInnerHtml = "";
        tblH_message.Visible = false;
        if(Session["uad"] != null){
            sUad  = Session["uad"].ToString();
            Session.Remove("uad");
            tblH_message.Visible = true;
        }
        if(sUad=="0"){
            szInnerHtml = szActivationError;
        }else if(sUad=="1"){
            szInnerHtml = szActivated; 
        }else if(sUad=="10"){
            szInnerHtml = szSessionExpired;
        }else if(sUad=="11"){
            szInnerHtml = szUpdated;
        }else{
            szInnerHtml = sUad.Trim();
        }
        div_msgText.InnerHtml = szInnerHtml;
    }
    protected void Page_PreRender(object sender, EventArgs e){
        PlaceHolder objPH = (PlaceHolder) Master.FindControl("mpScriptPH");
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("function DoPageEvents() {");
        sbScript.Append("SetLeftMargin();");
        //
        sbScript.Append("fSetMessageLM();");
        sbScript.Append("fMCH(1);");
        sbScript.Append("fSetTopMnuActv('mnuHome');");
        sbScript.Append("$('#ctl00_ContentBox_tblH_message').delay(10000).fadeOut(4000);");
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