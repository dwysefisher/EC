using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Inovas.Common.Data;
using ICSharpCode.SharpZipLib.Zip;

namespace Inovas.NetPrice
{
    /// <summary>
    /// Utility class for NetPrice
    /// </summary>
    public class NetPriceUtils
    {
        public NetPriceUtils()
        {
        }

        /// <summary>
        /// Convert number to InstitutionType enum
        /// </summary>
        /// <param name="institutionTypeNumber">string variable that represents number</param>
        /// <returns>InstitutionType</returns>
        public static InstitutionType ParseInstitutionType(string institutionTypeNumber)
        {
            int institutionType = -1;
            InstitutionType returnValue = InstitutionType.Unknown;
            if (int.TryParse(institutionTypeNumber, out institutionType))
            {
                try
                {
                    returnValue = (InstitutionType)institutionType;
                }
                catch { }                    
            }
            return returnValue;
        }

        /// <summary>
        /// Method accepts AppContext and returns string contains HTML page
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static string GenerateHtmlText(AppContext appContext)
        {
            return NetPriceUtils.GenerateHtmlText(appContext, null, null);
        }

        /// <summary>
        /// Method accepts AppContext and returns string contains HTML page
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static string GenerateHtmlText(AppContext appContext, string instName,string welcomeMessage)
        {
            string htmlContent = string.Empty;

            // ORIGINAL CODE FROM BGP
            StringBuilder sbPoa = new StringBuilder();
            StringBuilder sbTga = new StringBuilder();
            StringBuilder extraJS = new StringBuilder();
            StringBuilder jsVariables = new StringBuilder();
            string jsInstitutionWelcomeWindows = string.Empty;

            // "1"-yes, "0"-no
            string oncampus = "0";
            string offcampuswithfamily = "1"; // by default it is visible
            string offcampusnotwithfamily = "1"; // by default it is visible

            string indistrict = "0";
            string instate = "0";
            string outofstate = "0";

            int _livingstauts = 0;
            int _residencystatus = 0;
            int _residencyValueCounter = 0;
            int _livingStatusValueCounter = 0;


            if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
            {
                oncampus = "1";
                if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                {
                    offcampusnotwithfamily = "0";
                    offcampuswithfamily = "0";
                }
            }

            if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == true)
            {
                if (appContext.ChargeForInDistrict.HasValue && appContext.ChargeForInDistrict.Value == true)
                    indistrict = "1";
                if (appContext.ChargeForInState.HasValue && appContext.ChargeForInState.Value == true)
                    instate = "1";
                if (appContext.ChargeForOutOfState.HasValue && appContext.ChargeForOutOfState.Value == true)
                    outofstate = "1";               
            }
            else if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == false)
            {
                outofstate = "0";
                indistrict = "0";
                instate = "0";
            }

