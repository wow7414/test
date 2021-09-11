using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;

namespace factory
{
    public partial class upd_p_data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            SQLDB db = new SQLDB();
            List<SqlParameter> p_list = new List<SqlParameter>();
            string sql = "SELECT * FROM S_User WHERE User_ID = @User_ID AND User_pwd = @User_pwd";
            p_list.Add(new SqlParameter("@User_ID", Session["User_ID"].ToString()));
            others o = new others();
            string en = o.encryption(tb_pwd.Text);
            p_list.Add(new SqlParameter("@User_pwd", en));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                if (tb_pwd.Text == tb_npwd.Text)
                {
                    lb_err.Text = "舊密碼不能跟新密碼一樣!";
                }
                else if (tb_npwd.Text != tb_cfpwd.Text)
                {
                    lb_err.Attributes.CssStyle.Add("margin-left", "50px");
                    lb_err.Attributes.CssStyle.Add("color", "red");
                    lb_err.Text = "確認密碼有誤!";
                }
                else
                {
                    p_list.Clear();
                    sql = "UPDATE S_User SET User_Pwd = @User_Pwd WHERE User_ID = @User_ID";
                    en = o.encryption(tb_npwd.Text);
                    p_list.Add(new SqlParameter("@User_Pwd", en));
                    p_list.Add(new SqlParameter("@User_ID", Session["User_ID"].ToString()));
                    db.RunCmd(sql,p_list,CommandType.Text);
                    lb_err.Attributes.CssStyle.Add("margin-left", "70px");
                    lb_err.Attributes.CssStyle.Add("color", "red");
                    lb_err.Text = "修改成功!";
                }
            }
            else
            {
                lb_err.Attributes.CssStyle.Add("margin-left", "70px");
                lb_err.Attributes.CssStyle.Add("color", "red");
                lb_err.Text = "密碼有誤!";
            }

        }
    }
}