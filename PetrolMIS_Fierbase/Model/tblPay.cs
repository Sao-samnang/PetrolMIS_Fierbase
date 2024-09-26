using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolMIS_Fierbase.Model
{
    public class tblPay
    {
        public string IdPay { get; set; }
        public string CusID { get; set; }
        public DateTime Datepay { get; set; }
        public double PayAmount {  get; set; }
        public tblPay(string idPay, string cusID, DateTime datepay, double payAmount)
        {
            IdPay = idPay;
            CusID = cusID;
            Datepay = datepay;
            PayAmount = payAmount;
        }
        //--------

    }
}
