using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolMIS_Fierbase.Model
{
    public class tblItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public tblItem(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
