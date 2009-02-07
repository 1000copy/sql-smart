using System;
using System.Collections.Generic;
using SqlSmart;


namespace SqlSmartTest
{

    // 自动根据数据库来生成的内容
    public class CompanyDb : SSDatabase
    {
        public Dept Dept = new Dept();
        public Person Person = new Person();

    }

    public class Dept : SSTable
    {
        public SSKeyField<int> Id = null;
        public SSField<string> Name = null;
        public override string ToString()
        {
            return "Dept";
        }
        public Dept()
        {
            Id = new SSKeyField<int>(this, "id");
            Name = new SSField<string>(this, "name");                   
            fields.Add("id", Id);
            fields.Add("name", Name);
        }
    }
    public class Person : SSTable
    {
        public SSKeyField<int> Id = null;
        public SSField<string> Name = null;
        public SSField<int> DeptId = null;
        public override string ToString()
        {
            return "Person";
        }
        public Person()
        {
            Id = new SSKeyField<int>(this, "id");
            Name = new SSField<string>(this, "name");
            DeptId = new SSField<int>(this, "deptid");
            fields.Add("id", Id);
            fields.Add("name", Name);
            fields.Add("deptid", DeptId);
        }
    }
}
