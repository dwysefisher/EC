<%@ Page Language="C#" MasterPageFile="~/Npc.master" AutoEventWireup="true" CodeFile="Npc_2.aspx.cs" Inherits="Inovas.NetPrice.Npc_2" %>
<%@ MasterType VirtualPath="~/Npc.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function ValidateForm() {
            if($('#divYearRadioButtonControls input:checked').length != 1) {
                alert('Please select year.');
                return false;
            }
            if($('#divInstitutionTypeRadioButtonControls input:checked').length != 1) {
                alert('Please select institution\'s predominant calendar system.');
                return false;
            }
            return true;
        }
    </script>
    
    <div style="margin-bottom:5px;" class="step-title">Step 1: Determine representative year and calendar system</div>
    <div style="margin-top:9px; margin-bottom:15px; margin-right:12px;">HEOA requires that your institution use the data from the <b>most recent year available</b> when setting up this calculator. Data should be provided for <b>full-time, first-time 
        degree/certificate-seeking undergraduate students</b>. Cost and aid data must be for the same year – e.g., the most recent year for which data are available for both.</div>
   
    <table border="0" cellpadding="0" cellspacing="0" width="525px" style="table-layout:fixed;">
        <tr>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_left.gif" alt="" title="" /></td>
            <td align="left" valign="top" style="background-image:url(images/npcb_top_center.gif);"><img src="images/blank.gif" /></td>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_right.gif" alt="" title="" /></td>
        </tr>
        <tr>
            <td align="left" valign="top" style="background:url(images/npcb_middle_left.gif) repeat-y;"></td>
            <td align="left" valign="top" style="padding:6px 10px 20px 8px; background-color:#dfecf1;">
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="text-align:left; vertical-align:top; width:16px;">1.</td>
                        <td>
                            <div style="margin-bottom:5px;">The data you are entering are representative of the year:</div>
                            <!-- This div is used by jQuery to check if one radio button is selected -->
                            <div id="divYearRadioButtonControls">
                                <asp:Repeater ID="repYearList" runat="server">                                    
                                    <ItemTemplate>
                                        <div style="margin-bottom:2px;">
                                            <input type="radio" id="rbYear<%# Eval("YearIndex") %>" name="rbYearIndex" value="<%# Eval("YearIndex") %>|<%# Eval("YearText") %>" <%# CheckYear(Eval("YearIndex")) %> class="radiobutton" /><label for="rbYear<%# Eval("YearIndex") %>"><%# Eval("YearText") %></label><br />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top; width:16px;">2.</td>
                        <td>
                            <div style="margin-bottom:5px;">What is your institution’s predominant calendar system?</div>
                            <!-- This div is used by jQuery to check if one radio button is selected -->
                            <div id="divInstitutionTypeRadioButtonControls">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:23px; text-align:center; vertical-align:top;"><input type="radio" id="rbAcademic" name="rbInstitutionType" value="1" <%= CheckInstitutionType("1")  %> class="radiobutton" /></td>
                                        <td style="text-align:left; vertical-align:top;"><label for="rbAcademic">Academic (semester, quarter, trimester, 4-1-4, or other academic)</label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:23px; text-align:center; vertical-align:top; padding-top:2px;"><input type="radio" id="rbProgram" name="rbInstitutionType" value="2" <%= CheckInstitutionType("2")  %> class="radiobutton" /></td>
                                        <td style="text-align:left; vertical-align:top; padding-top:2px;"><label for="rbProgram">Program (differs by program, continuous basis)</label></td>
                                    </tr>
                                </table>             
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="top" style="background:url(images/npcb_middle_right.gif) repeat-y;"></td>
        </tr>
        <tr>
            <td align="left" valign="top" width="10px"><img src="images/npcb_bottom_left.gif" alt="" title="" /></td>
            <td align="left" valign="top" style="background-image:url(images/npcb_bottom_center.gif);"><img src="images/blank.gif" /></td>
            <td align="left" valign="top" width="10px"><img src="images/npcb_bottom_right.gif" alt="" title="" /></td>
        </tr>
    </table>

    <div style="text-align:center;margin-top:18px;margin-bottom:28px;">
        <center>
            <table border="0" cellpadding="7" cellspacing="0">
                <tr>
                    <td style="vertical-align:top;"><a href="Default.aspx"><img src="images/button_previous.gif" alt="Previous" title="Previous" style="border-width:0px;" /></a></td>
                    <td style="vertical-align:top;"><asp:ImageButton ID="ibtnContinue" runat="server" alt="Continue" title="Continue" ImageUrl="~/images/button_continue.gif" OnClick="ibtnContinue_Click" OnClientClick="return ValidateForm();" /></td>
                </tr>
            </table>
        </center>                    
    </div>

</asp:Content>

