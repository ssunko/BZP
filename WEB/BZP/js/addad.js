/////////////////////
var taMLColors = [['20%', '#9d1d1d'], ['10%', '#f12c5b']]
var aDChKeyCodes = /(8)|(10)|(13)|(16)|(17)|(18)|(19)|(20)|(127)/
taMLColors.sort(function(a, b) { return parseInt(a[0]) - parseInt(b[0]) }) //sort taMLColors by percentage, ascending

function fSetMaxSize($fields, optsize, OutputDiv) {
    var $ = jQuery
    $fields.each(function(i) {
        var $field = $(this)
        $field.data('maxsize', optsize || parseInt($field.attr('data-maxsize'))) //max character limit
        var DivStatus = OutputDiv || $field.attr('data-output') //id of DIV to output status
        $field.data('$statusdiv', $('#' + DivStatus).length == 1 ? $('#' + DivStatus) : null)
        $field.unbind('keypress.restrict').bind('keypress.restrict', function(e) {
            fSetMaxSize.restrict($field, e)
        })
        $field.unbind('keyup.show').bind('keyup.show', function(e) {
            fSetMaxSize.showlimit($field)
        })
        fSetMaxSize.showlimit($field) //show status to start
    })
}
fSetMaxSize.restrict = function($field, e) {
    var keyunicode = e.charCode || e.keyCode
    if (!aDChKeyCodes.test(keyunicode)) {
        if ($field.val().length >= $field.data('maxsize')) { //if characters entered exceed allowed
            if (e.preventDefault)
                e.preventDefault()
            return false
        }
    }
}
fSetMaxSize.showlimit = function($field) {
    if ($field.val().length > $field.data('maxsize')) {
        var trimmedtext = $field.val().substring(0, $field.data('maxsize'))
        $field.val(trimmedtext)
    }
    if ($field.data('$statusdiv')) {
        $field.data('$statusdiv').css('color', '').html($field.val().length)
        var pctremaining = ($field.data('maxsize') - $field.val().length) / $field.data('maxsize') * 100 //calculate chars remaining in terms of percentage
        for (var i = 0; i < taMLColors.length; i++) {
            if (pctremaining <= parseInt(taMLColors[i][0])) {
                $field.data('$statusdiv').css('color', taMLColors[i][1])
                break
            }
        }
    }
}
/////////////////////

var xmlTypes = null;
var sFilesErrs = '';
var cType_ID = '';
var cTypeVal = '';
var oAddedAd = new Object();

function fWriteFileName(sFN, oDiv) {
    var sFName = fGetFileName(sFN);
    if (sFName == '' || CheckImgExtention(sFName.toLowerCase())) {
        oDiv.className = 'divFile';
        sFilesErrs = sFilesErrs.replace(oDiv.id, '');
    } else {
        oDiv.className = 'divFileErr';
        if(sFilesErrs.indexOf(oDiv.id) == -1) sFilesErrs += oDiv.id;
    }
    fGetObj('lblAddAdErr').innerHTML = fAACheckErrors(true);
    if (sFName.length > 30) {
        oDiv.title = sFName;
        sFName = fLeft(sFName, 10) + '~...~' + fRight(sFName, 15)
    } else
        oDiv.title = '';
    oDiv.innerHTML = '&nbsp;&nbsp;' + sFName + '&nbsp;&nbsp;';
}
function fAppendFiles() {
    var oTblFiles = fGetObj('tblFiles').cloneNode(true);
    oTblFiles.style.position = 'static';
    oTblFiles.style.visibility = 'visible';
    fGetObj('dFilesCont').appendChild(oTblFiles);
    document.forms[0].reset(); // clear form
    // add text area maxsize limiter
    fSetMaxSize($("input[data-maxsize], textarea[data-maxsize]"));
}

