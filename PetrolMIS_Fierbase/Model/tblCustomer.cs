using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolMIS_Fierbase.Model
{
    public class tblCustomer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender {  get; set; }
        public string Address { get; set; }
        public string Phone {  get; set; }
        public string Station {  get; set; }
        public string DOB {  get; set; }
        //------------
        public tblCustomer(string Id, string Fname,string lname,string gender,string add,string phone,string station,string dob)
        {
            this.Id = Id;
            this.FirstName = Fname;
            this.LastName = lname;
            this.Gender = gender;
            this.Address = add ;
            this.Phone = phone;
            this.Station = station;
            this.DOB = dob;
        }
    }
}
