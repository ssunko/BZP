<%@ Page Language="C#" MasterPageFile="MPmain.master" ViewStateEncryptionMode="Never" EnableEventValidation="false" EnableViewState="False" AutoEventWireup="true"  CodeFile="BZPError.aspx.cs" Inherits="BZPError" %>
<%@ OutputCache  NoStore="false"  Duration="1" VaryByParam="none" %>
<asp:Content ID="cntError" ContentPlaceHolderID="ContentBox" runat="server">
<div id="content" class="content" style="border-top: 1px solid #0595DB;margin: 26px 0px 0px 0px;">
<table cellpadding="0" cellspacing="0" style="background-color:#F4F9FD;height:340px;width:880px;background-position:20px 36px;background-image:url('images/error.png');background-repeat:no-repeat;border:solid 1px #CDE1F9;">
<tr>
<td id="BGholder" style="height:340px;background-image:url('images/Clouds.png');background-repeat:no-repeat;background-position:0px 145px;"><div runat="server" id="divError" style="height:180px;color:#664b8e;margin-left:270px;margin-top:140px;font: 9pt  Verdana;"></div>
<div style="margin-left:790px;">
<span id="spnStopM" class="spanHlp" onclick="fStartStopBG(false);">stop ...</span>
<span id="spnStartM" class="spanHlp" onclick="fStartStopBG(true);" style="display:none;">start ...</span>
</div>
</td>
</tr>
</table>
</div>
</asp:Content>
