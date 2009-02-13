using System;
using System.Collections.Generic;
using SqlSmart;
using System.Reflection;
using System.Data.Common;


namespace SqlSmartTest
{

    // 自动根据数据库来生成的内容
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
