using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;


namespace factory.factory_index
{
    public partial class index : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public String doughnut()
        {
            if (lb_power.Text != "無資料" && lb_power.Text != "0")
            {
                Double power = Math.Round(Convert.ToDouble(lb_power.Text.Replace(",", "")), 0);
                List<List<string>> r_list = new List<List<string>>();
                List<string> par_list = new List<string>();
                List<string> name = new List<string>();
                List<string> lb_list = new List<string>();
                List<string> div_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    //新增前端8個Label的名稱
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                    //新增前端8個div的名稱
                    string d = "d" + j.ToString();
                    div_list.Add(d);
                }
                //資料
                Double other = 100;
                for (int i = 0; i < 8; i++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[i]);
                    if (div.Visible == true)
                    {
                        Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                        Double d = Convert.ToDouble(lb.Text.Replace(",", ""));
                        Double r = Math.Round(d / power * 100, 0);
                        other -= r;
                        par_list.Add(r.ToString());
                        string n = "#" + (i + 1).ToString() + " " + r.ToString() + "%";
                        name.Add(n);
                    }
                }
                par_list.Add(other.ToString());
                name.Add("#其它" + " " + other.ToString() + "%");

                r_list.Add(par_list);
                r_list.Add(name);
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(r_list);
                return datas;
            }
            else
            {
                return"";
            }
        }

        

        //顯示畫面和資料
        public void data_visible(string Month)
        {
            
            string F = Request.QueryString["F"];
            //給DIV超連結
            string D = tb_SDATE.Text.Replace("-", "/");
            link_d.Attributes.Add("onclick", "location.href='/power/Power_D.aspx?F="+F+"&D="+D+"'");
            
            SQLDB db = new SQLDB();
            //判斷哪些機器的資料要顯示
            //先去取得該工廠有哪些機器
            string sql = "SELECT * FROM G_Power_Mapping WHERE FactoryID = @F_ID";
            List<SqlParameter> p_list = new List<SqlParameter>();
            List<string> s_list = new List<string>();
            List<string> div_list = new List<string>();
            p_list.Add(new SqlParameter("@F_ID", F));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < 8; i++)
                {
                    //其它8台機器
                    if (dt.Rows[0][i*2 + 9].ToString() != "")
                    {
                        s_list.Add(dt.Rows[0][i*2 + 9].ToString());
                    }
                    //新增前端8個div的名稱
                    string d = "d" + (i+1).ToString();
                    div_list.Add(d);
                }
            }

            //隱藏沒有資料的
            if (s_list.Count > 0)
            {
                for (int k = 0; k < 8 - s_list.Count; k++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[7 - k]);
                    div.Visible = false;
                }
            }
            
            //顯示該工廠的機器資料
            string DMonth = Month.Replace("-", "");
            sql = "SELECT * FROM G_Power_M WHERE FactoryID = @F_ID AND DMonth = @DMonth";
            p_list.Clear();
            p_list.Add(new SqlParameter("@F_ID", F));
            p_list.Add(new SqlParameter("@DMonth", DMonth));
            dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                //新增前端8個Label的名稱
                List<string> lb_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                }
                //用電量
                lb_power.Text = "0";
                if (dt.Rows[0][15].ToString() != "")
                {
                    lb_power.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[0][15].ToString()));
                }
                //機器資料
                for (int i = 0; i < s_list.Count; i++)
                {
                    string d = dt.Rows[0][19 + i].ToString();
                    Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                    lb.Text = "0";
                    if (d != "")
                    {
                        lb.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(d));
                    }
                }
            }
            //無資料給0
            else
            {
                List<string> lb_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                }
                lb_power.Text = "無資料";
                for (int i = 0; i < 8; i++)
                {
                    Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                    lb.Text = "無資料";
                }
            }
            //廠區無資料全部隱藏
            /*
            else
            {
                List<string> div_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "d" + j.ToString();
                    div_list.Add(n);
                }
                Control d = Page.Master.FindControl("ContentPlaceHolder1").FindControl("d");
                d.Visible = false;
                Control div_power = Page.Master.FindControl("ContentPlaceHolder1").FindControl("div_power");
                div_power.Visible = false;
                for (int k = 0; k < 8; k++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[k]);
                    div.Visible = false;
                }
            }
            */
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                string time_s = DateTime.Now.AddDays(-3).ToString("yyyy-MM");
                tb_SDATE.Text = time_s;
                string F = Request.QueryString["F"];
                string name = ff.factory_name(F);
                if (name == "")
                {
                    Response.Redirect("/factory_index/index.aspx?F=KY-T1HIST");
                }
                lb_factory.Text = name;
                //取得工廠機器資料並顯示
                if (Session["p"] == null)
                {
                    data_visible(tb_SDATE.Text);
                }
                else
                {
                    tb_SDATE.Text = Session["p"].ToString();
                    data_visible(Session["p"].ToString());
                }
                //判斷當前月份 停用/啟動 下一個月的功能
                ff.En_imgb_n(tb_SDATE.Text, imgb_n);

                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";

            }
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_ym(n,tb_SDATE);
            data_visible(n);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text,imgb_n);
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_ym(n, tb_SDATE);
            string time_s = Convert.ToDateTime(n).AddMonths(-1).ToString("yyyy-MM");
            tb_SDATE.Text = time_s;
            data_visible(time_s);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text, imgb_n);
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_ym(n, tb_SDATE);
            string time_s = tb_SDATE.Text;
            time_s = Convert.ToDateTime(time_s).AddMonths(1).ToString("yyyy-MM");
            tb_SDATE.Text = time_s;
            data_visible(time_s);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text, imgb_n);

        }

        protected void imgb_pdf_Click(object sender, ImageClickEventArgs e)
        {
            //https://www.cc.ntu.edu.tw/chinese/epaper/0015/20101220_1509.htm
            var doc1 = new Document(PageSize.A4, 50, 50, 80, 50);
            MemoryStream memory = new MemoryStream();
            PdfWriter pdfw = PdfWriter.GetInstance(doc1, memory);

            doc1.Open();
            doc1.Add(new Paragraph(10f, "Hello, 大家好"));
            doc1.Close();

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "test" + ".pdf");
            //增加HTTP表頭讓EDGE可以用
            Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
            Response.OutputStream.Write(memory.GetBuffer(), 0, memory.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush(); //== 不寫這兩段程式，輸出EXCEL檔並開啟後，會出現檔案內容混損，需要修復的字母
            Response.End();


        }
    }
}