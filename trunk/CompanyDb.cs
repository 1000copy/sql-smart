using System;
using System.Collections.Generic;
using SqlSmart;
using System.Reflection;
using System.Data.Common;


namespace SqlSmartTest
{

    // 自动根据数据库来生成的内容
    public class CompanyApp : SSApp
    {
        public static CompanyDb CompanyDb
        {
            get
            {
                return (Database as CompanyDb);
            }
        }
    }
    public class CompanyDb : SSDatabase
    {
        public Dept Dept = new Dept();
        public Person Person = new Person();

        public override string ToString()
        {
            return "CompanyDb.db";
        }
    }
    public class DeptList : SSQuery<Dept>
    {
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Dept.SelectAllSql();
        }
    }
    public class Dept : SSObject
    {
        public SSField Id = null;
        public SSField Name = null;
        public override string ToString()
        {
            return "Dept";
        }
        public Dept()
        {
            Id = new SSField(this, "id",SSFieldType.Int,true);
            Name = new SSField(this, "name",SSFieldType.String);             
        }
    }

    public class PersonList : SSQuery<Person>
    {
        protected override string GetSql()
        {
            return CompanyApp.CompanyDb.Person.SelectAllSql();
        }
    }
    public class Person : SSObject
    {
        public SSField Id = null;
        public SSField Name = null;
        public SSField DeptId = null;
        public SSField Birthday = null;
        public override string ToString()
        {
            return "Person";
        }
        public Person()
        {
            Id = CreateField( "id",SSFieldType.Int,true);
            Name = CreateField("name", SSFieldType.String);
            DeptId = CreateField("deptid", SSFieldType.Int);
            Birthday = CreateField("birthday", SSFieldType.DateTime);
        }
    }
}
