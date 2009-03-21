using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SqlSmartTest;
using SqlSmart;

namespace openilas
{
    public partial class ReaderList : Form
    {
        public ReaderList()
        {
            InitializeComponent();
        }
        CompanyApp app = new CompanyApp();
        CompanyDb db = null;
        QueryPersonsByName persons = null;
        DataGridView grid1 = null;
        private void refresh()
        {
            persons = new QueryPersonsByName(app,this.textBox1.Text );
            persons.DoQuery();
            grid1.DataSource = persons;
            grid1.Refresh();
        }
        
        private void query_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void ReaderList_Load(object sender, EventArgs e)
        {
            grid1 = new DataGridView();
            grid1.Parent = this;
            //grid1.Dock = DockStyle.Fill;
            grid1.Top = 100;
            grid1.Width = 300;
            grid1.Height = 400;
            grid1.Left = 10;
            persons = new QueryPersonsByName(app,"");
            this.Text = "User List";
            db = new CompanyDb(app);
            app.CreateApp(new DbHelper(db.ToString()), db);
            grid1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn col = null;
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = QueryPersonMeta.Id.ToString() ;
            col.HeaderText = QueryPersonMeta.Id.Caption;
            grid1.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = QueryPersonMeta.Name.ToString();
            col.HeaderText = QueryPersonMeta.Name.Caption;
            grid1.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = QueryPersonMeta.DeptName.ToString();
            col.HeaderText = QueryPersonMeta.DeptName.Caption;
            grid1.Columns.Add(col);

            grid1.AllowUserToAddRows = false;
            grid1.AllowUserToDeleteRows = false;
            refresh();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            /*
            DataTable table_add = ReaderAdd.do_add();
            if (table_add != null)
            {
                DataRow row_add = table_add.Rows[0];
                DataRow row = table.NewRow();
                foreach (DataColumn column in row.Table.Columns)
                {
                    row[column.ColumnName] = row_add[column.ColumnName];
                }
                table.Rows.Add(row);

            }*/

        }

        private void edit_Click(object sender, EventArgs e)
        {
            /*
            DataTable table_add = ReaderAdd.do_add();
            if (table_add != null)
            {
                DataRow row_add = table_add.Rows[0];
                DataRow row = table.NewRow();
                foreach (DataColumn column in row.Table.Columns)
                {
                    row[column.ColumnName] = row_add[column.ColumnName];
                }
                table.Rows.Add(row);
                // TODO:

            }*/
        }


        private void exit_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (grid1.SelectedRows.Count > 0)
            {
                (grid1.SelectedRows[0].DataBoundItem as QueryPerson).Delete();
                //grid1.Rows.Remove(grid1.SelectedRows[0]);
                refresh();
            }
        }

        private void add_Click_1(object sender, EventArgs e)
        {
            db.Person.DeleteAll();
            db.Dept.DeleteAll();
            db.Person.Id.Value = 1;
            db.Person.Name.Value = "1000copy";
            db.Person.DeptId.Value = 1;
            db.Person.Insert();
            db.Dept.Id.Value = 1;
            db.Dept.Name.Value = "trd";
            db.Dept.Insert();
        }

        private void query_Click_1(object sender, EventArgs e)
        {
            refresh();
        }

        private void edit_Click_1(object sender, EventArgs e)
        {

        }
    }
}
