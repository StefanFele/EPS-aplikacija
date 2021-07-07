using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Elektrodistribucija_baza
{
    public partial class Brisanje : Form
    {
        public Brisanje()
        {
            InitializeComponent();
        }
        
        
        OleDbConnection konekcija;
        DataSet ds;
        
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = string.Format("DELETE FROM Korisnici WHERE JMBG=" + textBox3.Text);
            try
            {
                konekcija.Open();
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Da li zaista zelite da izbrisete podatke iz baze", "UPOZORENJE", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Podatak izbrisan iz baze");
                    konekcija.Close();
                }
                else
                {
                    if (dr == DialogResult.Cancel) return;
                    else
                        return;
                }
            }
            catch
            {
                MessageBox.Show("Greska");
            }
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            Brisanje_Load(sender, e);
            konekcija.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            OleDbConnection konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            string prvi1 = "SELECT JMBG, Ime, Prezime, Adresa";
            string drugi = "FROM Korisnici";
            string treci = "WHERE Ime LIKE '%" + textBox1.Text + "%'" + "AND Prezime LIKE '%" + textBox2.Text + "%'";
            komanda.CommandText = prvi1 + " " + drugi + " " + treci;
            try
            {
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = komanda;
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch
            {
                MessageBox.Show("Ne postoji");
            }
        }

        

        private void Brisanje_Load(object sender, EventArgs e)
        {
            konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = "SELECT JMBG, Ime, Prezime, Adresa From Korisnici";
            ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            textBox3.DataBindings.Add("Text", ds.Tables[0], "JMBG");
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            string prvi1 = "SELECT JMBG, Ime, Prezime, Adresa";
            string drugi = "FROM Korisnici";
            string treci = "WHERE Ime LIKE '%" + textBox1.Text + "%'" + "AND Prezime LIKE '%" + textBox2.Text + "%'";
            komanda.CommandText = prvi1 + " " + drugi + " " + treci;
            try
            {
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = komanda;
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch
            {
                MessageBox.Show("Ne postoji");
            }
        }
    }
}
