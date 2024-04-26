﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace Magazine_System
{
    public partial class Form1 : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;"; 
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            //check table name
            cmd.CommandText = "select MagazineId from Magazines";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdstring = "SELECT TITLE FROM Magazines WHERE MagazineCategory = :category";
            OracleCommand cmdSelect = new OracleCommand(cmdstring, conn);
            cmdSelect.Parameters.Add(":category", textBox1.Text);
            OracleDataReader reader = cmdSelect.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
        
    }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cmdstring = "insert into Magazines Values(:mId,:aId,:Title,:PubDate,0,0,0,:Cat)";
            OracleCommand cmdSelect = new OracleCommand(cmdstring, conn);
            cmdSelect.Parameters.Add("mId", richTextBox3.Text);
            cmdSelect.Parameters.Add("aId", richTextBox2.Text);
            cmdSelect.Parameters.Add("Title", comboBox2.Text);
            cmdSelect.Parameters.Add("PubDate", richTextBox4.Text);
            cmdSelect.Parameters.Add("Cat", richTextBox5.Text);
            int r = cmdSelect.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Added Successfully");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

           OracleCommand cmdProc = new OracleCommand("GET_MAGAZINE_INFO", conn);
     cmdProc.CommandType = System.Data.CommandType.StoredProcedure;
     cmdProc.Parameters.Add(":p_magazine_id", comboBox2.SelectedItem.ToString());
     //check here
     cmdProc.Parameters.Add("p_title", OracleDbType.Varchar2,ParameterDirection.Output);
     cmdProc.Parameters.Add("p_author_id", OracleDbType.Int32, ParameterDirection.Output);
     cmdProc.Parameters.Add("p_publication_date", OracleDbType.Date, ParameterDirection.Output);
     cmdProc.Parameters.Add("p_category", OracleDbType.Varchar2, ParameterDirection.Output);
            int r = cmdProc.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Added Successfully");
            }

            richTextBox3.Text = cmdProc.Parameters["p_title"].Value.ToString();
     richTextBox2.Text = cmdProc.Parameters["p_author_id"].Value.ToString();
     richTextBox4.Text = ((DateTime)cmdProc.Parameters["p_publication_date"].Value).ToString("yyyy-MM-dd");
     richTextBox5.Text = cmdProc.Parameters["p_category"].Value.ToString();
            


 
           
        }

    }
}
