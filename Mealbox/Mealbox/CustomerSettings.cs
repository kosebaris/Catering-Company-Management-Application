﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mealbox
{
    public partial class CustomerSettings : Form
    {

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;

        int sonmenuid;


        public CustomerSettings()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            MainPage frm = new MainPage();
            this.Hide();
            frm.Show();
        }

        private void CustomerSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBox_name.Text = "";
            textBox_lastname.Text = "";
            textBox_kart.Text = "";
            textBox_mobile.Text = "";
            textBox_email.Text = "";
        }

        void MusteriGetir()
        {
            baglanti = new SqlConnection("Data Source = MSI\\SQLEXPRESS; Initial Catalog = db_mealbox; Integrated Security = True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM CUSTOMER_TABLE", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();


        }

        private void CustomerSettings_Load(object sender, EventArgs e)
        {
            MusteriGetir();
            listBox1.Items.Clear();
            ListboxDoldur();
            

        }

        void ListboxDoldur()
        {
            baglanti = new SqlConnection("Data Source = MSI\\SQLEXPRESS; Initial Catalog = db_mealbox; Integrated Security = True");
            komut = new SqlCommand("SELECT * FROM MENU_TABLE", baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
                listBox1.Items.Add(dr["ID"].ToString());


            baglanti.Close();


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox_lastname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox_mobile.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox_email.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox_kart.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            /*int rowcount = dataGridView1.Rows.Count - 1;

            sonmenuid = Int32.Parse(dataGridView1.Rows[rowcount].Cells[0].Value.ToString());

            //MessageBox.Show(sonmenuid.ToString());
            */

            listBox1.Items.Clear();
            ListboxDoldur();

            
            int a = 0;

            while (a < listBox1.Items.Count)
            {

                if (Int32.Parse(listBox1.Items[a].ToString()) == Int32.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
                listBox1.SelectedIndex = a;

                
                a++;
            }


        }

        private void button_add_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO CUSTOMER_TABLE(FIRSTNAME,LASTNAME,PHONENUMBER,EMAIL,KART,SELECTEDMENU) VALUES (@FIRSTNAME,@LASTNAME,@PHONENUMBER,@EMAIL,@KART,@SELECTEDMENU)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@FIRSTNAME", textBox_name.Text);
            komut.Parameters.AddWithValue("@LASTNAME", textBox_lastname.Text);
            komut.Parameters.AddWithValue("@PHONENUMBER", textBox_mobile.Text);
            komut.Parameters.AddWithValue("@EMAIL", textBox_email.Text);
            komut.Parameters.AddWithValue("@KART", textBox_kart.Text);
            komut.Parameters.AddWithValue("@SELECTEDMENU", listBox1.SelectedItem.ToString());
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }
    }
}