<%@ Page ErrorPage="BZPError.aspx" Language="C#" MasterPageFile="MPmain.master" EnableViewState="False" AutoEventWireup="true" CodeFile="AddAd.aspx.cs" Inherits="AddAd" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>

<asp:Content ID="cntAddAd" ContentPlaceHolderID="ContentBox" runat="server">
<ul id="tabmenu" >
<li onclick="fMCH(<% if (Session["UName"] == null){ %>8<%}else{ %>9<% } %>)"><a id="tab1">ADD FREE AD</a></li>
<li onclick="fMCH(10)"><a id="tab2">EDIT YOUR ADs</a></li>
<li onclick="fMCH(11)"><a id="tab3">ADD AD FAQ</a></li>
</ul>
<div id="content1" class="content"><div class="divLoader" id="divLoader1"><img src="images\/loader.GIF" /></div></div>
<div id="content2" class="content" style="display:none"><div class="divLoader" id="divLoader2"><img src="images\/loader.GIF" /></div></div>
<div id="content3" class="content" style="display:none"><div class="divLoader" id="divLoader3"><img src="images\/loader.GIF" /></div></div>



<table id="tblFiles" style="visibility:hidden; position:absolute;top:-500px;width:480px">
<tr style=" height:28px;"><td colspan="2" style="font: 10pt  Arial; "> Please select up to 4 images:</td></tr>
<tr style="height:24px;" >
<td style="width:200px;"><input type="file" id="file" accept="image/*" class="file" runat="server" onchange="fWriteFileName(this.value,fGetObj('sFile'));" /></td>
<td><div id="sFile" >&nbsp;</div></td>
</tr>
<tr style="height:24px;" >
<td><input type="file" id="file1" accept="image/*" class="file" runat="server" onchange="fWriteFileName(this.value,fGetObj('sFile1'));" /></td>
<td><div id="sFile1" >&nbsp;</div></td>
</tr>
<tr style="height:24px;" >
<td><input type="file" id="file2" accept="image/*" class="file" runat="server" onchange="fWriteFileName(this.value,fGetObj('sFile2'));" /></td>
<td><div id="sFile2" >&nbsp;</div></td>
</tr>
<tr style="height:24px;" >
<td><input type="file" id="file3" accept="image/*" class="file" runat="server" onchange="fWriteFileName(this.value,fGetObj('sFile3'));" /></td>
<td><div id="sFile3" >&nbsp;</div></td>
</tr>
</table>
<input type="hidden" runat="server" id="hTabNo" style="display:none" />
</asp:Content>

