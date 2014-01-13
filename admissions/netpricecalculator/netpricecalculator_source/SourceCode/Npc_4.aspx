<%@ Page Language="C#" MasterPageFile="~/Npc.master" AutoEventWireup="true" CodeFile="Npc_4.aspx.cs" Inherits="Inovas.NetPrice.Npc_4" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {            
            // if user selected 'No' to step 1, disable step 2
            if ($('#rb_q1_y').attr('checked') == true) {
                $('#rb_q2_y').removeAttr('disabled');
                $('#rb_q2_n').removeAttr('disabled');
                $('#divStep2').css('color', '');
            }
            
            // Bind autocomplete to textbox
            // on keyup - search programs
            $('#tbLargestProgram').bind('keyup', SearchProgramName);
            // on key down - navigate result search
            $('#tbLargestProgram').bind('keydown', NavigateResult);            
            // bind event for <body> so we can close autocomplete when user click outside of textbox
            $('body').bind('click', function(e) { ClearAutocomplete(); });
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
        function ContinueClick() {
            if ($('#rb_q1_y').attr('checked') == false && $('#rb_q1_n').attr('checked') == false) {
                alert('Please answer all questions before proceeding.');
                return false;
            }            
            
            if ($('#rb_q1_y').attr('checked') == true) {
                if ($('#rb_q2_y').attr('checked') == false && $('#rb_q2_n').attr('checked') == false) {
                    alert('Please answer all questions before proceeding.');
                    return false;
                }
            }
            
            if($('#tbLargestProgram').val().length == 0) {
                alert('Please enter largest program.');
                return false;
            }
            if($('#tbNumberOfMonths').val().length == 0) {
                alert('Please enter average number of months.');
                return false;
            }
            var reMonths = /^[0-9]{1,2}$/g;
            if($('#tbNumberOfMonths').val().match(reMonths) == null) {
                alert('Number of months should be numeric value.');
                return false;
            }                
            
            return true;
        }
        
        // Variables needed for autocomplete to work
        var arrMatchedPrograms = [];
        var drawProgram = true;
        var selectedMatchedProgramIndex = -1;
        
        // go through list of found matches
        function NavigateResult(e) {
            if(e.keyCode == '40' || e.keyCode == '38') {
                // 40: user pressed arrow down button
                // 38: user pressed arrow up button
                
                // Key Down
                if(e.keyCode == '40') {                    
                    if(selectedMatchedProgramIndex != arrMatchedPrograms.length - 1) {
                        // remove already marked div
                        $('#divMatchedPrograms .divMatchedProgramMarked').removeClass('divMatchedProgramMarked');
                        // mark new div and set focus to hyperlink, so div will scroll to new program
                        $('#divMatchedProgram'+ (selectedMatchedProgramIndex+1)).addClass('divMatchedProgramMarked').find('a').focus();
                        // set focus to textbox
                        $('#tbLargestProgram').focus();
                        ++selectedMatchedProgramIndex;
                    }
                } else if(e.keyCode == '38') {
                    if(selectedMatchedProgramIndex == -1 || selectedMatchedProgramIndex != 0) {
                        $('#divMatchedPrograms .divMatchedProgramMarked').removeClass('divMatchedProgramMarked');
                        $('#divMatchedProgram' + (selectedMatchedProgramIndex-1)).addClass('divMatchedProgramMarked').find('a').focus();
                        $('#tbLargestProgram').focus();
                        --selectedMatchedProgramIndex;
                    }
                }            
            }
            else if(e.keyCode == '13') {
                // User pressed ENTER
                SelectProgram();
            }
            else if(e.keyCode == '27') {
                // on ESC key clear autocomplete div
                ClearAutocomplete();
            }    
        }
        
        // Iterate predefined array to find matched programs
        function SearchProgramName(e)
        {
            if(e.keyCode == '40' || e.keyCode == '38' || e.keyCode == '13' || e.keyCode == '27') 
                return;            
            // Find matches in predefined array
            drawProgram = false;
            arrMatchedPrograms = [];
            selectedMatchedProgramIndex = -1;
            var enteredText = $('#tbLargestProgram').val();
            if(enteredText.length >= 4) {            
                // Iterate predefined array to find matches
                var arrProgramNamesLength = arrProgramNames.length;
                for(var i=0; i< arrProgramNamesLength; i++) {                    
                    if(arrProgramNames[i].toLowerCase().indexOf(enteredText.toLowerCase()) != -1) {
                        arrMatchedPrograms.push(arrProgramNames[i]);
                    }
                }
                if(arrMatchedPrograms.length>0) {
                    drawProgram = true;
                    DrawMatchedPrograms();
                } else {
                    ShowEmptyAutocomplete();
                }
                
            }
            else {
                ClearAutocomplete();
            }            
        }
        
        // Create HTML for found programs
        function DrawMatchedPrograms() {
            var matchedProgramsLength = arrMatchedPrograms.length;
            var sb = new String();
            // regular expression is used to bold search string
            var re = new RegExp('('+$('#tbLargestProgram').val().replace(/\\/g, '\\')+')', 'gi');            
            var arrSubstrings = []; 
            var programNameWithBoldText = '';            
            for(var i=0; i<matchedProgramsLength; i++) {
                if(drawProgram == false) {
                    $('#divMatchedPrograms')[0].innerHTML = '';
                    return;
                }
                try
                {
                    arrSubstrings = re.exec(arrMatchedPrograms[i]);                
                    programNameWithBoldText = arrMatchedPrograms[i].replace(re, '<b>$1</b>');                    
		            sb += "<div class='divMatchedProgramItem' id='divMatchedProgram" + i + "' onclick='SelectProgram()' onmouseover=\"ProgramMouseOver("+i+")\" onmouseout=\"ProgramMouseOut("+i+")\"><a href='javascript:void(0);'>" + programNameWithBoldText+ "<a/></div>";
		        }
		        catch(exception)
		        {
		            // In case of regular expression exception
		            programNameWithBoldText = arrMatchedPrograms[i];
		            sb += "<div class='divMatchedProgramItem' id='divMatchedProgram" + i + "' onclick='SelectProgram()' onmouseover=\"ProgramMouseOver("+i+")\" onmouseout=\"ProgramMouseOut("+i+")\"><a href='javascript:void(0);'>" + programNameWithBoldText+ "<a/></div>";
		        }
            }
            $('#divMatchedPrograms').html(sb);
            $('#divMatchedPrograms').show()[0].scrollTop = 0;
            
        }
        
        function ProgramMouseOver(index) {
            // remove marked row from list
            $('#divMatchedPrograms .divMatchedProgramMarked').removeClass('divMatchedProgramMarked');
            
            selectedMatchedProgramIndex = index;
            $('#divMatchedProgram'+index).addClass('divMatchedProgramMarked');
        }
        function ProgramMouseOut(index) {            
            $('#divMatchedProgram'+index).removeClass('divMatchedProgramMarked');
            selectedMatchedProgramIndex = -1;            
        }
        
        // Function executes when user select program. (Click or pressed Enter)
        function SelectProgram() {
            if(selectedMatchedProgramIndex != -1) {                                        
                // unbind keyup/keydown events so we can replace text in textbox
                $('#tbLargestProgram').unbind('keyup').unbind('keydown').val(arrMatchedPrograms[selectedMatchedProgramIndex]).focus();
                
                // after we updated text, bind events back
                $('#tbLargestProgram').bind('keyup', SearchProgramName).bind('keydown', NavigateResult);
                // empty autocomplete div                     
                ClearAutocomplete();
                
            }
        }
        
        function ClearAutocomplete() {
            $('#divMatchedPrograms').hide()[0].innerHTML = '';
            arrMatchedPrograms = [];
            selectedMatchedProgramIndex=-1;
        }
        function ShowEmptyAutocomplete() {
            $('#divMatchedPrograms').html('<div class=\'matchedProgramsNotFound\'>No matching programs found.</div>');
            arrMatchedPrograms = [];
            selectedMatchedProgramIndex=-1;
        }
        </script>
    
    <div style="margin-bottom:5px;" class="step-title">Step 2: Set up your data entry screens</div>
    <div style="margin-top:9px; margin-bottom:15px; margin-right:12px;">To get started, answer the questions below. Your responses will determine which data elements you are required to provide in the subsequent data input screen.</div>    
    <table border="0" cellpadding="0" cellspacing="0" width="525px" style="table-layout:fixed;">
        <tr>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_left.gif" alt="" title="" /></td>
            <td align="left" valign="top" style="background-image:url(images/npcb_top_center.gif);"><img src="images/blank.gif" /></td>
            <td align="left" valign="top" width="10px"><img src="images/npcb_top_right.gif" alt="" title="" /></td>
        </tr>
        <tr>
            <td align="left" valign="top" style="background:url(images/npcb_middle_left.gif) repeat-y;"></td>
            <td align="left" valign="top" style="padding:6px 10px 20px 8px; background-color:#dfecf1;">
                <!-- dv_npc_s1 -->
                <div id="dv_npc_s1" style="">
                    <!-- STEP 1 -->
                    <div id="divStep1">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align:left; vertical-align:top;width:16px;">1.</td>
                                <td style="text-align:left; vertical-align:top;">
                                    <span style="">Does your institution offer institutionally controlled housing (either on or off campus)?</span>
                                    <div style="margin-top:5px;"><input type="radio" name="rb_q1" id="rb_q1_y" value="y" <%= GetCheckedStatus("rb_q1","y") %> onclick="ChooseStep1()" /><label for="rb_q1_y">Yes</label></div>
                                    <div style="margin-top:2px;"><input type="radio" name="rb_q1" id="rb_q1_n" value="n" <%= GetCheckedStatus("rb_q1","n") %> onclick="ChooseStep1()" /><label for="rb_q1_n">No</label></div>
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
                                    <div style="margin-top:5px;"><input type="radio" name="rb_q2" id="rb_q2_y" value="y" <%= GetCheckedStatus("rb_q2","y") %> disabled="disabled" /><label for="rb_q2_y">Yes</label></div>
                                    <div style="margin-top:2px;"><input type="radio" name="rb_q2" id="rb_q2_n" value="n" <%= GetCheckedStatus("rb_q2","n") %> disabled="disabled" /><label for="rb_q2_n">No</label></div>
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
                                    Please specify the largest program your institution offered to full-time, first-time students during the <%= AppContext.YearText %> data year, and the average number of months it took a full-time student to complete the program. This is the program that will appear on the output screen for the calculator, and upon which all calculations will be based.
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;margin-top:7px;">
                                        <tr>
                                            <td style="background-image:url(images/graybg_topleft.gif); background-repeat:no-repeat;width:10px; height:10px;"></td>
                                            <td style="background-color:#ffffff;height:10px;"></td>
                                            <td style="background-image:url(images/graybg_topright.gif); background-repeat:no-repeat;width:10px; height:10px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#ffffff;width:10px;"></td>
                                            <td style="background-color:#ffffff;padding:0px 5px 5px 5px;">
                                                <!-- Content for gray box with program name -->
                                                <div style="font-size:10px;font-weight:normal;line-height:12px;">Note: The largest program is the program with the most students, <b>not</b> the program with the longest length. </div>
                                                <table border="0" cellpadding="0" cellspacing="0" style="margin-top:10px;width:100%;">
                                                    <tr>
                                                        <td style="text-align:left; vertical-align:top;font-weight:bold;width:190px;font-size:11px;"><label for="tbLargestProgram">Largest Program:</label></td>
                                                        <td style="text-align:left; vertical-align:top;">
                                                            <input type="text" id="tbLargestProgram" name="tbLargestProgram" style="width:220px;" autocomplete="off" value="<%= GetTextboxValue("tbLargestProgram") %>" maxlength="300" />
                                                            <!-- Div with autocomplete-->
                                                            <div style="position:relative;">
                                                                <div id="divMatchedPrograms" style="font-size:11px;"></div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:left; vertical-align:top;font-weight:bold;padding-top:12px;font-size:11px;line-height:14px;"><label for="tbNumberOfMonths">Average number of months it takes a full-time student to complete this program:</label></td>
                                                        <td style="text-align:left; vertical-align:top; padding-top:12px;"><input type="text" id="tbNumberOfMonths" name="tbNumberOfMonths" autocomplete="off"  size="2" maxlength="2" value="<%= GetTextboxValue("tbNumberOfMonths") %>" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="background-color:#ffffff;width:10px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="background-image:url(images/graybg_bottomleft.gif); background-repeat:no-repeat;width:10px; height:10px;"></td>
                                            <td style="background-color:#ffffff;height:10px;"></td>
                                            <td style="background-image:url(images/graybg_bottomright.gif); background-repeat:no-repeat;width:10px; height:10px;"></td>
                                        </tr>
                                    </table>
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
        <!-- Continue button -->
        <div style="text-align:center; margin-top:18px;margin-bottom:28px;">
            <center>
            <table border="0" cellpadding="7" cellspacing="0">
                <tr>
                    <td style="vertical-align:top;"><a href="Npc_2.aspx"><img src="images/button_previous.gif" alt="Previous" title="Previous" style="border-width:0px;" /></a></td>
                    <td style="vertical-align:top;">
                        <asp:LinkButton ID="ibtnSubmit" runat="server" OnClientClick="return ContinueClick()" OnClick="ibtnSubmit_Click">
                            <img src="images/button_continue.gif" alt="Continue" title="Continue" style="border-width:0px;" />
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            </center>                        
        </div>
</asp:Content>

