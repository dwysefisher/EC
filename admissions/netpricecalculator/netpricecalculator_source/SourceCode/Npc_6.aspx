<%@ Page Language="C#" MasterPageFile="~/Npc2.master" AutoEventWireup="true" CodeFile="Npc_6.aspx.cs" Inherits="Inovas.NetPrice.Npc_6" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript">
        function ValidatePage() {
            var value = $('#tbPercentage').val();
            if (value.length != 0) {
                if (value.match(/^\d{1,3}$/gi) != null) {
                    return true;
                }
                else {
                    alert('You must enter a numeric value for the percentage of all full-time, first-time degree/certificate-seeking students who received any grants or scholarships from Federal, State, or Local Governments, or the institution (item 1) in order to continue.');
                    $('#tbPercentage').val('');
                    return false;
                }
            }
            else {
                alert('You must enter a numeric value for the percentage of all full-time, first-time degree/certificate-seeking students who received any grants or scholarships from Federal, State, or Local Governments, or the institution (item 1) in order to continue.');
                return false;
            }            
        }
    </script>


    <div style=" margin-bottom:12px;" class="step-title">Step 4: Enter Explanations and Caveats</div>
    <div>The following information will appear at the bottom of the output screen for the calculator:</div>

                  
    <div style="margin-top:15px;"><b>Please note:</b> The estimates above apply to <b>full-time, first-time 
        degree/certificate-seeking undergraduate students only.</b></div>
    <div style="margin-top:12px;">These estimates do not represent a final determination, or actual award, of financial assistance or a final net price; they are only estimates based on cost of attendance and financial aid provided to students in <%= AppContext.YearText %>. Cost of attendance and financial aid availability changes year to year. These estimates shall not be binding on the Secretary of Education, the institution of higher education, or the State.</div>
    <div style="margin-top:12px">Not all students receive grant and scholarship aid. <b>In <%= AppContext.YearText %>, XX% of our full-time students enrolling for college for the first time were awarded grant/scholarship aid</b>. Students may also be eligible for student loans and work-study. Students must complete the Free Application for Federal Student Aid (FAFSA) in order to determine their eligibility for Federal financial aid that includes Federal grant, loan, or work-study assistance. For more information on applying for Federal student aid, go to <a href="http://www.fafsa.ed.gov/" target="_blank">http://www.fafsa.ed.gov/</a>.</div>
                                
    <table border="0" cellpadding="2" cellspacing="0" style="margin-top:20px;">
        <tr>
            <td style="text-align:left; vertical-align:top;width:20px;font-weight:bold;color:#333333;">1.</td>
            <td style="text-align:left; vertical-align:top;color:#333333;">
                <b>Please enter the following information to fill in the XX% above</b>: 
                What percentage of all full-time, first-time degree/certificate-seeking students were awarded any grant or scholarship 
                aid from Federal, State, or Local Governments, or the Institution (exclude those who were only 
                awarded private source grant or scholarship aid)? <input type="text" style="font-size:13px; font-family:Arial; width:50px;" name="tbPercentage" id="tbPercentage" style="width:40px;" maxlength="3" value="<%= AppContext.Percentage == 0 ? "" : AppContext.Percentage.ToString() %>" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left; vertical-align:top;width:20px;font-weight:bold;padding-top:14px;color:#333333;">2.</td>
            <td style="text-align:left; vertical-align:top;padding-top:14px;">
                <span style="color:#333333;font-weight:bold;">Would you like to add explanations to appear on the output screen (e.g., exclusions, URL for your financial aid web site, assumptions regarding room and meal-plan type; range of credits; level of personal expenses; etc.)? Please enter text for institutional explanations below. When you are done, click Continue. </span>
                <div style="margin-top:13px;">
                    <strong>Explanation #1,</strong> which appears at the end of first paragraph on the output screen, is optional but can be used to describe any groups excluded from the calculator (e.g., athletes, students receiving employee tuition remission grants).<br />
                    <textarea id='txt_information1' name="txt_information1" style="width:99%;height:90px;margin-top:7px;font-size:13px; font-family:Arial; color:#333333;"><%= AppContext.Explanation1 %></textarea>
                </div>                            
                <div style="margin-top:7px;">
                    <strong>Explanation #2,</strong> which appears after the URL for the FAFSA site, is optional but can be used to provide 1) instructions on how to access your institution’s financial aid web site, 2) possible advice on how to use this estimate (e.g., particular factors, such as the year of the estimate, to keep in mind when comparing with estimates from other institutions, expenses you include that other institutions may not), or 3) notification of any major planned changes to cost or grants that could affect the validity of these estimates.<br />
                    <textarea id='txt_information2' name="txt_information2" style="width:99%;height:90px;margin-top:7px;font-size:13px; font-family:Arial; color:#333333;"><%= AppContext.Explanation2 %></textarea>
                </div>                            
                <div style="margin-top:7px;">
                    <strong>Explanation #3,</strong> which appears at the very end, is optional but can be used for any other explanations or caveats you want to include, such as explaining parameters underlying cost estimates (e.g., number of credits covered by tuition estimate, room and board plan included in on-campus estimate, fees included in “required fees”, expenses covered in “other expenses” and how this value was calculated); explaining of types of costs not reflected (e.g., differential tuition rates associated with certain academic programs or guaranteed tuition plans);  or explaining types of grants not included, (e.g., private scholarships, employee tuition remission, ROTC scholarships not included); defining financial terms students may not be familiar with; or other caveats.<br />
                    <textarea id='txt_information3' name="txt_information3" style="width:99%;height:90px;margin-top:7px;font-size:13px; font-family:Arial; color:#333333;"><%= AppContext.Explanation3 %></textarea>
                </div>                            
            </td>
        </tr>                
    </table>
                
                
                
         

    <!-- Continue button -->
    <div style="text-align:center; margin-top:18px;margin-bottom:28px;">
        <center>
        <table border="0" cellpadding="7" cellspacing="0">
            <tr>
                <td style="vertical-align:top;"><a href="Npc_5.aspx"><img src="images/button_previous.gif" alt="Previous" title="Previous" style="border-width:0px;" /></a></td>
                <td style="vertical-align:top;">
                    <asp:LinkButton ID="ibtnSubmit" runat="server" OnClientClick="return ValidatePage()" OnClick="ibtnSubmit_Click"><img src="images/button_continue.gif" alt="Continue" title="Continue" style="border-width:0px;" /></asp:LinkButton>
                </td>
            </tr>
        </table>
        </center>                        
    </div>
    
</asp:Content>