            if (appContext.PoaValues.Length != 0 && appContext.TgaValues.Length != 0)
            {

                #region Build JavaScript POA and TGA arrays

                //////
                // POA arrays
                //////
                int counter = 0;
                int numberOfColumnsInArray = GetNumberOfResidencyLivingColumns(appContext) * GetNumberOfLivingStatusColumns(appContext);
                for (int i = 0; i < appContext.PoaValues.Length; i++)
                {
                    if (i % numberOfColumnsInArray == 0)
                    {
                        counter++;

                        if (counter != 1)
                            sbPoa.AppendLine("];");

                        if (counter == 1)
                            sbPoa.Append("POA_Total = [");
                        else if (counter == 2)
                            sbPoa.Append("POA_TRF = [");
                        else if (counter == 3)
                            sbPoa.Append("POA_BS = [");
                        else if (counter == 4)
                            sbPoa.Append("POA_RB = [");
                        else if (counter == 5)
                            sbPoa.Append("POA_O = [");
                        sbPoa.AppendFormat("'{0}'", appContext.PoaValues[i].Value);
                    }
                    else
                    {
                        sbPoa.AppendFormat(",'{0}'", appContext.PoaValues[i].Value);
                    }
                }
                if (counter > 0)
                    sbPoa.AppendLine("];");

                ///////
                // TGA arrays
                ///////                
                counter = 0;
                for (int i = 0; i < appContext.TgaValues.Length; i++)
                {
                    if (i % numberOfColumnsInArray == 0)
                    {
                        counter++;

                        if (counter != 1)
                            sbTga.AppendLine("];");

                        if (counter == 1)
                            sbTga.Append("TGA_0 = [");
                        else if (counter == 2)
                            sbTga.Append("TGA_1_1000 = [");
                        else if (counter == 3)
                            sbTga.Append("TGA_1001_2500 = [");
                        else if (counter == 4)
                            sbTga.Append("TGA_2501_5000 = [");
                        else if (counter == 5)
                            sbTga.Append("TGA_5001_7500 = [");
                        else if (counter == 6)
                            sbTga.Append("TGA_7501_10000 = [");
                        else if (counter == 7)
                            sbTga.Append("TGA_10001_12500 = [");
                        else if (counter == 8)
                            sbTga.Append("TGA_12501_15000 = [");
                        else if (counter == 9)
                            sbTga.Append("TGA_15001_20000 = [");
                        else if (counter == 10)
                            sbTga.Append("TGA_20001_30000 = [");
                        else if (counter == 11)
                            sbTga.Append("TGA_30001_40000 = [");
                        else if (counter == 12)
                            sbTga.Append("TGA_40000 = [");
                        else if (counter == 13)
                            sbTga.Append("TGA_NFAFSA = [");
                        sbTga.AppendFormat("'{0}'", appContext.TgaValues[i].Value);
                    }
                    else
                    {
                        sbTga.AppendFormat(",'{0}'", appContext.TgaValues[i].Value);
                    }
                }
                if (counter > 0)
                    sbTga.AppendLine("];");
                #endregion

                // Read Template File
                string filePath = System.Web.HttpContext.Current.Server.MapPath("npc_temp.htm");                

                // Read content of npc_temp.htm
                using (StreamReader sr = File.OpenText(filePath))
                {
                    htmlContent = sr.ReadToEnd();
                }
                if (!string.IsNullOrEmpty(htmlContent))
                {

                    #region pop up window with institution name and welcome message
                    if (!string.IsNullOrEmpty(instName))
                    {
                        if (string.IsNullOrEmpty(welcomeMessage))
                            welcomeMessage = string.Empty;
                        htmlContent = htmlContent.Replace("##POPUP-INSTNM##", instName).Replace("##POPUP-MESSAGE##", welcomeMessage.Replace(Environment.NewLine, "<br />"));
                        jsInstitutionWelcomeWindows = "var showWelcomeMessage = true; openInstitutionNameWindow();";

                    }
                    else
                        jsInstitutionWelcomeWindows = "var showWelcomeMessage = false;";
                    #endregion

                    // *********************************
                    // Show/Hide Living Status: On-campus
                    if (oncampus == "0")
                    {
                        // Hide On-Campus
                        extraJS.AppendLine("HideTag('s_ls_0');");
                        _livingstauts++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_livingstatus_0##", _livingStatusValueCounter.ToString());
                        _livingStatusValueCounter++;
                    }
                    // Show/Hide Living Status: Off-campus with family
                    if (offcampuswithfamily == "0")
                    {
                        // Hide Off-Campus with family
                        extraJS.AppendLine("HideTag('s_ls_1');");
                        _livingstauts++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_livingstatus_1##", _livingStatusValueCounter.ToString());
                        _livingStatusValueCounter++;
                    }
                    // Show/Hide Living Status: Off-campus NOT with family
                    if (offcampusnotwithfamily == "0")
                    {
                        // Hide Off-Campus not with family
                        extraJS.AppendLine("HideTag('s_ls_2');");
                        _livingstauts++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_livingstatus_2##", _livingStatusValueCounter.ToString());
                        _livingStatusValueCounter++;
                    }

                    if (_livingstauts == 3)
                    {
                        // Hide Living Status Question
                        extraJS.AppendLine("HideTag('tr_ls');");
                        extraJS.AppendLine("npc1_livingstatus = -1;");
                    }
                    else if (_livingstauts == 2)
                    {
                        // Hide Living Status Question
                        extraJS.AppendLine("HideTag('tr_ls');");
                        extraJS.AppendLine("npc1_livingstatus = '0';");
                        extraJS.AppendLine("npc1_isdefaultlivingstatus = '1';");
                    }
                    extraJS.AppendLine(string.Format("numberoflivingstatus = {0};", _livingStatusValueCounter));


                    // ******************************
                    // Show/Hide residency status
                    // In-district
                    if (indistrict == "0")
                    {
                        // Hide In-District
                        extraJS.AppendLine("HideTag('s_rs_0');");
                        _residencystatus++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_residencystatus_0##", _residencyValueCounter.ToString());
                        _residencyValueCounter++;
                    }

                    // In-state
                    if (instate == "0")
                    {
                        // Hide In-state
                        extraJS.AppendLine("HideTag('s_rs_1');");
                        _residencystatus++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_residencystatus_1##", _residencyValueCounter.ToString());
                        _residencyValueCounter++;
                    }
                    // Out-of-state
                    if (outofstate == "0")
                    {
                        // Hide out-of-state
                        extraJS.AppendLine("HideTag('s_rs_2');");
                        _residencystatus++;
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##rb_residencystatus_2##", _residencyValueCounter.ToString());
                        _residencyValueCounter++;
                    }

                    if (_residencystatus == 3)
                    {
                        // Hide Residency Status Question
                        extraJS.AppendLine("HideTag('tr_rs');");
                        extraJS.AppendLine("npc1_residencystatus = -1;");
                    }
                    else if (_residencystatus == 2)
                    {
                        // Hide Residency Status Question
                        extraJS.AppendLine("HideTag('tr_rs');");
                        extraJS.AppendLine("npc1_residencystatus = '0';");
                        extraJS.AppendLine("npc1_isdefaultresidencystatus = '1';");
                    }

                    if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                        extraJS.AppendLine("ShowTag('tr_requiredliveoncampus');");

                    // ---------
                    // Add POA & TGA Dictionary
                    htmlContent = htmlContent.Replace("////JAVASCRIPT ARRAYS////", sbPoa.ToString() + sbTga.ToString());


                    // Replace Year
                    if (!string.IsNullOrEmpty(appContext.YearText))
                        htmlContent = htmlContent.Replace("##YEAR##", appContext.YearText);
                    else
                        htmlContent = htmlContent.Replace("##YEAR##", "0000");

                    // Replace Percent
                    htmlContent = htmlContent.Replace("##PERCENT##", appContext.Percentage.GetValueOrDefault(0).ToString());

                    // Replace explanations
                    string explanation1 = "";
                    if (!string.IsNullOrEmpty(appContext.Explanation1))
                        explanation1 = appContext.Explanation1.Replace("\n", "<br />");
                    string explanation2 = "";
                    if (!string.IsNullOrEmpty(appContext.Explanation2))
                        explanation2 = appContext.Explanation2.Replace("\n", "<br />");
                    string explanation3 = "";
                    if (!string.IsNullOrEmpty(appContext.Explanation3))
                        explanation3 = appContext.Explanation3.Replace("\n", "<br />");
                    htmlContent = htmlContent.Replace("##ADDITIONAL INFORMATION1##", explanation1);
                    htmlContent = htmlContent.Replace("##ADDITIONAL INFORMATION2##", explanation2);
                    htmlContent = htmlContent.Replace("##ADDITIONAL INFORMATION3##", explanation3);

                    if (appContext.InstitutionType == InstitutionType.Program)
                    {
                        htmlContent = htmlContent.Replace("##LARGESTPROGRAM##", appContext.LargestProgram);
                        htmlContent = htmlContent.Replace("##NUMBEROFMONTHS##", appContext.NumberOfMonths.GetValueOrDefault(0).ToString());

                        htmlContent = htmlContent.Replace("##STYLEDISPLAYACADEMIC##", "display:none;");
                        htmlContent = htmlContent.Replace("##STYLEDISPLAYPROGRAM##", "");
                    }
                    else
                    {
                        htmlContent = htmlContent.Replace("##STYLEDISPLAYACADEMIC##", "");
                        htmlContent = htmlContent.Replace("##STYLEDISPLAYPROGRAM##", "display:none;");
                    }


                    #region Create EFC array for Dependent/Independent student
                    StringBuilder sbEFC = new StringBuilder();

                    // Dependent
                    GetEFCArrayJavaScript(appContext, sbEFC, "Dependent", "efcDependent");

                    // Independent without dependent
                    GetEFCArrayJavaScript(appContext, sbEFC, "IndepWithoutDep", "efcIndWithoutDep");

                    // Independent with dependent
                    GetEFCArrayJavaScript(appContext, sbEFC, "IndepWithDep", "efcIndWithDep");

                    htmlContent = htmlContent.Replace("##EFC_ARRAY##", sbEFC.ToString());

                    #endregion


                    // Replace ##ADDITIONAL JAVASCRIPT##
                    if (extraJS.Length > 0)
                    {
                        string tempStr = @"function SetupConstants() {" + extraJS.ToString() + "}; SetupConstants();";
                        if (!string.IsNullOrEmpty(appContext.InstitutionBanner))
                            tempStr += string.Format("var bannerFileName='{0}'; setupBanner();", appContext.InstitutionBanner);
                        if (!string.IsNullOrEmpty(jsInstitutionWelcomeWindows))
                            tempStr += " " + jsInstitutionWelcomeWindows;

                        htmlContent = htmlContent.Replace("##ADDITIONAL JAVASCRIPT##", "<script type='text/javascript'>" + tempStr + "</script>");
                    }
                    else
                        htmlContent = htmlContent.Replace("##ADDITIONAL JAVASCRIPT##", "");
                }
            }

