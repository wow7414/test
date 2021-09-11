using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;
using System.Net;
using System.Text.RegularExpressions;

namespace factory.Check
{
    public partial class add_data : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        protected string GetIPAddress_real()
        {
            string SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address).ToString();
            return SvrIP;
        }

        protected string Get_OS_Browser_DV()
        {
            HttpBrowserCapabilities browser = Request.Browser;
            OperatingSystem OSv = System.Environment.OSVersion;
            string data = OSv + "," + browser.Type + "," + browser.MobileDeviceModel;
            return data;
        }
        public string Get_userAgent()
        {
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            return userAgent;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string F = Request.QueryString["F"];
                //判斷網址
                if (F != "KY" && F != "BL" && F != "QX" && F != "ZB" && F != "KH" && F != "LD" && F != "LZ" && F != "HL")
                {
                    Response.Redirect("add_data.aspx?F=KY");
                }
                //取得各廠區的機器號碼
                string sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID LIKE @F+'%'"; ;
                SQLDB db = new SQLDB();
                List<SqlParameter> p_list = new List<SqlParameter>();
                p_list.Add(new SqlParameter("@F", F));
                DataTable dt = db.GetDataTable(sql,p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rbl_M.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
                    }
                    sql = "SELECT * FROM A_Product";
                    dt = db.GetDataTable(sql, CommandType.Text);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rbl_P.Items.Add(new ListItem("("+dt.Rows[i][0].ToString()+")"+dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                    }
                    rbl_M.Items[0].Selected = true;
                    rbl_P.Items[0].Selected = true;
                }
                else
                {
                    //如果沒有機器號碼則整個DIV隱藏
                    data.Visible = false;
                    datas.Visible = false;
                }
                //提示的DIV
                tip.Visible = false;

                //手機超連結和名稱
                string name = ff.f_name(F);
                
                hyl_factory.Text = name + " ＞";
                hyl_factory.NavigateUrl = "../phone/Factory_" + F + ".aspx";
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (tb_M.Text == "" && tb_R.Text == "" && tb_S.Text == "")
            {
                lb_error.Visible = true;
                btn_save.Attributes.Add("style", "margin-left:20px");
            }
            else
            {
                string F = Request.QueryString["F"];
                string m = rbl_M.SelectedValue;
                string p = rbl_P.SelectedValue;
                string data = GetIPAddress_real() + "," + GetIPAddress() + "," + Get_userAgent();

                //判斷是否有在同一小時內輸入資料 前一筆資料要給Visible X
                string sql = "SELECT TOP 1 FactoryID,Mill_ID, DTime FROM A_Product_Quality WHERE FactoryID LIKE @F+'%' AND Mill_ID = @M_ID " +
                    "AND DateTime = convert(char(13),GetDate(),120)+':00:00'";
                SQLDB db = new SQLDB();
                List<SqlParameter> p_list = new List<SqlParameter>();
                p_list.Add(new SqlParameter("@F", F));
                p_list.Add(new SqlParameter("@M_ID", m));
                //先修改再來新增!
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    p_list.Clear();
                    sql = "UPDATE A_Product_Quality SET Visible = 'X' WHERE FactoryID = @F AND Mill_ID = @M AND DTime >= @D";
                    p_list.Add(new SqlParameter("@F", dt.Rows[0][0].ToString()));
                    p_list.Add(new SqlParameter("@M", dt.Rows[0][1].ToString()));
                    string timeStr = dt.Rows[0][2].ToString();
                    DateTime x = new DateTime();
                    DateTime.TryParse(timeStr, out x);
                    p_list.Add(new SqlParameter("@D", x.ToString("yyyy-MM-dd HH:mm:ss")));
                    db.RunCmd(sql, p_list, CommandType.Text);
                }
                //bool ym = Regex.IsMatch(n, "^(([1-9]{1}\\d *)|(0{1}))(\\.\\d{ 0,2})?$");
                object tb_M_V = Regex.IsMatch(tb_M.Text, "^(([1-9]{0,2}\\d *)|(0{1}))(\\.\\d{ 0,2})?$") ? tb_M.Text : (object)DBNull.Value;
                object tb_S_V = Regex.IsMatch(tb_S.Text, "^(([1-9]{0,2}\\d *)|(0{1}))(\\.\\d{ 0,2})?$") ? tb_S.Text : (object)DBNull.Value;
                object tb_R_V = Regex.IsMatch(tb_R.Text, "^(([1-9]{0,2}\\d *)|(0{1}))(\\.\\d{ 0,2})?$") ? tb_R.Text : (object)DBNull.Value;


                sql = "INSERT INTO A_Product_Quality(FactoryID,Mill_ID,DTime,DateTime,ProductID,Moisture,Specific_Surface,Residual_On_Sieve,Visible,Client_Info)" +
                    " VALUES(@F,@M_ID,GETDATE(),convert(char(13),GetDate(),120)+':00:00',@P,@M,@S,@R,NULL,@data)";
                p_list.Clear();
                p_list.Add(new SqlParameter("@F", F));
                p_list.Add(new SqlParameter("@M_ID", m));
                p_list.Add(new SqlParameter("@P", p));
                p_list.Add(new SqlParameter("@M", tb_M_V));
                p_list.Add(new SqlParameter("@S", tb_S_V));
                p_list.Add(new SqlParameter("@R", tb_R_V));
                p_list.Add(new SqlParameter("@data", data));
                db.RunCmd(sql, p_list, CommandType.Text);
                lb_M1.Text = "水份 " + tb_M.Text + " %";
                lb_M2.Text = "比表面積 " + tb_S.Text + " ㎡/kg";
                lb_M3.Text = "篩餘 " + tb_R.Text + " %";
                tip.Visible = true;
                //儲存完後清空欄位
                tb_M.Text = "";
                tb_R.Text = "";
                tb_S.Text = "";
            }


        }
    }
}