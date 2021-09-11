using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace factory.lib
{
    public class SQLDB
    {
        public String DBConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RuntimeConnStr"].ConnectionString;
        
        private static SqlConnection conn;
        public void replace(int i)
        {
            if (i == 1)
            {
                DBConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ZDBConnStr"].ConnectionString;
            }
        }
        public void BuildConn()
        {
            conn = new SqlConnection(DBConnStr);
        }

        public String ConnnectionTest()
        {

            String ErrorMsg = "";
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    conn.Close();
                    ErrorMsg = "正常!";
                }

            }
            catch (InvalidCastException e)
            {
                ErrorMsg = "資料庫連線異常，錯誤訊息=" + e;
            }
            return ErrorMsg;
        }

        private static void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public DataTable GetDataTable(string sql)
        {       //回傳值且沒有Parameters

            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            OpenConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            CloseConnection();
            return dt;
        }

        public DataTable GetDataTable(string sql, List<List<string>> par_list)
        {       //回傳值且有Parameters

            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            foreach (List<string> x in par_list)
            {
                cmd.Parameters.AddWithValue(x[0], x[1]);
            }
            OpenConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            CloseConnection();
            return dt;

        }

        public void RunCmd(string sql)
        {       //增刪改 沒Parameters
            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            OpenConnection();
            cmd.ExecuteReader();
            CloseConnection();
        }

        public void RunCmd(string sql, List<List<string>> par_list)
        {       //增刪改 有Parameters
            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            foreach (List<string> x in par_list)
            {
                cmd.Parameters.AddWithValue(x[0], x[1]);
            }
            OpenConnection();
            cmd.ExecuteReader();
            CloseConnection();

        }
    }
}