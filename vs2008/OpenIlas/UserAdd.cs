using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace openilas
{
    public partial class ReaderAdd : Form
    {
        public ReaderAdd()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private DataTable table = null;
        public static DataTable do_add()
        {
            /*
            ReaderAdd reader = new ReaderAdd();
            // init data
            // TODO :
            string fields = "[READER_ID],[READER_BAR],[NAME],[SEX],[DEPT_CODE],[DEPT_NAME],[POSITION],[READER_LVL],[ISSUE_DATE],[EXPIREDATE],[EMAIL]";
            // todo:reader.table = db.Query(String.Format("select {0} from  reader where 1=0",fields));
            DataRow row = reader.table.NewRow();
            reader.table.Rows.Add(row);            
            if (reader.ShowDialog() == DialogResult.OK)
            {
                return reader.table;
            }
             *   * */
            return null;
           
        }

        private void ReaderAdd_Load(object sender, EventArgs e)
        {
            /*
            this.reader_bar.DataBindings.Add("text", table, "reader_bar");
            this.reader_id.DataBindings.Add("text", table, "reader_id");
            this.position.DataBindings.Add("text", table, "position");
            this.sex.DataBindings.Add("text", table, "sex");
            this.reader_lvl.DataBindings.Add("text", table, "reader_lvl");
            this.dept_code.DataBindings.Add("text", table, "dept_code");
            this.dept_name.DataBindings.Add("text", table, "dept_name");
            this.name.DataBindings.Add("text", table, "name");
            */
        }
    }
}
