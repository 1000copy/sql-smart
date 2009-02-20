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
            CreateApp(new DbHelper(companydb.ToString()), companydb);
        }
        public  CompanyDb CompanyDb
        {
            get
            {
                return (Database as CompanyDb);
            }
        }
    }
    public class CompanyDb : SLMDatabase
    {
        public Dept Dept = null ;
        public Person Person = null;
       
        public override string ToString()
        {
            return "CompanyDb.db";
        }
        
        public CompanyDb(CompanyApp app):base(app)
        {
            Dept = new Dept(app);
            Person = new Person(app);
            InitObjects();
        }
        public CompanyApp CompanyApp 
        {
            get { return App as CompanyApp; }
        }

    }
    public class DeptList : SLMQuery<Dept>
    {
        CompanyApp CompanyApp { get { return SLMApp as CompanyApp; } }
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Dept.SelectAllSql();
        }
        public DeptList(CompanyApp app)
            : base(app)
        {
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
        public Dept(CompanyApp app)
            : base(app)
        {
            Id = new SLMField(this, "id",SLMFieldType.Int,true);
            Name = new SLMField(this, "name",SLMFieldType.String);             
        }
    }

    public class PersonList : SLMQuery<Person>
    {
        CompanyApp CompanyApp { get { return SLMApp as CompanyApp; } }
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Person.SelectAllSql();
        }
        public PersonList(CompanyApp app) : base(app) { }
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
        public Person(CompanyApp app):base(app)
        {
            Id = CreateField( "id",SLMFieldType.Int,true);
            Name = CreateField("name", SLMFieldType.String);
            DeptId = CreateField("deptid", SLMFieldType.Int);
            Birthday = CreateField("birthday", SLMFieldType.DateTime);
        }
    }
}
