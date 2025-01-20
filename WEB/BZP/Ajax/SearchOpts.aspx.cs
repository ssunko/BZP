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

public partial class Ajax_SearchOpts : System.Web.UI.Page{
    string szOpt = "";
    int iOpt;
    const int   SEARCH   = 1,
                SEARCH_RESULTS  = 2,
                SEARCH_FAQ  = 99,
                SEARCH_GET_TYPES = 100,
                SEARCH_GET_RESULT = 102;


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
        switch (iOpt) {
            case SEARCH:
                szHTMLPath += "Search.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                szHTML = szHTML.Replace("DDSTATES", HTMLLib.GetDDStates("cboStates", "cssDropDown", true, "Please select state ...", "fDDStatesChange()"));
                break;
            case SEARCH_RESULTS:
                szHTMLPath += "SearchResults.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case SEARCH_FAQ:
                szHTMLPath += "SearchFAQ.htm";
                szHTML = GenericLib.LoadHTML(szHTMLPath);
                break;
            case SEARCH_GET_TYPES:
                szHTML =  "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><bzp>" + ClassifiedLib.GetAdTypes() + "</bzp>";
                break;
            case SEARCH_GET_RESULT:
                string szSearchFor = Request.QueryString["sf"];
                string szSearchOpt = Request.QueryString["so"];
                string szZIP = Request.QueryString["z"];
                string szSearchWithin = Request.QueryString["sw"];
                string szStateID = Request.QueryString["sid"];
                string szCity = Request.QueryString["c"];
                string szTypeID = Request.QueryString["tid"];
                string szCategoryID = Request.QueryString["cid"];
                string szTitleOnly = Request.QueryString["to"];
                string szPictureOnly = Request.QueryString["po"];
                string szPricedFrom = Request.QueryString["pf"];
                string szPricedTo = Request.QueryString["pt"];
                string szDateFrom = Request.QueryString["df"];
                string szDateTo = Request.QueryString["dt"];
                string szExcludeOpt = Request.QueryString["eo"];
                string szExcludeWords = Request.QueryString["e"];

                szHTML = ""; //szExcludeWords + " - " + szPricedFrom + "," + szPricedTo + "," + szDateFrom  + "," + szDateTo;
                break;
        }
        Response.Write(szHTML);
    }
}
