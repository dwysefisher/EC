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

namespace Inovas.NetPrice
{
    public partial class Npc2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page is PageBase)
            {
                PageBase pageBase = (PageBase)this.Page;
                if (pageBase.AppContext.InstitutionType != InstitutionType.Unknown)
                    ltInstitutionType.Text = pageBase.AppContext.InstitutionType.ToString();
                if (!string.IsNullOrEmpty(pageBase.AppContext.YearText))
                    ltYearText.Text = pageBase.AppContext.YearText;

            }
        }

        /// <summary>
        /// Show or hide div with institution type and year
        /// </summary>
        public bool TitleWithInstitutionTypeAndYearVisible
        {
            set
            {
                phTitleWithInstitutionTypeAndYear.Visible = value;
            }
            get
            {
                return phTitleWithInstitutionTypeAndYear.Visible;
            }
        }
    }
}
