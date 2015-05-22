using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FollowUp
{
    public partial class FrmStudent : Form
    {
        public FrmStudent()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string conn = @"Data Source=TQH-PC\SQLEXPRESS;Initial Catalog=HBC;Integrated Security=True";
            SqlConnection conDataBase = new SqlConnection(conn);
            SqlCommand cmdDataBase = new SqlCommand("Select ID, LastName from Student", conDataBase);
            this.Enabled = false;
            this.UseWaitCursor = true;

            try
            {
                SqlDataAdapter SQLDA = new SqlDataAdapter();
                SQLDA.SelectCommand = cmdDataBase;
                DataTable DBDataSet = new DataTable();
                SQLDA.Fill(DBDataSet);
                Application.DoEvents();
                BindingSource BindSource = new BindingSource();

                BindSource.DataSource = DBDataSet;
                GridStudent.DataSource = BindSource;
                SQLDA.Update(DBDataSet);

            }
            catch (Exception Ex) {
                MessageBox.Show("Data Base Error:" + Ex.Message);
            }

            this.Enabled = true;
            this.UseWaitCursor = false;
        }

        private void GridStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            GridStudent.AllowUserToAddRows = false;

            ClassDataBase db = new ClassDataBase();
            db.ConnectionString = "SQLExpress";
            MessageBox.Show(db.ConnectionString + " " + db.ReadData().ToString());
        }

        private void GridStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("R:" + GridStudent.CurrentCell.RowIndex.ToString() + " C:" +
            GridStudent.CurrentCell.ColumnIndex.ToString());
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("D Update successful.", "DB Update", MessageBoxButtons.OK, MessageBoxIcon.None);       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
