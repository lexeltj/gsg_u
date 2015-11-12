﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // meins

            label1.Text = "ich bin ein Text \nbeim start";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Text = "ich bin ein button";
            //MessageBox.Show("Test");
           int e3 =  myFunction();
            if (e3 == 1)
            {
                MessageBox.Show("yes");
            }
            else
            {
                MessageBox.Show("no");
            }
            Console.Write("hallo welt");

            //openFileDialog1.ShowDialog();
            //label1.Text = openFileDialog1.FileName;

            // Jetzt prüfen wir mal ob es ein bestimmtes Verzeichnis gibt
            string arma3_32_d = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\";
            string armavz;
            string arma32 = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\arma3.exe";
            string folder86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            Console.WriteLine(folder86);
            string arma64 = @"C:\Program Files\Steam\steamapps\common\Arma 3\arma3.exe"; ;
            Console.WriteLine(File.Exists(arma32) ? "File exists." : "File does not exist.");
            Console.WriteLine(File.Exists(arma64) ? "File exists." : "File does not exist.");
            label1.ForeColor = Color.Blue;
            label1.Text = arma32 + " " + (File.Exists(arma32) ? "File exists." : "File does not exist.") + "\n";
            label1.Text += arma64 + " " + (File.Exists(arma64) ? "File exists." : "File does not exist.") + "\n";
            label1.Text += (Directory.Exists(arma3_32_d) ? "Directory exists." : "Directory does not exist.") + ": ";
            label1.Text += arma3_32_d + "\n\n";

            /* if (File.Exists(arma32))
             {
                 label1.ForeColor = Color.Green;
                 label1.Text += arma32 + " \n";
                 armavz = arma32;
             }
             else
             {
                 label1.ForeColor = Color.Red;
                 label1.Text += arma32 + "\n";
                 armavz = arma64;
             }
             label1.Text = armavz;
             */
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
                label1.Text += arma3_32_d + mod[counter] + ": ";
                label1.Text +=  (Directory.Exists(arma3_32_d + mod[counter]) ? "File exists." : "File does not exist.") + " \n";
                
                counter++;
                Array.Resize(ref mod, counter + 1);

            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.
            System.Console.ReadLine();

            //label1.Text += "\n" + (Directory.Exists(arma3_32_d + mod[0]) ? "File exists." : "File does not exist.") + "\n";
            //label1.Text += arma3_32_d + mod[0] + "\n";

            label1.Text += "\n Read File: mods.txt success";
            textBox1.Text = label1.Text;
           // textBox1.ScrollToCaret();
            textBox1.Text += "\r\n" + "hallo welt";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        private int  myFunction()
        {
             int e2;
            
            DialogResult result =  MessageBox.Show("Hallo", "Nachricht von lexel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                e2 = 1;
            }
            else
            {
                e2 = 2;
            }

            return e2;
        }
    }
}