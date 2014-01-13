<%@ Page Language="C#" MasterPageFile="~/Npc2.master" AutoEventWireup="true" CodeFile="Npc_5.aspx.cs" Inherits="Inovas.NetPrice.Npc_5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<script type="text/javascript">
    $(document).ready(function() 
    {
        if (typeof (numberOfTableColumns) != 'undefined') {
            for (var i = 1; i <= numberOfTableColumns; i++) {
                var nn = i + 1;
                CalculateTotal('#txt_t1_2_' + nn);
            }
        }
    });
    
    function ValidateTGATable() {
        var returnValue = true;
        if(typeof(numberOfTableColumns) != 'undefined') {
            var tmpNumOfCols = numberOfTableColumns+1;
            for(var rowIndex=2; rowIndex<=tmpNumOfCols; rowIndex++) {               
                var numOfEnteredValues = 0;
                for(var cellIndex=1; cellIndex<=11; cellIndex++) {
                    var value = $('#txt_t2_'+cellIndex+'_'+rowIndex).val();
                    if(value.length > 0 && !isNaN(value)) {
                        numOfEnteredValues = numOfEnteredValues + 1;
                    }
                }
                
                if(numOfEnteredValues < 2) {
                    returnValue = false;
                    alert('Data must be entered in at least two cells per column. This does not include the >$40,000 or Non-FAFSA filers/unknown EFC rows.');
                    break;   
                }
            }
        }
        
        return returnValue;
    }

    // Validate textbox value
    function ValidateInput(ptr) {
        if (ptr) {
            var val = ptr.value;
            val = val.replace(/\$|\,/g, '');
            
            var indexOfPosition = val.indexOf('.');
            if(indexOfPosition != -1) {
                val = val.substring(0, indexOfPosition);
            }

            if (isNaN(val)) {
                alert('Please enter a valid, non-negative numeric value.');
                ptr.value = '0';
            } else {
                if((val*1) >= 0) { ptr.value = val; }
                else { 
                    alert('Please enter non-negative number.');
                    ptr.value = '0';
                }
            }
        }
    }
    
    // Function calculate total for row
    function CalculateTotal(ptr) {
        if (ptr) {
            var id = $(ptr).attr("id");
            if (id) {
                var s = id.split('_');
                var totalValue = 0;
                
                for(var rowIndex=2; rowIndex<=5; rowIndex++) {                    
                    totalValue += GetTextboxValue('#txt_t1_'+rowIndex+'_'+s[3]);
                }                
                $('#txt_t1_1_'+s[3]).val(totalValue);
            }
        }
    }
    
    // Function get textbox value, of zero if value is invalid or empty
    function GetTextboxValue(textboxId) {
        var returnResult = 0;
        if($(textboxId).length == 1) {
            var value = $(textboxId).val();
            value = value.replace(/\$|\,/g, '');
            if(value.length > 0 && !isNaN(value))
                returnResult = value;
            else
                returnResult = 0;
        }        
        return returnResult*1;

    }
    
