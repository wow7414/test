using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;

namespace factory
{
    public partial class Tag_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink h = (HyperLink)Master.FindControl("Tag_acct");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark";
        }

        protected void ddl_display_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV1.PageSize = Convert.ToInt32(ddl_display.SelectedItem.Text);
        }

        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            others o = new others();
            o.Page(sender, e);
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tag_report.aspx");
        }

        protected void btn_Trend_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tag_report.aspx");
        }
    }
}