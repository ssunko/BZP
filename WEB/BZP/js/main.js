var bPageIsLoading = false;
var iPageHeightDiff = 200;
var iPGWidth = 982;
var iSCrBarW = 20;
var iErrCnt = 0;
var sErrBorder = '1px solid  #ef5d78';
var sNoErrBorder = '1px solid  #CDE1F9';
var sActivePage;
var sStandardError = 'Error occurred during last operation,<br />please reload page and try again.';
var iLM = 0;
var sNotListed = ' [Not Listed] ';
var cities;
var dDaysBefore = 13;
var dDaysAfter = 1;
var iMinSrchChars = 3;

function fSetTopMnuActv(oID) {
    fGetObj(oID).className = "active";
}
function fGoToPage(sURL, oID) {
    if (fGetObj(oID).className != "active")
        document.location.href = sURL;
}
function fHideContents(tab, iTabCnt) {
    var oContent;
    for (var i = 1; i < iTabCnt + 1; i++) {
        oContent = fGetObj('content' + i);
        oContent.style.display = 'none';
    }
}

function fMCH(iI){
	//Home
	if(iI==1)
		fMnuClick(1,4,'Ajax/HomeOpts.aspx?Opt=1',null);
	else if(iI==2)
		fMnuClick(2,4,'Ajax/HomeOpts.aspx?Opt=2',null);
	else if(iI==3)
		fMnuClick(3,4,'Ajax/HomeOpts.aspx?Opt=3',null);
	else if(iI==4)
		fMnuClick(4,4,'Ajax/HomeOpts.aspx?Opt=99',null);
	//Search
	else if(iI==5)
	    fMnuClick(1, 3, 'Ajax/SearchOpts.aspx?Opt=1', 'fSelectSearchBy(0);fGetTypes();fAppendDP()');
	else if(iI==6)
		fMnuClick(2,3,'Ajax/SearchOpts.aspx?Opt=2',null);
	else if(iI==7)
		fMnuClick(3,3,'Ajax/SearchOpts.aspx?Opt=99',null);
	//Add Ad
	else if(iI==8)
		fMnuClick(1,3,'Ajax/AddAdOpts.aspx?Opt=77',null);
	else if(iI==9)
	    fMnuClick(1,3,'Ajax/AddAdOpts.aspx?Opt=1','fAppendFiles();fGetAdTypes();');
	else if(iI==10)
	    fMnuClick(2,3,'Ajax/AddAdOpts.aspx?Opt=2','fChangeTabNo(2)');
	else if(iI==11)
	    fMnuClick(3,3,'Ajax/AddAdOpts.aspx?Opt=99',null);	
	//Login / Register
	else if(iI==12)
	    fMnuClick(1,4,'Ajax/LoginOpts.aspx?Opt=1','fUNfromCookie()');
	else if(iI==13)
	    fMnuClick(2,4,'Ajax/LoginOpts.aspx?Opt=2',null);
	else if(iI==14)
	    fMnuClick(3,4,'Ajax/LoginOpts.aspx?Opt=3',null);
	else if(iI==15)
	    fMnuClick(4,4,'Ajax/LoginOpts.aspx?Opt=99',null);
	else if(iI==16)
	    fMnuClick(1,2,'Ajax/LoginOpts.aspx?Opt=21','fLoadProfile()');
	else if(iI==17)
	    fMnuClick(2,2,'Ajax/LoginOpts.aspx?Opt=99',null);
	//FAQ
	else if(iI==18)
	    fMnuClick(1,4,'Ajax/FaqOpts.aspx?Opt=1',null);
	else if(iI==19)
	    fMnuClick(2,4,'Ajax/FaqOpts.aspx?Opt=2',null);
	else if(iI==20)
	    fMnuClick(3,4,'Ajax/FaqOpts.aspx?Opt=3',null);
	else if(iI==21)
	    fMnuClick(4,4,'Ajax/FaqOpts.aspx?Opt=4',null);
}

