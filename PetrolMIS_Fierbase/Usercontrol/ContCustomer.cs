using FireSharp.Interfaces;
using Guna.UI2.WinForms;
using PetrolMIS_Fierbase.classes;
using PetrolMIS_Fierbase.Model;
using PetrolMIS_Fierbase.Process;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PetrolMIS_Fierbase.Usercontrol
{
    public partial class ContCustomer : UserControl
    {
        private QueryFirebase<tblCustomer> queryCus;
        private Dictionary<string, tblCustomer> dataCustomer=new Dictionary<string, tblCustomer>();
        private IFirebaseConfig firebaseConfig;
        //---------------------
        public ContCustomer()
        {
            InitializeComponent();
        }
        public ContCustomer(IFirebaseConfig config)
        {
            InitializeComponent();
            this.firebaseConfig = config;
            queryCus = new QueryFirebase<tblCustomer>(config);

        }
        private void DisplayData()
        {
            DatagridCus.Rows.Clear();
            if (dataCustomer!=null)
            {
                txtid.Text=dataCustomer.Count+1001+" ";
                foreach (KeyValuePair<string, tblCustomer> key in dataCustomer)
                {
                    DatagridCus.Rows.Add(key.Value.Id, key.Value.FirstName, key.Value.LastName,key.Value.Station,key.Value.Gender,key.Value.DOB,key.Value.Phone,key.Value.Address);
                }
            }
        }
        private async void ContCustomer_Load(object sender, EventArgs e)
        {
            dataCustomer = await queryCus.GetAllData(@"tblCustomer");
            DisplayData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtfristname.Text != "" && txtlastname.Text != ""&& txtComboBoxGender.SelectedItem.ToString()!="")
            {
                tblCustomer tbl = new tblCustomer(txtid.Text, txtfristname.Text, txtlastname.Text,txtComboBoxGender.Text,txtaddress.Text,txtphone.Text,txtstation.Text,txtPickerDOB.Text);
                if (btnAdd.Text == "Add")
                {
                    if (dataCustomer == null)
                    {
                        dataCustomer = new Dictionary<string, tblCustomer>();
                        dataCustomer.Add(txtid.Text, tbl);
                        queryCus.AddData(@"tblCustomer/" + txtid.Text, tbl);
                        MessageBox.Show("Recorde add successful...!");
                        ContCustomer_Load(sender, e);
                        clearData();
                    }
                    else
                    {
                        if (dataCustomer.ContainsKey(txtid.Text) == true)
                        {
                            MessageBox.Show("Your item id is already exis...!");
                            txtid.Focus();
                        }
                        else
                        {
                            dataCustomer.Add(txtid.Text, tbl);
                            queryCus.AddData(@"tblCustomer/" + txtid.Text, tbl);
                            MessageBox.Show("Recorde add successful...!");
                            ContCustomer_Load(sender, e);
                            clearData();
                        }
                    }

                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Please field the information...!");
            }
        }
        private void clearData()
        {
            txtid.Text = null;
            txtaddress.Text = null;
            txtphone.Text = null;
            txtfristname.Text = null;
            txtlastname.Text = null;
            txtComboBoxGender.Text = null;
            txtComboBoxGender.SelectedIndex = 0;
            txtstation.Text = null;

        }
        private void ViewDataSearch(Guna2DataGridView dg, Dictionary<string, tblCustomer> dbCustomer)
        {
            dg.Rows.Clear();
            foreach (KeyValuePair<string, tblCustomer> key in dbCustomer)
            {
                dg.Rows.Add(key.Value.Id, key.Value.FirstName, key.Value.LastName, key.Value.Station, key.Value.Gender, key.Value.DOB, key.Value.Phone, key.Value.Address);
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ViewDataSearch(DatagridCus, ClassSearchById<tblCustomer>.ClassSearchId(txtSearch.Text, dataCustomer));
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
