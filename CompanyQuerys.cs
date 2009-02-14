using System;
using System.Collections.Generic;
using System.Text;
using SqlSmart;
using System.Data.Common;

namespace SqlSmartTest
{
    public class QueryPerson : SSObject
    {
        public SSField Id = null;
        public SSField Name = null;
        public SSField DeptName = null;
        public QueryPerson()
        {
            Id = new SSField(this, "id", SSFieldType.Int, true);
            Name = new SSField(this, "name", SSFieldType.String);
            DeptName = new SSField(this, "DeptName", SSFieldType.String);
        }
    }

    public class QueryPersons : SSQuery<QueryPerson>
    {
        protected override string GetSql()
        {
            string sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6}";
            Person person = CompanyApp.CompanyDb.Person ;
            Dept dept = CompanyApp.CompanyDb.Dept ;
            sql = string.Format(sql, person.Id, person.Name, dept.Name, person, dept, person.DeptId, dept.Id);
            return sql;
        }
    }
    public class QueryPersonCount : SSObject
    {
        public SSField Count = null;
        public QueryPersonCount()
        {
            Count = new SSField(this, "count", SSFieldType.Int, true);
        }
    }

    public class QueryPersonCounts : SSQuery<QueryPersonCount>
    {
        protected override string GetSql()
        {
            string sql = string.Format("select count(*) as count  from {0} ", (SSApp.Database as CompanyDb).Person); 
            return sql;
        }

    }
}