function fMnuClick(tab, iTabCnt, sUrl, sCallBack) {
    if (bPageIsLoading == false && sActivePage != tab) {
        bPageIsLoading = true;
        sActivePage = tab;
        fHideContents(tab, iTabCnt);
        var oContent = fGetObj('content' + tab);
        iErrCnt = 0;
        setContentHeight(oContent);
        oContent.style.display = '';
        ResetTabs(tab, iTabCnt);
        if (oContent.innerHTML.length < 100) {
            var jqxhr = $.ajax({ url: sUrl })
	        .success(function() {
	            oContent.innerHTML += jqxhr.responseText;
	            if (sCallBack != null) {
	                eval(sCallBack);
	                setTimeout(function() { fHideLoader(tab) }, 1000);
	            } else {
	                fHideLoader(tab);
	            }
	            oContent.style.height = null;
	            setContentHeight(oContent);
	        });
        } else {
            bPageIsLoading = false;
        }
    }
}
function fHideLoader(tab) {
    $('#divLoader' + tab).css('visibility', 'hidden');
    bPageIsLoading = false;
}
function setContentHeight(oContent){
    var iDocHeight = getDocHeight() - iPageHeightDiff;
    oContent.style.height = (iDocHeight < 300) ? '300px' : iDocHeight + 'px';
}
function getDocHeight() {
    var D = document;
    return Math.max(
        Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
        Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
        Math.max(D.body.clientHeight, D.documentElement.clientHeight)
    );
}
function getDocWidth() {
    var D = document;
    return Math.max(
        Math.max(D.body.scrollWidth, D.documentElement.scrollWidth),
        Math.max(D.body.offsetWidth, D.documentElement.offsetWidth),
        Math.max(D.body.clientWidth, D.documentElement.clientWidth)
    );
}
function SetLeftMargin() {
    iLM = (getDocWidth() - iPGWidth) / 2 - iSCrBarW;
    fGetObj('bzp_body').style.marginLeft = iLM + 'px';
    var iLL = iLM + 20;
    $('.divLoader').css('left', iLL + 'px');
}
function fSetMessageLM() {
    try{
        var iMessL = iLM + (iPGWidth / 2) - 200; //200 = 400 / 2 (400 - message width from css)
        fGetObj('ctl00_ContentBox_tblH_message').style.left = iMessL + 'px';
    }catch(err){}
}
function ResetTabs(tab, iTabCnt) {
    for (var i = 1; i < iTabCnt + 1; i++) {
        fGetObj("tab" + i).className = "";
    }
    fGetObj("tab" + tab).className = "active";
}
function DisabBtn(btnID) {
    oBtn = fGetObj(btnID);
    oBtn.disabled = true;
    oBtn.className = 'cssButtonDisab';
}
function EnabBtn(btnID) {
    oBtn = fGetObj(btnID);
    oBtn.disabled = false;
    oBtn.className = 'cssButton';
}
function fUNfromCookie() {
    var sUN = getCookie('bzp_UN');
    var tUN = null;
    if (sUN != null) tUN = fGetObj('txtUserName');
    if (tUN != null) { tUN.value = sUN; fGetObj('chkRememberMe').checked = true }
}

function setCookie(c_name, value, exdays) {
    try {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    } catch (e) { }
}

function getCookie(c_name) {
    try {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    } catch (e) { return ''; }
}
function fGetObj(oID) {
    var oObj = document.getElementById(oID);
    return oObj;
}
function fTrim(str) {
    var str = str.replace(/^\s\s*/, ''),
		ws = /\s/,
		i = str.length;
    while (ws.test(str.charAt(--i)));
    return str.slice(0, i + 1);
}

function fLeft(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0,n);
}
function fRight(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function fIsOdd(n) {
    if (n % 2)
        return true;
    else
        return false;
}
function fLogOff() {
    if (confirm('Do you want to Log Off?')) {
        var sUrl = 'Ajax/LoginOpts.aspx?Opt=102';
        var jqxhr = $.ajax({ url: sUrl })
    	.success(function() {
    	    sRes = jqxhr.responseText;
    	    fGetObj('ctl00_lblUser').style.display = 'none';
    	    fGetObj('ctl00_liLogOff').style.display = 'none';
    	    fGetObj('ctl00_spnLogOffSep').style.display = 'none';
    	    fGoHome();
    	});
    };
}
function fGetJS(sOpt) {
    fGetJSParam(sOpt, null, null, false);
}
function fGetJSCB(sOpt, sCallBack) {
    fGetJSParam(sOpt, null, sCallBack, false);
}
function fGetJSParam(sOpt, sParam, sCallBack, bEval) {
    var sUrl = 'Ajax/JS.aspx?Opt=' + sOpt;
    if (sParam != null) sUrl += '&p=' + sParam;
    var jqxhr = $.ajax({ url: sUrl })
	.success(function() {
	    var sResJ = jqxhr.responseText;
	    if (bEval) {
	        eval(sResJ);
	    } else {
	        var headID = document.getElementsByTagName("head")[0];
	        var newScript = document.createElement('script');
	        newScript.type = 'text/javascript';
	        newScript.text = sResJ;
	        headID.appendChild(newScript);
	    }
	    if (sCallBack != null) eval(sCallBack);
	});
}
function fSetSV(sVN, sVV, sCallBack) {
    var sUrl = 'Ajax/JS.aspx?Opt=57&VN=' + sVN + '&VV=' + encodeURIComponent(sVV);
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        if (sCallBack != null) eval(sCallBack);
    });
}
function fGetEncodedValue(oID) {
    return encodeURIComponent(fTrim(fGetObj(oID).value));
}

