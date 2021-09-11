using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Timers;

namespace factory
{
    public partial class Tag_report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink h = (HyperLink)Master.FindControl("Tag_acct");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark";
       
        }
    }
}