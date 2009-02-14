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
        static CompanyDb companydb = new CompanyDb();
        static void Main(string[] args)
        {
            SSApp.CreateApp(new DbHelper(companydb.ToString()), companydb);
            try
            {
                SSClear();
                SSInsert();
                SSSelect();
                SSJoin();
                SSCount();
                //SqliteConnTest();
               
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            Console.In.ReadLine();

        }

        private static void SSCount()
        {
            QueryPersonCounts counts = new QueryPersonCounts();
            QueryPersonCount count = counts.ExecFirst();
            string str = string.Format("count= {0}", count.Count.Value);
            Console.Out.WriteLine(str);
        }
        
        private static void SSJoin()
        {
            QueryPersons persons = new QueryPersons();
            persons.Exec();
            foreach (QueryPerson person in persons)
            {
                string str = string.Format("id= {0},name={1},deptname={2}", person.Id.Value, person.Name.Value,person.DeptName.Value);
                Console.Out.WriteLine(str);
            }
        }
        private static void SSSelect()
        {
            PersonList persons = new PersonList();
            persons.SelectAll();
                           
            foreach (Person person in persons)
            {
                string str = string.Format("id= {0},name={1}", person.Id.Value, person.Name.Value);
                Console.Out.WriteLine(str);
            }
       
            DeptList depts = new DeptList();
            depts.SelectAll();
            foreach (Dept dept in depts)
            {
                string str = string.Format("id= {0},name={1}", dept.Id.Value, dept.Name.Value);
                Console.Out.WriteLine(str);
            }
                      
        }
        private static void SSInsert()
        {
            companydb.Person.Id.Value = 1;
            companydb.Person.Name.Value = "1000copy";
            companydb.Person.DeptId.Value = 1;
            companydb.Person.Insert();
            companydb.Dept.Id.Value = 1;
            companydb.Dept.Name.Value = "trd";
            companydb.Dept.Insert();            
        }
       
        private static void SSClear()
        {
            string sql = "";
            sql = companydb.Person.DeleteAll();
            sql = companydb.Dept.DeleteAll();
        }

        /*
       private static void SqliteConnTest()
       {
           try
           {
               DbHelper db = new DbHelper("Data Source=companydb.db");
               db.Exec("INSERT INTO person(id,name) VALUES (1,'1000copy')");
               DbDataReader reader = db.QueryReader("SELECT ID, name FROM person");
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
       }*/
    }
}
