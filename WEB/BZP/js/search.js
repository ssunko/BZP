var oSrchTerms = new Object();
var bQuickViewShow = false;
var sImgID = '';
var sTR_ID = '';
var isQVMoving = false;
function fQuickViewShow(ImgID, TR_ID) {
    var iTR_No = TR_ID.replace('tr_', '');
 
    if (isQVMoving) return;
    if (sImgID == ImgID) {
        bQuickViewShow = !bQuickViewShow;
    } else {
        sImgID = ImgID;
        fSetOddEvenClass(sTR_ID);
        sTR_ID = TR_ID;
        bQuickViewShow = true;
    }

    if (bQuickViewShow) {
        isQVMoving = true;
        fGetDataQV();
        fGetObj(TR_ID).className = 'tr_hilit';
        var oImg = fGetObj(ImgID);
        var oQVTbl = fGetObj('tQuickView');
        var iL = iLM + oImg.parentNode.offsetLeft + oImg.offsetLeft + 56;
        var iT = oImg.parentNode.offsetTop;

        if (iTR_No > 7) {
            fGetObj('td_m_b').style.backgroundImage = 'url(images\/fr_m_b_ar.GIF)';
            fGetObj('td_m_t').style.backgroundImage = 'url(images\/fr_m_t.GIF)';
            iT -= 6;
        } else {
            fGetObj('td_m_b').style.backgroundImage = 'url(images\/fr_m_b.GIF)';
            fGetObj('td_m_t').style.backgroundImage = 'url(images\/fr_m_t_ar.GIF)';
            iT += 138;
        }        
        
        oQVTbl.style.left = iL + 'px';
        oQVTbl.style.top = iT + 'px';        
        
        $("#tQuickView").fadeTo(500, 1, function() { isQVMoving = false; })
    } else {
        fQVClose();
    }
}

function fQVClose() {
    bQuickViewShow = false;
    sImgID = '';
    $("#tQuickView").fadeOut(500, function() { fGetObj('tQuickView').style.top = '0px'; isQVMoving = false; });
    fSetOddEvenClass(sTR_ID);
    
}
function fSetOddEvenClass(TR_ID) {
    if (TR_ID == '') return;
    var n = fRight(TR_ID, 1);
    if (fIsOdd(n))
        fGetObj(sTR_ID).className = 'tr_odd';
    else
        fGetObj(sTR_ID).className = 'tr_even';
}