            return htmlContent;
        }

        /// <summary>
        /// This method is used by GenerateHtmlText
        /// </summary>
        /// <param name="sbEFC"></param>
        /// <param name="tableName"></param>
        /// <param name="javascriptArrayName"></param>
        private static void GetEFCArrayJavaScript(AppContext appContext, StringBuilder sbEFC, string tableName, string javascriptArrayName)
        {
            sbEFC.AppendLine();
            sbEFC.AppendLine();
            sbEFC.AppendFormat("var {0} = []; ", javascriptArrayName);
            using (SqlDataReader reader = SqlHelper.GetDataReader(string.Format("Select * From {0} Where YearIndex={1} order by NumberInCollege, NumberInFamily, [Order]", tableName, appContext.YearIndex)))
            {
                string numInCollege = string.Empty;
                string numInFamily = string.Empty;
                int incomeIndex = 0; // so we will start with index 0
                float efc = 0;

                int efcArrCounter = -1;
                while (reader.Read())
                {
                    string numInCollegeFromDB = reader["NumberInCollege"].ToString();
                    string numInFamilyFromDB = reader["NumberInFamily"].ToString();
                    if (reader["EFC"] != DBNull.Value)
                        efc = float.Parse(reader["EFC"].ToString());
                    else
                        efc = 0;

                    if (numInCollegeFromDB == numInCollege && numInFamilyFromDB == numInFamily)
                    {
                        // just add array item inside existing array item
                        sbEFC.AppendFormat("{0}[{1}].incomeRanges[{2}] = {3};  ", javascriptArrayName, efcArrCounter, incomeIndex, efc);
                    }
                    else
                    {
                        // reset income index  counter;
                        incomeIndex = 0;
                        efcArrCounter++;


                        numInCollege = numInCollegeFromDB;
                        numInFamily = numInFamilyFromDB;

                        // create new array item                                                                
                        sbEFC.AppendLine();
                        sbEFC.AppendFormat("{0}[{1}] = {{}};", javascriptArrayName, efcArrCounter);
                        sbEFC.AppendLine();
                        sbEFC.AppendFormat("{0}[{1}].numberInCollege={2};", javascriptArrayName, efcArrCounter, numInCollege);
                        sbEFC.AppendLine();
                        sbEFC.AppendFormat("{0}[{1}].numberInFamily={2};", javascriptArrayName, efcArrCounter, numInFamily);
                        sbEFC.AppendLine();
                        sbEFC.AppendFormat("{0}[{1}].incomeRanges= []; ", javascriptArrayName, efcArrCounter);
                        sbEFC.AppendLine();

                        sbEFC.AppendFormat("{0}[{1}].incomeRanges[{2}] = {3};  ", javascriptArrayName, efcArrCounter, incomeIndex, efc);

                    }
                    incomeIndex++;
                }
            }
        }

        /// <summary>
        /// Get number of columns for Residency living: In-state, In-distrinct, Out-of-state
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfResidencyLivingColumns(AppContext appContext)
        {
            int numberOfColumns = 0;
            if (appContext.InstitutionChargeDifferentTuition.HasValue)
            {
                if (appContext.InstitutionChargeDifferentTuition.Value == false)
                    numberOfColumns = 1;
                else
                {
                    // in-district
                    if (appContext.ChargeForInDistrict.HasValue && appContext.ChargeForInDistrict.Value == true)
                        numberOfColumns++;
                    // in-state
                    if (appContext.ChargeForInState.HasValue && appContext.ChargeForInState.Value == true)
                        numberOfColumns++;
                    // out-of-state
                    if (appContext.ChargeForOutOfState.HasValue && appContext.ChargeForOutOfState.Value == true)
                        numberOfColumns++;
                }
            }
            else if (appContext.InstitutionType == InstitutionType.Program)
            {
                // for InstitutionType='Program', numberOfColumns=1
                numberOfColumns = 1;
            }

            return numberOfColumns;
        }

        /// <summary>
        /// Get number of columns for living status: On-campus, Off-campus with family, Off-campus without family
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfLivingStatusColumns(AppContext appContext)
        {
            int numberOfColumns = 1;
            if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
            {
                // all 3 columns will be displayed
                numberOfColumns = 3;

                if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                    numberOfColumns = 1; // only on-campus is displayed
            }
            else
                numberOfColumns = 2; // only off-campus 2 columns will be displayed

            return numberOfColumns;
        }

        /// <summary>
        /// Create ZIP file that can contains files and folders
        /// </summary>
        /// <param name="absoluteZipFileName">Absolute path to new zip archive</param>
        /// <param name="sourceDirectoryPath">Absolute path to folder where source files are located</param>
        /// <param name="fileNamePattern">Pattern of file name that will be included into zip archive</param>
        /// <param name="directoryNamePattern">Pattern of folder names that will be included into zip archive</param>
        /// <returns></returns>
        public static bool CreateZipArchive(string absoluteZipFileName, string sourceDirectoryPath, string fileNamePattern, string directoryNamePattern)
        {
            bool createZipResult = true;
            FastZip fz = new FastZip();

            try
            {
                fz.CreateZip(absoluteZipFileName, sourceDirectoryPath, true, fileNamePattern, directoryNamePattern);
                createZipResult = true;
            }
            catch
            {
                createZipResult = false;
                // Good idea to log exception for future investigation
            }
            return createZipResult;
        }
    }
}