function fEmail(sEmail, sSubject, sBody) {
    var _link = 'mailto:' + sEmail + '?subject=' + sSubject;
    if (sBody != null)
        _link += '&body=' + sBody;
    document.location = _link;
}
function fDDAppendLast(oDD_Id, sOptText, sOptValue) {
    var oOpt = document.createElement('option');
    oOpt.text = sOptText;
    oOpt.value = sOptValue;
    var elSel = fGetObj(oDD_Id);
    try {
        elSel.add(oOpt, null); // Doesn't work in IE
    }
    catch (ex) {
        elSel.add(oOpt); // IE only
    }
}
function fDDRemoveByIndex(oDD_Id, iIndex) {
    var elSel = fGetObj(oDD_Id);
    if (elSel.length > 0) {
        elSel.remove(iIndex);
    }
}
function fDDRemoveAll(oDD_Id) {
    fGetObj(oDD_Id).length = 0;
}

/* MOVE BG */
var Step_px = 1;
var CurPix = 0;
var RestartPix;
var hBGMove;
function MoveBG(BGTop) {
    CurPix -= Step_px;
    if (CurPix == RestartPix) CurPix = 0;
    $('#BGholder').css('background-position', CurPix + 'px ' + BGTop + 'px');
}
/* MOVE BG */
function fGoHome(){
    document.location.href = 'Home.aspx';
}
function fGetFileName(sFN) {
    var iLIO = sFN.lastIndexOf('\\') + 1;
    if (iLIO == 0) iLIO = sFN.lastIndexOf('\/') + 1;
    var sFName = '';
    if (iLIO == 0) {
        sFName = sFN;
    } else {
        sFName = sFN.substr(iLIO);
    }
    return sFName;
}
function fSetBigPic(sPicFName) {
    var sPicName = fGetFileName(sPicFName);
    fGetObj('imgBigPic').src = 'Pictures\/' + sPicName
}
function fDisabObj(bDisab, ID) {
    fGetObj(ID).disabled = bDisab;
}
function isNumKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 47 && charCode < 58)
        return true;
    return false;
}
function ShowBody() {
    fGetObj('bzp_body').style.visibility = 'visible';
}



/*

function changeOpac(opacity, id) {
    var object = document.getElementById(id).style;
    object.opacity = (opacity / 100);
    object.MozOpacity = (opacity / 100);
    object.KhtmlOpacity = (opacity / 100);
    object.filter = "alpha(opacity=" + opacity + ")";
}
//changeOpac(75, 'BGholder');

function findTopPos(oElement) {
if (typeof (oElement.offsetParent) != 'undefined') {
for (var posY = 0; oElement; oElement = oElement.offsetParent) {
posY += oElement.offsetTop;
}
return posY;
} else {
return oElement.y;
}
}

function findPosition(oElement) {
if (typeof (oElement.offsetParent) != 'undefined') {
for (var posX = 0, posY = 0; oElement; oElement = oElement.offsetParent) {
posX += oElement.offsetLeft;
posY += oElement.offsetTop;
}
return [posX, posY];
} else {
return [oElement.x, oElement.y];
}
}
*/

/*
function TTT() {
fGetObj('div_CONTENT').innerText = '<code><pre>' + document.getElementsByTagName('html')[0].innerHTML + '</pre></code>';
}
<input type="button" onclick="TTT();" id="qwe" value="press me" />
<div id="div_CONTENT" ></div>
*/
/*

<% if (Session["UName"] == null){ %>
    <li onclick="fMCH(8)"><a id="tab1">ADD FREE AD</a></li>
<%}else{ %>
    <li onclick="fMCH(9)"><a id="tab1">ADD FREE AD</a></li>
<% } %>

*/