function fGetDataQV() {
    // GET DATA FROM SOMEWHERE ...
    var sTitle = '2004 Mercedes ML350 black 25K';
    var sPrice = '$18,900.00';
    var sDescription = 'For sale 2004 ML350. Extremely LOW miles mint barely used perfect condition no body work new brakes Mileage: 25,258 Body Style: SUV <b>Exterior Color:</b> Black Interior Color: Java Fuel: Gasoline Engine: 3.7L V6';
    //var sDescription = 'THIS IS A VERY GOOD DESCRIPTION OF ITEM I WOULD LIKE TO SELL. IT SHOULD BE LESS THAN 300 CHARACTERS  LONG. I HAVE TO SAY THAT THIS TEXT IS WRITTEN IN ORDER TO TEST HOW WOULD IT LOOK LIKE ON MY PAGE. WHAT ELSE CAN I SAY ABOUT IT, IT IS VERY GOOD STUFF I LOVE IT OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOW';
    
    if (sDescription.length < 220)
        sDescription = '<br />' + sDescription;
    if (sDescription.length < 80)
        sDescription = '<br />' + sDescription;
       
    var sPhone = '(718) 339-2021';
    var sEmail = 'person@bzpage.com';
    
    
    fGetObj('qv_Title').innerHTML = sTitle;
    fGetObj('qv_Price').innerHTML = sPrice;
    fGetObj('qv_Description').innerHTML = sDescription;
    fGetObj('qv_Phone').innerHTML = sPhone;
    var oqvEmail = fGetObj('qv_Email');
    oqvEmail.innerHTML = sEmail;
    oqvEmail.onclick = new Function('fEmail("' + sEmail + '", "' + sTitle + '", null);');
}
function fSrchB4Leave() {
    if (bQuickViewShow) fQVClose();
    fGetCities_CB();
}
function fGetCities_CB() {
    fClearAC();
    fDisabObj(false, 'sTxtCity');
    $('input#sTxtCity').focus().autocomplete(cities);
    $('input#sTxtCity').setOptions({max: 200});
}
function fClearAC(){
    $('input#sTxtCity').flushCache();
    $(".ac_results").remove();
}
function fDDStatesChange() {
    fDisabObj(true, 'sTxtCity');
    var StateID = fGetObj('cboStates').value;
    fGetObj('sTxtCity').value = '';
    if (StateID != -1) {
        fGetJSParam(77, StateID, 'fGetCities_CB()', true);
    } else { fClearAC() }
}
function fSelectSearchBy(optID) {
    var oTxtCity = fGetObj('sTxtCity');
    var oTxtZIP = fGetObj('sTxtZIP');
    var oCboStates = fGetObj('cboStates');
    var oCboSearchWithin = fGetObj('cboSearchWithin');
    if(optID==0){
        oTxtZIP.disabled = false;
        oTxtZIP.focus();
        $('#cboStates').css('color', 'transparent');
        oCboStates.disabled = true;
        $('#sTxtCity').css('color', 'transparent');
        oTxtCity.disabled = true;
        $('#cboSearchWithin').css('color', '#F4F9FD');
        oCboSearchWithin.disabled = false;
    } else {
        oTxtZIP.value = '';
        oTxtZIP.disabled = true;
        oCboStates.disabled = false;
        oCboStates.focus();
        $('#cboStates').css('color', '#002B57');
        $('#sTxtCity').css('color', '#002B57');
        if (oCboStates.value != -1) {
            oTxtCity.disabled = false;
            oTxtCity.focus();
        }
        $('#cboSearchWithin').css('color', 'transparent');
        oCboSearchWithin.disabled = true;
    }
}
var xmlTypes = null;
function fGetTypes() {
    var szXMLTypes = '';
    var sUrl = 'Ajax/SearchOpts.aspx?Opt=100';
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        szXMLTypes = jqxhr.responseText;
        if (window.DOMParser) {
            parser = new DOMParser();
            xmlTypes = parser.parseFromString(szXMLTypes, 'text/xml');
        } else { // Internet Explorer
            xmlTypes = new ActiveXObject('Microsoft.XMLDOM');
            xmlTypes.async = 'false';
            xmlTypes.loadXML(szXMLTypes);
        }
        fCreateDDTypes();
        fGetObj('ddTypes').selectedIndex = -1;
    });
}
function fCreateDDTypes() {
    var iTCnt = xmlTypes.getElementsByTagName('t').length;
    var nodeT;
    var sTID;
    for (var i = 0; i < iTCnt; i++) {        
        nodeT = xmlTypes.getElementsByTagName('t')[i];
        sTID = nodeT.attributes.getNamedItem('id').value;
        sCType = nodeT.attributes.getNamedItem('n').value;
        fDDAppendLast('ddTypes', sCType, sTID)
        fGetObj('ddTypes').style.visibility = 'visible';
    }
    fDDAppendLast('ddTypes', '***   All   *** (not recommended)', '-1');
}
function fddTypesChange(iIndex) {
    var cType_ID = fGetObj('ddTypes').options[iIndex].value;
    var xPath = "//t[@id='" + cType_ID + "']";
    var nodeT;
    var nodeC;
    fDDRemoveAll('ddCategories');
    fGetObj('ddCategories').style.visibility = 'hidden';
    // if IE
    if (window.ActiveXObject) {
        xmlTypes.setProperty('SelectionLanguage', 'XPath');
        nodeT = xmlTypes.selectNodes(xPath)[0];
    }
    // if Mozilla, Firefox, Opera, etc.
    else if (document.implementation && document.implementation.createDocument) {
        nodeT = xmlTypes.evaluate(xPath, xmlTypes, null, XPathResult.ANY_TYPE, null).iterateNext();
    }
    var iTCnt = nodeT.getElementsByTagName('c').length;
    if (iTCnt > 0) {        
        for (j = 0; j < iTCnt; j++) {
            nodeC = nodeT.getElementsByTagName('c')[j];
            fDDAppendLast('ddCategories', nodeC.attributes.getNamedItem('n').value, nodeC.attributes.getNamedItem('id').value)
        }
    }
    if (iTCnt > 1){
        fDDAppendLast('ddCategories', '***   All   *** (not recommended)', '-1');
        fGetObj('ddCategories').style.visibility = 'visible';
    }
}
function fShowAdvancedOpt() {
    if ($('#imgAdvancedOpt').attr("src").indexOf('plus_ico') > -1) {
        $('#imgAdvancedOpt').attr("src", 'images\/minus_ico.gif');
        $('#dAdvancedOpt').css('display', '');
        $("#chUseAdvancedOpt").attr("checked", "checked");
    } else {
        $('#imgAdvancedOpt').attr("src", 'images\/plus_ico.gif');
        $('#dAdvancedOpt').css('display', 'none');
        $("#chUseAdvancedOpt").attr("checked", "");
    }
}
function fAppendDP() {
$('.txtDate').datepicker({ minDate: -dDaysBefore, maxDate: dDaysAfter, showOn: 'both', buttonImageOnly: true, buttonImage: 'images/calendar.gif', buttonText: 'Select Date' });
}
function fSearch(){
    oSrchTerms.SearchOpt = 1; // exact phrase
    oSrchTerms.SearchFor = '';
    oSrchTerms.ZIP = null;
    oSrchTerms.SearchWithin = null;
    oSrchTerms.StateID = null;
    oSrchTerms.City = null;    
    oSrchTerms.TypeID = '';
    oSrchTerms.CategoryID = '';
    oSrchTerms.TitleOnly = true;
    // Advanced
    oSrchTerms.PicOnly = false;
    oSrchTerms.PricedFrom = null;
    oSrchTerms.PricedTo = null;
    oSrchTerms.DateFrom = null;
    oSrchTerms.DateTo = null;
    oSrchTerms.ExcludeOpt = 1; // exact phrase
    oSrchTerms.ExcludeWords = null;
    
    var sSearchErr = fValSrch();
    if (sSearchErr != '') {
        fGetObj('lblSearchErr').innerHTML = sSearchErr;
    } else {
        oSrchTerms.TitleOnly = fGetObj('rbSrchTit').checked;
        // Search Option
        var oSrchOpt = fGetObj('ddSearchOpt');
        oSrchTerms.SearchOpt = oSrchOpt.options[oSrchOpt.selectedIndex].value;
        fPopulateAdvancedOptions();
        var sUrl = 'Ajax/SearchOpts.aspx?Opt=102&sf=' + encodeURIComponent(oSrchTerms.SearchFor);
        sUrl += '&so=' + oSrchTerms.SearchOpt;
        sUrl += (oSrchTerms.ZIP != null)?'&z=' + oSrchTerms.ZIP:'&z=';
        sUrl += (oSrchTerms.SearchWithin != null)?'&sw=' + oSrchTerms.SearchWithin:'&sw=';
        sUrl += (oSrchTerms.StateID != null)?'&sid=' + oSrchTerms.StateID:'&sid=';
        sUrl += (oSrchTerms.City != null)?'&c=' + oSrchTerms.City:'&c=';
        sUrl += '&tid=' + oSrchTerms.TypeID;
        sUrl += '&cid=' + oSrchTerms.CategoryID;
        sUrl += '&to=' + oSrchTerms.TitleOnly;
        sUrl += '&po=' + oSrchTerms.PicOnly;
        sUrl += (oSrchTerms.PricedFrom != null)?'&pf=' + oSrchTerms.PricedFrom:'&pf=';
        sUrl += (oSrchTerms.PricedTo != null)?'&pt=' + oSrchTerms.PricedTo:'&pt=';
        sUrl += (oSrchTerms.DateFrom != null)?'&df=' + oSrchTerms.DateFrom:'&df=';
        sUrl += (oSrchTerms.DateTo != null)?'&dt=' + oSrchTerms.DateTo:'&dt=';
        sUrl += '&eo=' + oSrchTerms.ExcludeOpt;
        sUrl += (oSrchTerms.ExcludeWords != null)?'&e=' + encodeURIComponent(oSrchTerms.ExcludeWords):'&e=';
        var jqxhr = $.ajax({ url: sUrl })
    	.success(function() {
            sRes = jqxhr.responseText;
            alert(sRes);
    	    /*
    	    if (sRes != 0) {
    	        fGetObj('hClassifiedID').value = sRes;
    	        // submit pictures and long description
    	        if (fTrim(fGetObj('taLDescription').value) != '' || fImagesSelected())
    	            document.forms[0].submit();
    	        else
    	            fSetSV('uad', 'Your classified was saved successfully.<br />It will be reviewed and activated shortly.', 'fGoHome()'); // no pictures, no long description
    	    } else {
    	        // error saving
    	    }
    	    */
    	});
        
    }
    
}
function fValSrch() {
    fSClearError();
    var sSearchErr = '';
    var tSearchFor = fGetObj('sTxtSearchFor');
    var sSearchFor = fTrim(tSearchFor.value);
    if (sSearchFor.length < iMinSrchChars) {
        sSearchErr = '<img src="images/starR10.GIF" /> Search keywords must be ' + iMinSrchChars + ' or more characters long.<br />';
        tSearchFor.style.border = sErrBorder;
        tSearchFor.value = sSearchFor;
    } else {
        oSrchTerms.SearchFor = sSearchFor;
    }
    if(fGetObj('rSBZIP').checked == true){
        var oTxtZIP = fGetObj('sTxtZIP');
        var sZIP = fTrim(oTxtZIP.value);
        if (sZIP.length != 5){
            sSearchErr += '<img src="images/starR10.GIF" /> Please enter a 5-digit zip code.<br />';
            oTxtZIP.value = sZIP;
            oTxtZIP.style.border = sErrBorder;
        }else{
            oSrchTerms.ZIP = sZIP;
            var oSW = fGetObj('cboSearchWithin');
            oSrchTerms.SearchWithin = oSW.options[oSW.selectedIndex].value;
        }
    }else{
        var oCboStates = fGetObj('cboStates');
        var oTxtCity = fGetObj('sTxtCity');
        var sCity = fTrim(oTxtCity.value);
        if (oCboStates.selectedIndex == 0) {
            sSearchErr += '<img src="images/starR10.GIF" /> Please select a state.<br />';
            oCboStates.style.border = sErrBorder;
        } else if (sCity.length == 0 || cities.indexOf(sCity) < 0) {
            sSearchErr += '<img src="images/starR10.GIF" /> Please enter a valid city.<br />';
            oTxtCity.value = sCity;
            oTxtCity.style.border = sErrBorder;
        }else{
            oSrchTerms.StateID = oCboStates.options[oCboStates.selectedIndex].value;
            oSrchTerms.City = sCity;
        }
    }
    var oddTypes = fGetObj('ddTypes');
    if (oddTypes.selectedIndex == -1) {
        sSearchErr += '<img src="images/starR10.GIF" /> Please select a category.<br />';
        oddTypes.style.border = sErrBorder;
    }else{
        oSrchTerms.TypeID = oddTypes.options[oddTypes.selectedIndex].value;        
        var oddCategories = fGetObj('ddCategories');
        if(oddCategories.options.length > 0)
            oSrchTerms.CategoryID = oddCategories.options[oddCategories.selectedIndex].value;
    }
    return sSearchErr;
}
function fSClearError() {
    fGetObj('lblSearchErr').innerHTML = '';
    fGetObj('sTxtSearchFor').style.border = sNoErrBorder;
    fGetObj('sTxtZIP').style.border = sNoErrBorder;
    fGetObj('sTxtCity').style.border = sNoErrBorder;
    fGetObj('cboStates').style.border = sNoErrBorder;
    fGetObj('ddTypes').style.border = sNoErrBorder;
}

