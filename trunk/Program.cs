using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlSmart;



namespace SqlSmartTest
{
    class Program
    {
        static void Main(string[] args)
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
            db.Person.Id.Value = 1 ;
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
