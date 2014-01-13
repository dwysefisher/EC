using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Inovas.Common.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Inovas.NetPrice
{
    public partial class Npc_7 : PageBase
    {
        /* On-campus, Off-campus with family, Off-campus not with family */
        private int _numberOfResidencyLivingColumns = 0;
        private int _numberOfLivingStatusColumns = 0;

        private Dictionary<string, KeyValue> dictPoaValues = new Dictionary<string, KeyValue>();
        private Dictionary<string, KeyValue> dictTgaValues = new Dictionary<string, KeyValue>();

       


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContext();
            _numberOfResidencyLivingColumns = NetPriceUtils.GetNumberOfResidencyLivingColumns(AppContext);
            _numberOfLivingStatusColumns = NetPriceUtils.GetNumberOfLivingStatusColumns(AppContext);
            Master.TitleWithInstitutionTypeAndYearVisible = false;

            if (!IsPostBack)
            {
                ConvertAppContextArrayIntoDisctionary();
                BuildSummaryTable();                
                DrawPoa();
                DrawTGA();
                RemoveSomeBorderColors();
            }
            
        }



        /* ************************************** */
        /* Code related to building summary table */
        /* ************************************** */

        private void BuildSummaryTable()
        {
            AddSummaryTableRow(tblSummary, "Representative Year", AppContext.YearText);
            AddSummaryTableRow(tblSummary, "Predominant Calendar System", AppContext.InstitutionType.ToString());

            if (AppContext.InstitutionallyControlledHousingOffered.HasValue)
            {
                AddSummaryTableRow(tblSummary, "Institutionally Controlled Housing", BoolToString(AppContext.InstitutionallyControlledHousingOffered));
                if (AppContext.StudentsRequiredLiveOnCampusOrHousing.HasValue)
                {                    

                    AddSummaryTableRow(tblSummary, "Housing Restrictions for Full-Time, First-Time Undergraduates", BoolToString(AppContext.StudentsRequiredLiveOnCampusOrHousing));

                }
            }
            if (AppContext.InstitutionChargeDifferentTuition.HasValue)
            {                
                string inDistrictInStateText = "&nbsp;&nbsp;(";
                bool addComma = false;
                bool chargeForInDistrictOrInStateExists=false;
                if (AppContext.ChargeForInDistrict.HasValue && AppContext.ChargeForInDistrict.Value == true)
                {
                    inDistrictInStateText += "In-district";
                    addComma = true;
                    chargeForInDistrictOrInStateExists=true;
                }
                if (AppContext.ChargeForInState.HasValue && AppContext.ChargeForInState.Value == true)
                {
                    if (addComma)
                        inDistrictInStateText += ", ";
                    inDistrictInStateText += "In-state";
                    chargeForInDistrictOrInStateExists=true;
                    addComma = true;
                }
                if (AppContext.ChargeForOutOfState.HasValue && AppContext.ChargeForOutOfState.Value == true)
                {
                    if (addComma)
                        inDistrictInStateText += ", ";
                    inDistrictInStateText += "Out-of-state";
                    chargeForInDistrictOrInStateExists = true;
                }
                inDistrictInStateText += ")";
                if (chargeForInDistrictOrInStateExists == false)
                    inDistrictInStateText = "";

                AddSummaryTableRow(tblSummary, "Tuition Based on Residency Status", BoolToString(AppContext.InstitutionChargeDifferentTuition) + inDistrictInStateText);
            }
            if(!string.IsNullOrEmpty(AppContext.LargestProgram))
                AddSummaryTableRow(tblSummary, "Largest Program", AppContext.LargestProgram);
            if(AppContext.NumberOfMonths.HasValue)
                AddSummaryTableRow(tblSummary, "Largest Program Duration (in Months)", AppContext.NumberOfMonths.Value.ToString());

            // Summary table #2
            if (AppContext.Percentage.HasValue)
                AddSummaryTableRow(tblSummary2, "Percentage of Full-Time, First-Time Undergraduates Receiving Aid", AppContext.Percentage.Value.ToString() + "%");
            AddSummaryTableRow(tblSummary2, "Explanation #1", AppContext.Explanation1.Replace("\n", "<br />"));
            AddSummaryTableRow(tblSummary2, "Explanation #2", AppContext.Explanation2.Replace("\n", "<br />"));
            AddSummaryTableRow(tblSummary2, "Explanation #3", AppContext.Explanation3.Replace("\n", "<br />"));
        }

        /// <summary>
        /// Add row to summary table
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        private void AddSummaryTableRow(Table tblToAdd, string title, string value)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc = new TableCell();
            tc.Text = title;
            tc.HorizontalAlign = HorizontalAlign.Left;
            tc.VerticalAlign = VerticalAlign.Top;
            tc.Style.Add("font-weight", "bold");
            tc.Style.Add("font-size", "12px");
            tc.Style.Add("color", "#4f4d34");
            tc.Style.Add("border-bottom", "solid 1px #c8c6b4");
            tc.Style.Add("width", "320px");
            tc.Style.Add("padding-bottom", "4px");
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = value;
            tc.HorizontalAlign = HorizontalAlign.Left;
            tc.VerticalAlign = VerticalAlign.Middle;
            tc.Style.Add("font-weight", "normal");
            tc.Style.Add("color", "#4f4d34");
            tc.Style.Add("padding-left", "20px");
            tc.Style.Add("border-bottom", "solid 1px #c8c6b4");
            tc.Style.Add("padding-bottom", "4px");
            tr.Cells.Add(tc);
            tblToAdd.Rows.Add(tr);
        }

        /// <summary>
        /// Convert boolean to "Yes" or "No"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string BoolToString(bool? value)
        {
            if (value.HasValue)
            {
                if (value == true)
                    return "Yes";
                else
                    return "No";
            }
            else
                return "";
        }

        /// <summary>
        /// Method convert PoaValues and TgaValues arrays into dictionary 
        /// so we can easyly get value by key
        /// </summary>
        private void ConvertAppContextArrayIntoDisctionary()
        {
            // POA
            if (AppContext.PoaValues != null)
            {
                for (int i = 0; i < AppContext.PoaValues.Length; i++)
                    dictPoaValues.Add(AppContext.PoaValues[i].Key, AppContext.PoaValues[i]);
            }

            // TGA
            if (AppContext.TgaValues != null)
            {
                for (int i = 0; i < AppContext.TgaValues.Length; i++)
                    dictTgaValues.Add(AppContext.TgaValues[i].Key, AppContext.TgaValues[i]);
            }
        }

        /// <summary>
        /// Draw 'Price of Attendace' table
        /// </summary>
        private void DrawPoa()
        {
            #region POA and POACalc tables setup
            if (AppContext.InstitutionType == InstitutionType.Academic)
            {
                if (AppContext.InstitutionChargeDifferentTuition.HasValue && AppContext.InstitutionChargeDifferentTuition.Value == false)
                {
                    // We need to display "One Amount" column

                    tblPOA.Rows[1].Visible = true;
                    tblPOA.Rows[2].Visible = false;
                    tblPOA.Rows[3].Visible = false;
                    tblPOA.Rows[4].Visible = false;

                    // we need to show "One Amount"
                    tblPOACalc.Rows[12].Visible = true;
                    if (_numberOfLivingStatusColumns == 1)
                    {
                        tblPOACalc.Rows[13].Visible = true;
                        //lblAm_Oc.Text = (int.Parse(AppContext.PoaValues[1].Value) + int.Parse(AppContext.PoaValues[2].Value) + int.Parse(AppContext.PoaValues[3].Value) + int.Parse(AppContext.PoaValues[4].Value)).ToString();
                    }
                    else if(_numberOfLivingStatusColumns == 2)
                    {
                        tblPOACalc.Rows[14].Visible = true;
                        
                        tblPOACalc.Rows[15].Visible = true;
                    }
                    else if(_numberOfLivingStatusColumns == 3)
                    {
                        tblPOACalc.Rows[13].Visible = true;
                        tblPOACalc.Rows[14].Visible = true;
                        tblPOACalc.Rows[15].Visible = true;
                    }

                }
                else
                {
                    tblPOA.Rows[1].Visible = false;
                    if (AppContext.ChargeForInDistrict.HasValue && AppContext.ChargeForInDistrict.Value == true)
                    {
                        tblPOA.Rows[2].Visible = true;

                        // POACalc table
                        tblPOACalc.Rows[0].Visible = true;
                        if (_numberOfLivingStatusColumns == 1)
                        {
                            tblPOACalc.Rows[1].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 2)
                        {
                            tblPOACalc.Rows[2].Visible = true;
                            tblPOACalc.Rows[3].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 3)
                        {
                            tblPOACalc.Rows[1].Visible = true;
                            tblPOACalc.Rows[2].Visible = true;
                            tblPOACalc.Rows[3].Visible = true;
                        }
                    }
                    else
                        tblPOA.Rows[2].Visible = false;
                    if (AppContext.ChargeForInState.HasValue && AppContext.ChargeForInState.Value == true)
                    {
                        tblPOA.Rows[3].Visible = true;

                        // POACalc table
                        tblPOACalc.Rows[4].Visible = true;
                        if (_numberOfLivingStatusColumns == 1)
                        {
                            tblPOACalc.Rows[5].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 2)
                        {
                            tblPOACalc.Rows[6].Visible = true;
                            tblPOACalc.Rows[7].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 3)
                        {
                            tblPOACalc.Rows[5].Visible = true;
                            tblPOACalc.Rows[6].Visible = true;
                            tblPOACalc.Rows[7].Visible = true;
                        }
                    }
                    else
                        tblPOA.Rows[3].Visible = false;
                    if (AppContext.ChargeForOutOfState.HasValue && AppContext.ChargeForOutOfState.Value == true)
                    {
                        tblPOA.Rows[4].Visible = true;

                        // POACalc table
                        tblPOACalc.Rows[8].Visible = true;
                        if (_numberOfLivingStatusColumns == 1)
                        {
                            tblPOACalc.Rows[9].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 2)
                        {
                            tblPOACalc.Rows[10].Visible = true;
                            tblPOACalc.Rows[11].Visible = true;
                        }
                        else if (_numberOfLivingStatusColumns == 3)
                        {
                            tblPOACalc.Rows[9].Visible = true;
                            tblPOACalc.Rows[10].Visible = true;
                            tblPOACalc.Rows[11].Visible = true;
                        }
                    }
                    else
                        tblPOA.Rows[4].Visible = false;
                }

                if (_numberOfLivingStatusColumns != 3)
                {
                    if (_numberOfLivingStatusColumns == 1)
                    {
                        // we need to display only On-campus
                        tblPOA.Rows[6].Cells[2].Visible = false;
                        tblPOA.Rows[6].Cells[3].Visible = false;

                        tblPOA.Rows[7].Cells[2].Visible = false;
                        tblPOA.Rows[7].Cells[3].Visible = false;

                        tblPOA.Rows[8].Cells[2].Visible = false;
                        tblPOA.Rows[8].Cells[3].Visible = false;

                        tblPOA.Rows[6].Cells[1].Width = Unit.Empty;
                    }
                    else if (_numberOfLivingStatusColumns == 2)
                    {
                        // we need to display only off-campus not with family and off-campus with family
                        tblPOA.Rows[6].Cells[1].Visible = false;
                        tblPOA.Rows[7].Cells[1].Visible = false;
                        tblPOA.Rows[8].Cells[1].Visible = false;

                        tblPOA.Rows[6].Cells[2].Width = Unit.Pixel(230);
                    }
                }
            }
            else if (AppContext.InstitutionType == InstitutionType.Program)
            {
                tblPOA.Rows[1].Visible = true;
                tblPOA.Rows[2].Visible = false;
                tblPOA.Rows[3].Visible = false;
                tblPOA.Rows[4].Visible = false;

                // we need to show "One Amount"
                tblPOACalc.Rows[12].Visible = true;
                if (_numberOfLivingStatusColumns == 1)
                {
                    tblPOACalc.Rows[13].Visible = true;
                }
                else if (_numberOfLivingStatusColumns == 2)
                {
                    tblPOACalc.Rows[14].Visible = true;
                    tblPOACalc.Rows[15].Visible = true;
                }
                else if (_numberOfLivingStatusColumns == 3)
                {
                    tblPOACalc.Rows[13].Visible = true;
                    tblPOACalc.Rows[14].Visible = true;
                    tblPOACalc.Rows[15].Visible = true;
                }

                if (_numberOfLivingStatusColumns != 3)
                {
                    if (_numberOfLivingStatusColumns == 1)
                    {
                        // we need to display only On-campus
                        tblPOA.Rows[6].Cells[2].Visible = false;
                        tblPOA.Rows[6].Cells[3].Visible = false;

                        tblPOA.Rows[7].Cells[2].Visible = false;
                        tblPOA.Rows[7].Cells[3].Visible = false;

                        tblPOA.Rows[8].Cells[2].Visible = false;
                        tblPOA.Rows[8].Cells[3].Visible = false;
                    }
                    else if (_numberOfLivingStatusColumns == 2)
                    {
                        // we need to display only off-campus not with family and off-campus with family
                        tblPOA.Rows[6].Cells[1].Visible = false;
                        tblPOA.Rows[7].Cells[1].Visible = false;
                        tblPOA.Rows[8].Cells[1].Visible = false;
                    }
                }
            }
            #endregion

            lblPOA_t_amount.Text = FormatCurrency(AppContext.TuitionAmount.ToString());
            lblPOA_t_id.Text = FormatCurrency(AppContext.TuitionInDistrinct.ToString());
            lblPOA_t_is.Text = FormatCurrency(AppContext.TuitionInState.ToString());
            lblPOA_t_oos.Text = FormatCurrency(AppContext.TuitionOutOfState.ToString());

            lblPOA_bas.Text = FormatCurrency(AppContext.BooksAndSupplies.ToString());

            lblPOA_rb_oncampus.Text = FormatCurrency(AppContext.LivingOnCampus.ToString());
            lblPOA_rb_offcampus.Text = FormatCurrency(AppContext.LivingOffCampus.ToString());

            lblPOA_other_oncampus.Text = FormatCurrency(AppContext.OtherOnCampus.ToString());
            lblPOA_other_offcampus.Text = FormatCurrency(AppContext.OtherOffCampus.ToString());
            lblPOA_other_offcampuswf.Text = FormatCurrency(AppContext.OtherOffCampusWithFamily.ToString());

            if(_numberOfResidencyLivingColumns == 1)
            {
                // dispaly "One Amount"
                if(_numberOfLivingStatusColumns == 1 || _numberOfLivingStatusColumns == 3)
                {
                    lblAm_Oc.Text = FormatCurrency((AppContext.TuitionAmount + AppContext.BooksAndSupplies + AppContext.LivingOnCampus + AppContext.OtherOnCampus).ToString());
                }
                if(_numberOfLivingStatusColumns == 2 || _numberOfLivingStatusColumns == 3)
                {
                    lblAm_Offc.Text = FormatCurrency((AppContext.TuitionAmount + AppContext.BooksAndSupplies + AppContext.LivingOffCampus + AppContext.OtherOffCampus).ToString());
                    lblAm_Offcwf.Text = FormatCurrency((AppContext.TuitionAmount + AppContext.BooksAndSupplies + AppContext.LivingOffCampusWithFamily + AppContext.OtherOffCampusWithFamily).ToString());
                }
            }
            else
            {
                if(AppContext.ChargeForInDistrict.HasValue && AppContext.ChargeForInDistrict.Value == true)
                {
                    if (_numberOfLivingStatusColumns == 1 || _numberOfLivingStatusColumns == 3)
                    {
                        lblId_Oc.Text = FormatCurrency((AppContext.TuitionInDistrinct + AppContext.BooksAndSupplies + AppContext.LivingOnCampus + AppContext.OtherOnCampus).ToString());
                    }
                    if (_numberOfLivingStatusColumns == 2 || _numberOfLivingStatusColumns == 3)
                    {
                        lblId_Offc.Text = FormatCurrency((AppContext.TuitionInDistrinct + AppContext.BooksAndSupplies + AppContext.LivingOffCampus + AppContext.OtherOffCampus).ToString());
                        lblId_Offcwf.Text = FormatCurrency((AppContext.TuitionInDistrinct + AppContext.BooksAndSupplies + AppContext.LivingOffCampusWithFamily + AppContext.OtherOffCampusWithFamily).ToString());
                    }
                }
                if (AppContext.ChargeForInState.HasValue && AppContext.ChargeForInState.Value == true)
                {
                    if (_numberOfLivingStatusColumns == 1 || _numberOfLivingStatusColumns == 3)
                    {
                        lblIs_Oc.Text = FormatCurrency((AppContext.TuitionInState + AppContext.BooksAndSupplies + AppContext.LivingOnCampus + AppContext.OtherOnCampus).ToString());
                    }
                    if (_numberOfLivingStatusColumns == 2 || _numberOfLivingStatusColumns == 3)
                    {
                        lblIs_Offc.Text = FormatCurrency((AppContext.TuitionInState + AppContext.BooksAndSupplies + AppContext.LivingOffCampus + AppContext.OtherOffCampus).ToString());
                        lblIs_Offcwf.Text = FormatCurrency((AppContext.TuitionInState + AppContext.BooksAndSupplies + AppContext.LivingOffCampusWithFamily + AppContext.OtherOffCampusWithFamily).ToString());
                    }
                }
                if (AppContext.ChargeForOutOfState.HasValue && AppContext.ChargeForOutOfState.Value == true)
                {
                    if (_numberOfLivingStatusColumns == 1 || _numberOfLivingStatusColumns == 3)
                    {
                        lblOos_Oc.Text = FormatCurrency((AppContext.TuitionOutOfState + AppContext.BooksAndSupplies + AppContext.LivingOnCampus + AppContext.OtherOnCampus).ToString());
                    }
                    if (_numberOfLivingStatusColumns == 2 || _numberOfLivingStatusColumns == 3)
                    {
                        lblOos_Offc.Text = FormatCurrency((AppContext.TuitionOutOfState + AppContext.BooksAndSupplies + AppContext.LivingOffCampus + AppContext.OtherOffCampus).ToString());
                        lblOos_Offcwf.Text = FormatCurrency((AppContext.TuitionOutOfState + AppContext.BooksAndSupplies + AppContext.LivingOffCampusWithFamily + AppContext.OtherOffCampusWithFamily).ToString());
                    }
                }
            }
            
        }

        /// <summary>
        /// Draw 'Grands and Schoolarship studens'
        /// </summary>
        private void DrawTGA()
        {
            AddResidencyLivingColumns(tblTGA, "EFC Range ($)");
            AddLivingStatusColumns(tblTGA);
            tblTGA.Style.Add("border-bottom", "1px solid #c8c6b4");

            // Add rows with textboxes
            for (int rowIndex = 1; rowIndex <= 13; rowIndex++)
            {
                TableRow tr = new TableRow();
                if (rowIndex % 2 == 0)
                    tr.Style.Add("background-color", "#ecece1");                

                TableHeaderCell thc = new TableHeaderCell();
                //thc.Style.Add("width", "92px");
                //thc.Style.Add("padding-left", "10px");
                //thc.Style.Add("line-height", "13px");
                //thc.Style.Add("padding-top", "9px");
                //thc.Style.Add("padding-bottom", "9px");

                thc.Style.Add("padding-left", "10px");
                thc.Style.Add("padding-top", "9px");
                thc.Style.Add("padding-bottom", "9px");
                thc.Style.Add("line-height", "13px");
                thc.Style.Add("text-align", "left");
                thc.Style.Add("border-bottom", "1px solid #c8c6b4");
                thc.Style.Add("width", "180px");

                #region Write first column text with money amount
                switch (rowIndex)
                {
                    case 1:
                        thc.Text = "0";
                        break;
                    case 2:
                        thc.Text = "1-1,000";
                        break;
                    case 3:
                        thc.Text = "1,001-2,500";
                        break;
                    case 4:
                        thc.Text = "2,501-5,000";
                        break;
                    case 5:
                        thc.Text = "5,001-7,500";
                        break;
                    case 6:
                        thc.Text = "7,501-10,000";
                        break;
                    case 7:
                        thc.Text = "10,001-12,500";
                        break;
                    case 8:
                        thc.Text = "12,501-15,000";
                        break;
                    case 9:
                        thc.Text = "15,001-20,000";
                        break;
                    case 10:
                        thc.Text = "20,001-30,000";
                        break;
                    case 11:
                        thc.Text = "30,001-40,000";
                        break;
                    case 12:
                        thc.Text = ">40,000";
                        break;
                    case 13:
                        thc.Text = "Non-FAFSA filers/unknown EFC";
                        break;
                }
                #endregion
                tr.Cells.Add(thc);


                TableCell tc = new TableCell();
                //tc.Style.Add("width", "99px");
                //tc.Style.Add("padding-left", "10px");
                //tc.Style.Add("line-height", "13px");

                int colIndex = 2;
                for (int residencyLivingColumnIndex = 1; residencyLivingColumnIndex <= _numberOfResidencyLivingColumns; residencyLivingColumnIndex++)
                {
                    for (int livingStatusColumnIndex = 1; livingStatusColumnIndex <= _numberOfLivingStatusColumns; livingStatusColumnIndex++)
                    {
                        string header1 = tblTGA.Rows[0].Cells[residencyLivingColumnIndex].Attributes["id"];
                        //int header2CellIndex = (residencyLivingColumnIndex * _numberOfResidencyLivingColumns - _numberOfResidencyLivingColumns) + livingStatusColumnIndex - 1;
                        int header2CellIndex = ((residencyLivingColumnIndex - 1) * _numberOfLivingStatusColumns) + livingStatusColumnIndex - 1;
                        string header2 = tblTGA.Rows[1].Cells[header2CellIndex].Attributes["id"];

                        tc = new TableCell();
                        tc.Style.Add("border-bottom", "1px solid #c8c6b4");
                        if (livingStatusColumnIndex == 1)
                            tc.Style.Add("border-left", "1px solid #c8c6b4");
                        tc.Attributes.Add("headers", string.Format("{0} {1}", header1, header2));
                        FormatCellWithTextbox(tc);
                        tc.Text = GenerateTextboxHtml(2, rowIndex, colIndex); /* (residencyLivingColumnIndex * livingStatusColumnIndex) + 1 */
                        tr.Cells.Add(tc);
                        colIndex++;
                    }
                }
                tblTGA.Rows.Add(tr);
            }
        }

        /// <summary>
        /// Draw In-district, In-state, Out-of-state
        /// </summary> 
        private void AddResidencyLivingColumns(Table tblData, string firstCellText)
        {
            // new table row
            TableRow tr = new TableRow();

            // First cell is blank
            TableHeaderCell tc = new TableHeaderCell();
            tc.Text = firstCellText;
            tc.RowSpan = 2;
            tc.Style.Add("vertical-align", "bottom");
            tc.Style.Add("text-align", "left");
            tc.Style.Add("padding-left", "10px");
            tc.Style.Add("line-height", "14px");
            tc.Style.Add("background-color", "#cecec0");
            tc.Style.Add("font-weight", "bold");
            tc.Style.Add("color", "#686653");
            tr.Cells.Add(tc);

            if (AppContext.InstitutionChargeDifferentTuition.HasValue == false || (AppContext.InstitutionChargeDifferentTuition.HasValue && AppContext.InstitutionChargeDifferentTuition.Value == false))
            {
                // One column. Rename Out-of-state to 'Amount'
                tc = new TableHeaderCell();
                tc.Text = "Amount";
                tc.CssClass = "ColumnTableTitle";
                tc.Style.Add("background-color", "#cecec0");
                tc.Style.Add("color", "#cc6600");
                tc.Style.Add("font-size", "12px");
                tc.HorizontalAlign = HorizontalAlign.Center;
                tc.ColumnSpan = _numberOfLivingStatusColumns;
                tr.Cells.Add(tc);
            }
            else if (_numberOfResidencyLivingColumns > 1)
            {
                if (AppContext.ChargeForInDistrict.HasValue && AppContext.ChargeForInDistrict.Value == true)
                {
                    tc = new TableHeaderCell();
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("border-left", "1px solid #c8c6b4");
                    tc.Style.Add("color", "#cc6600");
                    tc.Style.Add("font-size", "12px");
                    tc.Text = "In-district";
                    tc.Attributes.Add("id", "InDistrict");
                    tc.CssClass = "ColumnTableTitle";
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tc.ColumnSpan = _numberOfLivingStatusColumns;
                    tr.Cells.Add(tc);
                }
                if (AppContext.ChargeForInState.HasValue && AppContext.ChargeForInState.Value == true)
                {
                    tc = new TableHeaderCell();
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("border-left", "1px solid #c8c6b4");
                    tc.Style.Add("color", "#cc6600");
                    tc.Style.Add("font-size", "12px");
                    tc.Text = "In-state";
                    tc.Attributes.Add("id", "InState");
                    tc.CssClass = "ColumnTableTitle";
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tc.ColumnSpan = _numberOfLivingStatusColumns;
                    tr.Cells.Add(tc);
                }
                if (AppContext.ChargeForOutOfState.HasValue && AppContext.ChargeForOutOfState.Value == true)
                {
                    tc = new TableHeaderCell();
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("border-left", "1px solid #c8c6b4");
                    tc.Text = "Out-of-state";
                    tc.Style.Add("color", "#cc6600");
                    tc.Style.Add("font-size", "12px");
                    tc.Attributes.Add("id", "OutOfState");
                    tc.CssClass = "ColumnTableTitle";
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tc.ColumnSpan = _numberOfLivingStatusColumns;
                    tr.Cells.Add(tc);
                }

            }

            // Add row to table
            tblData.Rows.Add(tr);
        }

        /// <summary>
        /// Draw n-campus, Off-campus with family, Off-campus without family
        /// </summary>
        private void AddLivingStatusColumns(Table tblData)
        {
            // new table row
            TableRow tr = new TableRow();
            TableHeaderCell tc;
            bool leftBorderNeeded = true;
            bool leftBorderAdded = false;
            // Display left border only if we not dispalying "One Amount"
            if (AppContext.InstitutionChargeDifferentTuition.HasValue == false || (AppContext.InstitutionChargeDifferentTuition.HasValue && AppContext.InstitutionChargeDifferentTuition.Value == false))
            {
                leftBorderNeeded = false;
            }

            for (int i = 1; i <= _numberOfResidencyLivingColumns; i++)
            {
                if (_numberOfLivingStatusColumns == 1 || _numberOfLivingStatusColumns == 3)
                {
                    tc = new TableHeaderCell();
                    if (leftBorderNeeded)
                        tc.Style.Add("border-left", "1px solid #c8c6b4");
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("font-weight", "bold");
                    tc.Style.Add("color", "#686653");
                    tc.Style.Add("vertical-align", "bottom");
                    tc.Text = "On-campus";
                    tc.Attributes.Add("id", string.Format("{0}OnCampus", i));
                    FormatCellWithLivingStatus(tc);
                    tr.Cells.Add(tc);
                    leftBorderAdded = true;
                }

                if (_numberOfLivingStatusColumns == 3 || _numberOfLivingStatusColumns == 2)
                {
                    tc = new TableHeaderCell();
                    tc.Style.Add("vertical-align", "bottom");
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("font-weight", "bold");
                    tc.Style.Add("color", "#686653");
                    tc.Text = "Off-campus not with family";
                    tc.Attributes.Add("id", string.Format("{0}OffCampusNotWithFamily", i));
                    FormatCellWithLivingStatus(tc);
                    if (leftBorderNeeded)
                    {
                        if (!leftBorderAdded)
                            tc.Style.Add("border-left", "1px solid #c8c6b4");
                    }
                    tr.Cells.Add(tc);

                    tc = new TableHeaderCell();
                    tc.Style.Add("background-color", "#cecec0");
                    tc.Style.Add("vertical-align", "bottom");
                    tc.Style.Add("font-weight", "bold");
                    tc.Style.Add("color", "#686653");
                    tc.Text = "Off-campus with family";
                    tc.Attributes.Add("id", string.Format("{0}OffCampusWithFamily", i));
                    FormatCellWithLivingStatus(tc);
                    tr.Cells.Add(tc);
                }
            }

            tblData.Rows.Add(tr);
        }       

        /// <summary>
        /// Generate html code for textbox
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="readonlyTextbox"></param>
        /// <param name="fillZeroForFirstRow"></param>
        /// <returns></returns>
        private string GenerateTextboxHtml(int tableNumber, int rowIndex, int colIndex)
        {
            int returnValue = 0;
            string returnStr = "";

            string textboxId = string.Format("txt_t{0}_{1}_{2}", tableNumber.ToString(), rowIndex, colIndex);
            if (tableNumber == 1)
            {
                if (dictPoaValues.ContainsKey(textboxId))
                {
                    returnValue = int.Parse(dictPoaValues[textboxId].Value);
                    if (returnValue == 0)
                        returnStr = dictPoaValues[textboxId].AutoGenerated == false ? returnValue.ToString() : "";
                    else
                        returnStr = returnValue.ToString();                    
                }
            }
            else if (tableNumber == 2)
            {
                if (dictTgaValues.ContainsKey(textboxId))
                {                    
                    if (dictTgaValues[textboxId].AutoGenerated == true)
                    {
                        returnValue = int.Parse(dictTgaValues[textboxId].Value);
                        returnStr = returnValue.ToString() + "&nbsp;‡";
                    }
                    else
                    {
                        returnValue = int.Parse(dictTgaValues[textboxId].Value);
                        returnStr = returnValue != 0 ? returnValue.ToString() : "0";
                    }
                }
            }


            return returnStr;
        }

        /// <summary>
        /// Method removes border from left nad right sides of table
        /// </summary>
        private void RemoveSomeBorderColors()
        {
            //TGA
            for (int rowIndex = 0; rowIndex < tblTGA.Rows.Count; rowIndex++)
            {
                //tblTGA.Rows[rowIndex].Cells[0].Style.Add("border-left", "solid 0px white");
                tblTGA.Rows[rowIndex].Cells[tblTGA.Rows[rowIndex].Cells.Count - 1].Style.Add("border-right", "solid 0px white");
            }

            tblTGA.Rows[0].Cells[0].Style.Add("border-bottom", "solid 0px white");
            tblTGA.Rows[1].Cells[0].Style.Add("border-top", "solid 0px white");
        }

        /// <summary>
        /// Add css style to cell which contains textbox
        /// </summary>
        /// <param name="tc"></param>
        private void FormatCellWithTextbox(TableCell tc)
        {
            tc.Style.Add("text-align", "center");
        }

        /// <summary>
        /// Format cell with living status
        /// </summary>
        /// <param name="?"></param>
        private void FormatCellWithLivingStatus(TableCell tc)
        {            
            tc.Style.Add("text-align", "center");
            tc.Style.Add("line-height", "14px");
        }

        /// <summary>
        /// Format string as currency with dollar symbol
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string FormatCurrency(string text)
        {
            return string.Format("{0:C0}", Convert.ToDecimal(text));
        }
               

        
        /* **************************** */
        /* Code related to download ZIP */
        /* **************************** */
        
        protected void ibtnSubmitYes_Click(object sender, EventArgs e)
        {
            string instnm = Request.Form["tbInstName"];
            string welcomeMessage = Request.Form["tbWelcomeMessage"];
            DownloadStudentApp(instnm, welcomeMessage);
        }

        protected void ibtnSubmitNo_Click(object sender, EventArgs e)
        {
            DownloadStudentApp(null, null);
        }

        private void DownloadStudentApp(string instName,string welcomeMessage)
        {
            string htmlContent = NetPriceUtils.GenerateHtmlText(AppContext, instName, welcomeMessage);

            string _tempFolderPath = ConfigurationManager.AppSettings["TEMPFOLDER"];
            string _tempFolder = Server.MapPath(_tempFolderPath);
            // Generate guid
            string _guid = Guid.NewGuid().ToString();
            //string outputfilename = Path.Combine(_tempFolder, "npcalc.htm");
            string outputzipfilename = Path.Combine(_tempFolder, _guid + ".zip");

            //File.WriteAllText(outputfilename, temp);


            // Get temporary folder
            DirectoryInfo dirInfoTemp = new DirectoryInfo(_tempFolder);
            // create new folder to zip content
            DirectoryInfo dirInfoNew = Directory.CreateDirectory(Path.Combine(_tempFolder, _guid));
            // create folder called 'images' inside newly created folder
            DirectoryInfo dirInfoNewImages = Directory.CreateDirectory(Path.Combine(dirInfoNew.FullName, "images"));
            // copy images from web applications folder into new folder "images"
            DirectoryInfo dirWebAppImagesFolder = new DirectoryInfo(Path.Combine(Server.MapPath("."), "TempFolderImages"));
            foreach (FileInfo fi in dirWebAppImagesFolder.GetFiles())
            {
                fi.CopyTo(Path.Combine(dirInfoNewImages.FullName, fi.Name), true);
            }


            // write html file
            string htmlFileName = Path.Combine(dirInfoNew.FullName, "npcalc.htm");
            File.WriteAllText(htmlFileName, htmlContent);



            if (NetPriceUtils.CreateZipArchive(outputzipfilename, dirInfoNew.FullName, "npcalc.htm;.gif;.jpg;.png", "images"))
            {
                //File.Delete(outputfilename); 
                try
                {
                    Directory.Delete(dirInfoNew.FullName, true);
                }
                catch { }

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = @"attachment/x-ouput";
                Response.AddHeader("Content-disposition", "attachment;filename=NetPriceCalculator.zip");
                Response.WriteFile(outputzipfilename, true);
                File.Delete(outputzipfilename);
                Response.End();
            }
        }
                

    }   

}