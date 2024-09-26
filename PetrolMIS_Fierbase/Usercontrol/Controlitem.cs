using FireSharp.Interfaces;
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
using System.Windows.Forms;

namespace PetrolMIS_Fierbase.Usercontrol
{
    public partial class Controlitem : UserControl
    {
        //
        private int indexCell;
        private QueryFirebase<tblItem> queryItem;
        private Dictionary<string, tblItem> item=new Dictionary<string, tblItem>();
        private IFirebaseConfig config;

        public Controlitem()
        {
            InitializeComponent();
        }
        public Controlitem(IFirebaseConfig config)
        {
            InitializeComponent();
            this.config = config;
            //-------
            queryItem = new QueryFirebase<tblItem>(config);
            
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void Controlitem_Load(object sender, EventArgs e)
        {
            item = await queryItem.GetAllData(@"tblItem");
            DisplayData();
        }
        private void DisplayData()
        {
            DatagrideItem.Rows.Clear();
            if (item.Count > 0)
            {
                foreach (KeyValuePair<string,tblItem> key in item)
                {
                    DatagrideItem.Rows.Add(key.Value.Id, key.Value.Name, key.Value.Description);
                }
            }
        }

        private void Controlitem_Load(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtname.Text != "" && txtdescription.Text != "")
            {
                tblItem tbl = new tblItem(txtid.Text, txtname.Text, txtdescription.Text);
                if (btnAdd.Text == "Add")
                {
                    if (item == null)
                    {
                        item = new Dictionary<string, tblItem>();
                        item.Add(txtid.Text, tbl);
                        queryItem.AddData(@"tblItem/" + txtid.Text, tbl);
                        MessageBox.Show("Recorde add successful...!");
                        Controlitem_Load(sender, e);
                        clearData();
                    }
                    else
                    {
                        if (item.ContainsKey(txtid.Text) == true)
                        {
                            MessageBox.Show("Your item id is already exis...!");
                            txtid.Focus();
                        }
                        else
                        {
                            item.Add(txtid.Text, tbl);
                            queryItem.AddData(@"tblItem/" + txtid.Text, tbl);
                            MessageBox.Show("Recorde add successful...!");
                            Controlitem_Load(sender, e);
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

        private void DatagrideItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexCell=e.RowIndex;
            btnAdd.Text = "Update";
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ClassViewControl.ViewControl(panelItem, new ControlView(config));
        }
        private void clearData()
        {
            txtid.Text = "";
            txtname.Text = "";
            txtdescription.Text = "";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add";
            clearData();
        }

        private void DatagrideItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelItem_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
