var iLoginTries = 1;
var iRecoverTries = 1;
var sAnswer = '';
var iRecoverTries2 = 1;
var iMaxLogTries = 5;
var iMaxRecTries = 3;
var sProfileString = '';

iErrCnt = 0

function ResetVars() {
    iLoginTries = 1;
    iRecoverTries = 1;
    sAnswer = '';
    iRecoverTries2 = 1;
}
function fLogin() {
    var oErrL = fGetObj('lblLoginErr');
    oErrL.innerHTML = '';
    oErrL.className = '';
    var sUserName = fTrim(fGetObj('txtUserName').value);
    var sPassword = fTrim(fGetObj('txtPassword').value);
    if (UserNameIsValid(sUserName) && PasswordIsValid(sPassword)) {
        var sUrl = 'Ajax/LoginOpts.aspx?Opt=100&UN=' + encodeURIComponent(sUserName) + '&PW=' + encodeURIComponent(sPassword);
        var jqxhr = $.ajax({ url: sUrl })
	    .success(function() {
	        if (jqxhr.responseText == '1') {
	            if (fGetObj('chkRememberMe').checked)
	                setCookie('bzp_UN', sUserName, 30);
	            else
	                setCookie('bzp_UN', sUserName, -1);
	            fGoHome();
	        } else {
	            if (iLoginTries > (iMaxLogTries - 1)) {
	                fGetObj('lblLoginErr').innerHTML = 'Forgot your password? <span class="spn_linkR" title="Forgot Password" onclick="fMCH(14)">Click here</span> to recover it.<br />Or reload the page and try again.';
	                DisabBtn('btnLogin');
	            } else {
	                iLoginTries += 1;
	                fInvalidUNPW();
	            }
	        }
	    });
    } else {
        fInvalidUNPW();
    }
}
function fRecoverPass() {
    fGetObj('divLoader').style.visibility = 'visible';
    var sUserName = fTrim(fGetObj('txtUserName').value);
    fGetObj('lblRecoverErr').innerHTML = '';
    var sUrl = 'Ajax/LoginOpts.aspx?Opt=106&UN=' + encodeURIComponent(sUserName);
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        if (jqxhr.responseText == '1') {
            fMCH(12);
            ResetVars();
            var oErrL = fGetObj('lblLoginErr');
            oErrL.innerHTML = 'Your password has been successfully emailed to you.<br />Please check your mail box.';
            oErrL.className = 'spnQuestionG';
        } else {
            fGetObj('lblRecoverErr').innerHTML = sStandardError;
        }
        fGetObj('divLoader').style.visibility = 'hidden';
    });
}
function fInvalidUN() {
    fGetObj('lblRecoverErr').innerHTML = 'Please enter valid user name or email.';
}
function fRecover_S1(iTN) {
    fGetObj('lblRecoverErr').innerHTML = '';
    var sUserName = fTrim(fGetObj('txtUserName').value);
    var sRes = '0';
    var sQA = new Array();
    if (UserNameIsValid(sUserName)) {
        var sUrl = 'Ajax/LoginOpts.aspx?Opt=104&UN=' + encodeURIComponent(sUserName) + '&TN=' + iTN;
        var jqxhr = $.ajax({ url: sUrl })
    	.success(function() {
    	    sRes = jqxhr.responseText;
    	    if (sRes != '0') {
    	        sQA = sRes.split('\<Q|A\>');
    	        ShowQuestion(sQA[0], sQA[1].toLowerCase());
    	    } else {
    	        if (iRecoverTries > (iMaxRecTries - 1)) {
    	            fMaxNumOfRecovery();
    	            DisabBtn('btnGo1');
    	        } else {
    	            iRecoverTries += 1;
    	            fInvalidUN();
    	        }
    	    }
    	});
    } else {
        fInvalidUN();
    }
}
function ShowQuestion(s_Question, s_Answer) {
    fGetObj('spnQuestion').innerHTML = s_Question;
    sAnswer = s_Answer;
    fGetObj('QTR0').style.visibility = 'visible';
    fGetObj('QTR1').style.visibility = 'visible';
    fGetObj('QTR2').style.visibility = 'visible';
    if (iRecoverTries2 == 1)
        fGetObj('spnQuestion').className = 'spnQuestionG';
    if (iRecoverTries2 == 2)
        fGetObj('spnQuestion').className = 'spnQuestionB';
    if (iRecoverTries2 == 3)
        fGetObj('spnQuestion').className = 'spnQuestionR';
    DisabBtn('btnGo1');
    fGetObj('txtUserName').disabled = 'disabled';
    fGetObj('txtAnswer').focus();
}

