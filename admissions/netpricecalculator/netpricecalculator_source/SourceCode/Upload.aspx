<%@ Page  MasterPageFile="~/Npc2.master" Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Inovas.NetPrice.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

<script type="text/javascript">
    function closeError() {
        var divErrors = document.getElementById('divErrors');
        if (divErrors)
            divErrors.style.display = 'none';   
    }
</script>

<div style="font-size:14px; font-weight:bold;color:#4D6DA8;">Bulk Data File Upload Tool </div>

<div style="margin-top:15px;">Before using this tool, please refer to the <a href="Download/NetPriceUpload.xlsx">Net Price Calculator Import Specifications</a> for information on setting up your file.</div>
<div style="margin-top:10px;">Once you are ready to upload the file, complete the steps below to configure the Net Price Calculator for the desired institution(s). If any problems are detected, an error report will be displayed on screen (when uploading data for a single institution); or in a separate text file within the downloaded zip file (where data for multiple institutions are being uploaded). </div>


<div style="margin-bottom:5px;margin-top:15px;"><b>Step 1:</b> What is your institution’s predominant calendar system?</div>
<div style="margin-bottom:15px;">
    <asp:RadioButton ID="rbAcademic" runat="server" Text="Academic (semester, quarter, trimester, 4-1-4, or other academic)" GroupName="CalendarSystem" /><br />
    <asp:RadioButton ID="rbProgram" runat="server" Text="Program (differs by program, continuous basis)" GroupName="CalendarSystem" />
</div>

<div style="margin-bottom:5px;margin-top:15px;"><b>Step 2:</b> Click "Browse" and select a .txt file to upload.</div>
<asp:FileUpload ID="fuSource" runat="server" />

<div style="margin-top:15px;margin-bottom:5px;"><b>Step 3:</b> Click the "Upload" button below. Please allow time for the .zip file to be generated.</div>
<asp:Button ID="btnUploadFile" runat="server" Text="Upload" onclick="btnUploadFile_Click" />

<div style="margin-top:20px; font-weight:bold; color:#ff5555;height:20px;">
    <span id="spanValidationResult"><%= _validationResultMessage %></span>    
</div>

<asp:PlaceHolder ID="phErrors" runat="server" Visible="false">    
    <div id="divErrors" style="height:200px;border:1px solid #cccccc; overflow:scroll; background-color:#eeeeee;padding:4px;">
        <div style="border-bottom:1px solid #666666; font-weight:bold;color:#ff5555;height:20px;">
            <div style="float:left;">Errors:</div>
            <div style="float:right;"><a href="javascript:closeError()" style="text-decoration:none;">[x] close</a></div>
        </div>
        <asp:Literal ID="ltErrors" runat="server" Mode="PassThrough" EnableViewState="false"></asp:Literal>
    </div>    
</asp:PlaceHolder>

<script type="text/javascript">
    setTimeout(function () {        
        document.getElementById('spanValidationResult').style.display = 'none';
    }, 4000);
    setTimeout(function () {
        var divError = document.getElementById('divErrors');
        if(divError)
            divError.style.display = 'none';
    }, 60000);
    var frm = document.getElementsByTagName('form')[0];
    if (frm) {
        frm.onsubmit = function () {
            var divError = document.getElementById('divErrors');
            if (divError)
                divError.style.display = 'none';
        }
    }     
</script>
</asp:Content>
