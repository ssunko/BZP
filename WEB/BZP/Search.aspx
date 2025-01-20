<%@ Page ErrorPage="BZPError.aspx" Language="C#" MasterPageFile="MPmain.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>
<asp:Content ID="headSearch" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/jquery-ui.min.js"></script>
<link rel='stylesheet' type='text/css' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/redmond/jquery-ui.css' />
<script type='text/javascript' src='js\jquery.autocomplete.pack.js'></script>
<script  type="text/javascript">
imgPreload = new Image();
imgUrl = new Array(); imgUrl[0] = 'images/loader.GIF'; imgUrl[1] = 'images/1x1px.GIF'; imgUrl[2] = 'images/CloseRa.gif'; imgUrl[3] = 'images/phone_ico.gif'; imgUrl[4] = 'images/email_ico.gif'; imgUrl[5] = 'images/fr_l_t.GIF'; imgUrl[6] = 'images/fr_m_t.GIF'; imgUrl[7] = 'images/fr_m_t.GIF'; imgUrl[8] = 'images/fr_r_t.GIF'; imgUrl[9] = 'images/fr_l_m.GIF'; imgUrl[10] = 'images/fr_r_m.GIF'; imgUrl[11] = 'images/fr_l_b.GIF'; imgUrl[12] = 'images/fr_m_b_ar.GIF'; imgUrl[13] = 'images/fr_m_b.GIF'; imgUrl[14] = 'images/fr_r_b.GIF'; imgUrl[15] = 'images/eyeB.GIF'; imgUrl[16] = 'images/fr_m_t_ar.GIF'; imgUrl[17] = 'images/iWhat.gif'; imgUrl[18] = 'images/iWhere.gif'; imgUrl[19] = 'images/iCategory.gif'; imgUrl[20] = 'images/WhatHBG.gif'; imgUrl[21] = 'images/WhereHBG.gif'; ; imgUrl[22] = 'images/plus_ico.gif'; imgUrl[23] = 'images/minus_ico.gif';imgUrl[24] = 'images/calendar.gif';
for (var i = 0; i < imgUrl.length; i++) imgPreload.src = imgUrl[i];
</script>
</asp:Content>
<asp:Content ID="cntSearch" ContentPlaceHolderID="ContentBox" runat="server">
<ul id="tabmenu" > 
<li onclick="fSrchB4Leave();fMCH(5)"><a id="tab1">&nbsp;&nbsp;&nbsp;SEARCH&nbsp;&nbsp;&nbsp;</a></li>
<li onclick="fSrchB4Leave();fMCH(6)"><a id="tab2">SEARCH RESULTS</a></li>
<li onclick="fSrchB4Leave();fMCH(7)"><a id="tab3">SEARCH FAQ</a></li>
</ul>
<div id="content1" class="content"><div class="divLoader" id="divLoader1"><img src="images\/loader.GIF" /></div></div>
<div id="content2" class="content" style="display:none"><div class="divLoader" id="divLoader2"><img src="images\/loader.GIF" /></div></div>
<div id="content3" class="content" style="display:none"><div class="divLoader" id="divLoader3"><img src="images\/loader.GIF" /></div></div>
</asp:Content>