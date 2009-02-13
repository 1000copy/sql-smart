using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;

namespace SqlSmart
{
    public class DbHelper
    {
        SQLiteConnection conn = null;
        public DbHelper(string str)
        {
            conn = new SQLiteConnection("Data Source=" + str);
            conn.Open();
        }
        public int Exec(string sql)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }
        public DbDataReader QueryReader(string sql)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteReader();
        }
    }
}
