using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlSmart;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;


namespace SqlSmartTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqliteConnTest();
            SSVsSqliteConnTest();
        }

        private static void SSVsSqliteConnTest()
        {
            try
            {
                CompanyDb company = new CompanyDb();
                DbHelper db = new DbHelper(company.ToString());                
                company.Person.Id.Value = 2;
                company.Person.Name.Value = "sunqin";
                string sql = "";
                sql = company.Person.Insert();
                db.Exec(sql);
                DbDataReader reader = db.Query(company.Person.SelectAll());
                // TODO : 还可以优化，更加表意，用PersonList之类的
                // testcase : 
                // List<Person> list = new List<Person>();
                // company.FillPersons() ->PersonList
                // List<Person> CompanyDb.FillPersons(DbDataReader reader)
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt16(0));
                        Console.WriteLine("name: " + reader.GetString(1));
                    }
                }
                // 优化区完成
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            Console.In.ReadLine();
        }
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
            public DbDataReader Query(string sql)
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                return cmd.ExecuteReader();
            }
        }
        private static void SqliteConnTest()
        {
            try
            {
                DbHelper db = new DbHelper("Data Source=companydb.db");
                db.Exec("INSERT INTO person(id,name) VALUES (1,'1000copy')");
                DbDataReader reader = db.Query("SELECT ID, name FROM person");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt16(0));
                        Console.WriteLine("name: " + reader.GetString(1));
                    }
                }
            }
            catch
            {
                Console.Out.WriteLine("some error");
            }
            Console.In.ReadLine();
        }

        private static void SSTest()
        {

            MyCompanyDb db = new MyCompanyDb();
            string sql = "";
            sql = db.Dept.SelectAll();
            Console.Out.WriteLine(sql);
            // Insert
            db.Dept.Id.Value = 1;
            db.Dept.Name.Value = "trd";
            sql = db.Dept.Insert();
            // update 
            Console.Out.WriteLine(sql);
            sql = db.Dept.Update();
            Console.Out.WriteLine(sql);
            // delete
            sql = db.Dept.Delete();
            Console.Out.WriteLine(sql);
            // left join sql 
            sql = db.PersonList();
            Console.Out.WriteLine(sql);
            // insert person
            db.Person.Birthday.Value = DateTime.Today.Date;
            db.Person.DeptId.Value = 1;
            db.Person.Name.Value = "1000copy";
            db.Person.Id.Value = 1;
            sql = db.Person.Insert();
            Console.Out.WriteLine(sql);
            // pause ,wait for user input 
            Console.In.ReadLine();
        }
        
        public class MyCompanyDb : CompanyDb
        {
            public string PersonList()
            {
                string sql = "";
                sql = "select {0} from {1} left join {2} on {3} ={4} ";
                sql = string.Format(sql, Dept.Name + "," + Person.FieldNamesPrefixTableName, Person, Dept, Person.DeptId, Dept.Id);
                return sql;
            }
        }
    }
}
