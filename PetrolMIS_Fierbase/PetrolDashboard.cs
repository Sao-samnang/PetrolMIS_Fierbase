using FireSharp.Interfaces;
using PetrolMIS_Fierbase.Usercontrol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetrolMIS_Fierbase
{
    public partial class PetrolDashboard : Form
    {
        private IFirebaseConfig _config = new FireSharp.Config.FirebaseConfig()
        {
            BasePath = "https://db-petrol-default-rtdb.asia-southeast1.firebasedatabase.app/",
            AuthSecret = "baM6W5eLuu9zff8792PxbCfeVquZJSYUYbvlL2L3"
        };
        public PetrolDashboard()
        {
            InitializeComponent();
        }
        

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void PetrolDashboard_Load(object sender, EventArgs e)
        {

        }

        private void CallUserFm(UserControl control)
        {
            PanelShowConrtrolMenu.Controls.Clear();
            PanelShowConrtrolMenu.Controls.Add(control);
            control.Dock= DockStyle.Fill;
            control.BringToFront();
        }
        private void btnMenuHome_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            CallUserFm(new Controlitem(_config));
        }

        private void btnMenuCustomer_Click(object sender, EventArgs e)
        {
            CallUserFm(new ContCustomer(_config));
        }

        private void btnMenuSale_Click(object sender, EventArgs e)
        {
            CallUserFm(new UCTSale(_config));
        }

        private void menuBtnPay_Click(object sender, EventArgs e)
        {
            CallUserFm(new UTCPay(_config));
        }
    }
}
