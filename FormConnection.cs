using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicConnectionString
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            //string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", cboServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text);
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", cboServer.Text, txtDatabase.Text);
            try
            {
                SqlHelper helper = new SqlHelper(connectionString);
                if(helper.IsConnection)
                    MessageBox.Show("Test connection succeeded","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //btnSaveConnection.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //btnSaveConnection.Enabled = false;
            }
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            cboServer.Items.Add(".");
            cboServer.Items.Add("(local)");
            cboServer.Items.Add(@".\SQLEXPRESS2019");
            cboServer.Items.Add(string.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
            cboServer.SelectedIndex = 2;
        }

        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
             string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", cboServer.Text, txtDatabase.Text);
            try
            {
                SqlHelper helper = new SqlHelper(connectionString);
                if (helper.IsConnection)
                {
                    AppSetting setting = new AppSetting();
                    setting.SaveConnectionString("cn", connectionString);
                    MessageBox.Show("Your connection string has been successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //btnSaveConnection.Enabled = false;
            }
        }
    }
}
