function PasswordIsValid(sValue) {
    if (fTrim(sValue)=='')
        return false;
    if (sValue.length < 6)
        return false;
    return true;
}
function UserNameIsValid(sValue) {
    if (sValue.length < 6)
        return false;    
    return true;
}


function AnswerIsValid(sValue) {
    if (sValue=='')
        return false;
    return true;
}
//
function EmailIsValid(address) {
    if (address.length < 6)
        return false;
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(address) == false) {
        return false;
    }
    return true;
}
function PhoneIsValid(phone) {
    var tPhone = phone.replace(/ /g, '');
    if (tPhone.length < 10 || isNaN(tPhone))
        return false;
    return true;
}
/* DATES */
function DateAdd(Dpart, dDate, iVal){
    switch (Dpart){
        case 'd': //add days
            dDate.setDate(dDate.getDate() + iVal)
            break;
        case 'm': //add months
            dDate.setMonth(dDate.getMonth() + iVal)
            break;
        case 'y': //add years
            dDate.setYear(dDate.getFullYear() + iVal)
            break;
        case 'h': //add hours
            dDate.setHours(dDate.getHours() + iVal)
            break;
        case 'n': //add minutes
            dDate.setMinutes(dDate.getMinutes() + iVal)
            break;
        case 's': //add seconds
            dDate.setSeconds(dDate.getSeconds() + iVal)
            break;
    }
    return dDate;
}
function isDate(str){
    var s = String(str).split(/[-\/., ]/);
    var dt = new Date(s[0] + '/' + s[1] + '/' + s[2]);
    if (dt.getMonth() + 1 == s[0] && dt.getDate() == s[1] && dt.getFullYear() == s[2]) {
        return true;
    }else{
        return false;
    }
}
function fDateDD(dDate){
    return new Date(fDateSD(dDate));
}
function fDateSD(dDate){
    return (dDate.getMonth()+1).toString() + '/' + dDate.getDate().toString() + '/' + dDate.getFullYear().toString();
}