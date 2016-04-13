using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace gsg_u
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            // meins

            label1.Text = "Hallo, \nIch update deine ARMA3 - GSG Mod's";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "beenden")
            {
                Application.Exit();
            }
            else
            {
                // ---------------- Arma Verzeichniss suchen ---------------

                string vz = sucheVerzeichnis();
                textBox1.Text += " gefunden.\r\n" + vz;

                // -------- Mod - Datei einlesen
                modDateiEinlesen(vz);
                // ------ test
               
               
                textBox1.Text += "\r\n" + md5_do(@"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\mods.txt");
                // --- serverdatei download
                serverModsEinlesen(vz);
                xmlLesen(vz + "gsg_mod.xml");
            }
        }
        private void modDateiEinlesen(string a_verzeichnis)
             {
            // Datei einlesen
            
            int counter = 0;
            string line;
            string[] mod = new string[1];

            
            // Read the file and display it line by line.
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\mods.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                mod[counter] = line;
                label1.Text += a_verzeichnis + mod[counter] + ": ";
                label1.Text +=  (Directory.Exists(a_verzeichnis + mod[counter]) ? "File exists." : "File does not exist.") + " \n";
                
                counter++;
                Array.Resize(ref mod, counter + 1);

            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.
            System.Console.ReadLine();
            
            textBox1.Text += "\r\n Read File: mods.txt success";
           
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
                button1.Text = "beenden";
                button1.Enabled = true;
            }
        
        private string sucheVerzeichnis()
        {
            // Jetzt prüfen wir mal ob es ein bestimmtes Verzeichnis gibt
            string arma32 = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\";
            string arma64 = @"C:\Program Files\Steam\steamapps\common\Arma 3\";

            if (Directory.Exists(arma32))
            {
                //textBox1.Text = arma32;
                return arma32;
            }
            else if (Directory.Exists(arma64))
            {
                //textBox1.Text = arma64;
                return arma64;
            }
            else
            {
                string arma_anders = "";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                     arma_anders = folderBrowserDialog1.SelectedPath;
                    
                }
                return arma_anders;
            }
        }

        public string  md5_do(string pbo)
        {
            
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(pbo))
                {
                    //return Encoding.Default.GetString(md5.ComputeHash(stream));
                   return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }
        public bool vergleich(string local, string remote)
        {
            // --- lokalen hash mit remote hash vergleichen ---
            return  (local == remote) ? true : false;
        }
        public void serverModsEinlesen(string arma_vz)
        {
            //---------- Aufrufen des Pfades zum Server / Modordner und Pbo's abfragen / MD5-Checksum erstellen ----------
            // ---- Download der Cheksum Datei ----
            WebClient webClient = new WebClient();
            //webClient.DownloadFile("http://server.grenzschutzgruppe.de/mod_updates/check.txt", @"c:\Users\lexel\check.txt"); // ist nur zum testen. wird noch angepasst
            // ---- Download der aktuellen XML Datei vom Server ----
            webClient.DownloadFile("http://server.grenzschutzgruppe.de/mod_updates/gsg_mod.xml", arma_vz + "gsg_mod.xml");
            // ---- Auslesen der Datei ----

        }
        public void xmlLesen(string xmlDatei)

        {

            int c = 0;
            XElement xelement = XElement.Load(xmlDatei);
            IEnumerable<XElement> gsg_mod = xelement.Elements();
            Console.WriteLine("List of all mod Names along with their ID and the rest:");
            foreach (var mod in gsg_mod)
            {
                Console.WriteLine("name {0} und wert {1} und key {2}",

                    mod.Element("pbo").Value,
                    mod.Element("hash").Value,
                    mod.Element("key"));
               
                    dataGridView1.Rows.Insert(c, mod.Element("pbo").Value, mod.Element("hash").Value, mod.Element("key").Value);
                   
                
                c++;
            }
        }
    }
}
