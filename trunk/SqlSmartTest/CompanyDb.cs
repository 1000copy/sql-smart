using System;
using System.Collections.Generic;
using SqlSmart;
using System.Reflection;
using System.Data.Common;


namespace SqlSmartTest
{

    // 自动根据数据库来生成的内容
    public class CompanyApp : SLMApp
    {
        public CompanyApp()
        {
            CompanyDb companydb = new CompanyDb(this);
            //CompanyDb companydb = new CompanyDb();
            CreateApp(new DbHelper(companydb.ToString()), companydb);
        }
        public static  CompanyDb CompanyDb
        {
            get
            {
                return (Database as CompanyDb);
            }
        }
    }
    public class CompanyDb : SLMDatabase
    {
        public Dept Dept = new Dept();
        public Person Person = new Person();
       
        public override string ToString()
        {
            return "CompanyDb.db";
        }
        
        public CompanyDb(SLMApp app):base(app)
        {
        }

    }
    public class DeptList : SLMQuery<Dept>
    {
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Dept.SelectAllSql();
        }
    }
    public class Dept : SLMObject
    {
        public SLMField Id = null;
        public SLMField Name = null;
        public override string ToString()
        {
            return "Dept";
        }
        public Dept()
        {
            Id = new SLMField(this, "id",SLMFieldType.Int,true);
            Name = new SLMField(this, "name",SLMFieldType.String);             
        }
    }

    public class PersonList : SLMQuery<Person>
    {
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Person.SelectAllSql();
        }
    }
    public class Person : SLMObject
    {
        public SLMField Id = null;
        public SLMField Name = null;
        public SLMField DeptId = null;
        public SLMField Birthday = null;
        public override string ToString()
        {
            return "Person";
        }
        public Person()
        {
            Id = CreateField( "id",SLMFieldType.Int,true);
            Name = CreateField("name", SLMFieldType.String);
            DeptId = CreateField("deptid", SLMFieldType.Int);
            Birthday = CreateField("birthday", SLMFieldType.DateTime);
        }
    }
}
