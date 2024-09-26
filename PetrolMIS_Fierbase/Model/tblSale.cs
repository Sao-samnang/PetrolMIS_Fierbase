using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolMIS_Fierbase.Model
{
    public class tblSale
    {
        public string InvoiceID {  get; set; }
        public string CutomerID { get; set;}
        public DateTime Date {  get; set; }
        public int Qty {  get; set; }
        public double Price {  get; set; }
        public string Petrol { get; set; }
        public string Petrol_type { get; set; }
        //public double Total {  get; set; }
        // Contructor
        public tblSale(string InvoiceID,string CutomerID,DateTime Date,int Qty,double Price,string Petrol,string Petrol_type)
        {
            this.InvoiceID = InvoiceID;
            this.CutomerID = CutomerID;
            this.Date = Date;
            this.Qty = Qty;
            this.Price = Price;
            this.Petrol = Petrol;
            this.Petrol_type = Petrol_type;

        }
        // method
        public double Total()
        {
            return Qty*Price;
        }
    }
}
