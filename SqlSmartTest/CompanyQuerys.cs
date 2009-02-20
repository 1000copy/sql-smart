using System;
using System.Collections.Generic;
using System.Text;
using SqlSmart;
using System.Data.Common;

namespace SqlSmartTest
{
    public class QueryPerson : SLMObject
    {
        public SLMField Id = null;
        public SLMField Name = null;
        public SLMField DeptName = null;
        public QueryPerson(CompanyApp app):base(app)
        {
            Id = new SLMField(this, "id", SLMFieldType.Int, true);
            Name = new SLMField(this, "name", SLMFieldType.String);
            DeptName = new SLMField(this, "DeptName", SLMFieldType.String);
        }
    }

    public class QueryPersons : SLMQuery<QueryPerson>
    {
        // TODO : สตภýปฏ
        CompanyApp CompanyApp {get{return SLMApp as CompanyApp;}}
        protected override string GetSql()
        {
            string sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6}";
            Person person = CompanyApp.CompanyDb.Person ;
            Dept dept = CompanyApp.CompanyDb.Dept ;
            sql = string.Format(sql, person.Id, person.Name, dept.Name, person, dept, person.DeptId, dept.Id);
            return sql;
        }
        public QueryPersons(CompanyApp app)
            : base(app)
        {
        }
    }

    public class QueryPersonsByName : SLMQuery<QueryPerson>
    {
        string _name = "";
        CompanyApp CompanyApp {get{return SLMApp as CompanyApp;}}
        protected override string GetSql()
        {
            string sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6} where {7} like '%{8}%'";
            Person person = CompanyApp.CompanyDb.Person;
            Dept dept = CompanyApp.CompanyDb.Dept;
            sql = string.Format(sql, person.Id, person.Name, dept.Name, person, dept, person.DeptId, dept.Id,person.Name,_name);
            return sql;
        }
        public QueryPersonsByName(CompanyApp app,string name):base(app)
        {
            _name = name;
        }
    }
    public class QueryPersonCount : SLMObject
    {
        public SLMField Count = null;
        public QueryPersonCount(CompanyApp app)
            : base(app)
        {
            Count = new SLMField(this, "count", SLMFieldType.Int, true);
        }
    }

    public class QueryPersonCounts : SLMQuery<QueryPersonCount>
    {
        protected override string GetSql()
        {
            string sql = string.Format("select count(*) as count  from {0} ", (SLMApp.Database as CompanyDb).Person); 
            return sql;
        }
        public QueryPersonCounts(CompanyApp app)
            : base(app)
        {
        }

    }
}