</script>


    <div style="margin-bottom:12px;" class="step-title">Step 3: Enter your institution's data</div>        
    <div id="divAcademicStepDescription" runat="server" visible="false" style="margin-bottom:10px;">For the tables below, please provide data for <b>full-time, first-time 
        degree/certificate-seeking undergraduate students</b>. Data for both tables should be for the <b>same year</b>.  Please fill-in each table as completely as possible.  These tables will be used to look up the appropriate cost of attendance and grant aid data for users of the calculator based on information they enter. When you have completed the data entry for both tables below, click <b>Continue</b>.</div>
    <div id="divProgramStepDescription" runat="server" visible="false" style="margin-bottom:10px;">For the tables below, please provide data for <b>full-time, first-time 
        degree/certificate-seeking undergraduate students</b> as applicable for the entire length of the <b>largest program</b> offered for entering students during the relevant data year. Data for both tables should be for the <b>same year</b>.  Please fill-in each table as completely as possible.  These tables will be used to look up the appropriate cost of attendance and grant aid data for users of the calculator based on information they enter. When you have completed the data entry for both tables below, click <b>Continue</b>.</div>
    
    <!-- Div with program name and number of months -->
    <div id="divProgramNameAndMonths" runat="server" visible="false" style="margin-top:15px;color:#cccccc;">
        <div style="font-weight:bold;">Largest program: &nbsp; <input type="text" readonly="readonly" disabled="disabled" value="<%= AppContext.LargestProgram %>" style="width:450px;color:#999999;font-weight:bold;" /></div>
        <div style="font-weight:bold;margin-top:6px;">Average number of months it takes a full-time students to complete this program: &nbsp; <input type="text" readonly="readonly" disabled="disabled" value="<%= AppContext.NumberOfMonths.ToString() %>" style="width:25px;color:#999999;font-weight:bold;" /></div>
    </div>
    
    
    
    <!--POA table-->
    <div style="font-weight:bold;margin-top:28px;margin-bottom:5px; font-size:13px;">Table 1: Cost of Attendance for Full-time, First-time Undergraduate Students: <%= AppContext.YearText %> </div>
    <div>Enter the amounts requested below. Estimates of expense for room and board, books and supplies, and other expenses should be those from the Cost of Attendance report used by your financial aid office for determining financial need. (Note: These estimates are reported by your institution in the Integrated Postsecondary Education Data System (IPEDS) on the Institutional Characteristics survey form – Part D- Student Charges- Price of Attendance). Estimated costs must be provided for each expense type even if those costs are not charged by the institution (e.g., off-campus not with family room and board, etc.).</div>
    <div style="font-weight:bold;font-size:10px;margin-top:10px;color:#4f4d34;">Report in whole dollars only</div>
    <asp:Table ID="tblPOA" runat="server" Visible="false" CssClass="mainTable" CellPadding="0" CellSpacing="0" Width="100%"></asp:Table>    
    <asp:Table ID="tblPOA2" runat="server" CssClass="poa-table" CellPadding="0" CellSpacing="0" Width="100%">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" CssClass="poa-header">Tuition and fees</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;Amount</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><input type="text" name="tbPOA_t_amount" id="tbPOA_t_amount" value="<%= AppContext.TuitionAmount %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;In-district</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><input type="text" name="tbPOA_t_id" id="tbPOA_t_id" value="<%= AppContext.TuitionInDistrinct %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;In-state</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><input type="text" name="tbPOA_t_is" id="tbPOA_t_is" value="<%= AppContext.TuitionInState %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-padding-left poa-child-elements">&nbsp;&nbsp;Out-of-state</asp:TableCell>
            <asp:TableCell ColumnSpan="3"><input type="text" name="tbPOA_t_oos" id="tbPOA_t_oos" value="<%= AppContext.TuitionOutOfState %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-header">Books and supplies</asp:TableCell>
            <asp:TableCell ColumnSpan="3" CssClass="poa-header"><input type="text" name="tbPOA_bas" id="tbPOA_bas" value="<%= AppContext.BooksAndSupplies %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-header">Living and other expenses</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center" Width="110px">On-campus</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center" Width="200px">Off-campus not with family (e.g., with roommates)</asp:TableCell>
            <asp:TableCell CssClass="poa-subheader poa-left-border" HorizontalAlign="Center">Off-campus with family (e.g., no rent paid directly by student)</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-child-elements">&nbsp;&nbsp;Room and board</asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><input type="text" name="tbPOA_rb_oncampus" id="tbPOA_rb_oncampus" value="<%= AppContext.LivingOnCampus %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><input type="text" name="tbPOA_rb_offcampus" id="tbPOA_rb_offcampus" value="<%= AppContext.LivingOffCampus %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>            
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center">N/A</asp:TableCell>
        </asp:TableRow>    
        <asp:TableRow>
            <asp:TableCell Width="220px" CssClass="poa-child-elements"><div style="margin-left:6px;">Other (personal, transportation, etc.)</div></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><input type="text" name="tbPOA_other_oncampus" id="tbPOA_other_oncampus" value="<%= AppContext.OtherOnCampus %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><input type="text" name="tbPOA_other_offcampus" id="tbPOA_other_offcampus" value="<%= AppContext.OtherOffCampus %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
            <asp:TableCell CssClass="poa-left-border" HorizontalAlign="Center"><input type="text" name="tbPOA_other_offcampuswf" id="tbPOA_other_offcampuswf" value="<%= AppContext.OtherOffCampusWithFamily %>" style="width:60px;" maxlength="6" onblur="ValidateInput(this)" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    
    <!--TGA table-->
    <div style="font-weight:bold;margin-top:28px;margin-bottom:5px; font-size:13px;">Table 2: Grants and Scholarships for Full-time, First-time Undergraduate Students: <%= AppContext.YearText %> </div>
    <div>Enter the amounts requested below. Data should represent the <b>median</b> amount of both need and non-need grant and scholarship aid <b>from Federal, State, or Local Governments, or the Institution (exclude private source grant or scholarship aid and work-study programs)</b> awarded to <b>full-time, first-time
        degree/certificate-seeking students</b> with the indicated living and residency category for each Expected Family Contribution (EFC) range. If you have fewer than three (3) observations for a cell, leave the cell blank. The system will calculate and insert the average of the surrounding cells. In the bottom row of the table, report the median amount of grant or scholarship aid for students for whom you do not know an EFC (e.g., they did not file a FAFSA or apply for need-based financial aid).</div>
    <div style="font-weight:bold;font-size:10px;margin-top:10px;color:#4f4d34;">Report in whole dollars only</div>
    <asp:Table ID="tblTGA" runat="server" CssClass="mainTable" CellPadding="0" CellSpacing="0" Width="100%"></asp:Table>
    
    
    <!-- Continue button -->
    <div style="text-align:center; margin-top:18px;margin-bottom:28px;">
        <center>
        <table border="0" cellpadding="7" cellspacing="0">
            <tr>
                <td style="vertical-align:top;"><a href="<%= GetBackButtonUrl() %>"><img src="images/button_previous.gif" alt="Previous" title="Previous" style="border-width:0px;" /></a></td>
                <td style="vertical-align:top;">
                    <asp:LinkButton ID="ibtnSubmit" runat="server" OnClick="ibtnSubmit_Click" OnClientClick="return ValidateTGATable()"><img src="images/button_continue.gif" alt="Continue" title="Continue" style="border-width:0px;" /></asp:LinkButton>
                </td>
            </tr>
        </table>
        </center>                        
    </div>
</asp:Content>

