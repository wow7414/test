using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Diagnostics;

namespace factory
{
    public partial class Tag_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            HyperLink h = (HyperLink)Master.FindControl("Tag_u");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
            
            Session["show"] = "1";
            Session["factory"] = "";

            HyperLink x = (HyperLink)Master.FindControl("Tag_acct");
            x.CssClass = "list-group-item list-group-item-action bg-light";
            */
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
            Response.Redirect("Tag_trend.aspx");
        }

        protected void ddl_fty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV1.PageIndex = 0;
        }

        protected void btn_month_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = Server.MapPath("process\\dist\\Value_30Day.schedule.exe");
            process.Start();
        }

        protected void btn_Trend_h_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tag_trend_h.aspx");
        }
    }
}