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

public partial class Ajax_JS : System.Web.UI.Page{

    string szOpt = "";
    int iOpt;
    string szJS = "";
    const int LOGIN_JS = 1,
              SEARCH_JS = 2,
              ADDAD_JS = 3,
              SET_SESSION_VAR = 57,
              GET_CITIES = 77;
    
    protected void Page_Load(object sender, EventArgs e){
        System.Uri uriReferrer = System.Web.HttpContext.Current.Request.UrlReferrer;
        if (uriReferrer == null)
            return;
        if (uriReferrer.Host != System.Web.HttpContext.Current.Request.Url.Host)
            Response.Redirect(uriReferrer.ToString());
        
        string szHTMLPath = Request.PhysicalApplicationPath + "js\\";
        szOpt = Request.QueryString["Opt"];
        if (szOpt == null || szOpt == "")
            szOpt = "1";
        iOpt = int.Parse(szOpt);
        string szParam = "";
        switch (iOpt){
            case LOGIN_JS:
                szHTMLPath += "login.js";
                szJS = GenericLib.LoadHTML(szHTMLPath);
                break;

            case SEARCH_JS:
                szHTMLPath += "search.js";
                szJS = GenericLib.LoadHTML(szHTMLPath);
                break;
            case ADDAD_JS:
                szHTMLPath += "addad.js";
                szJS = GenericLib.LoadHTML(szHTMLPath);
                szJS += "var sUZIP = '" + Session["ZIP"] + "'; var sUEmail = '" + Session["Email"] + "'; var sUPhone = '" + Session["Phone"] + "';";
                break;
            case SET_SESSION_VAR:
                Session[Request.QueryString["VN"].ToString()] = Request.QueryString["VV"].ToString();
                break;
            case GET_CITIES:
                szParam = Request.QueryString["p"];
                string szCities = SearchLib.GetCities(Int16.Parse(szParam));
                szJS = "cities = [" + szCities + "];";
                break;
        }
        Response.Write(szJS);
    }
}
