using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN.WF.RYDB.Demo.DB
{
    class DBHelper
    {
        public static SQLiteConnection My_Conn;
        //Data Source是数据库的地址，Version是数据库的版本
        public static string connectionstr = string.Format("Data Source=food.db;Version=3");

        public static SQLiteConnection getcon()
        {
            My_Conn = new SQLiteConnection(connectionstr);
            My_Conn.Open();
            return My_Conn;
        }

        public void con_open()
        {
            getcon();
        }

        public void conn_close()
        {
            if (My_Conn.State == ConnectionState.Open)
            {
                My_Conn.Close();
                My_Conn.Dispose();
            }
        }

        public SQLiteDataReader getsdr(string sqlstr)
        {
            getcon();
            SQLiteCommand My_com = My_Conn.CreateCommand();
            My_com.CommandText = sqlstr;
            SQLiteDataReader My_Reader = My_com.ExecuteReader();
            return My_Reader;
        }

        public void dosqlcom(string sqlstr)
        {
            getcon();
            SQLiteCommand sqlcom = new SQLiteCommand(sqlstr, My_Conn);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            conn_close();
        }

        public DataSet getDs(string sqlstr, string tableName)
        {
            getcon();
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(sqlstr, My_Conn);
            DataSet My_DataSet = new DataSet();
            sqlda.Fill(My_DataSet, tableName);
            conn_close();
            return My_DataSet;
        }
    }
}
