using PetrolMIS_Fierbase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolMIS_Fierbase.classes
{
    public static class ClassSearchById<tbl>
    {
        public static Dictionary<string,tbl> ClassSearchId(string id,Dictionary<string,tbl> db)
        {
            Dictionary<string ,tbl> t = new Dictionary<string ,tbl>();
            if(id.Length==0) return db;
            foreach(KeyValuePair<string,tbl> key in db)
            {
                if (id.Length > key.Key.Length) continue;
                string keyStr=key.Key;
                keyStr=keyStr.Substring(0, id.Length);
                if (id.ToLower().Equals( keyStr.ToLower())) t.Add(key.Key, key.Value);
            }
            return t;
        }
        
    }
}
