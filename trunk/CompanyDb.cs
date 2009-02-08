using System;
using System.Collections.Generic;
using SqlSmart;
using System.Reflection;


namespace SqlSmartTest
{

    // 自动根据数据库来生成的内容
    public class CompanyDb : SSDatabase
    {
        public Dept Dept = new Dept();
        public Person Person = new Person();
        public CompanyDb()
        {
            /*
            Dept.InitFields();
            Person.InitFields();
             */
        }
    }

    public class Dept : SSTable
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
    public class Person : SSTable
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
