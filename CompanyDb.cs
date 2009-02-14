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
        public CompanyDb()
        {
           
            
        }
        public override string ToString()
        {
            return "CompanyDb.db";
        }


    }
    public class DeptList : SSObjectList<Dept>
    {
        protected override string GetSql()
        {
            return   "select " +  " from " + this;
        }

        public override void SelectAll()
        {
            string sql = (SSApp.Database as CompanyDb).Dept.SelectAllSql();
            DbDataReader reader = SSApp.DbHelper.QueryReader(sql);
            reader = SSApp.DbHelper.QueryReader(sql);
            this.FromReader(reader);
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
    
    public class PersonList : SSObjectList<Person>
    {
        public  override void SelectAll()
        {
            string sql = CompanyApp.CompanyDb.Person.SelectAllSql();
            DbDataReader reader = SSApp.DbHelper.QueryReader(sql);
            reader = SSApp.DbHelper.QueryReader(sql);
            this.FromReader(reader);
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
            Id = new SSField(this, "id",SSFieldType.Int,true);
            Name = new SSField(this, "name",SSFieldType.String);
            DeptId = new SSField(this, "deptid", SSFieldType.Int);
            Birthday = new SSField(this, "birthday", SSFieldType.DateTime);
        }
    }
}
