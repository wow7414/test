using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Text.RegularExpressions;
namespace factory
{
    public partial class Tag_trend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session["unit"] = "'hour'";
                Session["displayFormats"] = "hour: 'YYYY-MM-DD HH:mm'";
                Session["min"] = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                Session["max"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                Session["stepSize"] = 12;

                //取得時間
                SQLDB db = new SQLDB();
                string time_s = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                string time_e = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00");
                string sql = "SELECT DataDateTime FROM Value_Min WHERE SourceServer like '%KY-T1HIST%' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";
                
                DataTable dt =  db.GetDataTable(sql,CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    //取得TagName
                    sql = "SELECT TagName FROM Value_Min WHERE SourceServer like '%KY-T1HIST%' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY TagName";
                    dt = db.GetDataTable(sql,CommandType.Text);
                    List<string> myLists = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        myLists.Add(dt.Rows[i][0].ToString());
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string namelists = oSerializer.Serialize(myLists);
                    Session["TagName"] = namelists;
                    Session["count_data"] = dt.Rows.Count;

                    //取得data
                    List<List<string>> par_list = new List<List<string>>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "select * from Value_Min where TagName = '" + dt.Rows[i][0].ToString() + "'AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "'";
                        DataTable dx = db.GetDataTable(sql,CommandType.Text);
                        
                        string d = "";
                        for (int j = 0; j < dx.Rows.Count; j++)
                        {
                            string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm");    
                            d += "{x:" + "'" +datatime + "'" +",y:"+dx.Rows[j][3].ToString() + "\"},";
                        }
                        par_list.Add(new List<string>() {d});
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string datas = o.Serialize(par_list);
                    datas = datas.Replace("\\", "");
                    datas = datas.Replace("\"", "");
                    datas = datas.Replace("u0027","'");
                    Session["datas"] = datas;
                    //Response.Write(datas);
                }
                else
                {
                    lb_err.Visible = true;
                }
                
                
            }
            /*
            HyperLink h = (HyperLink)Master.FindControl("Tag_t");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
            //select Datename(hour,DataDateTime)+':'+ datename(minute,DataDateTime) from Value_Min
            Session["show"] = "1";
            Session["factory"] = "";
            HyperLink x = (HyperLink)Master.FindControl("Tag_acct");
            x.CssClass = "list-group-item list-group-item-action bg-light";
            */
        }

        protected void btn_confrim_Click(object sender, EventArgs e)
        {

            DateTime date_1 = new DateTime(Convert.ToInt32(tb_EDATE.Text.Substring(0,4)), Convert.ToInt32(tb_EDATE.Text.Substring(5, 2)), Convert.ToInt32(tb_EDATE.Text.Substring(8, 2)));
            DateTime date_2 = new DateTime(Convert.ToInt32(tb_SDATE.Text.Substring(0,4)), Convert.ToInt32(tb_SDATE.Text.Substring(5, 2)), Convert.ToInt32(tb_SDATE.Text.Substring(8, 2)));
            int c = date_1.Subtract(date_2).Days;

            //取得時間
            SQLDB db = new SQLDB();
            string time_s = DateTime.Now.ToString("'" + tb_SDATE.Text + "' 00:00:00");
            string time_e = Convert.ToDateTime(tb_EDATE.Text).AddDays(1).ToString("yyyy-MM-dd 00:00:00");
            
            string sql = "SELECT DataDateTime FROM Value_Min WHERE SourceServer like " + "'%" + ddl_fty.SelectedValue + "%'" + " AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";
            if (c > 0 && c < 4)
            {
                Session["unit"] = "'hour'";
                Session["displayFormats"] = "hour: 'YYYY-MM-DD HH:mm'";
                Session["min"] = time_s;
                Session["max"] = time_e;
                Session["stepSize"] = 12;
            }
            else if (c < 30)
            {
                Session["unit"] = "'day'";
                Session["displayFormats"] = "day: 'YYYY-MM-DD'";
                Session["min"] = time_s;
                Session["max"] = time_e;
                Session["stepSize"] = 0;
            }
            else
            {
                Session["unit"] = "'month'";
                Session["displayFormats"] = "month: 'YYYY-MM'";
                Session["min"] = DateTime.Now.ToString("'" + tb_SDATE.Text.Substring(0,7)+"'");
                Session["max"] = Convert.ToDateTime(tb_EDATE.Text).AddDays(1).ToString("yyyy-MM");
                Session["stepSize"] = 0;
            }

            DataTable dt = db.GetDataTable(sql,CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                //取得TagName
                sql = "SELECT TagName FROM Value_Min WHERE SourceServer like '" + '%' + ddl_fty.SelectedValue + '%' + "' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY TagName";
                dt = db.GetDataTable(sql,CommandType.Text);
                List<string> myLists = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myLists.Add(dt.Rows[i][0].ToString());
                }
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string namelists = oSerializer.Serialize(myLists);
                Session["TagName"] = namelists;
                Session["count_data"] = dt.Rows.Count;
                
                //取得data
                List<List<string>> par_list = new List<List<string>>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "select * from Value_Min where TagName = '" + dt.Rows[i][0].ToString() + "'AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "'";
                    DataTable dx = db.GetDataTable(sql,CommandType.Text);

                    string d = "";
                    for (int j = 0; j < dx.Rows.Count; j++)
                    {
                        string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm");
                        d += "{x:" + "'" + datatime + "'" + ",y:" + dx.Rows[j][3].ToString() + "\"},";
                    }
                    par_list.Add(new List<string>() { d });
                }
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(par_list);
                datas = datas.Replace("\\", "");
                datas = datas.Replace("\"", "");
                datas = datas.Replace("u0027", "'");
                Session["datas"] = datas;
                lb_err.Visible = false;
                    //Response.Write(datas);
                }
            else
            {
                lb_err.Visible = true;
            }
        }  
    }
}