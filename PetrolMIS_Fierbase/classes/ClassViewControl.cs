using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PetrolMIS_Fierbase.classes
{
    public static class ClassViewControl
    {
        public static void ViewControl(System.Windows.Forms.Panel pl, System.Windows.Forms.UserControl uc)
        {
            pl.Controls.Clear();
            pl.Controls.Add(uc);
            uc.Dock=DockStyle.Fill;
            uc.BringToFront();
        }
    }
}
