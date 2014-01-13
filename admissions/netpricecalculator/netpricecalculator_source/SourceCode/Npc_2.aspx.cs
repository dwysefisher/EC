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

namespace Inovas.NetPrice
{
    public partial class Npc_2 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContext();
            // hide div with institution type and year
            Master.TitleWithInstitutionTypeAndYearVisible = false;
            if (!IsPostBack)
                BindYears();
        }

        private void BindYears()
        {
            repYearList.DataSource = SqlHelper.GetDataTable("Select YearIndex, YearText from yearSelection order by YearIndex");
            repYearList.DataBind();
        }

        protected string CheckInstitutionType(string institutionTypeNumber)
        {
            string returnValue = "";
            if (AppContext.InstitutionType != InstitutionType.Unknown)
            {
                if (institutionTypeNumber == "1" && AppContext.InstitutionType == InstitutionType.Academic)
                    returnValue = "checked='checked'";
                if (institutionTypeNumber == "2" && AppContext.InstitutionType == InstitutionType.Program)
                    returnValue = "checked='checked'";
            }
            return returnValue;
        }

        protected string CheckYear(object objYearIndex)
        {
            if (objYearIndex != null && objYearIndex != DBNull.Value)
            {
                int yearIndex = int.Parse(objYearIndex.ToString());
                if (AppContext.YearIndex == yearIndex)
                    return "checked='checked'";
            }
            return "";
        }

        protected void ibtnContinue_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(Request["rbYearIndex"]) || string.IsNullOrEmpty(Request["rbInstitutionType"]))
                Response.Redirect(Request.Path);
            else
            {
                string[] tmpYearValues = Request["rbYearIndex"].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (tmpYearValues.Length == 2)
                {
                    this.AppContext.YearIndex = int.Parse(tmpYearValues[0]);
                    this.AppContext.YearText = tmpYearValues[1];
                    this.AppContext.InstitutionType = NetPriceUtils.ParseInstitutionType(Request["rbInstitutionType"].Trim());
                    SaveContext();


                    if (AppContext.InstitutionType == InstitutionType.Academic)
                        Response.Redirect("Npc_3.aspx");
                    else if (AppContext.InstitutionType == InstitutionType.Program)
                        Response.Redirect("Npc_4.aspx");
                    else
                        Response.Redirect(Request.Path);
                }
                else
                    Response.Redirect(Request.Path);
            }
        }
    }

}