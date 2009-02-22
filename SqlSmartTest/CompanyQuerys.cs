using System;
using System.Collections.Generic;
using System.Text;
using SqlSmart;
using System.Data.Common;

namespace SqlSmartTest
{
    public class QueryPerson : SLMObject
    {
        private SLMField _id = null;

        public SLMField Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private SLMField _name = null;

        public SLMField Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private SLMField _deptName = null;

        public SLMField DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }
        public QueryPerson(CompanyApp app):base(app)
        {
            Id = new SLMField(this, "id", SLMFieldType.Int, true);
            Name = new SLMField(this, "name", SLMFieldType.String);
            DeptName = new SLMField(this, "DeptName", SLMFieldType.String);
        }
    }

    public class QueryPersons : SLMQuery<QueryPerson>
    {
        // DONE : 实例化
        CompanyApp CompanyApp {get{return SLMApp as CompanyApp;}}
        protected override string GetSql()
        {
            string sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6}";
            Person person = CompanyApp.CompanyDb.Person ;
            Dept dept = CompanyApp.CompanyDb.Dept ;
            sql = string.Format(sql, person.Id.FieldName, person.Name.FieldName, dept.Name.FieldName, person, dept, person.DeptId.FieldName, dept.Id.FieldName);
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
            sql = string.Format(sql, person.Id.FieldName, person.Name.FieldName, dept.Name.FieldName, person, dept, person.DeptId.FieldName, dept.Id.FieldName,person.Name.FieldName,_name);
            return sql;
        }
        public QueryPersonsByName(CompanyApp app,string name):base(app)
        {
            _name = name;
        }
    }
   
    public class QueryPersonsAlias : SLMQuery<QueryPerson>
    {
        // 效果2
        //string sql = "select p.name,p.id,d.name as deptname from person p left join dept d on p.deptid=d.id";

        protected override string GetSql()
        {
            string sql = "select {0},{1},{2} as deptname from {3} left join {4} on {5}={6} ";
            Person person = CompanyApp.CompanyDb.Person;
            Dept dept = CompanyApp.CompanyDb.Dept;
            person.Alias = "p";
            dept.Alias = "d";
            person.UseAlias = true; 
            dept.UseAlias = true;
            sql = string.Format(sql, person.Id.FieldName,person.Name.FieldName,dept.Name.FieldName ,person, dept, person.DeptId.FieldName, dept.Id.FieldName);
            return sql;
        }
        public QueryPersonsAlias(CompanyApp app)
            : base(app)
        {
          
        }
        CompanyApp CompanyApp { get { return SLMApp as CompanyApp; } }

    }
    public class QueryPersonCount : SLMObject
    {
        private SLMField _count = null;

        public SLMField Count
        {
            get { return _count; }
            set { _count = value; }
        }
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
