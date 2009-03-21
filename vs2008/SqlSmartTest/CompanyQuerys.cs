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
        public override string GetTableName()
        {
            return this.ToString();
        }
        public override string ToString()
        {
            return "Person";
        }
        public QueryPerson(CompanyApp app):base(app)
        {
            Id = new SLMField(this, "id", SLMFieldType.Int, true);
            Name = new SLMField(this, "name", SLMFieldType.String);
            DeptName = new SLMField(this, "DeptName", SLMFieldType.String);
            InitFields();
        }
    }
    public class FieldMeta
    {
        public string Name = "";
        public string Caption = "";
        public FieldMeta(string _name,string _caption)
        {
            Name = _name;
            Caption = _caption;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public class QueryPersonMeta
    {
        public static FieldMeta Id = new FieldMeta("id","No");
        public static FieldMeta Name = new FieldMeta("name", "����");
        public static FieldMeta DeptName = new FieldMeta("deptname", "����");
    }

    public class QueryPersons : SLMQuery<QueryPerson>
    {
        // DONE : ʵ����
        CompanyApp CompanyApp {get{return SLMApp as CompanyApp;}}
        protected override string GetSql()
        {
            string sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6}";
            Person person = CompanyApp.CompanyDb.Person ;
            Dept dept = CompanyApp.CompanyDb.Dept ;
            sql = string.Format(sql, person.Id.FieldNameWithPrefix, person.Name.FieldNameWithPrefix, dept.Name.FieldNameWithPrefix, person, dept, person.DeptId.FieldNameWithPrefix, dept.Id.FieldNameWithPrefix);
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
            Person person = CompanyApp.CompanyDb.Person;
            Dept dept = CompanyApp.CompanyDb.Dept;
            string sql = "";
            if (_name != "")
            {
                sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6} where {7} like '%{8}%'";
                sql = string.Format(sql, person.Id.FieldNameWithPrefix, person.Name.FieldNameWithPrefix, dept.Name.FieldNameWithPrefix, person, dept, person.DeptId.FieldNameWithPrefix, dept.Id.FieldNameWithPrefix, person.Name.FieldNameWithPrefix, _name);
            }
            else
            {
                sql = "select {0} as id ,{1} as name ,{2} as DeptName from {3} left join {4} on {5}={6}";
                sql = string.Format(sql, person.Id.FieldNameWithPrefix, person.Name.FieldNameWithPrefix, dept.Name.FieldNameWithPrefix, person, dept, person.DeptId.FieldNameWithPrefix, dept.Id.FieldNameWithPrefix);
            }
            return sql;
        }
        public QueryPersonsByName(CompanyApp app,string name):base(app)
        {
            _name = name;
        }
    }
   
    public class QueryPersonsAlias : SLMQuery<QueryPerson>
    {
        // Ч��2
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
            sql = string.Format(sql, person.Id.FieldNameWithPrefix,person.Name.FieldNameWithPrefix,dept.Name.FieldNameWithPrefix ,person, dept, person.DeptId.FieldNameWithPrefix, dept.Id.FieldNameWithPrefix);
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