function fRecover_S2() {
    fGetObj('lblRecoverErr').innerHTML = '';
    var szAnswer = fTrim(fGetObj('txtAnswer').value).toLowerCase();
    if (AnswerIsValid(szAnswer)) {
        if (sAnswer == szAnswer) {
            fRecoverPass();
        } else {
            if (iRecoverTries2 > (iMaxRecTries - 1)) {
                fMaxNumOfRecovery();
                DisabBtn('btnGo2');
            } else {
                iRecoverTries2 += 1;
                fRecover_S1(iRecoverTries2);
                fGetObj('lblRecoverErr').innerHTML = 'Incorrect answer.';
                fGetObj('txtAnswer').value = '';
            }
        }
    }
}
function fSaveUser() {
    var iRes = 1;
    if (iErrCnt > 0) {
        fReSetHeight('content', (iErrCnt * 15));
        fReSetHeight('divFRAME01', (iErrCnt * 15));
        // reset borders from previous errors
        fGetObj('txtLoginID').style.border = sNoErrBorder;
        fGetObj('txtPassword').style.border = sNoErrBorder;
        fGetObj('txtPasswordRe').style.border = sNoErrBorder;
        fGetObj('txtEmail').style.border = sNoErrBorder;
        fGetObj('txtFName').style.border = sNoErrBorder;
        fGetObj('RecQues_01').style.border = sNoErrBorder;
        fGetObj('txtAnswer1').style.border = sNoErrBorder;
        fGetObj('lbl_Err').innerHTML = '';
    }
    iErrCnt = 0;
    //
    
    // validate required fields
    var LoginID = fGetEncodedValue('txtLoginID');
    if (!UserNameIsValid(LoginID)) {iRes = 0; SetError('User Name must be at least 6 characters.', 'txtLoginID'); }

    var Password = fGetEncodedValue('txtPassword');
    if (!PasswordIsValid(Password)) {iRes = 0; SetError('Password must be at least 6 characters.', 'txtPassword'); }

    var PasswordRe = fGetEncodedValue('txtPasswordRe');
    if (!PasswordIsValid(PasswordRe)) { iRes = 0; SetError('Re-entered Password must be at least 6 characters.', 'txtPasswordRe'); }
    if (Password != PasswordRe) { iRes = 0; SetError('Password and Re-entered Password do not match.', 'txtPasswordRe'); }
        
    var Email = fGetEncodedValue('txtEmail');
    if (!EmailIsValid(fGetObj('txtEmail').value)) { iRes = 0; SetError('Please enter correct email.', 'txtEmail'); }

    var FName = fGetEncodedValue('txtFName');
    if (FName == '') { iRes = 0; SetError('First Name can not be empty.', 'txtFName'); }

    var QuestionID1 = fGetObj('RecQues_01').value;
    if (QuestionID1 == '-1') { iRes = 0; SetError('Please select first securiry question.', 'RecQues_01'); }
    
    var Answer1 = fGetEncodedValue('txtAnswer1');
    if (Answer1 == '') { iRes = 0; SetError('Question 1 field can not be empty.', 'txtAnswer1'); }
    
    if(iRes == 0) return;
    
    var LName = fGetEncodedValue('txtLName');
    var Phone1 = fGetEncodedValue('txtPhone1');
    var Phone2 = fGetEncodedValue('txtPhone2');
    var FAX = fGetEncodedValue('txtFAX');
    var Address1 = fGetEncodedValue('txtAddress1');
    var Address2 = fGetEncodedValue('txtAddress2');
    var City = fGetEncodedValue('txtCity');
    var StateID = fGetObj('cboStates').value;
    var ZIP = fGetEncodedValue('txtZIP');   
    var QuestionID2 = fGetObj('RecQues_02').value;
    var Answer2 = fGetEncodedValue('txtAnswer2');
    var QuestionID3 = fGetObj('RecQues_03').value;
    var Answer3 = fGetEncodedValue('txtAnswer3');

    var sParams = '&LoginID=' + LoginID;
    sParams += '&Password=' + Password;
    sParams += '&Email=' + Email;
    sParams += '&FName=' + FName;
    sParams += '&QuestionID1=' + QuestionID1;
    sParams += '&Answer1=' + Answer1;    
    if (LName != '') sParams += '&LName=' + LName;
    if (Phone1 != '') sParams += '&Phone1=' + Phone1;
    if (Phone2 != '') sParams += '&Phone2=' + Phone2;
    if (FAX != '') sParams += '&FAX=' + FAX;
    if (Address1 != '') sParams += '&Address1=' + Address1;
    if (Address2 != '') sParams += '&Address2=' + Address2;
    if (City != '') sParams += '&City=' + City;
    if (StateID != '-1') sParams += '&StateID=' + StateID;
    if (ZIP != '') sParams += '&ZIP=' + ZIP;
    if (QuestionID2 != '-1') sParams += '&QuestionID2=' + QuestionID2;
    if (Answer2 != '') sParams += '&Answer2=' + Answer2;
    if (QuestionID3 != '-1') sParams += '&QuestionID3=' + QuestionID3;
    if (Answer3 != '') sParams += '&Answer3=' + Answer3;

    var sRes;
    var sUrl = 'Ajax/LoginOpts.aspx?Opt=108' + sParams;
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        sRes = jqxhr.responseText;
        if (sRes == -1) {
            // User Name exists
            SetError('User Name <b>' + fGetObj('txtLoginID').value + '</b> already exists.', 'txtLoginID');
        }
        else if (sRes == -2) {
            // email exists
            SetError('User with email <b>' + fGetObj('txtEmail').value + '</b> already exists.', 'txtEmail');
        }
        else if (sRes == 0) {
            // general error
            SetError(sStandardError, '');
        }
        else if (sRes == 37) {
            // email error
            SetError('User <b>' + LoginID + '</b> was successfully created. <br />But email was not sent to you, due system error.', '');
        }
        else if (sRes == 1) {
            // success
            fMCH(12);
            ResetVars();
            var oErrL = fGetObj('lblLoginErr');
            oErrL.innerHTML = 'User <b>' + LoginID + '</b> was successfully created. <br />Please check your mail box and activate your account.';
            oErrL.className = 'spnQuestionG';
        }
        fGetObj('divLoader').style.visibility = 'hidden';
    });
    fGetObj('divLoader').style.visibility = 'visible';
}
function fMaxNumOfRecovery() {
    fGetObj('lblRecoverErr').innerHTML = 'You have exceeded the maximum number of recovery attempts.<br />Please contact us if you believe that you get this message in error.';
}
function fInvalidUNPW() {
    fGetObj('lblLoginErr').innerHTML = 'Please enter valid user name and password.';
}
function SetError(sErrMsg, oObjID) {
    iErrCnt += 1;
    if (oObjID != '') fGetObj(oObjID).style.border = sErrBorder;
    fGetObj('lbl_Err').innerHTML += '<img src="images/starR10.GIF" /> ' + sErrMsg + '<br />';

    fSetHeight('content', 15);
    fSetHeight('divFRAME01', 15);
}
function fSetHeight(oID, iH) {
    var oObj = fGetObj(oID);
    var sHt = parseInt((oObj.style.height).replace('px', '')) + iH;
    sHt += 'px';
    oObj.style.height = sHt;
}
function fReSetHeight(oID, iH) {
    var oObj = fGetObj(oID);
    var sHt = parseInt((oObj.style.height).replace('px', '')) - iH;
    sHt += 'px';
    oObj.style.height = sHt;
}

