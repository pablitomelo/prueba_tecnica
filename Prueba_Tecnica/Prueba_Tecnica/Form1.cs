using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_Tecnica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1_Load();
        }

        private void Form1_Load()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Team Name", 100);
            listView1.Columns.Add("Team Logo", 100);

            string[] arr = new string[3];
            ListViewItem itm;

            WebRequest request = WebRequest.Create("https://randomuser.me/api?results100");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream DataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(DataStream);

            string responseFromServer = reader.ReadToEnd();

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //JSONPerson arra = js.Deserialize<JSONPerson>(responseFromServer);
            DataContractJsonSerializer jsonObjectPersonInfo =
            new DataContractJsonSerializer(typeof(JSONPerson));
            MemoryStream stream1 =
            new MemoryStream(Encoding.UTF8.GetBytes(responseFromServer));
        
            JSONPerson personInfoModel =
                    (JSONPerson)jsonObjectPersonInfo.ReadObject(stream1);
           


            arr[0] = "Real Madrid";
            arr[1] = "Puto";
            itm = new ListViewItem(arr);
            listView1.Items.Add(itm);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
