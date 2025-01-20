<%@ Page ErrorPage="BZPError.aspx" Language="C#" MasterPageFile="MPmain.master" EnableViewState="False" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>
<asp:Content ID="cntFAQ" ContentPlaceHolderID="ContentBox" runat="server">

<ul id="tabmenu" > 
<li onclick="fMCH(18)"><a id="tab1">GENERAL FAQ</a></li>
<li onclick="fMCH(19)"><a id="tab2">SEARCH FAQ</a></li>
<li onclick="fMCH(20)"><a id="tab3">ADD AD FAQ</a></li>
<li onclick="fMCH(21)"><a id="tab4">LOGIN / REGISTER FAQ</a></li>
</ul>
<div id="content1" class="content"><div class="divLoader" id="divLoader1"><img src="images\/loader.GIF" /></div></div>
<div id="content2" class="content" style="display:none"><div class="divLoader" id="divLoader2"><img src="images\/loader.GIF" /></div></div>
<div id="content3" class="content" style="display:none"><div class="divLoader" id="divLoader3"><img src="images\/loader.GIF" /></div></div>
<div id="content4" class="content" style="display:none"><div class="divLoader" id="divLoader4"><img src="images\/loader.GIF" /></div></div>
</asp:Content>

