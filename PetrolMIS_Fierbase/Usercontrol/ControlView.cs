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
using System.Windows.Forms;

namespace PetrolMIS_Fierbase.Usercontrol
{
    public partial class ControlView : UserControl
    {
        private int cellIndex;
        private IFirebaseConfig _config;
        private QueryFirebase<tblItem> queryItem;
        private Dictionary<string, tblItem> dbitem;
        public ControlView()
        {
            InitializeComponent();
        }
        public ControlView(IFirebaseConfig config)
        {
            InitializeComponent();
            this._config = config;
            queryItem = new QueryFirebase<tblItem>(config);
        }

        private async void ControlView_Load(object sender, EventArgs e)
        {
            dbitem = await queryItem.GetAllData(@"tblItem/");
            DisplayData();
        }
        private void DisplayData()
        {
            DatagrideItem.Rows.Clear();
            if (dbitem.Count > 0)
            {
                foreach (KeyValuePair<string, tblItem> key in dbitem)
                {
                    DatagrideItem.Rows.Add(key.Value.Id, key.Value.Name, key.Value.Description);
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ViewDataSearch(Guna2DataGridView dg,Dictionary<string ,tblItem> dbitem)
        {
            dg.Rows.Clear();
            foreach (KeyValuePair<string, tblItem> key in dbitem)
            {
                dg.Rows.Add(key.Value.Id, key.Value.Name, key.Value.Description);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            ViewDataSearch(DatagrideItem, ClassSearchById<tblItem>.ClassSearchId(txtsearch.Text, dbitem));
        }

        private async void DatagrideItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cellIndex=e.RowIndex;
            if (e.ColumnIndex==3) {
                DialogResult rs = MessageBox.Show("Do you want to delete ?", "", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (DialogResult.OK == rs)
                {
                    string id = DatagrideItem.Rows[cellIndex].Cells[0].Value.ToString();
                    queryItem.Delete(@"tblItem/" + id);
                    dbitem.Remove(id);
                    ControlView_Load(sender, e);
                }
            }
        }
    }
}
