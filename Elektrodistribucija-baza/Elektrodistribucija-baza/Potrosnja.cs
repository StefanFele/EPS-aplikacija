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
    public partial class Potrosnja : Form
    {
        public Potrosnja()
        {
            InitializeComponent();
        }
        
        
        OleDbConnection konekcija;
        DataSet ds;
      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Potrosaci p = new Potrosaci();
                p.Datum_izdavanja = monthCalendar2.SelectionStart;
                p.Datum_uplate = monthCalendar1.SelectionStart;

                p.Kilovati = Convert.ToDouble(textBox2.Text);
                if (p.Kilovati <= 350)
                {
                    p.Cena = 5 * (int)p.Kilovati;
                    pictureBox3.BackColor = Color.Green;
                    textBox7.Text = "Zelena";
                }
                else if (p.Kilovati > 350 && p.Kilovati <= 1600)
                {
                    p.Cena = 7 * (int)p.Kilovati;
                    pictureBox3.BackColor = Color.Blue;
                    textBox7.Text = "Plava";
                }
                else if (p.Kilovati > 1600)
                {
                    p.Cena = 15 * (int)p.Kilovati;
                    pictureBox3.BackColor = Color.Red;
                    textBox7.Text = "Crvena";
                }
                textBox3.Text = p.Cena.ToString();
                if (p.Dani() <= 15)
                {
                    p.Umanjena_cena = p.Cena - (p.Cena / 10);
                    textBox4.Text = p.Umanjena_cena.ToString();
                }
                else
                {
                    textBox4.Text = p.Umanjena_cena.ToString();
                }
            }
            catch { MessageBox.Show("Unesite broj potrosenih kilovata", "UPOZORENJE"); }
        }

        private void Potrosnja_Load(object sender, EventArgs e)
        {
            Potrosaci p = new Potrosaci();
            //Form1 f = new Form1();
            textBox1.Text = Form1.maticni;
            textBox5.Text = Form1.maticni1;
            textBox6.Text = Form1.maticni2;
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            konekcija = new OleDbConnection();
            konekcija.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Elektrodistribucija.accdb";
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = string.Format("INSERT INTO Potrosnja(Potroseni_kilovati,Cena,Umanjena_cena,Zona,Datum_izdavanja,Datum_uplate,JMBG)VALUES(@Potroseni_kilovati,@Cena,@Umanjena_cena,@Zona,@Datum_izdavanja,@Datum_uplate,@JMBG)");
            komanda.Parameters.AddWithValue("@Potroseni_kilovati", textBox2.Text);
            komanda.Parameters.AddWithValue("@Cena", textBox3.Text);
            komanda.Parameters.AddWithValue("@Umanjena_cena", textBox4.Text);
            komanda.Parameters.AddWithValue("@Zona", textBox7.Text);
            komanda.Parameters.AddWithValue("@Datum_izdavanja", monthCalendar2.SelectionRange.Start.Date);
            komanda.Parameters.AddWithValue("@Datum_uplate", monthCalendar1.SelectionRange.Start.Date);
            komanda.Parameters.AddWithValue("@JMBG", textBox1.Text);
           
            ds = new DataSet();
            try
            {
                konekcija.Open();
                // for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    if (Convert.ToInt32(textBox1.Text) == Convert.ToInt32(ds.Tables[0].Rows[i]["JMBG"]))
                //    {
                //        postoji = true;
                //    }
                //}
                //if (postoji == false)
                //{
                komanda.ExecuteNonQuery();
                MessageBox.Show("Podatak dodat u bazu");
                konekcija.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Podatak postoji u bazi");
                //    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = " ";
                //    textBox1.Focus();
                //    konekcija.Close();
                //}
            }
            catch
            {
                MessageBox.Show("Greska");
            }
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();
            textBox5.DataBindings.Clear();
            textBox6.DataBindings.Clear();
            konekcija.Close();
        }
    }
}
