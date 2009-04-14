using System;
using System.Collections.Generic;
using System.Text;
using SqlSmart;
using System.Data.Common;
// TEST VISUALSVN COMMIT ,集成到vs内。
namespace SqlSmartTest
{
    class TestSqlite
    {
        public  static void SqliteConnTest()
        {
            try
            {
                DbHelper db = new DbHelper("CompanyDb.db");
                db.Exec("INSERT INTO person(id,name) VALUES (1,'1000copy')");
                DbDataReader reader = db.QueryReader("SELECT ID, name FROM person");
                Console.Out.WriteLine("write by SqliteConnTest-------");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt16(0));
                        Console.WriteLine("name: " + reader.GetString(1));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }
    }
}
