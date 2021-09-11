using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using factory.lib;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace factory
{
    public partial class testgridview : System.Web.UI.Page
    {
        public List<string> g()
        {
            List<string> s = new List<string>();
            s.Add("3");
            s.Add("4");
            return s;
        }

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
        private List<string> GetHostIPAddress()
        {
            List<string> lstIPAddress = new List<string>();
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in IpEntry.AddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    lstIPAddress.Add(ipa.ToString());
            }
            return lstIPAddress; // result: 192.168.1.17 ......
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                //IP
                string i = GetIPAddress();
                HttpBrowserCapabilities browser = Request.Browser;
                //作業系統+版本
                OperatingSystem OSv = System.Environment.OSVersion;
                Response.Write("作業系統版本 : " + OSv.ToString());
                //瀏覽器+版本
                Response.Write(browser.Type);
                Response.Write("<br>");
                //取得行動裝置品牌
                Response.Write(browser.MobileDeviceModel);
                Response.Write("<br>");
                */
                //string x = GetIPAddress();
                //string i = Get_OS_Browser_DV();
                //IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                //Response.Write(SvrIP);
                /*
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
                */
                string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
                Response.Write(userAgent);
                /*
                if (agent.Length > 0)
                {
                    string userPhonetype = agent[1]; //其中，“/”拆分的第一项的内容就是用户的手机品牌信息
                    Response.Write(userAgent);
                }
                */
                //目前網頁
                Response.Write(System.IO.Path.GetFileName(Request.PhysicalPath));
            }
            
        }

    }
}