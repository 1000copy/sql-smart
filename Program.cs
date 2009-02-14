using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlSmart;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

// TODO : 支持别名，比如select d.name,p.name,p.id from person  p left join dept d on p.deptid = d.id where p.name ="lcj"
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
                //TestSqlite.SqliteConnTest();
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
            persons.DoQuery();
            foreach (QueryPerson person in persons)
            {
                string str = string.Format("id= {0},name={1},deptname={2}", person.Id.Value, person.Name.Value,person.DeptName.Value);
                Console.Out.WriteLine(str);
            }
        }
        private static void SSSelect()
        {
            PersonList persons = new PersonList();
            persons.DoQuery();
                           
            foreach (Person person in persons)
            {
                string str = string.Format("id= {0},name={1}", person.Id.Value, person.Name.Value);
                Console.Out.WriteLine(str);
            }
       
            DeptList depts = new DeptList();
            depts.DoQuery();
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
            companydb.Person.DeleteAll();
            companydb.Dept.DeleteAll();
        }
    }
}
