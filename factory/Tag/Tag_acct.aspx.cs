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
    public partial class Tag_acct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //被選中的選單
            HyperLink h = (HyperLink)Master.FindControl("Tag_s");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";

            //icon方向
            HyperLink x = (HyperLink)Master.FindControl("Tag_acct");
            x.CssClass = "list-group-item list-group-item-action bg-light";
            */
            if (!IsPostBack)
            {
                SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName  like  '%KY-T1HIST%' AND L.TagName not like '%$%' escape '/' order by L.DateTime desc";
                Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName  like  '%KY-T1HIST%' AND L.TagName not like '%$%' escape '/' order by L.DateTime desc";
            }
            
        }
        
        protected void btn_select_Click(object sender, EventArgs e)
        {
            if (cbl_check.Items[0].Selected)
            {
                //SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE T.Description = '" + tb_Desc.Text + tb_tag.Text + tb_source.Text + "' order by L.DateTime desc";
                if (tb_Desc.Text != "")
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }
                else if (tb_source.Text != "")
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                }
                else
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                }
            }
            else
            {
                if (tb_Desc.Text != "")
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "'  AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "'  AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }

                else if (tb_source.Text != "")
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null  order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null  order by L.DateTime desc";
                }
                else
                {
                    SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }
            }
        }

        protected void ddl_display_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GV1.PageSize = Convert.ToInt32(ddl_display.SelectedItem.Text);
            SDS2.SelectCommand = Session["page"].ToString();
        }
        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string i = Convert.ToString(Session["page"]);
            SDS2.SelectCommand = i;
            others o = new others();
            o.Page(sender, e);

        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GV1.SelectedRow;
            DataTable dt = new DataTable();
            if (GV2.Rows.Count == 0)
            {
                dt.Columns.Add("ServerName", typeof(string));
                dt.Columns.Add("TagName", typeof(string));
                dt.Columns.Add("SourceTag", typeof(string));
                dt.Columns.Add("TagDesc", typeof(string));
                //dt.Columns.Add("Rep_Live", typeof(CheckBox));
                dt.Columns.Add("Rep_Hour", typeof(CheckBox));
                dt.Columns.Add("Rep_Min", typeof(CheckBox));
            }
            else
            {
                dt = ViewState["GV2"] as DataTable;
            }

            Label lb_TagName = (Label)GV1.Rows[GV1.SelectedIndex].FindControl("lb_TagName");
            string TagName = lb_TagName.Text;
            string SourceTag = row.Cells[3].Text;
            string TagDesc = row.Cells[1].Text;
            if (row.Cells[2].Text == "&nbsp;")
            {
                TagName = " ";
            }

            if (row.Cells[3].Text == "&nbsp;")
            {
                SourceTag = " ";
            }

            if (row.Cells[1].Text == "&nbsp;")
            {
                TagDesc = " ";
            }
            DataRow NewRow = dt.NewRow();
            string ServerName = TagName.Substring(0, 9);
            NewRow["ServerName"] = ServerName;
            NewRow["TagName"] = TagName;
            NewRow["SourceTag"] = SourceTag;
            NewRow["TagDesc"] = TagDesc;

            int x = 0;
            if (GV2.Rows.Count == 0)
            {
                dt.Rows.Add(NewRow);
            }
            else
            {
                for (int i = 0; i < GV2.Rows.Count; i++)
                {
                    if (GV2.Rows[i].Cells[3].Text == SourceTag)
                    {
                        x++;
                    } 
                }
                if (x == 0)
                {
                    dt.Rows.Add(NewRow);
                }
            }
           
            ViewState["GV2"] = dt;
            GV2.DataSource = dt;
            GV2.DataBind();
            btn_confirm.Visible = true;
        }

        protected void GV2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            for (int i = 0; i < GV2.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = GV2.Columns[i].HeaderText;
                dt.Columns.Add(dc);
            }
            for (int i = 0; i < GV2.Rows.Count; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < GV2.Columns.Count; j++)
                {
                    dr[j] = GV2.Rows[i].Cells[j].Text;
                }
                dt.Rows.Add(dr);
            }
            dt.Rows[GV2.SelectedIndex].Delete();
            ViewState["GV2"] = dt;
            GV2.DataSource = dt;
            GV2.DataBind();
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            SQLDB db = new SQLDB();

            for (int i = 0; i < GV2.Rows.Count; i++)
            {
                //CheckBox Rep_Live = (CheckBox)GV2.Rows[i].FindControl("cb_Rep_Live");
                CheckBox Rep_Hour = (CheckBox)GV2.Rows[i].FindControl("cb_Rep_Hour");
                CheckBox Rep_Min = (CheckBox)GV2.Rows[i].FindControl("cb_Rep_Min");
                bool bl_Rep_Live = false;
                bool bl_Rep_Hour = false;
                bool bl_Rep_Min = false;
                /*
                if (Rep_Live.Checked)
                {
                    bl_Rep_Live = true;
                }
                */
                if (Rep_Hour.Checked)
                {
                    bl_Rep_Hour = true;
                }
                if (Rep_Min.Checked)
                {
                    bl_Rep_Min = true;
                }
                string ServerName = GV2.Rows[i].Cells[1].Text;
                string TagName = GV2.Rows[i].Cells[2].Text;
                string SourceTag = GV2.Rows[i].Cells[3].Text;
                string TagDesc = GV2.Rows[i].Cells[4].Text;

                string sql = "SELECT isnull(MAX(tag_order)+1,1) FROM TagList";
                DataTable dt = db.GetDataTable(sql,CommandType.Text);

                sql = "IF NOT EXISTS (SELECT * FROM TagList WHERE TagName = '"+TagName+ "') BEGIN INSERT INTO TagList VALUES ('"+ServerName+"', '"+TagName+"','"+SourceTag+"','"+TagDesc+"','"+ bl_Rep_Live + "','"+ bl_Rep_Hour + "','"+ bl_Rep_Min + "','"+dt.Rows[0][0].ToString()+"') END";
               
                db.RunCmd(sql,CommandType.Text);
            }
            Response.Redirect("Tag_Modify.aspx");
        }

        protected void ddl_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string factory = ddl_factory.SelectedValue;
            SDS2.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName  like '" + '%' + factory + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
            Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName  like '" + '%' + factory + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
            GV1.PageIndex = 0;
        }
    }
}