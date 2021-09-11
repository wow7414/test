using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using factory.lib;

namespace factory.Light
{
    public partial class Factory_state : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //取得資料顯示燈號
                SQLDB db = new SQLDB();
                string sql = "SELECT F.* FROM (SELECT TOP 8 * FROM Factory_State ORDER BY DTime DESC) AS F " +
                    "LEFT JOIN Factory AS Y ON F.FactoryID = Y.FactoryID ORDER BY Y.aOrder ASC";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                List<ImageButton> images = new List<ImageButton>();
                images.Add(imgb_KY);
                images.Add(imgb_BL);
                images.Add(imgb_QX);
                images.Add(imgb_ZB);
                images.Add(imgb_KH);
                images.Add(imgb_LD);
                images.Add(imgb_LZ);
                images.Add(imgb_HL);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == "True")
                    {
                        ImageButton img = images[i];
                        img.ImageUrl = "~/img/link.png";
                    }
                }
                Session["l"] = "SELECT * FROM [Factory_State] ORDER BY [DTime] DESC";
            }
        }

        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //分頁
            SDS1.SelectCommand = Session["l"].ToString();
            others o = new others();
            o.Page(sender, e);

        }

        protected void imgb_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            string f = clickedButton.ID.ToString();
            string sql = "";
            if (f == "imgb_KY")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'KY-T1HIST' ORDER BY DTime DESC";
            }
            else if (f == "imgb_BL")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'BL-T1HIST' ORDER BY DTime DESC";
            }
            else if (f == "imgb_QX")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'QX-T1HIST' ORDER BY DTime DESC";
            }
            else if (f == "imgb_ZB")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'ZB-T1HIST' ORDER BY DTime DESC";
            }
            else if (f == "imgb_KH")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'KH-PCC-LH' ORDER BY DTime DESC";
            }
            else if (f == "imgb_LD")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'LD-T1HIST' ORDER BY DTime DESC";
            }
            else if (f == "imgb_LZ")
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'LZ-T1HIST' ORDER BY DTime DESC";
            }
            else
            {
                sql = "SELECT * FROM Factory_State WHERE FactoryID = 'HL-T1HIST' ORDER BY DTime DESC";
            }
            GV1.PageIndex = 0;
            SDS1.SelectCommand = sql;
            Session["l"] = sql;
        }

        protected void GV1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                Label lb_img = (Label)GV1.Rows[i].FindControl("lb_light");
                Image img = (Image)GV1.Rows[i].FindControl("img_light");
                if (lb_img.Text == "True")
                {
                    img.ImageUrl = "~/img/link.png";
                }
                else
                {
                    img.ImageUrl = "~/img/disconnected.png";
                }
            }
        }
    }
}