function fPopulateAdvancedOptions() {
    if (fGetObj('chUseAdvancedOpt').checked) {
        // Picture Only
        oSrchTerms.PicOnly = fGetObj('chkOnlyPics').checked;
        // Priced From and Priced To
        var sPricedFrom = fTrim(fGetObj('txtPriceFrom').value);
        var sPricedTo = fTrim(fGetObj('txtPriceTo').value);
        sPricedFrom = (sPricedFrom=='')?0:sPricedFrom;
        sPricedTo = (sPricedTo=='')?0:sPricedTo;
        if (sPricedFrom > 0 && sPricedTo > 0 && sPricedFrom > sPricedTo) {
            var tmpPrice = sPricedTo;
            sPricedTo = sPricedFrom;
            sPricedFrom = tmpPrice;
        }
        if (sPricedFrom > 0) {
            oSrchTerms.PricedFrom = sPricedFrom;
        }
        if (sPricedTo > 0) {
            oSrchTerms.PricedTo = sPricedTo;
        }
        // Date From and Date To
        var sDateFrom = fTrim(fGetObj('txtDFrom').value);
        var sDateTo = fTrim(fGetObj('txtDTo').value);
        var ds = fDateDD(DateAdd('d', new Date(), -dDaysBefore));
        var de = fDateDD(DateAdd('d', new Date(), dDaysAfter));
        var dDateFrom = null;
        var dDateTo = null;
        if(isDate(sDateFrom)){
            dDateFrom = new Date(sDateFrom);
            if(dDateFrom < ds || dDateFrom > de)
                dDateFrom = null;
        }
        if (isDate(sDateTo)) {
            dDateTo = new Date(sDateTo);            
            if(dDateTo < ds || dDateTo > de)
                dDateTo = null;
        }
        if(dDateFrom != null && dDateTo != null){
            if (dDateFrom > dDateTo) {
                var tmpDate = dDateTo;
                dDateTo = dDateFrom;
                dDateFrom = tmpDate;
            }
        }
        if(dDateFrom != null)
            oSrchTerms.DateFrom = fDateSD(dDateFrom);
        if(dDateTo != null)
            oSrchTerms.DateTo = fDateSD(dDateTo);        
        // Exclude Option
        var oExcludeOpt = fGetObj('ddExcludeOpt');
        oSrchTerms.ExcludeOpt = oExcludeOpt.options[oExcludeOpt.selectedIndex].value;
        // Exclude Words
        var sExclude = fTrim(fGetObj('txtExclude').value);
        if (sExclude.length >= iMinSrchChars)
            oSrchTerms.ExcludeWords = sExclude;
    }
}

