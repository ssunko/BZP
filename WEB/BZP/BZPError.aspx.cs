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

public partial class BZPError : System.Web.UI.Page{

    //public string szRef = "Home.aspx";

    
    protected void Page_Load(object sender, EventArgs e){
        //System.Uri uriReferrer = System.Web.HttpContext.Current.Request.UrlReferrer;
        //if (uriReferrer != null){
        //    szRef = uriReferrer.ToString().Substring(uriReferrer.ToString().LastIndexOf('/') + 1);
        //}

        string szErrCode = Request.QueryString["ErrCode"];
        if (szErrCode == null || szErrCode == "") szErrCode = "0";

        string szErrMsg = "";
        switch (szErrCode){
            case "0":
                szErrMsg = "An error occurred during the execution of the current web request."  + AttachModifyClassified();
                break;
            case "404":
                szErrMsg = "Page not found." + AttachModifyClassified();
                break;
            case "490":
                szErrMsg = "Unexpected error occurred during save operation."  + AttachModifyClassified();
                break;
            case "500":
                szErrMsg = String.Format("One or more files you've selected exceed the attachment size limit: {0} MB.<br />", GenericLib.MaxSize / 1048576) + AttachModifyClassified();
                break;
            case "510":
                szErrMsg = "One or more files you've selected is/are not a valid image file(s)." + AttachModifyClassified();
                break;
            default:
                szErrMsg = "Unknown error occurred during the execution of the current web request." + AttachModifyClassified();
                break;
        }
        divError.InnerHtml = szErrMsg;
    }
    protected void Page_PreRender(object sender, EventArgs e){
        PlaceHolder objPH = (PlaceHolder) Master.FindControl("mpScriptPH");
        StringBuilder sbScript = new StringBuilder();
        //145 - background top position; 20 - speed in miliseconds        
        sbScript.Append("function fStartStopBG(bStart){if(bStart){hBGMove = setInterval('MoveBG(145)', 20);fGetObj('spnStartM').style.display = 'none';fGetObj('spnStopM').style.display='';}else{hBGMove=clearInterval(hBGMove);fGetObj('spnStartM').style.display='';fGetObj('spnStopM').style.display = 'none';}}");
        //
        sbScript.Append("function DoPageEvents() {");
        sbScript.Append("SetLeftMargin();");
        //
        sbScript.Append("setContentHeight(fGetObj('content'));");
        sbScript.Append("RestartPix=-(2268-880);"); // RestartPix=-(BGImageWidth - DivisionWidth)
        sbScript.Append("fStartStopBG(true);");
        //
        sbScript.Append("ShowBody();");
        sbScript.Append("}");
        sbScript.Append("$(document).ready(function(){DoPageEvents();});");

        HtmlGenericControl objScript = new HtmlGenericControl("script");
        objScript.Attributes.Add("type", "text/javascript");
        objScript.InnerHtml = sbScript.ToString();
        objPH.Controls.Add(objScript);
    }
    private string AttachModifyClassified(){
        if(Session["ClassifiedID"]!=null){
            string sR = "<br />Please <span class='spn_link' title='Edit classified' onclick='document.location.href=\"AddAd.aspx\";'>click here</span> in order to modify your classified.";
            return sR;
        }else{
            return AttachGoHome();
        }
    }
    private string AttachGoHome(){
        return "<br />Please <span class='spn_link' title='Return home' onclick='fGoHome();'>click here</span> in order to return to Home page.";
    }
}
