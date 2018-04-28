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
using System.Data.SQLite;

using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_Tecnica
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private JSONPerson personInfoModel;
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
            ImageList imagelist = new ImageList();
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Location", 100);
            listView1.Columns.Add("Phone", 100);
            listView1.Columns.Add("Cell", 100);
            listView1.Columns.Add("Email", 100);
            ListViewItem itm;

            WebRequest request = WebRequest.Create("https://randomuser.me/api?results=100");
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
            Image i;
            
            personInfoModel =
                    (JSONPerson)jsonObjectPersonInfo.ReadObject(stream1);

            string[] arr = new string[6];
            foreach (Person p in personInfoModel.results)
            {
                arr[0] = p.name.title + " " + p.name.first + " " + p.name.last;
                arr[1] = p.location.street + ", " + p.location.city + ", " + p.location.state + " - " + p.location.postcode;
                arr[2] = p.phone;
                arr[3] = p.cell;
                arr[4] = p.email;
                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
                itm.ImageKey = p.email;
                WebRequest requestImg = WebRequest.Create(p.picture.thumbnail);
                response = (HttpWebResponse)requestImg.GetResponse();
                DataStream = response.GetResponseStream();
                //reader = new StreamReader(DataStream);
                //Byte[] resp = Encoding.ASCII.GetBytes(reader.ReadToEnd());
                //MemoryStream ms = new MemoryStream(resp);
                i = new Bitmap(DataStream);
                imagelist.Images.Add(p.email, i);
                //File.Create(@"C:\Users\IEUser\Pictures\" + p.login.username);
                //FileStream f = File.Open(@"C:\Users\IEUser\Pictures\" + p.login.username, FileMode.Open, FileAccess.Write);
                //f.Write(resp, 0, resp.Length);
                //f.Close();
            }
            listView1.SmallImageList = imagelist;
        }

        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd;
            using (connection = new SQLiteConnection(@"DataSource=C:\Users\IEUser\Desktop\prueba_tecnica.db")) {
                connection.Open();
                foreach (Person p in personInfoModel.results)
                {
                    WebRequest requestImg = WebRequest.Create(p.picture.thumbnail);
                    HttpWebResponse response = (HttpWebResponse)requestImg.GetResponse();
                    Stream DataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(DataStream);
                    Byte[] resp = Encoding.ASCII.GetBytes(reader.ReadToEnd());
                    File.Create(@"C:\Users\IEUser\Pictures\" + p.login.username);
                    FileStream f = File.Open(@"C:\Users\IEUser\Pictures\" + p.login.username, FileMode.Open, FileAccess.Write);
                    f.Write(resp, 0, resp.Length);
                    f.Close();
                    cmd = "insert into PERSONS (name, location, phone, cell, email) values ('" + p.name.title.Replace("'","") + " " + p.name.first.Replace("'", "") + " " + p.name.last.Replace("'", "") + "', '" + p.location.street.Replace("'", "") + " " + p.location.city.Replace("'", "") + " " + p.location.state.Replace("'", "") + " - " + p.location.postcode.Replace("'", "") + "', '" + p.phone + "', '" + p.cell + "', '" + p.email.Replace("'", "") + "')";
                    using (command = new SQLiteCommand(cmd, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }

            //connection.ConnectionString = @"DataSource=C:\Users\IEUser\Desktop\prueba_tecnica.db";
            //command = new SQLiteCommand();
            //command = connection.CreateCommand();
            //connection.Open();
            //foreach (Person p in personInfoModel.results)
            //{
              //  command.CommandText = "insert into PERSONS (name, location, phone, cell, email) values ('"+ p.name.title + " " + p.name.first + " " + p.name.last + "', '"+ p.location.street + ", " + p.location.city + ", " + p.location.state + " - " + p.location.postcode + "','"+p.phone+"', '"+p.cell+"', '"+p.email+"')";
                //command.ExecuteNonQuery();
            //}
            //connection.Close();
        }

    }
}
