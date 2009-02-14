/*
* // 好处1 ： 可以代码提示，不改变sql编写的习惯
* // 好处2：sql 可以集中写
* // 好处3 ： 容易查找引用，从而，容易重构--在哪里引用了表，数据库，一目了然
*/


using System.Collections.Generic;
using System;
using System.Reflection;
using System.Data.Common;

namespace SqlSmart
{
    public interface IDbHelper
    {
        int Exec(string sql);
        DbDataReader QueryReader(string sql);
    }
    
    public class SSApp
    {
        private static IDbHelper _dbhelper = null;
        private static SSDatabase _database = null;

        public static IDbHelper DbHelper { get { return _dbhelper; } }

        public static SSDatabase Database { get { return _database; } }

        public static void CreateApp(IDbHelper dbhelper, SSDatabase database)
        {
            _dbhelper = dbhelper;
            _database = database;
        }
    }
    public abstract class SSQuery<SSObject> : SSObjectList<SSObject>
    {
        
        public virtual void Exec()
        {
            DbDataReader reader = SSApp.DbHelper.QueryReader(GetSql());
            this.FromReader(reader);
        }
        public virtual SSObject ExecFirst()
        {
            DbDataReader reader = SSApp.DbHelper.QueryReader(GetSql());
            FromReader(reader);
            return First();
        }
        public virtual SSObject First()
        {
            if (this.Count > 0)
                return this[0];
            else
                return default(SSObject);
        }
    }
    public class SSObjectList<SSObject> : List<SSObject>
    {
        public virtual void SelectAll()
        {

        }
        protected virtual string GetSql()
        {
            return "";
        }
        public void FromReader(DbDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SSObject tableobject = System.Activator.CreateInstance<SSObject>();
                    Type type = tableobject.GetType();
                    foreach (FieldInfo fi in type.GetFields())
                    {

                        if (fi.FieldType == typeof(SSField))
                        {
                            string fieldname = fi.Name;
                            try
                            {
                                int fieldindex = reader.GetOrdinal(fieldname);
                                SSField field = fi.GetValue(tableobject) as SSField;
                                field.Value = reader.GetValue(fieldindex);
                            }
                            catch (IndexOutOfRangeException)
                            {
                            }
                        }
                    }
                    this.Add(tableobject);
                }
            }
        }
    }
    public class SSObject
    {
        public Dictionary<string, SSField> fields = null;
        public SSObject()
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
                
                List<string> list = new List<string>();                
                foreach (KeyValuePair<string, SSField> keyvalue in fields)
                {   
                    SSField field = keyvalue.Value;
                    object obj = ((SSField)field).Value;
                    if (obj != null)
                    {
                        if (field.FieldType == SSFieldType.Int)
                        {
                           list.Add(obj.ToString());
                        }
                        else if (field.FieldType == SSFieldType.String || field.FieldType == SSFieldType.DateTime)
                        {
                            string temp =  string.Format("'{0}'", field.Value.ToString());
                            list.Add(temp);
                        }
                        else
                            UnsupportException();                
                    }
                }
                int i = 0;
                foreach (string str in list)
                {
                    i++;
                    r += str;
                    if (i != list.Count)
                        r += ",";
                }
                return r;
            }
        }
        public string FieldNamesWhichHasValue
        {
            get
            {
                string r = "";

                List<string> list = new List<string>();
                foreach (KeyValuePair<string, SSField> keyvalue in fields)
                {
                    SSField field = keyvalue.Value;
                    object obj = ((SSField)field).Value;
                    if (obj != null)
                    {
                        list.Add(keyvalue.Key);
                    }
                }
                int i = 0;
                foreach (string str in list)
                {
                    i++;
                    r += str;
                    if (i != list.Count)
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
        public int Insert()
        {
            string sql = "insert into {0} ({1}) values({2})";
            sql = string.Format(sql, this, FieldNamesWhichHasValue, FieldValues);
            return SSApp.DbHelper.Exec(sql);
        }
        public int Update()
        {
            string sql = "update {0} set {1} where {2}={3}";
            sql = string.Format(sql, this, FieldEqualValues, TableKeyName, TableKeyValue);
            return SSApp.DbHelper.Exec(sql);
        }
        public int Delete()
        {
            string sql = "delete from {0} where {1}={2}";
            sql = string.Format(sql, this, TableKeyName, TableKeyValue);
            return SSApp.DbHelper.Exec(sql);
        }
        public int DeleteAll()
        {
            string sql = "delete from {0} ";
            sql = string.Format(sql, this);
            return SSApp.DbHelper.Exec(sql);
        }
        public string SelectAllSql()
        {
            return  "select " + FieldNames + " from " + this;
        }
    }
    public class SSDatabase
    {
        public SSDatabase()
        {
            InitObjects();
        }

        public void InitObjects()
        {
            // 找到所有SSTable类型的成员，并且调用它的InitFields 方法
            Type type = this.GetType();
            foreach (FieldInfo fi in type.GetFields())
            {
                if (fi.FieldType.BaseType == typeof(SSObject))
                {
                    (fi.GetValue(this) as SSObject).InitFields();
                }
            }
        }

    }
    public enum SSFieldType { Int,String,DateTime};
    public class SSField
    {
        string _fieldname = null;
        SSObject _ownerTable = null;
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
        public SSField(SSObject table, string fieldname, SSFieldType fieldtype,bool iskey)
            : this(table, fieldname,fieldtype)
        {
            _iskey = iskey;
        }
        public SSField(SSObject table, string fieldname,SSFieldType fieldtype)
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