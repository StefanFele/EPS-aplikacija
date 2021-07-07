using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace Elektrodistribucija_baza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string maticni = "";
        public static string maticni1 = "";
        public static string maticni2 = "";
        OleDbDataReader reader;
        
        OleDbConnection konekcija;
        DataSet ds;
       
        private void Form1_Load(object sender, EventArgs e)
        {
            konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;

            string prvi = "SELECT Grad FROM Mesto";
            komanda.CommandText = prvi  ;
            konekcija.Open();
            reader = komanda.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0]);
            }
            konekcija.Close();
            komanda.CommandText = "SELECT * FROM Mesto";
            konekcija.Open();
            ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            konekcija.Close();
            string prvi1 = "SELECT Korisnici.JMBG, Korisnici.Ime, Korisnici.Prezime, Korisnici.Grad,Korisnici.Adresa,Potrosnja.Potroseni_kilovati,Potrosnja.Zona,Datum_izdavanja,Datum_uplate,Potrosnja.Cena,Potrosnja.Umanjena_cena";
            string drugi = "FROM(Korisnici INNER JOIN Potrosnja ON Korisnici.JMBG=Potrosnja.JMBG)";

            komanda.CommandText = prvi1 + " " + drugi;
            konekcija.Open();
            ds = new DataSet();
            OleDbDataAdapter da1 = new OleDbDataAdapter();
            da1.SelectCommand = komanda;
            da1.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            konekcija.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = string.Format("INSERT INTO Korisnici(JMBG,Ime,Prezime,Grad,Adresa)VALUES(@JMBG,@Ime,@Prezime,@Grad,@Adresa)");
            komanda.Parameters.AddWithValue("@JMBG", textBox1.Text);
            komanda.Parameters.AddWithValue("@Ime", textBox2.Text);
            komanda.Parameters.AddWithValue("@Prezime", textBox3.Text);
            komanda.Parameters.AddWithValue("@Grad", comboBox1.Text);
            komanda.Parameters.AddWithValue("@Adresa", textBox4.Text);


            int i;
            bool postoji = false;
            //ds = new DataSet();
            try
            {
                konekcija.Open();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt64(textBox1.Text) == Convert.ToInt64(ds.Tables[0].Rows[i]["JMBG"]))
                    {
                        postoji = true;
                    }
                }
                if (postoji == false)
                {
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Podatak dodat u bazu");
                    konekcija.Close();
                }
                else
                {
                    MessageBox.Show("Podatak postoji u bazi");
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = " ";
                    textBox1.Focus();
                    konekcija.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska: " + ex.Message);
            }
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = " ";
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();
            comboBox1.DataBindings.Clear();
            comboBox1.Items.Clear();
            Form1_Load(sender, e);
            konekcija.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = string.Format("INSERT INTO Mesto(IDmesto,Grad,IDpodrucja)VALUES(@IDmesto,@Grad,@IDpodrucja)");
            komanda.Parameters.AddWithValue("@IDmesto", textBox5.Text);
            komanda.Parameters.AddWithValue("@Grad", textBox6.Text);
            komanda.Parameters.AddWithValue("@IDpodrucja", comboBox2.Text);


            int i;
            bool postoji = false;
            
            try
            {
                konekcija.Open();
                 for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(textBox1.Text) == Convert.ToInt32(ds.Tables[0].Rows[i]["IDmesto"]))
                    {
                        postoji = true;
                    }
                }
                if (postoji == false)
                {
                komanda.ExecuteNonQuery();
                MessageBox.Show("Podatak dodat u bazu");
                konekcija.Close();
                }
                else
                {
                    MessageBox.Show("Podatak postoji u bazi");
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = " ";
                    textBox1.Focus();
                    konekcija.Close();
                }
            }
            catch
            {
                MessageBox.Show("Greska");
            }
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();
            comboBox1.DataBindings.Clear();
            comboBox1.Items.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox2.Text = "";
            Form1_Load(sender, e);
            konekcija.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                maticni = textBox7.Text;
                maticni1 = textBox8.Text;
                maticni2 = textBox9.Text;
                Potrosaci p = new Potrosaci();

                Potrosnja potrosnja = new Potrosnja();
                potrosnja.Show();
            }
            catch {
                MessageBox.Show("Izaberite nekog korisnika");
            }
            

           
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            string prvi1 = "SELECT JMBG, Ime, Prezime, Adresa";
            string drugi = "FROM Korisnici";
            string treci = "WHERE Ime LIKE '%" + textBox10.Text + "%'" + "AND Prezime LIKE '%" + textBox11.Text + "%'";
            komanda.CommandText = prvi1 + " " + drugi + " " + treci;
            try
            {
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = komanda;
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                textBox7.DataBindings.Add("Text", ds.Tables[0], "JMBG");
                textBox8.DataBindings.Add("Text", ds.Tables[0], "Ime");
                textBox9.DataBindings.Add("Text", ds.Tables[0], "Prezime");
                
            }
            catch
            {
                MessageBox.Show("Ne postoji");
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Brisanje br = new Brisanje();
            br.Show();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            string prvi1 = "SELECT Korisnici.JMBG, Korisnici.Ime, Korisnici.Prezime, Korisnici.Grad,Korisnici.Adresa,Potrosnja.Potroseni_kilovati,Potrosnja.Zona,Datum_izdavanja,Datum_uplate,Potrosnja.Cena,Potrosnja.Umanjena_cena";
            string drugi = "FROM(Korisnici INNER JOIN Potrosnja ON Korisnici.JMBG=Potrosnja.JMBG)";
            string treci = "WHERE Ime LIKE '%" + textBox12.Text + "%'" + "AND Prezime LIKE '%" + textBox2.Text + "%'";
            komanda.CommandText = prvi1 + " " + drugi + " " + treci;
            try
            {
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = komanda;
                da.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];

            }
            catch
            {
                MessageBox.Show("Ne postoji");
            }
        }

        

        
    }
}
