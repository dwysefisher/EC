<%@ Page Language="C#" MasterPageFile="~/Npc.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Inovas.NetPrice.Default" %>
<%@ MasterType VirtualPath="~/Npc.master" %>
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
            }
            else {
                $('#spanStep3_YesOptions').css('color', '#cccccc');
                $('#cb_indistrict').attr('disabled', 'disabled').attr('checked',false);
                $('#cb_instate').attr('disabled', 'disabled').attr('checked', false);
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
                
            if ($('#rb_q3_y').attr('checked') == true && $('#cb_indistrict').attr('checked') == false && $('#cb_instate').attr('checked') == false) {
                alert('Please select one or more tuition plans.');
                return false;
            }
            
            return true;
        }
    </script>    


    <div style="background-image:url(images/defaultpage_bg.gif); background-repeat:no-repeat; width:521px; height:307px;">
        <div style="padding: 39px 25px 20px 35px;">
            <b>Please read.</b> Welcome to the Net Price Calculator application. 
            This application will assist you in setting up a Net Price Calculator to post on your institution’s website 
            as required in the Higher Education Opportunity Act of 2008 (see HEOA Sec. 111 which amended HEA Title I, 
            Part C: added HEA Sec. 132(a), Sec. 132(h)  (20 U.S.C. 1015a(a), 20 U.S.C. 1015a(h))). 
            <!--<span>Click Continue to begin.</span> -->
            <div style="margin-top:12px;">
                Before proceeding, please download and review the Quick Start Guide and accompanying glossary of key terms (accessible by clicking on the <b>Help</b> button in the upper-right hand corner of the screen) for assistance with correctly inputting data and setting up your institution’s net price calculator. Once you are ready, click <b>Continue</b> to begin.
            </div>
            <div style="text-align:center;margin-top:15px;">
                <a href="Npc_2.aspx"><img src="images/defaultpage_continue_button.gif" alt="Continue" style="border-width:0px;"  title="Continue" /></a>
            </div>
        </div>
    </div>    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" Runat="Server">
<div style="margin-top:33px;margin-bottom:30px; color:#666666;font-weight:normal;font-size:11px; line-height:15px; margin-left:18px; margin-right:13px;">
<strong>Note:</strong> The Higher Education Opportunity Act defines net price as the net price for full-time, first-time 
degree/certificate-seeking students. Title IV institutions that do not enroll full-time, first-time students are not required to have a net price calculator under the HEOA. 
<br /><br />


Additional resources, such as a bulk data file upload tool and frequently asked questions related to both the net price calculator requirement and the Department’s template are also available online at: <a href="http://www.nces.ed.gov/ipeds/resource/net_price_calculator.asp" target="_blank">http://www.nces.ed.gov/ipeds/resource/net_price_calculator.asp</a>. 
</div>
</asp:Content>

