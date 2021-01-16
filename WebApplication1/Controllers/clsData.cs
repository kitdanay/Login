using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Net;

public class clsData
{
    public DataTable sqlDataTable(string sql_)
    {
        string connString = ConfigurationManager.ConnectionStrings["RegisterPR9"].ToString();
        NpgsqlConnection conn = new NpgsqlConnection(connString);
        NpgsqlCommand cmd;//= new SqlCommand(sql_.ToString());
        NpgsqlDataAdapter da = new NpgsqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd = new NpgsqlCommand(sql_.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            if (da != null)
            {
                da.Dispose();
                da = null;
            }

            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
        }
    }
    public Boolean sqlExecute(string sql)
    {
        string connString = ConfigurationManager.ConnectionStrings["RegisterPR9"].ToString();
        NpgsqlConnection conn = new NpgsqlConnection(connString);
        try
        {

            conn.Open();
            NpgsqlCommand addCmd = new NpgsqlCommand(sql, conn);
            addCmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        catch (Exception ex)
        {


            // System.Console.Write(me.Message);

            return false;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }


        }
    }
    public string Return(string strSql)
    {
        #region Remark
        /*############################ Example ############################
        รัน Query เพื่อคืนค่าค่าเดียว
        clsSQL.Return("SELECT MAX(id) FROM member","MySQL","cs");
        #################################################################*/
        #endregion

        string csSQL = ConfigurationManager.ConnectionStrings["RegisterPR9"].ToString();
        //string csSQL = "Host=10.99.23.182;Port=5432;Username=postgres;Password=P@11681168;Database=dashqueue_dev";
        string strReturn = "";

        if (!string.IsNullOrEmpty(csSQL))
        {

            NpgsqlConnection myConn_SQL = new NpgsqlConnection(csSQL);
            NpgsqlCommand myCmd_SQL = new NpgsqlCommand(strSql, myConn_SQL);
            try
            {
                myConn_SQL.Open();
                strReturn = myCmd_SQL.ExecuteScalar().ToString();
                myConn_SQL.Close();
                myCmd_SQL.Dispose();
            }
            catch (Exception ex)
            {
                //Queuelogcontroller cs = new Queuelogcontroller();
                //cs.InsertQueueLog("", "", "0", ex.ToString().Replace("'", "''"), strSql);
                //cs = null;
                ex.Message.ToString();
            }
            finally
            {
                myCmd_SQL.Dispose();
                myConn_SQL.Close();
            }
        }
        return strReturn;
    }

}
