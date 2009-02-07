/*
* // 好处1 ： 可以代码提示，不改变sql编写的习惯
* // 好处2：sql 可以集中写
* // 好处3 ： 容易查找引用，从而，容易重构--在哪里引用了表，数据库，一目了然
*/


using System.Collections.Generic;
using System;
using System.Reflection;
namespace SqlSmart
{
    // 系统类
    public class SSTable
    {
        public Dictionary<string, object> fields = null;
        public SSTable()
        {
            fields = new Dictionary<string, object>();
        }
        // 把字段SSField加入fields
        public void InitFields()
        {
            Type type = this.GetType();
            foreach (FieldInfo fi in type.GetFields())
            {
                if (IsSupportFieldType(fi.FieldType))               
                    fields.Add(fi.Name.ToLower(), fi.GetValue(this));                
            }
            // end
        }
        private bool IsSupportFieldType(Type fieldtype)
        {
            return     fieldtype == typeof(SSKeyField<int>)
                    || fieldtype == typeof(SSKeyField<string>)
                    || fieldtype == typeof(SSField<string>)
                    || fieldtype == typeof(SSField<int>)
                    || fieldtype == typeof(SSField<DateTime>);
        }
        private void CheckSupportFieldType(Type fieldtype)
        {
            if (!IsSupportFieldType(fieldtype))
                throw new Exception(string.Format("Unsupport field type:",fieldtype.Name));
        }
        private string _tableKeyName = null;
        public virtual string TableKeyName { get { return _tableKeyName == null ? "id" : _tableKeyName; } set { _tableKeyName = value; } }
        public virtual object TableKeyValue {
            get 
            {
                object obj = fields[TableKeyName] ;
                if (obj is SSKeyField<int>)
                    return ((SSKeyField<int>)obj).Value;
                else if (obj is SSKeyField<string>)
                    return ((SSKeyField<string>)obj).Value;
                else
                    return null;
            }
        }
        public string FieldNames
        {
            get
            {
                string r = "";
                int i = 0;
                foreach (string key in fields.Keys)
                {
                    i++;
                    r += key;
                    if (i != fields.Keys.Count)
                        r += ",";
                }
                return r;
            }
        }
        public string FieldNamesPrefixTableName
        {
            get
            {
                string r = "";
                int i = 0;
                foreach (string key in fields.Keys)
                {
                    i++;
                    r += this + "."+key;
                    if (i != fields.Keys.Count)
                        r += ",";
                }
                return r;
            }
        }
        public string FieldValues
        {
            get
            {
                string r = "";
                int i = 0;
                foreach (KeyValuePair<string, object> keyvalue in fields)
                {
                    i++;
                    object obj = keyvalue.Value;
                    if (obj is SSField<int>)
                    {
                        r += ((SSField<int>)obj).Value.ToString();
                    }
                    else if (obj is SSField<string>)
                    {
                        r += string.Format("'{0}'", ((SSField<string>)obj).Value);
                    }
                    else if (obj is SSField<DateTime>)
                    {
                        r += string.Format("'{0}'", ((SSField<DateTime>)obj).Value.ToShortDateString());
                    }
                    if (i != fields.Keys.Count)
                        r += ",";
                }
                return r;
            }
        }
        public string FieldEqualValues
        {
            get
            {
                string r = "";
                int i = 0;
                foreach (KeyValuePair<string, object> keyvalue in fields)
                {
                    i++;
                    object obj = keyvalue.Value;
                    CheckSupportFieldType(obj.GetType());
                    if (obj is SSField<int>)
                    {
                        SSField<int> IntField = (SSField<int>)obj;
                        r += string.Format("{0}={1}", IntField.FieldName, IntField.Value.ToString());
                    }
                    else if (obj is SSField<string>)
                    {
                        SSField<string> StringField = (SSField<string>)obj;
                        r += string.Format("{0}='{1}'", StringField.FieldName, StringField.Value.ToString());
                    }
                    if (i != fields.Keys.Count)
                        r += ",";
                }
                return r;
            }
        }
        public string Insert()
        {
            string insertsql = "insert into {0} ({1}) values({2})";
            return string.Format(insertsql, this, FieldNames, FieldValues);
        }
        public string Update()
        {
            string insertsql = "update {0} set {1} where {2}={3}";
            return string.Format(insertsql, this, FieldEqualValues, TableKeyName, TableKeyValue);
        }
        public string Delete()
        {
            string insertsql = "delete from {0} where {1}={2}";
            return string.Format(insertsql, this, TableKeyName, TableKeyValue);
        }
        public string SelectAll()
        {

            return "select " + FieldNames + " from " + this;
        }
    }
    public class SSDatabase
    {


    }
    public class SSField<DbFieldType>
    {
        string _fieldname = null;
        SSTable _ownerTable = null;
        public string FieldName
        {
            get { return _fieldname; }
            set { _fieldname = value; }
        }
        public override string ToString()
        {
            return _ownerTable.ToString() + "." + FieldName;
        }
        public SSField(SSTable table, string fieldname)
        {
            _ownerTable = table;
            _fieldname = fieldname;
        }
        private DbFieldType _value;
        public virtual DbFieldType Value
        {
            set { _value = value; }
            get { return _value; }
        }
    }
    public class SSKeyField<DbFieldType> : SSField<DbFieldType>
    {
        public SSKeyField(SSTable table, string fieldname)
            : base(table, fieldname)
        {
        }
    }
}