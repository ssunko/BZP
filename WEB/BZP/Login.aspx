<%@ Page ErrorPage="BZPError.aspx" Language="C#" MasterPageFile="MPmain.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>
<asp:Content ID="cntLogin" ContentPlaceHolderID="ContentBox" runat="server">
<% if (Session["UName"] == null){ %>
<ul id="tabmenu" >
<li onclick="fMCH(12)"><a id="tab1">&nbsp;&nbsp;&nbsp;LOGIN&nbsp;&nbsp;&nbsp;</a></li>
<li onclick="fMCH(13)"><a id="tab2">REGISTER</a></li>
<li onclick="fMCH(14)"><a id="tab3">RECOVER PASSWORD</a></li>
<li onclick="fMCH(15)"><a id="tab4">LOGIN / REGISTER FAQ</a></li>
</ul>
<% }else{ %>
<ul id="tabmenu" >
<li onclick="fMCH(16)"><a id="tab1">EDIT FROFILE</a></li>
<li onclick="fMCH(17)"><a id="tab2">LOGIN / REGISTER FAQ</a></li>
</ul>
<% } %>
<div id="content1" class="content"><div class="divLoader" id="divLoader1"><img src="images\/loader.GIF" /></div></div>
<div id="content2" class="content" style="display:none"><div class="divLoader" id="divLoader2"><img src="images\/loader.GIF" /></div></div>
<div id="content3" class="content" style="display:none"><div class="divLoader" id="divLoader3"><img src="images\/loader.GIF" /></div></div>
<div id="content4" class="content" style="display:none"><div class="divLoader" id="divLoader4"><img src="images\/loader.GIF" /></div></div>
</asp:Content>
