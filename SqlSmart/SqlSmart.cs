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
        public Dictionary<string, SSField> fields = null;
        public SSTable()
        {
            fields = new Dictionary<string, SSField>();
        }
        // 把字段SSField加入fields
        public void InitFields()
        {
            Type type = this.GetType();
            foreach (FieldInfo fi in type.GetFields())
            {
                if (IsSupportFieldType(fi.FieldType))               
                    fields.Add(fi.Name.ToLower(), (SSField)(fi.GetValue(this)));                
            }
            // end
        }
        private bool IsSupportFieldType(Type fieldtype)
        {
            return fieldtype == typeof(SSField);
        }
        private void UnsupportException ()
        {
            new Exception(string.Format("Unsupport field type:")); 
        }
        private void CheckSupportFieldType(Type fieldtype)
        {
            if (!IsSupportFieldType(fieldtype))
                UnsupportException();
        }
        public virtual string TableKeyName
        {
            get
            {
                foreach (KeyValuePair<string, SSField> keyvalue in fields)
                {
                    string key = keyvalue.Key;
                    SSField value = keyvalue.Value;
                    if (value.IsKey) return key;
                }
                return "";
            }
        }
        public virtual object TableKeyValue {
            get 
            {
                if (fields.ContainsKey(TableKeyName))
                    return fields[TableKeyName].Value ;
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
                foreach (KeyValuePair<string, SSField> keyvalue in fields)
                {
                    i++;
                    SSField field = keyvalue.Value;
                    if (field.FieldType == SSFieldType.Int)
                    {
                        r += ((SSField)field).Value.ToString();
                    }
                    else if (field.FieldType == SSFieldType.String || field.FieldType == SSFieldType.DateTime)
                    {
                        r += string.Format("'{0}'", field.Value.ToString());
                    }
                    else 
                        UnsupportException();
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
                foreach (KeyValuePair<string, SSField> keyvalue in fields)
                {
                    i++;
                    SSField field = keyvalue.Value;
                    CheckSupportFieldType(field.GetType());
                    if (field.FieldType == SSFieldType.Int)
                    {
                        r += string.Format("{0}={1}", field.FieldName, field.Value.ToString());
                    }
                    else if (field.FieldType == SSFieldType.String)
                    {
                        r += string.Format("{0}='{1}'", field.FieldName, field.Value.ToString());
                    }
                    else
                        UnsupportException();
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
        public SSDatabase()
        {
            // 找到所有SSTable类型的成员，并且调用它的InitFields 方法
            Type type = this.GetType();
            foreach (FieldInfo fi in type.GetFields())
            {
                if (fi.FieldType.BaseType == typeof(SSTable))
                    (fi.GetValue(this) as SSTable).InitFields();
            }
        }

    }
    public enum SSFieldType { Int,String,DateTime};
    public class SSField
    {
        string _fieldname = null;
        SSTable _ownerTable = null;
        private SSFieldType _fieldtype;

        public SSFieldType FieldType
        {
            get { return _fieldtype; }
            set { _fieldtype = value; }
        }
        public string FieldName
        {
            get { return _fieldname; }
            set { _fieldname = value; }
        }
        public override string ToString()
        {
            return _ownerTable.ToString() + "." + FieldName;
        }
        public SSField(SSTable table, string fieldname, SSFieldType fieldtype,bool iskey)
            : this(table, fieldname,fieldtype)
        {
            _iskey = iskey;
        }
        public SSField(SSTable table, string fieldname,SSFieldType fieldtype)
        {
            _ownerTable = table;
            _fieldname = fieldname;
            _fieldtype = fieldtype;
        }
        private object _value;
        public virtual object Value
        {
            set { _value = value; }
            get { return _value; }
        }
        private bool _iskey = false;

        public bool IsKey
        {
            get { return _iskey; }
            set { _iskey = value; }
        }
    }
}