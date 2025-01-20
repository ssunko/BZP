using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

public partial class AddAd : System.Web.UI.Page{		

        public void Page_Error(object sender,EventArgs e){
            string ErrCode = "0";
            System.Exception appException=Server.GetLastError();
            HttpException checkException=(HttpException) appException;
            //Verfy the expected error
            int code = checkException.GetHttpCode(); 
            if((code==500) ||(checkException.ErrorCode==-2147467259)) {
                ErrCode = "500";
            }
            Server.ClearError();
            Response.Redirect("BZPError.aspx?ErrCode=" + ErrCode, false);
        }

		private void Page_Load(object sender, System.EventArgs e){
            this.Title = "Add Ad";
            string tErrCode;
            string ErrCode = "";
            Int16 iFCnt = 0;
            if(IsPostBack){
                if(hTabNo.Value == "1"){
                    string szLDescription = Request.Form["taLDescription"];
                    Int64 iClassifiedID = Int64.Parse(Request.Form["hClassifiedID"]);
                    Int16 iPicNo = 0;
                    string szFileName = "";
                    iFCnt += 1;
                    szFileName = iClassifiedID.ToString() + iFCnt.ToString() + ".jpg";
                    tErrCode = CheckNSavePicture(file.PostedFile, szFileName, ref iPicNo);
                    if (tErrCode != "") ErrCode = tErrCode;
                    else iFCnt += 1;
                    szFileName = iClassifiedID.ToString() + iFCnt.ToString() + ".jpg";
                    tErrCode = CheckNSavePicture(file1.PostedFile, szFileName, ref iPicNo);
                    if (tErrCode != "") ErrCode = tErrCode;
                    else iFCnt += 1;
                    szFileName = iClassifiedID.ToString() + iFCnt.ToString() + ".jpg";
                    tErrCode = CheckNSavePicture(file2.PostedFile, szFileName, ref iPicNo);
                    if (tErrCode != "") ErrCode = tErrCode;
                    else iFCnt += 1;
                    szFileName = iClassifiedID.ToString() + iFCnt.ToString() + ".jpg";
                    tErrCode = CheckNSavePicture(file3.PostedFile, szFileName, ref iPicNo);
                    if (tErrCode != "") ErrCode = tErrCode;
                    if (ClassifiedLib.UpdateLongDescription(iClassifiedID, szLDescription, iPicNo.ToString()) != 1){
                        ErrCode = "490";
                    }
                    if (ErrCode != ""){
                        Response.Redirect("BZPError.aspx?ErrCode=" + ErrCode, false);
                        return;
                    }

                    if(Session["ClassifiedID"]!=null) Session.Remove("ClassifiedID");
                    Session["uad"] = "Your classified was saved successfully.<br />It will be reviewed and activated shortly.";
                    Response.Redirect("Home.aspx");
                }else if(hTabNo.Value == "2"){
                    // Modify ad submited...
                }
			}
		}
    protected void Page_PreRender(object sender, EventArgs e){
        PlaceHolder objPH = (PlaceHolder) Master.FindControl("mpScriptPH");
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("function DoPageEvents() {");
        sbScript.Append("SetLeftMargin();");
        //
        
        if (Session["UName"] == null){
            hTabNo.Value = "1";
		    sbScript.Append("fGetJSCB(3,'fMCH(8)');");
	    }else if (Session["ClassifiedID"] == null){
            sbScript.Append("fGetJSCB(3,'fMCH(9)');");
	    }else{
            sbScript.Append("fGetJSCB(3,'fMCH(10)');");
            //Session.Remove("ClassifiedID");
        }
        sbScript.Append("fSetTopMnuActv('mnuAddAd');");
        //
        sbScript.Append("ShowBody();");
        sbScript.Append("}");
        sbScript.Append("$(document).ready(function(){DoPageEvents();});");
        HtmlGenericControl objScript = new HtmlGenericControl("script");
        objScript.Attributes.Add("type", "text/javascript");
        objScript.InnerHtml = sbScript.ToString();
        objPH.Controls.Add(objScript);
    }

	private bool IsImage(HttpPostedFile file){
		if(file != null &&	file.ContentLength > 0 && Regex.IsMatch(file.ContentType, "image/\\S+") )
			return true;
		return false;
	}
	protected string CheckNSavePicture(HttpPostedFile file, string szFileName, ref Int16 iPicNo){
        string szRet = "";
        if(IsImage(file)){
			int contentLength = file.ContentLength;
			if(contentLength < GenericLib.MaxSize){
			    return ImageLib.UploadImage(file, MapPath(GenericLib.UploadImgFolder), MapPath(GenericLib.UploadTmbFolder), szFileName, ref iPicNo);
			}
			else{
                szRet = "500";
            }
		}else{
            // NOT AN IMAGE
            if(file.FileName!=""){
                szRet = "510";
            }
        }
        return szRet;
	}

}// partial class AddAd
