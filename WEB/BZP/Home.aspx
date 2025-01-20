<%@ Page ErrorPage="BZPError.aspx" Language="C#" MasterPageFile="MPmain.master" ViewStateEncryptionMode="Never" EnableEventValidation="false" EnableViewState="False" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>
<asp:Content ID="cntHome" ContentPlaceHolderID="ContentBox" runat="server">

<ul id="tabmenu" > 
<li onclick="fMCH(1)"><a id="tab1">WELCOME TO BZP</a></li> 
<li onclick="fMCH(2)"><a id="tab2">BZP NEWS</a></li>
<li onclick="fMCH(3)"><a id="tab3">SPECIAL DEAL</a></li> 
<li onclick="fMCH(4)"><a id="tab4">GENERAL FAQ</a></li> 
</ul>
<table cellpadding="0" cellspacing="0" class="cssMessage01" title = "Click to hide message." runat="server" id = "tblH_message" onclick="this.style.display='none'">
    <tr style="height:14px;"><td colspan="2" style="text-align:right; background-image:url('images/MessageBG.gif');"><img src='images/CloseR.gif' /></td></tr>
    <tr><td style="width:60px;"><img style="margin-left:10px;" src="images/InfoIconB.GIF" /></td>
    <td><div id = "div_msgText" runat="server"  style="color: #2D2E2C;font: 10pt  Arial;padding-top:4px;">&nbsp;</div></td>
</tr>
</table>
<div id="content1" class="content"><div class="divLoader" id="divLoader1"><img src="images\/loader.GIF" /></div></div>
<div id="content2" class="content" style="display:none"><div class="divLoader" id="divLoader2"><img src="images\/loader.GIF" /></div></div>
<div id="content3" class="content" style="display:none"><div class="divLoader" id="divLoader3"><img src="images\/loader.GIF" /></div></div>
<div id="content4" class="content" style="display:none"><div class="divLoader" id="divLoader4"><img src="images\/loader.GIF" /></div></div>
</asp:Content>