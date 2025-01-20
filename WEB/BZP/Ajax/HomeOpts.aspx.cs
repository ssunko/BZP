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

public partial class Ajax_HomeOpts : System.Web.UI.Page
{
    string szOpt = "";
    int iOpt;
    const int   WELCOME_TO_BZP = 1,
                BZP_NEWS = 2,
                SPECIAL_DEAL = 3,
                GENERAL_FAQ = 99;

    protected void Page_Load(object sender, EventArgs e) {

        System.Uri uriReferrer = System.Web.HttpContext.Current.Request.UrlReferrer;
        if (uriReferrer == null)
            return;
        if (uriReferrer.Host != System.Web.HttpContext.Current.Request.Url.Host)
            Response.Redirect(uriReferrer.ToString());

        szOpt = Request.QueryString["Opt"];
        if (szOpt == null || szOpt == "")
            szOpt = "1";
        iOpt = int.Parse(szOpt);
        string szHTML = "";
        string szHTMLPath = Request.PhysicalApplicationPath + "HTML\\";
        switch (iOpt) {
            case WELCOME_TO_BZP:
                szHTMLPath += "Welcome.htm";
                break;
            case BZP_NEWS:
                szHTMLPath += "News.htm";
                break;
            case SPECIAL_DEAL:
                szHTMLPath += "SpecialDeal.htm";
                break;
            case GENERAL_FAQ:
                szHTMLPath += "GeneralFAQ.htm";
                break;
        }
        szHTML = GenericLib.LoadHTML(szHTMLPath);
        Response.Write(szHTML);
    }
}