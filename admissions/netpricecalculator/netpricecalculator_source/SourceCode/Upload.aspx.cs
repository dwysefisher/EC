using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Xml.XPath;
using Inovas.NetPrice;
using Inovas.Common.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Inovas.NetPrice
{
    public partial class Upload : Inovas.NetPrice.PageBase
    {
        List<AppContext> listAppContex = new List<AppContext>();
        // Dictionary to store index for year
        Dictionary<string, int> dictYearIndexes = new Dictionary<string, int>();
        // Collection of error messages
        List<string> listErrorMessages = new List<string>();
        // This variable contains validation result message
        protected string _validationResultMessage = string.Empty;
        // The content of uploaded file
        private string[] fileLines;

        protected void Page_Load(object sender, EventArgs e)
        {
            phErrors.Visible = false;
            LoadContext();
            (this.Master as Npc2).TitleWithInstitutionTypeAndYearVisible = false;


            #region Load year indexes from DB
            using (SqlDataReader reader = SqlHelper.GetDataReader("Select * from [YearSelection] order by [yearIndex] desc"))
            {
                while (reader.Read())
                {
                    string yearText = reader["YearText"].ToString().Trim();
                    int yearIndex = Convert.ToInt32(reader["yearIndex"].ToString());
                    if (!dictYearIndexes.ContainsKey(yearText))
                        dictYearIndexes.Add(yearText, yearIndex);
                }
            }
            #endregion
        }

        /// <summary>
        /// Convert string from XML file into boolean value
        /// </summary>
        /// <param name="yesNoType"></param>
        /// <returns></returns>
        private bool ConvertToBool(string yesNoType)
        {
            if (yesNoType == "0")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns part of the string. The start index is NOT zero-based.
        /// </summary>
        /// <param name="fileLine">file line</param>
        /// <param name="startIndex">start position (not zero-based)</param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ReadData(int fileLine, int startPosition, int length)
        {
            if (fileLines.Length < fileLine)
                return string.Empty;

            if (fileLines[fileLine - 1].Length < startPosition - 1)
                return string.Empty;
            else
            {
                if (fileLines[fileLine - 1].Length < startPosition - 1 + length)
                    return fileLines[fileLine - 1].Substring(startPosition - 1).Trim();
                else
                    return fileLines[fileLine - 1].Substring(startPosition - 1, length).Trim();
            }
        }

        /// <summary>
        /// Get value position related to two entered values: 
        /// 1 - Means that the missing values is above.
        /// 2 - The missing value is in between the given values
        /// 3 - The missing value is below the given ones
        /// </summary>
        /// <param name="arrTGA"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        private int GetValuePosition(int[,] arrTGA, int rowIndex, int cellIndex, out int enteredValueIndex1, out int enteredValue1, out int enteredValueIndex2, out int enteredValue2, Dictionary<string, KeyValue> dictEnteredTga)
        {
            int valuePosition = -1;
            enteredValueIndex1 = -1;
            enteredValue1 = -1;
            enteredValueIndex2 = -1;
            enteredValue2 = -1;


            // this variables are used to find entered values
            int aboveCloseValue = -1;
            int aboveCloseIndex = -1;
            int aboveFarValue = -1;
            int aboveFarIndex = -1;

            int belowCloseValue = -1;
            int belowCloseIndex = -1;
            int belowFarValue = -1;
            int belowFarIndex = -1;



            // EXAMLE
            // --------
            // | 1 | - far value   }
            // |----               } ABOVE VALUES
            // | 2 | - close value }
            // |----
            // | X | -- missing value
            // |----
            // | 0 |               }
            // |----               }
            // | 3 | - close value }
            // |----               } BELOW VALUES 
            // | 4 | - far value   }
            // |____

            for (int cellIndexFind = 0; cellIndexFind < 11; cellIndexFind++)
            {
                if (cellIndexFind == cellIndex)
                    continue;

                int cell = cellIndexFind + 1;
                int row = rowIndex + 2;

                string dictKey = string.Format("txt_t2_{0}_{1}", cell, row);
                if (arrTGA[rowIndex, cellIndexFind] != 0 || (dictEnteredTga[dictKey].AutoGenerated == false && dictEnteredTga[dictKey].Value == "0"))
                {
                    // we found a value ABOVE missing value
                    if (cellIndexFind < cellIndex)
                    {
                        // set far value to current close value
                        if (aboveCloseValue != -1)
                        {
                            aboveFarIndex = aboveCloseIndex;
                            aboveFarValue = aboveCloseValue;
                        }

                        // set new close value
                        aboveCloseValue = arrTGA[rowIndex, cellIndexFind];
                        aboveCloseIndex = cellIndexFind;
                    }
                    else
                    {
                        if (belowCloseIndex == -1)
                        {
                            belowCloseIndex = cellIndexFind;
                            belowCloseValue = arrTGA[rowIndex, cellIndexFind];
                        }
                        else if (belowFarIndex == -1)
                        {
                            belowFarIndex = cellIndexFind;
                            belowFarValue = arrTGA[rowIndex, cellIndexFind];
                        }
                    }
                }
            }



            // BETWEEN range has higher priority
            if (aboveCloseIndex != -1 && belowCloseIndex != -1)
            {
                valuePosition = 2;

                enteredValueIndex1 = aboveCloseIndex;
                enteredValue1 = aboveCloseValue;

                enteredValueIndex2 = belowCloseIndex;
                enteredValue2 = belowCloseValue;
            }
            else if (aboveCloseIndex != -1 && aboveFarIndex != -1)
            {
                // Entered values ABOVE missing values
                valuePosition = 3;

                enteredValueIndex1 = aboveFarIndex;
                enteredValue1 = aboveFarValue;

                enteredValueIndex2 = aboveCloseIndex;
                enteredValue2 = aboveCloseValue;
            }
            else if (belowCloseIndex != -1 && belowFarIndex != -1)
            {
                // Entered values BELOW missing values
                valuePosition = 1;

                enteredValueIndex1 = belowCloseIndex;
                enteredValue1 = belowCloseValue;

                enteredValueIndex2 = belowFarIndex;
                enteredValue2 = belowFarValue;

            }

            return valuePosition;
        }

        /// <summary>
        /// Call database function to calculate average for a cell with missing value
        /// </summary>
        /// <param name="valuePosition"></param>
        /// <param name="enteredValueIndex1"></param>
        /// <param name="enteredValue1"></param>
        /// <param name="enteredValueIndex2"></param>
        /// <param name="enteredValue2"></param>
        /// <returns></returns>
        private int GetAverageValue(int valuePosition, int missingIndex, int enteredValueIndex1, int enteredValue1, int enteredValueIndex2, int enteredValue2)
        {
            object returnValue = SqlHelper.ExecScalar(string.Format("SELECT 'MissingValue' = dbo.getMissingValue({0},{1},{2},{3},{4},{5})", missingIndex, valuePosition, enteredValueIndex1, enteredValue1, enteredValueIndex2, enteredValue2));
            if (returnValue != null && returnValue != DBNull.Value)
                return int.Parse(returnValue.ToString());
            else
                return 0;
        }

        /// <summary>
        /// Save uploaded xml file into temp folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (rbAcademic.Checked || rbProgram.Checked)
            {
                bool isAcademic = rbAcademic.Checked;

                if (fuSource.HasFile && fuSource.FileName.ToLower().EndsWith(".txt"))
                {
                    string tempFolderPath = Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]);
                    string uploadedFilePath = Path.Combine(tempFolderPath, Guid.NewGuid().ToString() + ".txt");
                    fuSource.SaveAs(uploadedFilePath);
                    
                    // now read the file and plit line for each unit id
                    Dictionary<string, List<string>> dictFileContent = new Dictionary<string, List<string>>();
                    fileLines = File.ReadAllLines(uploadedFilePath);
                    int currentUnitId = 0;
					bool errorWhileCreatingDictionary = false;

					try
					{
						for (int lineIndex = 0; lineIndex < fileLines.Length; lineIndex++)
						{
							if (fileLines[lineIndex].Trim().Length == 0)
								continue;
							int unitId;
							if (int.TryParse(fileLines[lineIndex].Substring(0, 6), out unitId))
							{
								if (currentUnitId != unitId)
								{
									dictFileContent.Add(unitId.ToString(), new List<string>());
									currentUnitId = unitId;
								}
								dictFileContent[unitId.ToString()].Add(fileLines[lineIndex]);
							}
						}
					}
					catch
					{
						errorWhileCreatingDictionary = true;
					}


					if (errorWhileCreatingDictionary)
					{
						ltErrors.Text += "Invalid file format.<br />";  
						phErrors.Visible = true;
					}
					else
					{
						if (isAcademic)
							ProcessAndGenerateAcademic(dictFileContent);
						else
							ProcessAndGenerateProgram(dictFileContent);
					}
                }
                else
                    _validationResultMessage = "You must select a .txt file to upload.";
            }
            else
                _validationResultMessage = "Please choose a predominant calendar system.";
        }

        private void ProcessAndGenerateAcademic(Dictionary<string, List<string>> dictFileContent)
        {
            foreach (KeyValuePair<string, List<string>> kvp in dictFileContent)
            {
                fileLines = new string[kvp.Value.Count];
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    fileLines[i] = kvp.Value[i];
                }

                if (fileLines.Length == 0)
                    continue;

                AppContext appContext = new AppContext();
                appContext.InstitutionType = InstitutionType.Academic;

                int numTableColumns = 0;
                int numResidencyLiving = 0;
                int numLivingStatus = 0;
                
                int line = 1;                
                try
                {
                    appContext.InstitutionId = ReadData(line, 1, 6);
                    appContext.InstitutionName = ReadData(line, 9, 100);
                    appContext.InstitutionBanner = ReadData(line, 109, 100);
                    appContext.YearText = ReadData(line, 209, 7);
                    if (dictYearIndexes.ContainsKey(appContext.YearText))
                        appContext.YearIndex = dictYearIndexes[appContext.YearText];
                    else
                    {
                        // if we can't find year in dictionary, use latest year
                        foreach (string key in dictYearIndexes.Keys)
                        {
                            appContext.YearText = key;
                            appContext.YearIndex = dictYearIndexes[key];
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(ReadData(line, 216, 3)))
                        appContext.Percentage = int.Parse(ReadData(line, 216, 3));
                }
                catch
                {
                    listErrorMessages.Add("An error occurred while reading the data entered for Institution Information.");
                    if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                        listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;

                    continue;
                }


                // POA table
                try
                {
                    ++line;
                    string inDistrictTuition = ReadData(line, 9, 6);
                    string inStateTuition = ReadData(line, 15, 6);
                    string outOfStateTuition = ReadData(line, 21, 6);
                    string tuitionAmount = ReadData(line, 27, 6);

                    string booksAndSupplies = ReadData(line, 33, 6);
                    // RaB - room and board
                    string onCampusRaB = ReadData(line, 39, 6);
                    string onCampusOE = ReadData(line, 45, 6);
                    string offCampusRaB = ReadData(line, 51, 6);
                    string offCampusWithoutFamilyOE = ReadData(line, 57, 6);
                    string offCampusWithFamilyOE = ReadData(line, 63, 6);

                    if (!string.IsNullOrEmpty(onCampusRaB))
                    {
                        appContext.InstitutionallyControlledHousingOffered = true;
                        if (string.IsNullOrEmpty(offCampusRaB) && string.IsNullOrEmpty(offCampusWithoutFamilyOE) && string.IsNullOrEmpty(offCampusWithFamilyOE))
                            appContext.StudentsRequiredLiveOnCampusOrHousing = true;
                        else
                            appContext.StudentsRequiredLiveOnCampusOrHousing = false;
                    }
                    else
                        appContext.InstitutionallyControlledHousingOffered = false;
                    
                    int residencyCounter = 0;
                    if (!string.IsNullOrEmpty(inDistrictTuition))
                    {
                        appContext.InstitutionChargeDifferentTuition = true;
                        appContext.ChargeForInDistrict = true;
                        ++residencyCounter;
                    }
                    if (!string.IsNullOrEmpty(inStateTuition))
                    {
                        appContext.InstitutionChargeDifferentTuition = true;
                        appContext.ChargeForInState = true;
                        ++residencyCounter;
                    }
                    if (!string.IsNullOrEmpty(outOfStateTuition))
                    {
                        appContext.InstitutionChargeDifferentTuition = true;
                        appContext.ChargeForOutOfState = true;
                        ++residencyCounter;
                    }
                    if (residencyCounter == 0)
                    {
                        if (!string.IsNullOrEmpty(tuitionAmount))
                            appContext.InstitutionChargeDifferentTuition = false;
                        else
                            throw new Exception();
                    }
                    else if (residencyCounter < 2)
                    {
                        throw new Exception();
                    }


                    // this variable has number of column in POA and TGA tables
                    numTableColumns = NetPriceUtils.GetNumberOfLivingStatusColumns(appContext) * NetPriceUtils.GetNumberOfResidencyLivingColumns(appContext);
                    numResidencyLiving = NetPriceUtils.GetNumberOfResidencyLivingColumns(appContext);
                    numLivingStatus = NetPriceUtils.GetNumberOfLivingStatusColumns(appContext);

                    // If there is no table column, throw exception and skip current unit id
                    if (numTableColumns == 0)
                        throw new Exception();

                    #region Parse data for "Price of Attendance" table
                    // this list contains values for tuition for all columns
                    List<string> listPoaTuition = new List<string>();
                    // if tuition is vary, then we need to read "in-district", "in-state" and "out-of-state"
                    if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == true)
                    {
                        // add "in-district" to list
                        if (!string.IsNullOrEmpty(inDistrictTuition))
                        {
                            for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                                listPoaTuition.Add(inDistrictTuition);
                        }
                        // add "in-state" to list
                        if (!string.IsNullOrEmpty(inStateTuition))
                        {
                            for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                                listPoaTuition.Add(inStateTuition);
                        }
                        // add "out-of-state" to list
                        if (!string.IsNullOrEmpty(outOfStateTuition))
                        {
                            for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                                listPoaTuition.Add(outOfStateTuition);
                        }

                    }
                    else
                    {
                        // read one amount                
                        for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                            listPoaTuition.Add(tuitionAmount);
                    }


                    // Parse data for "Books and supplies"
                    List<string> listPoaBooksAndSupplies = new List<string>();
                    for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                    {
                        for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                            listPoaBooksAndSupplies.Add(booksAndSupplies);
                    }

                    // Parse data for "Room and board"
                    List<string> listPoaRoomAndBoard = new List<string>();
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            // we have 3 living statuses
                            for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                            {
                                listPoaRoomAndBoard.Add(onCampusRaB);
                                listPoaRoomAndBoard.Add(offCampusRaB);
                                listPoaRoomAndBoard.Add("0");
                            }
                        }
                        else if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                        {
                            // we have 1 living status                    
                            for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                            {
                                listPoaRoomAndBoard.Add(onCampusRaB);
                            }
                        }
                    }
                    else if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == false)
                    {
                        // we have 1 living status                
                        for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                        {
                            listPoaRoomAndBoard.Add(offCampusRaB);
                            listPoaRoomAndBoard.Add("0");
                        }
                    }


                    // Parse data for "Other"
                    List<string> listPoaOtherExpenses = new List<string>();
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            // we have 3 living statuses                    
                            for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                            {
                                listPoaOtherExpenses.Add(onCampusOE);
                                listPoaOtherExpenses.Add(offCampusWithoutFamilyOE);
                                listPoaOtherExpenses.Add(offCampusWithFamilyOE);
                            }
                        }
                        else if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                        {
                            // we have 1 living status                    
                            for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                            {
                                listPoaOtherExpenses.Add(onCampusOE);
                            }
                        }
                    }
                    else if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == false)
                    {
                        // we have 1 living status                
                        for (int residencyLivingIndex = 0; residencyLivingIndex < numResidencyLiving; residencyLivingIndex++)
                        {
                            listPoaOtherExpenses.Add(offCampusWithoutFamilyOE);
                            listPoaOtherExpenses.Add(offCampusWithFamilyOE);
                        }
                    }

                    // Now we need to fill appContext.PoaValues[] array
                    appContext.PoaValues = new KeyValue[5 * numTableColumns];
                    // first, add columns with totals
                    int counter = 0;
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[i] = new KeyValue("txt_t1_1_" + (i + 2), Convert.ToString(Convert.ToInt64(listPoaTuition[i]) + Convert.ToInt64(listPoaBooksAndSupplies[i])
                            + Convert.ToInt64(listPoaRoomAndBoard[i]) + Convert.ToInt64(listPoaOtherExpenses[i])), false);
                    }
                    // add tuition
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 1) + i] = new KeyValue("txt_t1_2_" + (i + 2), listPoaTuition[i], false);
                    }
                    // add books and supplies
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 2) + i] = new KeyValue("txt_t1_3_" + (i + 2), listPoaBooksAndSupplies[i], false);
                    }
                    // add room and board
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 3) + i] = new KeyValue("txt_t1_4_" + (i + 2), listPoaRoomAndBoard[i], false);
                    }
                    // add other expenses
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 4) + i] = new KeyValue("txt_t1_5_" + (i + 2), listPoaOtherExpenses[i], false);
                    }
                    #endregion
                }
                catch
                {
                    listErrorMessages.Add("An error occurred while reading the data entered for Price of Attendance.");
                    if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                        listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;

                    continue;
                }

                try
                {
                    #region Parse data for "Grants and Scholarships"
                    line = 2;
                    Dictionary<string, string> dictTga = new Dictionary<string, string>();
                    if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == true)
                    {                        
                        for (int colIndex = 1; colIndex <= (numLivingStatus * numResidencyLiving); colIndex++)
                        {
                            int valuesEntered = 0;
                            for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                            {
                                int startPos = ((efcIndex - 1) * 6) + 9;
                                string valueStr = ReadData(line + colIndex, startPos, 6);
                                int value;
                                if (int.TryParse(valueStr, out value))
                                {
                                    dictTga[efcIndex + "_" + colIndex] = valueStr;
                                    ++valuesEntered;
                                }
                                else
                                    dictTga[efcIndex + "_" + colIndex] = "0";
                            }
                            if (valuesEntered < 2)
                            {
                                listErrorMessages.Add("Data must be entered in at least two cells per column. This does not include the >$40,000 or Non-FAFSA filers/unknown EFC rows.");
                                throw new Exception();
                            }
                        }
                    }
                    else
                    {
                        for (int colIndex = 1; colIndex <= 3; colIndex++)
                        {
                            int valuesEntered = 0;
                            for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                            {
                                int startPos = ((efcIndex - 1) * 6) + 9;
                                string valueStr = ReadData(line + colIndex, startPos, 6);
                                int value;
                                if (int.TryParse(valueStr, out value))
                                {
                                    dictTga[efcIndex + "_" + colIndex] = valueStr;
                                    ++valuesEntered;
                                }
                                else
                                    dictTga[efcIndex + "_" + colIndex] = "0";
                            }
                            if (valuesEntered < 2)
                            {
                                listErrorMessages.Add("Data must be entered in at least two cells per column. This does not include the >$40,000 or Non-FAFSA filers/unknown EFC rows.");
                                throw new Exception();
                            }
                        }

                    }

                    // these variables used for living status and residency type
                    bool isTuitionVary = false;
                    bool isInDistrict = false;
                    bool isInState = false;
                    bool isOutOfState = false;
                    bool isOnCampus = false;
                    bool isOffCampusNotWithFamily = false;
                    bool isOffCampusWithFamily = false;

                    // these are the index of item "table"
                    int inDistrictIndex = 0;
                    int inStateIndex = 0;
                    int outOfStateIndex = 0;
                    int onCampusIndex = 0;
                    int offCampusNotWithFamilyIndex = 0;
                    int offCampusWithFamilyIndex = 0;
                    #region set boolean variables above
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            isOnCampus = true;
                            onCampusIndex = 1;

                            isOffCampusNotWithFamily = true;
                            offCampusNotWithFamilyIndex = 2;

                            isOffCampusWithFamily = true;
                            offCampusWithFamilyIndex = 3;
                        }
                        else
                        {
                            onCampusIndex = 1;
                            isOnCampus = true;
                        }
                    }
                    else
                    {
                        isOffCampusNotWithFamily = true;
                        offCampusNotWithFamilyIndex = 1;

                        isOffCampusWithFamily = true;
                        offCampusWithFamilyIndex = 2;
                    }

                    if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == true)
                    {
                        isTuitionVary = true;
                        if (appContext.ChargeForInDistrict.HasValue && appContext.ChargeForInDistrict.Value == true)
                        {
                            isInDistrict = true;
                            inDistrictIndex = 1;
                        }
                        if (appContext.ChargeForInState.HasValue && appContext.ChargeForInState.Value == true)
                        {
                            isInState = true;
                            inStateIndex = inDistrictIndex + 1;

                        }
                        if (appContext.ChargeForOutOfState.HasValue && appContext.ChargeForOutOfState.Value == true)
                        {
                            isOutOfState = true;
                            outOfStateIndex = inStateIndex + 1;
                        }
                    }
                    #endregion


                    List<KeyValue> listTga = new List<KeyValue>();
                    // In TGA table, there are 13 rows, so we itareate cycle 13 times to build list of values
                    for (int rowIndex = 0; rowIndex < 13; rowIndex++)
                        for (int colIndex = 0; colIndex < numTableColumns; colIndex++)
                            listTga.Add(new KeyValue("txt_t2_" + (rowIndex + 1) + "_" + (colIndex + 2), "0", false));


                    if (isTuitionVary == true)
                    {
                        int listIndex = 0;
                        for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                        {
                            int colIndex = 0;
                            if (isInDistrict)
                            {
                                if (isOnCampus)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusNotWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                            }
                            if (isInState)
                            {
                                if (isOnCampus)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusNotWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                            }
                            if (isOutOfState)
                            {
                                if (isOnCampus)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusNotWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                                if (isOffCampusWithFamily)
                                {
                                    listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                                    ++listIndex;
                                }
                            }
                        }
                    }
                    else
                    {
                        int listIndex = 0;
                        for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                        {
                            if (isOnCampus)
                            {
                                listTga[listIndex].Value = dictTga[efcIndex + "_" + 1];
                                ++listIndex;
                            }
                            if (isOffCampusNotWithFamily)
                            {
                                listTga[listIndex].Value = dictTga[efcIndex + "_" + 2];
                                ++listIndex;
                            }
                            if (isOffCampusWithFamily)
                            {
                                listTga[listIndex].Value = dictTga[efcIndex + "_" + 3];
                                ++listIndex;
                            }

                        }
                    }

                    GenerateAutoValues(appContext, listTga, numTableColumns);
                    #endregion
                }
                catch
                {
                    if (listErrorMessages.Count == 0)
                    {
                        listErrorMessages.Add("An error occurred while reading the data entered for Grants and Scholarships.");
                        if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                            listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;
                    }

                    continue;
                }

                #region Parse explanations
                if (appContext.InstitutionChargeDifferentTuition.HasValue && appContext.InstitutionChargeDifferentTuition.Value == true)
                {
                    int startIndex = 2 + (numResidencyLiving * numLivingStatus);
                    appContext.Explanation1 = ReadData(startIndex + 1, 9, 1000);
                    appContext.Explanation2 = ReadData(startIndex + 2, 9, 1000);
                    appContext.Explanation3 = ReadData(startIndex + 3, 9, 1000);
                }
                else
                {
                    appContext.Explanation1 = ReadData(6, 9, 1000);
                    appContext.Explanation2 = ReadData(7, 9, 1000);
                    appContext.Explanation3 = ReadData(8, 9, 1000);
                }
                #endregion

                listAppContex.Add(appContext);
            }
            
            DownloadZipFile();
        }

        private void ProcessAndGenerateProgram(Dictionary<string, List<string>> dictFileContent)
        {
            foreach (KeyValuePair<string, List<string>> kvp in dictFileContent)
            {
                fileLines = new string[kvp.Value.Count];
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    fileLines[i] = kvp.Value[i];
                }
                AppContext appContext = new AppContext();
                appContext.InstitutionType = InstitutionType.Program;

                int numTableColumns = 0;
                int numResidencyLiving = 0;
                int numLivingStatus = 0;

                int line = 1;
                try
                {
                    appContext.InstitutionId = ReadData(line, 1, 6);
                    appContext.InstitutionName = ReadData(line, 9, 100);
                    appContext.InstitutionBanner = ReadData(line, 109, 100);
                    appContext.YearText = ReadData(line, 209, 7);
                    if (dictYearIndexes.ContainsKey(appContext.YearText))
                        appContext.YearIndex = dictYearIndexes[appContext.YearText];
                    else
                    {
                        // if we can't find year in dictionary, use latest year
                        foreach (string key in dictYearIndexes.Keys)
                        {
                            appContext.YearText = key;
                            appContext.YearIndex = dictYearIndexes[key];
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(ReadData(line, 216, 3)))
                        appContext.Percentage = int.Parse(ReadData(line, 216, 3));

                    appContext.LargestProgram = ReadData(2, 9, 100);
                    if (string.IsNullOrEmpty(appContext.LargestProgram))
                        throw new Exception();
                    appContext.NumberOfMonths = int.Parse(ReadData(2, 119, 2));
                    if (appContext.NumberOfMonths.HasValue == false)
                        throw new Exception();                    
                }
                catch
                {
                    listErrorMessages.Add("An error occurred while reading the data entered for Institution Information.");
                    if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                        listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;

                    continue;
                }

                // Line 2
                try
                {
                    ++line;                    
                    string tuitionAmount = ReadData(line, 121, 6);
                    string booksAndSupplies = ReadData(line, 127, 6);
                    string onCampusRaB = ReadData(line, 133, 6);
                    string onCampusOE = ReadData(line, 139, 6);
                    string offCampusRaB = ReadData(line, 145, 6);
                    string offCampusWithoutFamilyOE = ReadData(line, 151, 6);
                    string offCampusWithFamilyOE = ReadData(line, 157, 6);

                    if (!string.IsNullOrEmpty(onCampusRaB))
                    {
                        appContext.InstitutionallyControlledHousingOffered = true;
                        if (string.IsNullOrEmpty(offCampusRaB) && string.IsNullOrEmpty(offCampusWithoutFamilyOE) && string.IsNullOrEmpty(offCampusWithFamilyOE))
                            appContext.StudentsRequiredLiveOnCampusOrHousing = true;
                        else
                            appContext.StudentsRequiredLiveOnCampusOrHousing = false;
                    }
                    else
                        appContext.InstitutionallyControlledHousingOffered = false;

                    // this variable has number of column in POA and TGA tables
                    numTableColumns = NetPriceUtils.GetNumberOfLivingStatusColumns(appContext) * NetPriceUtils.GetNumberOfResidencyLivingColumns(appContext);
                    numResidencyLiving = NetPriceUtils.GetNumberOfResidencyLivingColumns(appContext);
                    numLivingStatus = NetPriceUtils.GetNumberOfLivingStatusColumns(appContext);

                    #region Parse data for "Price of Attendance" table
                    // this list contains values for tuition for all columns
                    List<string> listPoaTuition = new List<string>();
                    for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                        listPoaTuition.Add(tuitionAmount);

                    // Parse data for "Books and supplies"
                    List<string> listPoaBooksAndSupplies = new List<string>();
                    for (int livingStatusIndex = 0; livingStatusIndex < numLivingStatus; livingStatusIndex++)
                        listPoaBooksAndSupplies.Add(booksAndSupplies);


                    // Parse data for "Room and board"
                    List<string> listPoaRoomAndBoard = new List<string>();
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            listPoaRoomAndBoard.Add(onCampusRaB);
                            listPoaRoomAndBoard.Add(offCampusRaB);
                            listPoaRoomAndBoard.Add("0");
                        }
                        else if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                        {
                            listPoaRoomAndBoard.Add(onCampusRaB);
                        }
                    }
                    else if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == false)
                    {
                        listPoaRoomAndBoard.Add(offCampusRaB);
                        listPoaRoomAndBoard.Add("0");
                    }


                    // Parse data for "Other"
                    List<string> listPoaOtherExpenses = new List<string>();
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            listPoaOtherExpenses.Add(onCampusOE);
                            listPoaOtherExpenses.Add(offCampusWithoutFamilyOE);
                            listPoaOtherExpenses.Add(offCampusWithFamilyOE);
                        }
                        else if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == true)
                        {
                            listPoaOtherExpenses.Add(onCampusOE);
                        }
                    }
                    else if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == false)
                    {
                        listPoaOtherExpenses.Add(offCampusWithoutFamilyOE);
                        listPoaOtherExpenses.Add(offCampusWithFamilyOE);
                    }

                    // Now we need to fill appContext.PoaValues[] array
                    appContext.PoaValues = new KeyValue[5 * numTableColumns];
                    // first, add columns with totals
                    int counter = 0;
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[i] = new KeyValue("txt_t1_1_" + (i + 2), Convert.ToString(Convert.ToInt64(listPoaTuition[i]) + Convert.ToInt64(listPoaBooksAndSupplies[i])
                            + Convert.ToInt64(listPoaRoomAndBoard[i]) + Convert.ToInt64(listPoaOtherExpenses[i])), false);
                    }
                    // add tuition
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 1) + i] = new KeyValue("txt_t1_2_" + (i + 2), listPoaTuition[i], false);
                    }
                    // add books and supplies
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 2) + i] = new KeyValue("txt_t1_3_" + (i + 2), listPoaBooksAndSupplies[i], false);
                    }
                    // add room and board
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 3) + i] = new KeyValue("txt_t1_4_" + (i + 2), listPoaRoomAndBoard[i], false);
                    }
                    // add other expenses
                    for (int i = 0; i < numTableColumns; i++)
                    {
                        counter++;
                        appContext.PoaValues[(numTableColumns * 4) + i] = new KeyValue("txt_t1_5_" + (i + 2), listPoaOtherExpenses[i], false);
                    }
                    #endregion
                }
                catch
                {
                    listErrorMessages.Add("An error occurred while reading the data entered for Price of Attendance.");
                    if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                        listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;

                    continue;
                }

                try
                {
                    #region Parse data for "Grants and Scholarships"
                    line = 2;
                    Dictionary<string, string> dictTga = new Dictionary<string, string>();
                    for (int colIndex = 1; colIndex <= (numResidencyLiving * numLivingStatus); colIndex++)
                    {
                        int valuesEntered = 0;
                        for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                        {
                            int startPos = ((efcIndex - 1) * 6) + 9;
                            string valueStr = ReadData(line + colIndex, startPos, 6);
                            int value;
                            if(int.TryParse(valueStr, out value))
                            {
                                dictTga[efcIndex + "_" + colIndex] = valueStr;
                                ++valuesEntered;
                            }
                            else
                                dictTga[efcIndex + "_" + colIndex] = "0";
                        }

                        if (valuesEntered < 2)
                        {
                            listErrorMessages.Add("Data must be entered in at least two cells per column. This does not include the >$40,000 or Non-FAFSA filers/unknown EFC rows.");
                            throw new Exception();
                        }
                    }

                    // these variables used for living status and residency type                                
                    bool isOnCampus = false;
                    bool isOffCampusNotWithFamily = false;
                    bool isOffCampusWithFamily = false;

                    // these are the index of item "table"                
                    int onCampusIndex = 0;
                    int offCampusNotWithFamilyIndex = 0;
                    int offCampusWithFamilyIndex = 0;
                    #region set boolean variables above
                    if (appContext.InstitutionallyControlledHousingOffered.HasValue && appContext.InstitutionallyControlledHousingOffered.Value == true)
                    {
                        if (appContext.StudentsRequiredLiveOnCampusOrHousing.HasValue && appContext.StudentsRequiredLiveOnCampusOrHousing.Value == false)
                        {
                            isOnCampus = true;
                            onCampusIndex = 1;

                            isOffCampusNotWithFamily = true;
                            offCampusNotWithFamilyIndex = 2;

                            isOffCampusWithFamily = true;
                            offCampusWithFamilyIndex = 3;
                        }
                        else
                        {
                            onCampusIndex = 1;
                            isOnCampus = true;
                        }
                    }
                    else
                    {
                        isOffCampusNotWithFamily = true;
                        offCampusNotWithFamilyIndex = 1;

                        isOffCampusWithFamily = true;
                        offCampusWithFamilyIndex = 2;
                    }
                    #endregion


                    List<KeyValue> listTga = new List<KeyValue>();
                    // In TGA table, there are 13 rows, so we itareate cycle 13 times to build list of values
                    for (int rowIndex = 0; rowIndex < 13; rowIndex++)
                        for (int colIndex = 0; colIndex < numTableColumns; colIndex++)
                            listTga.Add(new KeyValue("txt_t2_" + (rowIndex + 1) + "_" + (colIndex + 2), "0", false));


                    int listIndex = 0;
                    for (int efcIndex = 1; efcIndex <= 13; efcIndex++)
                    {
                        int colIndex = 0;
                        if (isOnCampus)
                        {
                            listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                            ++listIndex;
                        }
                        if (isOffCampusNotWithFamily)
                        {
                            listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                            ++listIndex;
                        }
                        if (isOffCampusWithFamily)
                        {
                            listTga[listIndex].Value = dictTga[efcIndex + "_" + (++colIndex)];
                            ++listIndex;
                        }
                    }

                    GenerateAutoValues(appContext, listTga, numTableColumns);

                    #endregion
                }
                catch
                {
                    listErrorMessages.Add("An error occurred while reading the data entered for Grants and Scholarships.");
                    if (appContext != null && !string.IsNullOrEmpty(appContext.InstitutionId))
                        listErrorMessages[listErrorMessages.Count - 1] += " Unit id: " + appContext.InstitutionId;

                    continue;
                }

                #region Parse explanations
                int startIndex = 2 + (numResidencyLiving * numLivingStatus);
                appContext.Explanation1 = ReadData(startIndex + 1, 9, 1000);
                appContext.Explanation2 = ReadData(startIndex + 2, 9, 1000);
                appContext.Explanation3 = ReadData(startIndex + 3, 9, 1000);
                #endregion

                listAppContex.Add(appContext);

            }
            
            DownloadZipFile();
        }
        
        private void GenerateAutoValues(AppContext appContext, List<KeyValue> listTga, int numTableColumns)
        {
            appContext.TgaValues = new KeyValue[listTga.Count];
            for (int i = 0; i < listTga.Count; i++)
            {
                appContext.TgaValues[i] = listTga[i];
                if (string.IsNullOrEmpty(appContext.TgaValues[i].Value))
                    appContext.TgaValues[i].Value = "0";
            }

            int[,] arrTGA = new int[numTableColumns, 11];
            bool[,] arrTGAAutoGeneratedMarks = new bool[numTableColumns, 11];

            // This dictionary is used in method that generated numbers. Dictionary is useful because we can access item by key name
            Dictionary<string, KeyValue> dictEnteredTga = new Dictionary<string, KeyValue>();
            for (int i = 0; i < listTga.Count; i++)
                dictEnteredTga.Add(listTga[i].Key, listTga[i]);


            // Create int array with TGA values
            for (int i = 0; i < appContext.TgaValues.Length; i++)
            {
                string[] id = appContext.TgaValues[i].Key.Replace("txt_t2_", "").Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                int cell = int.Parse(id[0]);
                int row = int.Parse(id[1]);

                --cell;
                row = row - 2;

                if (cell < 11)
                    arrTGA[row, cell] = int.Parse(appContext.TgaValues[i].Value);
            }


            // Now go through all rows and calculate average
            for (int rowIndex = 0; rowIndex < numTableColumns; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < 11; cellIndex++)
                {
                    if (arrTGA[rowIndex, cellIndex] == 0)
                    {
                        // Check that zero is autogenerated                            
                        int enteredValueIndex1 = -1;
                        int enteredValue1 = -1;

                        int enteredValueIndex2 = -1;
                        int enteredValue2 = -1;

                        int valuePosition = GetValuePosition(arrTGA, rowIndex, cellIndex, out enteredValueIndex1, out enteredValue1, out enteredValueIndex2, out enteredValue2, dictEnteredTga);

                        if (valuePosition != -1 && enteredValueIndex1 != -1 && enteredValue1 != -1 && enteredValueIndex2 != -1 && enteredValue2 != -1)
                        {
                            // Call database function to calculate average for missing cell
                            arrTGA[rowIndex, cellIndex] = GetAverageValue(valuePosition, cellIndex, enteredValueIndex1, enteredValue1, enteredValueIndex2, enteredValue2);
                            arrTGAAutoGeneratedMarks[rowIndex, cellIndex] = true;
                        }
                    }
                }
            }

            // UPDATE AppContext.TgaValues
            for (int tgaValuesIndex = 0; tgaValuesIndex < appContext.TgaValues.Length; tgaValuesIndex++)
            {
                string[] id = appContext.TgaValues[tgaValuesIndex].Key.Replace("txt_t2_", "").Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                int cell = int.Parse(id[0]);
                int row = int.Parse(id[1]);

                --cell;
                row = row - 2;
                if (cell < 11)
                {
                    appContext.TgaValues[tgaValuesIndex].Value = arrTGA[row, cell].ToString();

                    if (arrTGAAutoGeneratedMarks[row, cell] == true)
                        appContext.TgaValues[tgaValuesIndex].AutoGenerated = true;
                }
            }
        }

        private void DownloadZipFile()
        {
            if (listAppContex.Count == 0)
            {
                _validationResultMessage = "There are no institutions to download.";                
                if (listErrorMessages.Count > 0)
                {
                    phErrors.Visible = true;
                    foreach (string error in listErrorMessages)                    
                        ltErrors.Text += error + "<br />";                    
                }
                else
                    phErrors.Visible = false;
                return;
            }

            string tempFolderSetting = ConfigurationManager.AppSettings["TEMPFOLDER"];
            string tempFolderPath = Server.MapPath(tempFolderSetting);
            DirectoryInfo diTempFolder = new DirectoryInfo(tempFolderPath);

            // Create folder that will store all zip files for all contexes
            string guid = Guid.NewGuid().ToString();
            DirectoryInfo diZipsContainer = Directory.CreateDirectory(Path.Combine(diTempFolder.FullName, guid));


            // Create folder called 'images' inside folder that will contain zip files
            DirectoryInfo diImagesForHtmlApp = Directory.CreateDirectory(Path.Combine(diZipsContainer.FullName, "images"));
            // Copy images used by HTML application into folder that will contain zip files
            DirectoryInfo diWebAppImagesFolder = new DirectoryInfo(Path.Combine(Server.MapPath("."), "TempFolderImages"));
            foreach (FileInfo fi in diWebAppImagesFolder.GetFiles())
            {
                try
                {
                    fi.IsReadOnly = false;
                }
                catch
                {
                }
                fi.CopyTo(Path.Combine(diImagesForHtmlApp.FullName, fi.Name), true);
            }

            // This dictionary is used to keep track of unique unit ids, and in case of repeating unit id, we don't overwrite existing zip file
            Dictionary<string, int> dictUnitIdTrack = new Dictionary<string, int>();
            // Now loop all contexes and generate ZIP file
            foreach (AppContext appContext in listAppContex)
            {
                string htmlContent = NetPriceUtils.GenerateHtmlText(appContext);
                if (!string.IsNullOrEmpty(htmlContent))
                {
                    // htmlFileName is used to include into zip file filter, since the name can vary
                    string htmlFileName = "";
                    string htmlFilePath = "";
                    if (!string.IsNullOrEmpty(appContext.InstitutionId))
                    {
                        htmlFileName = string.Format("npcalc-{0}.htm", appContext.InstitutionId);
                        htmlFilePath = Path.Combine(diZipsContainer.FullName, string.Format("npcalc-{0}.htm", appContext.InstitutionId));
                    }
                    else
                    {
                        htmlFileName = "npcalc.htm";
                        htmlFilePath = Path.Combine(diZipsContainer.FullName, "npcalc.htm");
                    }
                    File.WriteAllText(htmlFilePath, htmlContent);


                    string zipFileName = string.Empty;
                    if (!string.IsNullOrEmpty(appContext.InstitutionId))
                    {
                        // check if we already created zip with unit id
                        if (!dictUnitIdTrack.ContainsKey(appContext.InstitutionId))
                        {
                            zipFileName = Path.Combine(diZipsContainer.FullName, string.Format("{0}.zip", appContext.InstitutionId));
                            // after process unit id for the first time, add counter to dictionary
                            dictUnitIdTrack[appContext.InstitutionId] = 1;
                        }
                        else
                        {
                            // increment
                            int counter = dictUnitIdTrack[appContext.InstitutionId];
                            dictUnitIdTrack[appContext.InstitutionId] = ++counter;
                            zipFileName = Path.Combine(diZipsContainer.FullName, string.Format("{0}-({1}).zip", appContext.InstitutionId, dictUnitIdTrack[appContext.InstitutionId]));
                        }
                    }
                    else
                        zipFileName = Path.Combine(diZipsContainer.FullName, Guid.NewGuid().ToString() + ".zip");


                    // Now all html file and images folder into zip                    
                    if (NetPriceUtils.CreateZipArchive(zipFileName, diZipsContainer.FullName, "npcalc.htm;.gif;.jpg;.png;" + htmlFileName, "images"))
                    {
                        File.Delete(htmlFilePath);
                    }
                }
            }

            // if we have errors, add them to text file
            if (listErrorMessages.Count > 0)
            {
                using (StreamWriter sw = File.CreateText(Path.Combine(diZipsContainer.FullName, "errors.txt")))
                {
                    foreach (string error in listErrorMessages)
                    {
                        sw.WriteLine(error);
                    }
                    sw.Close();
                }
            }

            // Now, zip all created zip files into one zip and download it
            string zipContainerFilePath = Path.Combine(diZipsContainer.FullName, guid);
            if (NetPriceUtils.CreateZipArchive(zipContainerFilePath, diZipsContainer.FullName, ".zip;.txt", ""))
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = @"attachment/x-ouput";
                Response.AddHeader("Content-disposition", "attachment;filename=NetPriceCalculator.zip");
                Response.WriteFile(zipContainerFilePath, true);
                File.Delete(zipContainerFilePath);
                Directory.Delete(diImagesForHtmlApp.FullName, true);
                Response.End();
            }
        }
    }
}