/************   EDIT PROFILE  ************************/
var sSeparator = '|!~j';

function fLoadProfile() {
    var sRes;
    var oUser = new Array();
    var sUrl = 'Ajax/LoginOpts.aspx?Opt=110';
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        sRes = jqxhr.responseText;
        oUser = sRes.split(sSeparator);

        fGetObj('txtLoginID').value = oUser[0];
        fGetObj('txtEmail').value = oUser[1];
        fGetObj('txtFName').value = oUser[2];
        fGetObj('txtPassword').value = oUser[3];
        fGetObj('txtPasswordRe').value = oUser[3];
        SetDDSelectedItem('RecQues_01', oUser[4]);
        fGetObj('txtAnswer1').value = oUser[5];
        if (oUser[6] != '') fGetObj('txtLName').value = oUser[6];
        if (oUser[7] != '') fGetObj('txtPhone1').value = oUser[7];
        if (oUser[8] != '') fGetObj('txtPhone2').value = oUser[8];
        if (oUser[9] != '') fGetObj('txtFAX').value = oUser[9];
        if (oUser[10] != '') fGetObj('txtAddress1').value = oUser[10];
        if (oUser[11] != '') fGetObj('txtAddress2').value = oUser[11];
        if (oUser[12] != '') fGetObj('txtCity').value = oUser[12];
        if (oUser[13] != '') SetDDSelectedItem('cboStates', oUser[13]);
        if (oUser[14] != '') fGetObj('txtZIP').value = oUser[14];
        if (oUser[15] != '') SetDDSelectedItem('RecQues_02', oUser[15]);
        if (oUser[16] != '') fGetObj('txtAnswer2').value = oUser[16];
        if (oUser[17] != '') SetDDSelectedItem('RecQues_03', oUser[17]);
        if (oUser[18] != '') fGetObj('txtAnswer3').value = oUser[18];
        sProfileString = fGetFormControlString('aspnetForm');
    });
}
function fEditUser() {
    var sPString = fGetFormControlString('aspnetForm');

    var iRes = 1;
    if (iErrCnt > 0) {
        fReSetHeight('content', (iErrCnt * 15));
        fReSetHeight('divFRAME01', (iErrCnt * 15));
        // reset borders from previous errors
        fGetObj('txtLoginID').style.border = sNoErrBorder;
        fGetObj('txtPassword').style.border = sNoErrBorder;
        fGetObj('txtPasswordRe').style.border = sNoErrBorder;
        fGetObj('txtEmail').style.border = sNoErrBorder;
        fGetObj('txtFName').style.border = sNoErrBorder;
        fGetObj('RecQues_01').style.border = sNoErrBorder;
        fGetObj('txtAnswer1').style.border = sNoErrBorder;
        fGetObj('lbl_Err').innerHTML = '';
    }
    iErrCnt = 0;

    if (sProfileString == sPString) {
        SetError('Please make some modifications before hitting <b>Save</b> button. &nbsp;Or hit <b>Cancel</b> button and return to home page.<br />', '');
        return;
    }

    // validate required fields
    var LoginID = fGetEncodedValue('txtLoginID');
    if (!UserNameIsValid(LoginID)) { iRes = 0; SetError('User Name must be at least 6 characters.', 'txtLoginID'); }

    var Password = fGetEncodedValue('txtPassword');
    if (!PasswordIsValid(Password)) { iRes = 0; SetError('Password must be at least 6 characters.', 'txtPassword'); }

    var PasswordRe = fGetEncodedValue('txtPasswordRe');
    if (!PasswordIsValid(PasswordRe)) { iRes = 0; SetError('Re-entered Password must be at least 6 characters.', 'txtPasswordRe'); }
    if (Password != PasswordRe) { iRes = 0; SetError('Password and Re-entered Password do not match.', 'txtPasswordRe'); }

    var Email = fGetEncodedValue('txtEmail');
    if (!EmailIsValid(fGetObj('txtEmail').value)) { iRes = 0; SetError('Please enter correct email.', 'txtEmail'); }

    var FName = fGetEncodedValue('txtFName');
    if (FName == '') { iRes = 0; SetError('First Name can not be empty.', 'txtFName'); }

    var QuestionID1 = fGetObj('RecQues_01').value;
    if (QuestionID1 == '-1') { iRes = 0; SetError('Please select first securiry question.', 'RecQues_01'); }

    var Answer1 = fGetEncodedValue('txtAnswer1');
    if (Answer1 == '') { iRes = 0; SetError('Question 1 field can not be empty.', 'txtAnswer1'); }

    if (iRes == 0) return;

    var LName = fGetEncodedValue('txtLName');
    var Phone1 = fGetEncodedValue('txtPhone1');
    var Phone2 = fGetEncodedValue('txtPhone2');
    var FAX = fGetEncodedValue('txtFAX');
    var Address1 = fGetEncodedValue('txtAddress1');
    var Address2 = fGetEncodedValue('txtAddress2');
    var City = fGetEncodedValue('txtCity');
    var StateID = fGetObj('cboStates').value;
    var ZIP = fGetEncodedValue('txtZIP');
    var QuestionID2 = fGetObj('RecQues_02').value;
    var Answer2 = fGetEncodedValue('txtAnswer2');
    var QuestionID3 = fGetObj('RecQues_03').value;
    var Answer3 = fGetEncodedValue('txtAnswer3');

    var sParams = '&LoginID=' + LoginID;
    sParams += '&Password=' + Password;
    sParams += '&Email=' + Email;
    sParams += '&FName=' + FName;
    sParams += '&QuestionID1=' + QuestionID1;
    sParams += '&Answer1=' + Answer1;
    if (LName != '') sParams += '&LName=' + LName;
    if (Phone1 != '') sParams += '&Phone1=' + Phone1;
    if (Phone2 != '') sParams += '&Phone2=' + Phone2;
    if (FAX != '') sParams += '&FAX=' + FAX;
    if (Address1 != '') sParams += '&Address1=' + Address1;
    if (Address2 != '') sParams += '&Address2=' + Address2;
    if (City != '') sParams += '&City=' + City;
    if (StateID != '-1') sParams += '&StateID=' + StateID;
    if (ZIP != '') sParams += '&ZIP=' + ZIP;
    if (QuestionID2 != '-1') sParams += '&QuestionID2=' + QuestionID2;
    if (Answer2 != '') sParams += '&Answer2=' + Answer2;
    if (QuestionID3 != '-1') sParams += '&QuestionID3=' + QuestionID3;
    if (Answer3 != '') sParams += '&Answer3=' + Answer3;

    var sRes;
    var sUrl = 'Ajax/LoginOpts.aspx?Opt=112' + sParams;
    var jqxhr = $.ajax({ url: sUrl })
    .success(function() {
        sRes = jqxhr.responseText;
        if (sRes == -1) {
            // User Name exists
            SetError('User Name <b>' + fGetObj('txtLoginID').value + '</b> already exists.', 'txtLoginID');
        } else if (sRes == -2) {
            // email exists
            SetError('User with email <b>' + fGetObj('txtEmail').value + '</b> already exists.', 'txtEmail');
        } else if (sRes == 0) {
            // general error
            SetError(sStandardError, '');
        } else if (sRes == 1) {
            fGoHome();
        }
        fGetObj('divLoader').style.visibility = 'hidden';
    });
    fGetObj('divLoader').style.visibility = 'visible';
}
function SetDDSelectedItem(sDD_ID, sVal) {
    var oDD = fGetObj(sDD_ID);
    for (var i = 0; i < oDD.length; i++) {
        if (oDD[i].value == sVal) {
            oDD[i].selected = true;
            break;
        }
    }
}

function fGetFormControlString(FormID) {
    var sRes = '';
    var oEls = document.forms[FormID].elements;
    for (i = 0; i < oEls.length; i++) {
        if (oEls[i].type == 'checkbox') {
            sRes += (oEls[i].checked) ? "1" : "";
        }
        if (oEls[i].type == 'text' || oEls[i].type == 'password') {
            sRes += (fTrim(oEls[i].value) != '') ? fTrim(oEls[i].value) : '';
        }
        if (oEls[i].type == 'select-one') {
            sRes += oEls[i].value;
        }
    }
    return sRes;
}

function fProfileCancel() {
    fGoHome();
}