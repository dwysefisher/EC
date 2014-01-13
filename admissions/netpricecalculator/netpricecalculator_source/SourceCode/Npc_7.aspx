<%@ Page Language="C#" MasterPageFile="~/Npc2.master" AutoEventWireup="true" CodeFile="Npc_7.aspx.cs" Inherits="Inovas.NetPrice.Npc_7" %>
<%@ MasterType VirtualPath="~/Npc2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">    
    <script type="text/javascript">
        var year = '<%= AppContext.YearText %>';
        addEvent = function (domEl, eventName, fn) {
            if (domEl) {
                if (eventName.indexOf('on') == 0)
                    eventName = eventName.substring(2);
                if (domEl.addEventListener) {
                    domEl.addEventListener(eventName, fn, false);
                }
                else if (domEl.attachEvent) {
                    domEl.attachEvent('on' + eventName, fn);
                }
                else {
                    alert('syslib.addEvent: old browser detected');
                    domEl['on' + eventName] = fn;
                }
            }
        }
        function openInstitutionNameDialog() {            
            $('#divGrayOut').css({ "display": "block", opacity: 0.7, "width": $(document).width() + 20, "height": $(document).height() + 270 });
            $('#divInstitutionNameDialog').show();

            var tbWelcomeMessage = document.getElementById('tbWelcomeMessage');
            tbWelcomeMessage.disabled = true;
            tbWelcomeMessage.value = 'Welcome to College ABC\'s net price calculator. Begin by reading and agreeing to the statement below. Then follow the instructions on the subsequent screens to receive an estimate of how much students similar to you paid to attend College ABC in Year.';

            var tbInstName = document.getElementById('tbInstName');
            tbInstName.disabled = true;
            tbInstName.value = '';
        }

        function closeInstitutionNameDialog() {
            $('#divGrayOut').css({ "display": "none" });
            $('#divInstitutionNameDialog').hide();           

            var lbtnDownloadWithWelcomeMessage = document.getElementById('<%= lbtnDownloadWithWelcomeMessage.ClientID %>');
            lbtnDownloadWithWelcomeMessage.style.display = 'none';

            var imgButtonDownloadGray = document.getElementById('imgButtonDownloadGray');
            imgButtonDownloadGray.style.display = '';

            var divInputControls = document.getElementById('divInputControls');
            divInputControls.style.opacity = '0.5';
        }

        function checkInstitutionName() {
            var tbInstName = document.getElementById('tbInstName');
            var instName = tbInstName.value;
            if (instName.length != 0) {
                closeInstitutionNameDialog();
            }
            else {
                alert('You must enter an Institution Name to include a welcome message with your Net Price Calculator. Otherwise, if you do not wish to include a welcome message, please change your response to No.');
                return false;
            }
        }

        function enableInputControls() {
            var tbWelcomeMessage = document.getElementById('tbWelcomeMessage');
            tbWelcomeMessage.disabled = false;
            tbWelcomeMessage.value = 'Welcome to College ABC\'s net price calculator. Begin by reading and agreeing to the statement below. Then follow the instructions on the subsequent screens to receive an estimate of how much students similar to you paid to attend College ABC in Year.';

            var tbInstName = document.getElementById('tbInstName');
            tbInstName.disabled = false;
            tbInstName.value = '';

            var lbtnDownloadWithWelcomeMessage = document.getElementById('<%= lbtnDownloadWithWelcomeMessage.ClientID %>');
            lbtnDownloadWithWelcomeMessage.style.display = '';

            var imgButtonDownloadGray = document.getElementById('imgButtonDownloadGray');
            imgButtonDownloadGray.style.display = 'none';

            var divInputControls = document.getElementById('divInputControls');
            divInputControls.style.opacity = '1';

        }

        function keyUpEvent(ev) {
            var template = 'Welcome to ##instnm##\'s net price calculator. Begin by reading and agreeing to the statement below. Then follow the instructions on the subsequent screens to receive an estimate of how much students similar to you paid to attend ##instnm## in ##year##.';
            ev = ev || window.event;
            var target;
            if (ev.target)
                target = ev.target;
            else
                target = ev.srcElement;

            var tbWelcomeMessage = document.getElementById('tbWelcomeMessage');
            if (target.value.length > 0) {
                var tmpMessage = template.replace(/##instnm##/g, target.value);
                tmpMessage = tmpMessage.replace(/##year##/g, year);
                tbWelcomeMessage.value = tmpMessage;
            }
            else {
                tbWelcomeMessage.value = '';
            }
        }

        $(document).ready(function () {
            addEvent(document.getElementById('tbInstName'), 'keyup', keyUpEvent);

            var lbtnDownloadWithWelcomeMessage = document.getElementById('<%= lbtnDownloadWithWelcomeMessage.ClientID %>');
            lbtnDownloadWithWelcomeMessage.style.display = 'none';

            var imgButtonDownloadGray = document.getElementById('imgButtonDownloadGray');
            imgButtonDownloadGray.style.display = '';
        });
                
    </script>

    


    <div style="margin-bottom:12px;" class="step-title">Step 5: Create Calculator </div>
    <div style="margin-bottom:19px;">You have completed the data entry for the Net Price Calculator. Please review the information you have provided. You can click Modify to return to Step 1 and edit this information, or if you are happy with the current selections, click Continue to produce a zip file containing the files necessary to run the Net Price Calculator on your institution’s website. (For detailed instructions on downloading, extracting, and hosting the application on your institution’s website, please refer to the Quick Start Guide included with this template.) </div>
    <!-- Summary table 1 -->  
    <asp:Table ID="tblSummary" runat="server" BorderWidth="0px" CellPadding="2" CellSpacing="0" GridLines="None" Width="100%"></asp:Table>
    
    <!--POA table-->
    <div style="font-weight:bold;margin-top:25px;margin-bottom:7px;">Table 1: Cost of Attendance for Full-time, First-time Undergraduate Students: <%= AppContext.YearText %> </div>
    <asp:Table ID="tblPOA" runat="server" CssClass="poa-table" CellPadding="0" CellSpacing="0" Width="100%">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" CssClass="poa-header">Tuition and fees</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;Amount</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><asp:Label runat="server" ID="lblPOA_t_amount" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;In-district</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><asp:Label runat="server" ID="lblPOA_t_id" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;In-state</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><asp:Label runat="server" ID="lblPOA_t_is" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;Out-of-state</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><asp:Label runat="server" ID="lblPOA_t_oos" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-header">Books and supplies</asp:TableCell>
            <asp:TableCell ColumnSpan="3" CssClass="poa-header" style="font-weight:normal;color:inherit;"><asp:Label runat="server" ID="lblPOA_bas" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-header">Living and other expenses</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center" Width="110px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center" Width="200px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-child-elements">&nbsp;&nbsp;Room and board</asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><asp:Label runat="server" ID="lblPOA_rb_oncampus"  /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><asp:Label runat="server" ID="lblPOA_rb_offcampus"  /></asp:TableCell>            
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center">N/A</asp:TableCell>
        </asp:TableRow>    
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-child-elements"><div style="margin-left:6px;">Other (personal, transportation, etc.)</div></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><asp:Label runat="server" ID="lblPOA_other_oncampus" /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><asp:Label runat="server" ID="lblPOA_other_offcampus" /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><asp:Label runat="server" ID="lblPOA_other_offcampuswf" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br /><br />
    <asp:Table ID="tblPOACalc" runat="server" CssClass="poa-table" CellPadding="0" CellSpacing="0" Width="460px">
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-header" Width="350px">In-district</asp:TableCell>
            <asp:TableCell CssClass="poa-header">&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblId_Oc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblId_Offc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblId_Offcwf" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-header" Width="350px">In-state</asp:TableCell>
            <asp:TableCell CssClass="poa-header">&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblIs_Oc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblIs_Offc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblIs_Offcwf" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-header" Width="350px">Out-of-state</asp:TableCell>
            <asp:TableCell CssClass="poa-header">&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblOos_Oc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblOos_Offc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblOos_Offcwf" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-header" Width="350px">Amount</asp:TableCell>
            <asp:TableCell CssClass="poa-header">&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblAm_Oc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblAm_Offc" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell CssClass="poa-padding-left poa-child-elements" Width="350px">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
            <asp:TableCell CssClass="poa-padding-left poa-child-elements"><asp:Label ID="lblAm_Offcwf" runat="server" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>


    
    <!--TGA table-->
    <div style="font-weight:bold;margin-top:32px;margin-bottom:7px;">Table 2: Grants and Scholarships for Full-time, First-time Undergraduate Students: <%= AppContext.YearText %> </div>
    <asp:Table ID="tblTGA" runat="server" CssClass="mainTable" CellPadding="0" CellSpacing="0" Width="100%"></asp:Table>
    <div style="font-weight:bold;font-size:10px;margin-top:5px;color:#666666;">‡ Indicates that the number shown was generated/inserted by the system.</div>
           
    
    
    <!-- Summary table 2 -->    
    <div style="margin-top:35px;">
        <asp:Table ID="tblSummary2" runat="server" BorderWidth="0px" CellPadding="2" CellSpacing="0" GridLines="none" Width="100%"></asp:Table>
    </div>
       
    
    
    <div id="divInstitutionNameDialog" style="z-index:500;position: absolute; display:none;">
        <div style="position:relative; top:-400px; left:80px; ">
            <img src="images/instnm_dialog_top.png" alt="" style="_margin-bottom:-3px;*margin-bottom:-3px;" />
            <div style="background-image:url(images/instnm_dialog_middle.png); background-repeat:repeat-y; height:300px; width:634px;">
                <div style="padding:30px 10px 0px 30px;">
                    <center><b>Would you like to include a welcome message about the calculator?</b></center>
                    <div style="text-align:center; margin-top:10px;">
                        <img src="images/button_yes.gif" alt="Yes" style="cursor:pointer;" onclick="enableInputControls()" />
                        &nbsp;
                        <asp:LinkButton ID="ibtnSubmit" runat="server" OnClick="ibtnSubmitNo_Click" OnClientClick="closeInstitutionNameDialog()"><img src="images/button_no.gif" alt="No" title="No" style="border-width:0px;" /></asp:LinkButton>
                    </div>

                    <div id="divInputControls" style="margin-top:20px;opacity:0.5;">
                        <div style=" margin-left:5px; margin-right:30px;">
                            <div style="font-size:11px; font-weight:bold;">Institution Name:</div>
                            <input type="text" name="tbInstName" id="tbInstName" autocomplete="off" style="width:560px; font-family:Arial; font-size:13px;" disabled="disabled" />
                        </div>
                        <div style=" margin-left:5px; margin-right:30px;">
                            <textarea id="tbWelcomeMessage" name="tbWelcomeMessage" style="width:560px; height:100px;font-family:Arial; font-size:13px; margin-top:8px;" disabled="disabled">Welcome to the College ABC's net price calculator. Begin by reading and agreeing to the statement below. Then follow the instructions on the subsequent screens to receive an estimate of how much students similar to you paid to attend College ABC in Year.</textarea>
                        </div>

                        <div style="margin-top:10px; text-align:center;">
                            <img id="imgButtonDownloadGray" src="images/button_download_gray.gif" alt="" title="" />
                            <asp:LinkButton ID="lbtnDownloadWithWelcomeMessage" runat="server" OnClick="ibtnSubmitYes_Click" OnClientClick="return checkInstitutionName()"><img src="images/button_download.gif" alt="Download" title="Download" style="border-width:0px;" /></asp:LinkButton>                            
                        </div>
                    </div>
                </div>        
            </div>
            <img src="images/instnm_dialog_bottom.png" alt="" />
        </div>
    </div>
   
   
    <!-- Continue button -->
    <div style="text-align:center; margin-top:18px;margin-bottom:28px;">
        <center>
        <table border="0" cellpadding="7" cellspacing="0">
            <tr>
                <td style="vertical-align:top;"><a href="Npc_2.aspx"><img src="images/button_modify.gif" alt="Modify" title="Modify" style="border-width:0px;" /></a></td>
                <td style="vertical-align:top;">
                    <img src="images/button_continue.gif" id="btnContinue" alt="Continue" title="Continue" onclick="openInstitutionNameDialog()" style="cursor:pointer;" />
                    
                </td>
            </tr>
        </table>
        </center>                        
    </div>  
    
    
    
   
</asp:Content>

