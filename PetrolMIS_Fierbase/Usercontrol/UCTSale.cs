using FireSharp.Interfaces;
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

namespace PetrolMIS_Fierbase.Usercontrol
{
    public partial class UCTSale : UserControl
    {
        private QueryFirebase<tblSale> QuerySale;
        private Dictionary<string, tblSale> dataSale=new Dictionary<string, tblSale>();
        private IFirebaseConfig firebaseConfig;
        //----------
        private QueryFirebase<tblCustomer> QueryCus;
        private Dictionary<string , tblCustomer> dataCus=new Dictionary<string, tblCustomer>();
        private List<string> CustomerID = new List<string>();
        //-------- item petrol
        public UCTSale()
        {
            InitializeComponent();
        }
        public UCTSale(IFirebaseConfig Config)
        {
            InitializeComponent();
            this.firebaseConfig = Config;
            QuerySale = new QueryFirebase<tblSale>(Config);
            QueryCus = new QueryFirebase<tblCustomer>(Config);
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }
        private async void UCTSale_Load(object sender, EventArgs e)
        {
            dataSale = await QuerySale.GetAllData(@"tblSale");
            dataCus = await QueryCus.GetAllData(@"tblCustomer");
            txtInvoiceID.Enabled = false;
            if (dataSale == null)
            {
                txtInvoiceID.Text = 1 + " ";
            }
            else
            {
                txtInvoiceID.Text = dataSale.Count + 1 + " ";
            }
            if (dataCus != null)
            {
                listBoxName.Items.Clear();
                foreach(KeyValuePair<string,tblCustomer> cus in dataCus)
                {
                    listBoxName.Items.Add(cus.Value.FirstName + " " + cus.Value.LastName);
                    CustomerID.Add(cus.Key);
                }
            }
        }
        // view customer info
        private void viewCustomerInfo(tblCustomer CustomerTbl)
        {
            lblCusID.Text=CustomerTbl.Id.ToString();
            lblCusname.Text=CustomerTbl.FirstName.ToString()+" "+CustomerTbl.LastName.ToString();
            lbldob.Text=CustomerTbl.DOB.ToString();
            lblgender.Text=CustomerTbl.Gender.ToString();
            lblAdd.Text=CustomerTbl.Address.ToString();
            lblPhone.Text=CustomerTbl.Phone.ToString();
            lblStation.Text=CustomerTbl.Station.ToString();
        }

        private void listBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            int index=listBoxName.FindString(txtName.Text);
            if (index != -1)
            {
                listBoxName.SetSelected(index, true);
            }
            else
            {
                listBoxName.SelectedIndex = -1;
            }
        }

        private void listBoxName_MouseClick(object sender, MouseEventArgs e)
        {
            string key = CustomerID[listBoxName.SelectedIndex];
            if (key != "")
            {
                viewCustomerInfo(dataCus[key]);
            }
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            if (txtInvoiceID.Text == "" && txtQTY.Text == "" && txtPrice.Text == "")
            {
                MessageBox.Show("Please input information in the field..!");
            }
            else
            {
                tblSale tbsale = new tblSale(txtInvoiceID.Text, lblCusID.Text, txtPickerDOB.Value, int.Parse(txtQTY.Text), double.Parse(txtPrice.Text),txtComboPetrol.SelectedItem.ToString(),txtcomboPetrolType.SelectedItem.ToString());
                if (dataSale == null)
                {
                    if (lblCusID.Text != "")
                    {
                        dataSale = new Dictionary<string, tblSale>();
                        dataSale.Add(txtInvoiceID.Text, tbsale);
                        QuerySale.AddData(@"tblSale/" + txtInvoiceID.Text, tbsale);
                        DatagridSale.Rows.Add(txtInvoiceID.Text,txtPickerDOB.Value,txtComboPetrol.SelectedItem.ToString(),txtcomboPetrolType.SelectedItem.ToString(),int.Parse(txtQTY.Text),double.Parse(txtPrice.Text));
                        MessageBox.Show("Successfully..!");
                    }
                }else if (dataSale.ContainsKey(txtInvoiceID.Text)==true)
                {
                    MessageBox.Show("Invoice ID exis...!");
                }
                else
                {
                    dataSale.Add(txtInvoiceID.Text, tbsale);
                    QuerySale.AddData(@"tblSale/" + txtInvoiceID.Text, tbsale);
                    DatagridSale.Rows.Add(tbsale.InvoiceID, tbsale.Date, tbsale.Petrol, tbsale.Petrol_type, tbsale.Qty, tbsale.Price);
                    MessageBox.Show("Successfully..!");
                }
            }
        }

        private void DatagridSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
