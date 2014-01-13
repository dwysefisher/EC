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

namespace Inovas.NetPrice
{
    public partial class Npc_6 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContext();            
        }

        protected void ibtnSubmit_Click(object sender, EventArgs e)
        {
            int percentage = 0;
            if (int.TryParse(Request["tbPercentage"], out percentage))
                AppContext.Percentage = percentage;
            else
                AppContext.Percentage = null;

            AppContext.Explanation1 = HttpUtility.HtmlEncode(Request["txt_information1"].Trim());
            AppContext.Explanation2 = HttpUtility.HtmlEncode(Request["txt_information2"].Trim());
            AppContext.Explanation3 = HttpUtility.HtmlEncode(Request["txt_information3"].Trim());

            SaveContext();
            Response.Redirect("Npc_7.aspx");
        }
}   

}