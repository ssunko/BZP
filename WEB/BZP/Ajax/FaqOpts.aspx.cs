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

public partial class Ajax_FaqOpts : System.Web.UI.Page{
    string szOpt = "";
    int iOpt;
    const int   GENERAL_FAQ = 1,
                SEARCH_FAQ = 2,
                ADD_AD_FAQ = 3,
                LOGIN_REGISTER_FAQ = 4;



    protected void Page_Load(object sender, EventArgs e){

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
            case GENERAL_FAQ:
                szHTMLPath += "GeneralFAQ.htm";
                break;
            case SEARCH_FAQ:
                szHTMLPath += "SearchFAQ.htm";
                break;
            case ADD_AD_FAQ:
                szHTMLPath += "AddAdFAQ.htm";
                break;
            case LOGIN_REGISTER_FAQ:
                szHTMLPath += "LoginRegFAQ.htm";
                break;
        }
        szHTML = GenericLib.LoadHTML(szHTMLPath);
        Response.Write(szHTML);
    }
}
