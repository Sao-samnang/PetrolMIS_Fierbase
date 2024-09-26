using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
namespace PetrolMIS_Fierbase.Process
{
    public class QueryFirebase<tbl>
    {
        private IFirebaseClient _client;
        public QueryFirebase() { 
            
        }
        public QueryFirebase(IFirebaseConfig conf)
        {
            try
            {
                _client = new FirebaseClient(conf);
            }catch (Exception ex)
            {
                MessageBox.Show("No Internet...!"+ex);
            }
        }
        // add data
        public async void AddData(string table, tbl tb)
        {
            try
            {
                await _client.SetAsync(table, tb);
            }catch(Exception e)
            {
                MessageBox.Show(e + "");
            }
        }
        //------- update
        public async void Update(string table, tbl tb)
        {
            try
            {
                await _client.UpdateAsync(table, tb);
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
        }
        //------------> delete
        public async void Delete(string key)
        {
            try
            {
                await _client.DeleteAsync(key);
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
        }       
        //---- get 1 data
        public async Task<tbl> Get(string key)
        {
            var obj=await _client.GetAsync(key);
            tbl tb=obj.ResultAs<tbl>();
            return tb;
        }
        public async Task<Dictionary<string,tbl>> GetAllData(string table)
        {
            Dictionary<string,tbl> dis = new Dictionary<string,tbl>();
            try
            {
                FirebaseResponse res=await _client.GetAsync(table);
                dis=JsonConvert.DeserializeObject<Dictionary<string, tbl>>(res.Body.ToString());
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
            return dis;
        }
    }
}
