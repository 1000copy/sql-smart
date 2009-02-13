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
        static DbHelper db = new DbHelper(companydb.ToString());
        static void Main(string[] args)
        {            
            try
            {
                SSClear();
                SSInsert();
                SSSelect();
                SSJoin();
                //SqliteConnTest();
                Console.In.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

        }
        private class QueriedPerson:SSObject
        {
            public SSField Id = null;
            public SSField Name = null;
            public SSField DeptName = null;
            public QueriedPerson()
            {
                Id = new SSField(this, "id", SSFieldType.Int, true);
                Name = new SSField(this, "name", SSFieldType.String);
                DeptName = new SSField(this, "DeptName", SSFieldType.String);
            }
        }
    
        private class QueriedPersons :SSObjectList<QueriedPerson>
        {
        }
        private static void SSJoin()
        {
            string sql = "select Person.Id,Person.Name,Dept.Name as DeptName from person left join dept on person.deptid=dept.id";
            DbDataReader reader = db.QueryReader(sql);
            QueriedPersons personwithdepts = new QueriedPersons();
            personwithdepts.FromReader(reader);
            foreach (QueriedPerson person in personwithdepts)
            {
                string str = string.Format("id= {0},name={1},deptname={2}", person.Id.Value, person.Name.Value,person.DeptName.Value);
                Console.Out.WriteLine(str);
            }
        }
        private static void SSSelect()
        {

            {
                PersonList list = new PersonList();
                list.FromReader(db.QueryReader(companydb.Person.SelectAll()));
                foreach (Person person in list)
                {
                    string str = string.Format("id= {0},name={1}", person.Id.Value, person.Name.Value);
                    Console.Out.WriteLine(str);
                }
            }
            {
                DeptList list = new DeptList();
                list.FromReader(db.QueryReader(companydb.Dept.SelectAll()));
                foreach (Dept dept in list)
                {
                    string str = string.Format("id= {0},name={1}", dept.Id.Value, dept.Name.Value);
                    Console.Out.WriteLine(str);
                }
            }           
        }

        private static void SSInsert()
        {
            companydb.Person.Id.Value = 1;
            companydb.Person.Name.Value = "1000copy";
            companydb.Person.DeptId.Value = 1;
            db.Exec(companydb.Person.Insert());
            companydb.Dept.Id.Value = 1;
            companydb.Dept.Name.Value = "trd";
            db.Exec(companydb.Dept.Insert());
        }

        private static void SSClear()
        {
            string sql = "";
            sql = companydb.Person.DeleteAll();
            db.Exec(sql);
            sql = companydb.Dept.DeleteAll();
            db.Exec(sql);
        }
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
        }
    }
}
