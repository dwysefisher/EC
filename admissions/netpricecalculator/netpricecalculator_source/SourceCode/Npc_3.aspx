<%@ Page Language="C#" MasterPageFile="~/Npc.master" AutoEventWireup="true" CodeFile="Npc_3.aspx.cs" Inherits="Inovas.NetPrice.Npc_3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {            
            // if user selected 'No' to step 1, disable step 2
            if ($('#rb_q1_y').attr('checked') == true) {
                $('#rb_q2_y').removeAttr('disabled');
                $('#rb_q2_n').removeAttr('disabled');
                $('#divStep2').css('color', '');
            }            
            // check step 2
            if ($('#rb_q3_y').attr('checked') == true) {
                $('#spanStep3_YesOptions').css('color', '');
                $('#cb_q3_id').removeAttr('disabled');
                $('#cb_q3_is').removeAttr('disabled');                
            }
            else if ($('#rb_q3_n').attr('checked') == true) {
                $('#spanStep3_YesOptions').css('color', '#cccccc');
                $('#cb_q3_id').attr('disabled', 'disabled');
                $('#cb_q3_is').attr('disabled', 'disabled');
            }
            ChooseStep3();
        });

        function ChooseStep1()
        {            
            // if user selected 'No' to step 1, disable step 2
            if($('#rb_q1_n').attr('checked') == true) {
                $('#rb_q2_y').attr('disabled', 'disabled').attr('checked', false);
                $('#rb_q2_n').attr('disabled', 'disabled').attr('checked', false);
                $('#divStep2').css('color', '#cccccc');
            }
            else {
                $('#rb_q2_y').removeAttr('disabled');
                $('#rb_q2_n').removeAttr('disabled');
                $('#divStep2').css('color', '');                
            }
        }
        function ChooseStep3()
        {            
            // if user selected 'No' to step 1, disable step 2
            if($('#rb_q3_y').attr('checked') == true) {                
                $('#spanStep3_YesOptions').css('color', '');
                $('#cb_indistrict').removeAttr('disabled');
                $('#cb_instate').removeAttr('disabled');
                $('#cb_outofstate').removeAttr('disabled');
            }
            else {
                $('#spanStep3_YesOptions').css('color', '#cccccc');
                $('#cb_indistrict').attr('disabled', 'disabled').attr('checked',false);
                $('#cb_instate').attr('disabled', 'disabled').attr('checked', false);
                $('#cb_outofstate').attr('disabled', 'disabled').attr('checked', false);
            }
        }
        function ContinueClick() {
            if ($('#rb_q1_y').attr('checked') == false && $('#rb_q1_n').attr('checked') == false) {
                alert('Please answer all questions before proceeding.');
                return false;
            }
            if ($('#rb_q3_y').attr('checked') == false && $('#rb_q3_n').attr('checked') == false) {
                alert('Please answer all questions before proceeding.');
                return false;
            }
            
            if ($('#rb_q1_y').attr('checked') == true) {
                if ($('#rb_q2_y').attr('checked') == false && $('#rb_q2_n').attr('checked') == false) {
                    alert('Please answer question number 2.');
                    return false;
                }
            }

            if ($('#rb_q3_y').attr('checked') == true)
            { 
                var totalsTuitionPlansSelected = 0;           
                if($('#cb_indistrict').attr('checked') == true)
                    totalsTuitionPlansSelected++;
                if($('#cb_instate').attr('checked') == true)
                    totalsTuitionPlansSelected++;
                if($('#cb_outofstate').attr('checked') == true)
                    totalsTuitionPlansSelected++;

                if (totalsTuitionPlansSelected < 2) {
                    alert('You must select at least two different tuition rates in response to Question 3. Otherwise, if your institution charges only one tuition rate, please change your response to this question to No.');
                    return false;
                }
            }            
            return true;
        }
        </script>
    
    <div style="margin-bottom:5px;" class="step-title">Step 2: Set up your data entry screens</div>
    <div style="margin-bottom:15px;">To get started, answer the questions below. Your responses will determine which data elements you are required to provide in the subsequent data input screen.</div>    
    <table border="0" cellpadding="0" cellspacing="0" width="525px" style="table-layout:fixed;">
        <tr>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_left.gif" alt="" title="" /></td>
            <td align="left" valign="top" style="background-image:url(images/npcb_top_center.gif);"><img src="images/blank.gif" /></td>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_right.gif" alt="" title="" /></td>
        </tr>
        <tr>
            <td align="left" valign="top" style="background:url(images/npcb_middle_left.gif) repeat-y;"></td>
            <td align="left" valign="top" style="padding:6px 10px 10px 8px; background-color:#dfecf1;">
                <!-- dv_npc_s1 -->
                <div id="dv_npc_s1">
                    <!-- STEP 1 -->
                    <div id="divStep1">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:left; vertical-align:top;width:16px;">1.</td>
                                <td style="text-align:left; vertical-align:top;">
                                    <span style="">Does your institution offer institutionally controlled housing (either on or off campus)?</span>
                                    <div>
                                        <div style="margin-top:5px;"><input type="radio" name="rb_q1" id="rb_q1_y" value="y" <%= GetCheckedStatus("rb_q1","y") %> onclick="ChooseStep1()" /><label for="rb_q1_y">Yes</label></div>
                                        <div style="margin-top:0px;"><input type="radio" name="rb_q1" id="rb_q1_n" value="n" <%= GetCheckedStatus("rb_q1","n") %> onclick="ChooseStep1()" /><label for="rb_q1_n">No</label></div>
                                    </div>
                                </td>
                            </tr>
                        </table>                                                
                    </div>
                    
                    <!-- STEP 2 -->
                    <div id="divStep2" style="margin-top:15px;color:#cccccc;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:left; vertical-align:top;width:16px;">2.</td>
                                <td style="text-align:left; vertical-align:top;">
                                    <span style="">If yes, are all full-time, first-time degree/certificate seeking students required to live on campus or in institutionally-controlled housing?</span>
                                    <div>
                                        <div style="margin-top:5px;"><input type="radio" name="rb_q2" id="rb_q2_y" value="y" <%= GetCheckedStatus("rb_q2","y") %> disabled="disabled" /><label for="rb_q2_y">Yes</label></div>
                                        <div style="margin-top:0px;"><input type="radio" name="rb_q2" id="rb_q2_n" value="n" <%= GetCheckedStatus("rb_q2","n") %> disabled="disabled" /><label for="rb_q2_n">No</label></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <!-- STEP 3 -->
                    <div id="divStep3" style="margin-top:15px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:left; vertical-align:top;width:16px;">3.</td>
                                <td style="text-align:left; vertical-align:top;">
                                    <span style="">Does your institution charge different tuition for in-district, in-state, or out-of-state students?</span>
                                    <div style="margin-top:5px;">
                                        <input type="radio" name="rb_q3" id="rb_q3_y" value="y" <%= GetCheckedStatus("rb_q3","y") %> onclick="ChooseStep3()" /><label for="rb_q3_y">Yes</label>
                                        <span id="spanStep3_YesOptions" style="color:#cccccc;">&nbsp;</span>                                            
                                        <span style="font-size:10px; font-weight:bold;">&gt;&gt;</span>&nbsp; Check all that apply:                                        
                                            <input type="checkbox" name="cb_indistrict" id="cb_indistrict" value="1" <%= GetCheckedStatus("cb_indistrict","") %> disabled="disabled"  /><label for="cb_indistrict">In-district</label>&nbsp;
                                            <input type="checkbox" name="cb_instate" id="cb_instate" value="1" <%= GetCheckedStatus("cb_instate","") %> disabled="disabled" /><label for="cb_instate">In-state</label>&nbsp;
                                            <input type="checkbox" name="cb_outofstate" id="cb_outofstate" value="1" <%= GetCheckedStatus("cb_outofstate","") %> disabled="disabled" /><label for="cb_outofstate">Out-of-state</label>                                        
                                    </div>
                                    <div style="margin-top:0px;"><input type="radio" name="rb_q3" id="rb_q3_n" value="n" <%= GetCheckedStatus("rb_q3","n") %> onclick="ChooseStep3()" /><label for="rb_q3_n">No</label></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td align="left" valign="top" style="background:url(images/npcb_middle_right.gif) repeat-y;"></td>
        </tr>
        <tr>
            <td align="left" valign="top" width="10px"><img src="images/npcb_bottom_left.gif" alt="" title="" /></td>
            <td align="left" valign="top" style="background-image:url(images/npcb_bottom_center.gif);"><img src="images/blank.gif" /></td>
            <td align="left" valign="top" width="10px"><img src="images/npcb_bottom_right.gif" alt="" title="" /></td>
        </tr>
    </table>

    <div style="text-align:center; margin-top:18px;margin-bottom:28px;">
        <center>
            <table border="0" cellpadding="7" cellspacing="0">
                <tr>
                    <td style="vertical-align:top;"><a href="Npc_2.aspx"><img src="images/button_previous.gif" alt="Previous" title="Previous" style="border-width:0px;" /></a></td>
                    <td style="vertical-align:top;">
                            <asp:LinkButton ID="ibtnSubmit" runat="server" OnClientClick="return ContinueClick()" OnClick="ibtnSubmit_Click">
                            <img src="images/button_continue.gif" border="0" alt="Continue" title="Continue" />
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </center>
    </div>

</asp:Content>

