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

namespace Inovas.NetPrice
{
    public partial class Npc_4 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContext();

            #region Check for previous inputs
            if (AppContext.YearIndex == -1 || AppContext.InstitutionType == InstitutionType.Unknown)
                Response.Redirect("Npc_2.aspx");
            #endregion

            // Clear some properties from AppContext
            AppContext.InstitutionChargeDifferentTuition = null;
            AppContext.ChargeForInDistrict = null;
            AppContext.ChargeForInState = null;

            RegisterJavaScriptArrayWithPrograms();
        }

        protected void ibtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["rb_q1"]) || string.IsNullOrEmpty(Request["tbNumberOfMonths"]) || string.IsNullOrEmpty(Request["tbLargestProgram"]))
                Response.Redirect("Npc_4.aspx?errorCode=EmptyQuestion");
            else
            {

                AppContext.InstitutionallyControlledHousingOffered = ParseRadioButtonValue(Request["rb_q1"]);
                if (AppContext.InstitutionallyControlledHousingOffered == true)
                    AppContext.StudentsRequiredLiveOnCampusOrHousing = ParseRadioButtonValue(Request["rb_q2"]);
                else
                    AppContext.StudentsRequiredLiveOnCampusOrHousing = null;

                AppContext.LargestProgram = Server.HtmlEncode(Request["tbLargestProgram"].Trim());
                int numberOfMonths;
                if (int.TryParse(Request["tbNumberOfMonths"], out numberOfMonths))
                    AppContext.NumberOfMonths = numberOfMonths;
                else
                    Response.Redirect(Request.Path);
                

                SaveContext();
                Response.Redirect("Npc_5.aspx");
            }
        }

        private void RegisterJavaScriptArrayWithPrograms()
        {
            StringBuilder sbJS = new StringBuilder();
            using (SqlDataReader reader = SqlHelper.GetDataReader("Select CipCode, ProgramName from ProgramList order by ProgramName asc"))
            {
                while (reader.Read())
                {                    
                    ClientScript.RegisterArrayDeclaration("arrProgramNames", "'" + reader["ProgramName"].ToString().Replace("'", "\'") + "'");
                }
            }
        }

        /// <summary>
        /// Parse radio button value: y equal to TRUE, n equal to FALSE
        /// </summary>
        /// <param name="rbValue"></param>
        /// <returns></returns>
        private bool ParseRadioButtonValue(string rbValue)
        {
            if (rbValue.Trim().ToLower() == "y")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get boolean value if checkbox exists in Request object
        /// </summary>
        /// <param name="cbValue"></param>
        /// <returns></returns>
        private bool? GetCheckboxBooleanValueIfExists(string cbValue)
        {
            bool? returnValue = null;
            if (!string.IsNullOrEmpty(cbValue))
                returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// Method returns checked='checked' if AppContext has value for this control
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        protected string GetCheckedStatus(string inputName, string inputValue)
        {
            string checkedAttr = "checked='checked'";
            string emptyAttr = "";
            
            // Question 1
            if (inputName.ToLower() == "rb_q1")
            {
                if (inputValue.ToLower() == "y" && AppContext.InstitutionallyControlledHousingOffered == true)
                    return checkedAttr;
                else if (inputValue.ToLower() == "n" && AppContext.InstitutionallyControlledHousingOffered == false)
                    return checkedAttr;
                else
                    return emptyAttr;
                
            }

            // Question 2
            if (inputName.ToLower() == "rb_q2")
            {
                if (AppContext.InstitutionallyControlledHousingOffered == true)
                {
                    if (inputValue.ToLower() == "y" && AppContext.StudentsRequiredLiveOnCampusOrHousing == true)
                        return checkedAttr;
                    else if (inputValue.ToLower() == "n" && AppContext.StudentsRequiredLiveOnCampusOrHousing == false)
                        return checkedAttr;
                    else
                        return emptyAttr;
                }
                else
                    return emptyAttr;
            }

            // Question 3
            if (inputName.ToLower() == "rb_q3")
            {
                if (inputValue.ToLower() == "y" && AppContext.InstitutionChargeDifferentTuition == true)                
                    return checkedAttr;
                else if (inputValue.ToLower() == "n" && AppContext.InstitutionChargeDifferentTuition == false)
                    return checkedAttr;
                else
                    return emptyAttr;
            }

            // In-district, In-state checking
            if (inputName.ToLower() == "cb_indistrict")
            {
                if (AppContext.InstitutionChargeDifferentTuition == true)
                {
                    if (AppContext.ChargeForInDistrict == true)
                        return checkedAttr;
                    else
                        return emptyAttr;
                }
            }
            if (inputName.ToLower() == "cb_instate")
            {
                if (AppContext.InstitutionChargeDifferentTuition == true)
                {
                    if (AppContext.ChargeForInState == true)
                        return checkedAttr;
                    else
                        return emptyAttr;
                }
            }

            return "";
        }

        /// <summary>
        /// Get string value for textobx if AppContext has value
        /// </summary>
        /// <param name="tbControlName"></param>
        /// <returns></returns>
        protected string GetTextboxValue(string tbControlName)
        {
            string result = "";
            if (tbControlName.ToLower() == "tbnumberofmonths" && AppContext.NumberOfMonths.HasValue)
                result = AppContext.NumberOfMonths.Value.ToString();
            else if (tbControlName.ToLower() == "tblargestprogram" && !string.IsNullOrEmpty(AppContext.LargestProgram))
                result = AppContext.LargestProgram;
            
            return result;
        }
    }

}