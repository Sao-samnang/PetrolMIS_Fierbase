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
using System.Windows.Forms;

namespace PetrolMIS_Fierbase.Usercontrol
{
    public partial class UTCPay : UserControl
    {
        private QueryFirebase<tblPay> querypay;
        private Dictionary<string, tblPay> dataPay = new Dictionary<string, tblPay>();
        private IFirebaseConfig firebaseConfig;
        //----------
        private QueryFirebase<tblCustomer> queryCus;
        private Dictionary<string, tblCustomer> dataCus = new Dictionary<string, tblCustomer>();
        private List<string> CustomerID = new List<string>();
        //--------------
        private QueryFirebase<tblSale> querySale;
        private Dictionary<string, tblSale> dataSale = new Dictionary<string, tblSale>();
        private List<string> SaleID = new List<string>();
        public UTCPay()
        {
            InitializeComponent();
        }
        public UTCPay(IFirebaseConfig config)
        {
            InitializeComponent();
            this.firebaseConfig = config;
            queryCus = new QueryFirebase<tblCustomer>(config);
            querypay = new QueryFirebase<tblPay>(config);
            querySale = new QueryFirebase<tblSale>(config);
        }

        private async void UTCPay_Load(object sender, EventArgs e)
        {
            dataCus = await queryCus.GetAllData(@"tblCustomer");
            dataPay = await querypay.GetAllData(@"tblPay");
            dataSale = await querySale.GetAllData(@"tblSale");
            if (dataPay == null)
            {
                txtPayID.Text = 1 + " ";
            }
            else
            {
                txtPayID.Text = dataPay.Count + 1 + " ";
            }
            //----------
           
            foreach (KeyValuePair<string,tblCustomer> cus in dataCus)
            {
                listBoxName.Items.Add(cus.Value.FirstName+" "+cus.Value.LastName);
                CustomerID.Add(cus.Key);
            }
        }
        private void viewCustomerInfo(tblCustomer CustomerTbl)
        {
            lblCusID.Text = CustomerTbl.Id.ToString();
            lblCusname.Text = CustomerTbl.FirstName.ToString() + " " + CustomerTbl.LastName.ToString();
            lbldob.Text = CustomerTbl.DOB.ToString();
            lblgender.Text = CustomerTbl.Gender.ToString();
            lblAdd.Text = CustomerTbl.Address.ToString();
            lblPhone.Text = CustomerTbl.Phone.ToString();
            lblStation.Text = CustomerTbl.Station.ToString();
        }
        private void ClearData()
        {
            txtPayID.Text= null;
            txtName.Text= null;
            txtQTY.Text= null;
            txtPrice.Text= null;
            txtAmount.Text= null;
            lblCusID.Text= null;
            lblCusname.Text= null;
            lbldob.Text= null;
            lblgender.Text= null;
            lblAdd.Text= null;
            lblPhone.Text= null;
            lblStation.Text= null;
        }
        private void listBoxName_MouseClick(object sender, MouseEventArgs e)
        {
            string key = CustomerID[listBoxName.SelectedIndex];
            if (key != "")
            {
                viewCustomerInfo(dataCus[key]);
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            int index = listBoxName.FindString(txtName.Text);
            if (index != -1)
            {
                listBoxName.SetSelected(index, true);
            }
            else
            {
                listBoxName.SelectedIndex = -1;
            }
        }

        private void txtPayID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnpay_Click(object sender, EventArgs e)
        {
            if (txtPayID.Text != "" && lblCusID.Text != "" && txtAmount.Text != "" && txtPickerdatepay.Text != "")
            {
                tblPay pay = new tblPay(txtPayID.Text, lblCusID.Text, txtPickerdatepay.Value,double.Parse(txtAmount.Text));
                if (dataPay == null)
                {
                    dataPay = new Dictionary<string, tblPay>();
                    dataPay.Add(txtPayID.Text,pay);
                    querypay.AddData(@"tblPay/"+ txtPayID.Text, pay);
                    MessageBox.Show("Successfully..!");
                    ClearData();
                    txtPayID.Text = (dataPay.Count + 1) + "";
                }
                else if (dataPay.ContainsKey(txtPayID.Text) == true)
                {
                    MessageBox.Show("Invoice ID exis...!");
                }
                else
                {
                    dataPay.Add(txtPayID.Text, pay);
                    querypay.AddData(@"tblPay/" + txtPayID.Text, pay);
                    MessageBox.Show("Successfully..!");
                    ClearData();
                    txtPayID.Text = (dataPay.Count + 1) + "";
                }
            }
        }
    }
}
