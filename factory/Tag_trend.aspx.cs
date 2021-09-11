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
    public partial class Tag_trend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink h = (HyperLink)Master.FindControl("Tag_acct");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark";
            //select Datename(hour,DataDateTime)+':'+ datename(minute,DataDateTime) from Value_Min

            if (!IsPostBack)
            {
                int ddl_s_list = Convert.ToInt32(DateTime.Now.ToString("HH"));
                for (int i = 0; i < ddl_s_list +1; i++)
                {
                    ddl_s.Items.Add(i.ToString());
                }
                ddl_s.Items[ddl_s.Items.Count - 1].Selected = true;
                //取得時間
                SQLDB db = new SQLDB();
                db.replace(1);
                string time_s = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
                string time_e = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00");
                string sql = "SELECT DataDateTime FROM Value_Min WHERE DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";

                DataTable dt =  db.GetDataTable(sql);
                string t = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime dd = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    t += '\''+dd.ToString("HH:mm")+'\''+',';
                }
                t = t.Substring(0, t.Length - 1);
                Session["t"] = t;

                //取得TagName
                sql = "SELECT TagName FROM Value_Min GROUP BY TagName";
                dt = db.GetDataTable(sql);
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
                    sql = "select * from Value_Min where TagName = '"+dt.Rows[i][0].ToString()+ "'AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "'";
                    DataTable dx = db.GetDataTable(sql);
                    string d = "";
                    for (int j = 0; j < dx.Rows.Count; j++)
                    {

                        if (j == 0)
                        {
                            d += dx.Rows[j][3].ToString() + "\",";
                        }
                        else if (j == dx.Rows.Count - 1)
                        {
                            d += "\"" + dx.Rows[j][3].ToString();
                        }
                        else
                        {
                            d += "\""+dx.Rows[j][3].ToString()+"\",";
                        }
                    }
                    par_list.Add(new List<string>() { d });
                }
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(par_list);
                datas = datas.Replace("\\", "");
                Session["datas"] = datas;
            }

        }

        protected void btn_confrim_Click(object sender, EventArgs e)
        {

            //取得時間
            SQLDB db = new SQLDB();
            db.replace(1);
            string time_s = DateTime.Now.ToString("yyyy-MM-dd"+" "+ddl_s.SelectedValue +":00:00");
            string time_e = Convert.ToDateTime(time_s).AddHours(1).ToString("yyyy-MM-dd HH:00:00");
            string sql = "SELECT DataDateTime FROM Value_Min WHERE DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";

            
            DataTable dt = db.GetDataTable(sql);
            string t = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime dd = Convert.ToDateTime(dt.Rows[i][0].ToString());
                t += '\'' + dd.ToString("HH:mm") + '\'' + ',';
            }
            t = t.Substring(0, t.Length - 1);
            Session["t"] = t;

            //取得TagName
            sql = "SELECT TagName FROM Value_Min WHERE DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY TagName";
            dt = db.GetDataTable(sql);
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
                DataTable dx = db.GetDataTable(sql);
                string d = "";
                for (int j = 0; j < dx.Rows.Count; j++)
                {

                    if (j == 0)
                    {
                        d += dx.Rows[j][3].ToString() + "\",";
                    }
                    else if (j == dx.Rows.Count - 1)
                    {
                        d += "\"" + dx.Rows[j][3].ToString();
                    }
                    else
                    {
                        d += "\"" + dx.Rows[j][3].ToString() + "\",";
                    }
                }
                par_list.Add(new List<string>() { d });
            }
            System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
            string datas = o.Serialize(par_list);
            datas = datas.Replace("\\", "");
            Session["datas"] = datas;
            
            }
    }
}