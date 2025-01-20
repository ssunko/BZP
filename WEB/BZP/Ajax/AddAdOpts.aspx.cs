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

public partial class Ajax_AddAdOpts : System.Web.UI.Page{
    string szOpt = "";
    int iOpt;
    const int   ADD_FREE_AD = 1,
                EDIT_YOUR_ADs = 2,                
                ADD_AD_SHOW_MUSTLOGIN = 77,
                ADD_AD_FAQ = 99,
                ADD_AD_GET_TYPES = 100,
                ADD_AD_CHECK_ZIP = 102,
                ADD_AD_SAVE_1 = 104;
    
    protected void Page_Load(object sender, EventArgs e){
        string szZIP = "";
        System.Uri uriReferrer = System.Web.HttpContext.Current.Request.UrlReferrer;
        if (uriReferrer == null)
            return;
        if (uriReferrer.Host != System.Web.HttpContext.Current.Request.Url.Host)
            Response.Redirect(uriReferrer.ToString());

        string szHTMLPath = Request.PhysicalApplicationPath + "HTML\\";

        szOpt = Request.QueryString["Opt"];

        if (szOpt == null || szOpt == "")
            szOpt = "1";
        iOpt = int.Parse(szOpt);
        string szHTML = "";
        switch (iOpt){
            case ADD_FREE_AD:
                szHTMLPath += "AddFreeAd.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case EDIT_YOUR_ADs:
                szHTMLPath += "EditYourAds.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case ADD_AD_SHOW_MUSTLOGIN:
                szHTMLPath += "MustLoginMSG.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                szHTML = szHTML.Replace("[BZP_FTR]", "ADD FREE AD");
                break;
            case ADD_AD_FAQ:
                szHTMLPath += "AddAdFAQ.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case ADD_AD_GET_TYPES:
                szHTML =  "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><bzp>" + ClassifiedLib.GetAdTypes() + "</bzp>";
                break;
            case ADD_AD_CHECK_ZIP:
                szZIP = Request.QueryString["z"];
                szHTML = (GenericLib.CheckZIP(szZIP)) ? "1" : "0";
                break;
            case ADD_AD_SAVE_1:
                string szTitle = Request.QueryString["t"];
                string szSDescription = Request.QueryString["d"];
                       szZIP = Request.QueryString["z"];
                string szPhone = Request.QueryString["p"];
                string szEmail = Request.QueryString["e"];
                string szCategoryID = Request.QueryString["c"];
                Int64 iUserID = (Int64)Session["UserID"];
                Int64 iClassifiedID = ClassifiedLib.AddClassified(szTitle,szSDescription,szZIP,szPhone,szEmail,szCategoryID,iUserID,0);
                szHTML = iClassifiedID.ToString();
                Session["ClassifiedID"] = szHTML;
                break;
        }
        Response.Write(szHTML);
    }
} //partial class Ajax_AddAdOpts