function CheckImgExtention(sFN) {
    var filePath = sFN.replace(/^\s|\s$/g, '');
    if (/\.\w+$/.test(filePath)) {
        var m = filePath.match(/([^\/\\]+)\.(\w+)$/);
        if (m) {
            if (m[2] == 'gif' || m[2] == 'jpg' || m[2] == 'jpeg' || m[2] == 'png' || m[2] == 'bmp') {
                return true;
            }
        }
    }
    return false;
}

function fSaveAdd() {
    var sAddAdErr = fAACheckErrors(false);
    var sRes = '0';
    if (sAddAdErr != '') {
        fGetObj('lblAddAdErr').innerHTML = sAddAdErr;
    } else {
        var sUrl = 'Ajax/AddAdOpts.aspx?Opt=104&t=' + encodeURIComponent(fGetObj('txtTitle').value);
        sUrl += '&d=' + encodeURIComponent(fGetObj('taSDescription').value);
        sUrl += '&z=' + oAddedAd.ZIP;
        sUrl += '&p=' + oAddedAd.Phone;
        sUrl += '&e=' + encodeURIComponent(oAddedAd.Email);
        sUrl += '&c=' + oAddedAd.CategoryID;
        var jqxhr = $.ajax({ url: sUrl })
    	.success(function() {
            sRes = jqxhr.responseText;
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
    	});
    }
}
function fImagesSelected() {
    if (fTrim(fGetObj('ctl00_ContentBox_file').value) != '')
        return true;
    if (fTrim(fGetObj('ctl00_ContentBox_file1').value) != '')
        return true;
    if (fTrim(fGetObj('ctl00_ContentBox_file2').value) != '')
        return true;
    if (fTrim(fGetObj('ctl00_ContentBox_file3').value) != '')
        return true;
    return false;
}
function fAACheckErrors(bCheckFileNameOnly) {
    var sAddAdErr = '';
    var tAATitle = fGetObj('txtTitle');
    var tAADescription = fGetObj('taSDescription');
    fClearError();
    if (bCheckFileNameOnly == false) {
        if (fTrim(tAATitle.value) == '') {
            sAddAdErr = '<img src="images/starR10.GIF" /> Title is a required field.';
            tAATitle.style.border = sErrBorder;
        }
        if (fTrim(tAADescription.value) == '') {
            sAddAdErr = (sAddAdErr == '') ? '' : sAddAdErr + '<br />';
            sAddAdErr += '<img src="images/starR10.GIF" /> Short Description is a required field.';
            tAADescription.style.border = sErrBorder;
        }
    }
    if (sFilesErrs != '') {
        sAddAdErr = (sAddAdErr == '') ? '' : sAddAdErr + '<br />';
        sAddAdErr += '<img src="images/starR10.GIF" /> You have selected one or more non-image files.';
    }        
    return sAddAdErr;
}
function fClearError() {
    fGetObj('lblAddAdErr').innerHTML = '';
    fGetObj('txtTitle').style.border = sNoErrBorder;
    fGetObj('taSDescription').style.border = sNoErrBorder;
}
function fStartOverAddAd() {
    sFilesErrs = '';
    var oDiv = fGetObj('sFile');
    oDiv.innerHTML = '';
    oDiv.className = '';
    oDiv = fGetObj('sFile1');
    oDiv.innerHTML = '';
    oDiv.className = '';
    oDiv = fGetObj('sFile2');
    oDiv.innerHTML = '';
    oDiv.className = '';
    oDiv = fGetObj('sFile3');
    oDiv.innerHTML = '';
    oDiv.className = '';
    fGetObj('statSDescription').innerHTML = '0';
    fGetObj('statTitle').innerHTML = '0';
    fGetObj('statLDescription').innerHTML = '0';
    fClearError();
    document.forms[0].reset();
    fDDRemoveAll('ddTypes');
    fGetObj('ddTypes').style.visibility = 'hidden';
    fGetObj('sPSSC').style.visibility = 'hidden';
    cType_ID = '';
    fPopulateUserInfo();
    fShowDivTypes(true);
}
function fGetAdTypes() {
    var szXMLTypes = '';
    var sUrl = 'Ajax/AddAdOpts.aspx?Opt=100';
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
        fShowTypes();
        fPopulateUserInfo();
    });
    fChangeTabNo(1);
}
function fSelectCType(rID) {
    cType_ID = rID.replace('r', '');
    cTypeVal = fGetObj(rID).value;
    var xPath = "//t[@id='" + cType_ID + "']";
    var nodeT;
    var nodeC;
    fDDRemoveAll('ddTypes');
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
            fDDAppendLast('ddTypes', nodeC.attributes.getNamedItem('n').value, nodeC.attributes.getNamedItem('id').value)
        }
    }
    if (iTCnt > 1) {
        fGetObj('sPSSC').style.visibility = 'visible';
        fGetObj('ddTypes').style.visibility = 'visible';
    } else {
        fGetObj('sPSSC').style.visibility = 'hidden';
        fGetObj('ddTypes').style.visibility = 'hidden';       
    }
}
function fShowTypes() {
    var oRBType;
    var iTCnt = xmlTypes.getElementsByTagName('t').length;
    var nodeT;
    var sTId;
    var sCType;
    var oSpan;
    var oTDT;
    for (var i = 0; i < iTCnt; i++) {
        oTDT = fGetObj('tdRBs');
        nodeT = xmlTypes.getElementsByTagName('t')[i];
        sTId = 'r' + nodeT.attributes.getNamedItem('id').value;
        sCType = nodeT.attributes.getNamedItem('n').value;
        oRBType = document.createElement('input');
        oRBType.style.border = 'none';
        oRBType.setAttribute("type", "radio");
        oRBType.setAttribute("id", sTId);
        oRBType.setAttribute("value", sCType);        
        oRBType.setAttribute("name", "rType");
        oRBType.setAttribute("onclick", "fSelectCType(this.id);");
        oRBType.style.marginTop = '8px';
        oTDT.appendChild(oRBType);
        oSpan = document.createElement('span');
        oSpan.setAttribute("onclick", "fGetObj('" + sTId + "').click();");
        oSpan.style.cursor = 'pointer';
        oSpan.innerHTML = sCType + '<br />';
        oTDT.appendChild(oSpan);
    }
}
function fADD_ADNext() {
    var oAddAdErr = fGetObj('lblAdTypesErr');
    oAddAdErr.innerHTML = '';
    var oDDTypes = fGetObj('ddTypes');
    var sZIP = fTrim(fGetObj('txtZIP').value);

    if (fAddAdFormIsValud(sZIP, oAddAdErr, oDDTypes)) {
        var sRes;
        var pCategoryID = '';
        var pCategory = '';
        var pCat_Subcat = '';
        var pEmail = '';
        var pPhone = '';
        var sUrl = 'Ajax/AddAdOpts.aspx?Opt=102&z=' + sZIP;
        var jqxhr = $.ajax({ url: sUrl })
        .success(function() {
            sRes = jqxhr.responseText;
            if (sRes == '1') {
                if (oDDTypes.style.visibility != 'hidden') {
                    pCategoryID = oDDTypes.options[oDDTypes.selectedIndex].value;
                    pCategory = oDDTypes.options[oDDTypes.selectedIndex].text;
                } else {
                    pCategoryID = oDDTypes.options[0].value;
                    pCategory = oDDTypes.options[0].text;
                }
                pPhone = fGetObj('txtShowPhone').value; pPhone = (pPhone == '') ? sNotListed : pPhone;
                pEmail = fGetObj('txtShowEmail').value; pEmail = (pEmail == '') ? sNotListed : pEmail;

                oAddedAd.CategoryID = pCategoryID;
                oAddedAd.Category = pCategory;
                oAddedAd.Type_ID = cType_ID;
                oAddedAd.Type = cTypeVal;
                oAddedAd.ZIP = sZIP;
                oAddedAd.Phone = pPhone;
                oAddedAd.Email = pEmail;

                pCat_Subcat = oAddedAd.Type;
                pCat_Subcat += (pCategory == '') ? '' : ' >> ' + oAddedAd.Category;

                fGetObj('pCat_Subcat').innerHTML = pCat_Subcat;
                fGetObj('pZIP').innerHTML = oAddedAd.ZIP;
                fGetObj('pEmail').innerHTML = pEmail;
                fGetObj('pPhone').innerHTML = pPhone;
                // go to next screen ...
                fShowDivTypes(false);
            } else {
                oAddAdErr.innerHTML = '<img src="images/starR10.GIF" /> Please enter valid 5-digit zip code.<br />';
            }
        });
    }
}
function fAddAdFormIsValud(sZIP, oAddAdErr, oDDTypes) {
    var bValid = true;
    var oTxtZIP = fGetObj('txtZIP');
    var oChkShowEmail = fGetObj('chkShowEmail');
    var oTxtShowEmail = fGetObj('txtShowEmail');
    var oTxtShowPhone = fGetObj('txtShowPhone');
    var oChkShowPhone = fGetObj('chkShowPhone');    
    
    // reset borders from previous errors
    oTxtZIP.style.border = sNoErrBorder;
    oTxtShowEmail.style.border = sNoErrBorder;
    oTxtShowPhone.style.border = sNoErrBorder;
    
    if (cType_ID == '') {
        oAddAdErr.innerHTML += '<img src="images/starR10.GIF" /> Please select category.<br />';
        bValid = false;
    }
    if (oDDTypes.style.visibility != 'hidden') {
        if (oDDTypes.selectedIndex == -1) {
            oAddAdErr.innerHTML += '<img src="images/starR10.GIF" /> Please select subcategory.<br />';
            bValid = false;
        }
    }    
    if (sZIP.length != 5 || isNaN(sZIP)) {
        oAddAdErr.innerHTML += '<img src="images/starR10.GIF" /> Please enter a 5-digit zip code.<br />';
        oTxtZIP.value = sZIP;
        oTxtZIP.style.border = sErrBorder;
        bValid = false;
    }
    if(oChkShowEmail.checked == true && !EmailIsValid(oTxtShowEmail.value)){
        oAddAdErr.innerHTML += '<img src="images/starR10.GIF" /> Please enter valid email, or uncheck "List this email" checkbox.<br />';
        oTxtShowEmail.style.border = sErrBorder;
        bValid = false;
    }
    if (oChkShowPhone.checked == true && !PhoneIsValid(oTxtShowPhone.value)) {
        oAddAdErr.innerHTML += '<img src="images/starR10.GIF" /> Please enter valid phone, or uncheck "List this phone" checkbox.<br />';
        oTxtShowPhone.style.border = sErrBorder;
        bValid = false;
    }
    return bValid;
}

function fShowDivTypes(bST) {
    if (bST) {
        fGetObj('dTypes').style.display = '';
        fGetObj('dFiles').style.display = 'none';
    } else {
        fGetObj('dTypes').style.display = 'none';
        fGetObj('dFiles').style.display = '';
    }
}
function fPopulateUserInfo() {
    if (sUEmail != '') {
        fGetObj('txtShowEmail').value = sUEmail;
        fGetObj('chkShowEmail').checked = true;
    }
    if (sUPhone != '') {
        fGetObj('txtShowPhone').value = sUPhone;
        fGetObj('chkShowPhone').checked = true;
    }
    fGetObj('txtZIP').value = sUZIP;
}
function fChkBChecked(oChk, TxtID, sTxtVal) {
    var oTxtA = fGetObj(TxtID);
    if (oChk.checked) {
        oTxtA.value = sTxtVal;
        oTxtA.disabled = false;
    } else {
        oTxtA.value = '';
        oTxtA.disabled = true;
    }
    oTxtA.select();
}
function fChangeTabNo(TabNo) {
    fGetObj('ctl00_ContentBox_hTabNo').value = TabNo;
}