﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Pr_Outomation
{
    public partial class DigerCihazlar : Form
    {
        public DigerCihazlar()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        //DataTable Table;

        void Griddol()

        {
            con = new SqlConnection("Data Source=localhost;Initial Catalog=TavBel;Integrated Security=True");
            da = new SqlDataAdapter("Select *From Digrchaz", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Digrchaz");
            dataGridView1.DataSource = ds.Tables["Digrchaz"];
            con.Close();
        }
        private void DigerCihazlar_Load(object sender, EventArgs e)
        {
            searchData("");
            Griddol();
        }
        public void searchData(string valueToSearch)
        {
            /*
             string query = "SELECT * FROM Digrchaz WHERE Model like '%"+valueToSearch+"%'";
              SqlCommand cmd = new SqlCommand(query, con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               ds = new DataSet();
               con.Open();
               da.Fill(ds, "Dgrchaz");
               con.Close();
            dataGridView1.DataSource = ds.Tables["Dgrchaz"];
            */

        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            AnaMenu f2 = new AnaMenu();
            f2.Show();
            this.Hide();
        }

        private void Cihaz_btn_Click(object sender, EventArgs e)
        {
            Cihazlar ci2 = new Cihazlar();
            ci2.Show();
            this.Hide();
        }

        private void Ekle_btn_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Digrchaz (Marka,Model,Açıklama,Tarih) values (@Marka,@Model,@Açıklama,@Tarih)";
            cmd.Parameters.AddWithValue("@Marka", Marka.Text);
            cmd.Parameters.AddWithValue("@Model", Model.Text);
            cmd.Parameters.AddWithValue("@Açıklama", Açıklama.Text);
            cmd.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);

            int i = cmd.ExecuteNonQuery();

            if (i == 0)

            {
                MessageBox.Show("Kayıt ekleme işlemi başarısız.Veritabanı Hatası!");
            }
            else if (i == 1)

            {
                MessageBox.Show("Kayıt ekleme işlemi başarılı.");
            }

            con.Close();
            Griddol();
        }

        private void guncelle_btn_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Digrchaz SET Marka=@Marka,Model=@Model,Açıklama=@Açıklama,Tarih=@Tarih WHERE Marka=@Marka";
            cmd.Parameters.AddWithValue("@Marka", Marka.Text);
            cmd.Parameters.AddWithValue("@Model", Model.Text);
            cmd.Parameters.AddWithValue("@Açıklama", Açıklama.Text);
            cmd.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);
            int i = cmd.ExecuteNonQuery();

            if (i == 0)

            {
                MessageBox.Show("Kayıt güncelleme işlemi başarısız.Veritabanı Hatası!");
            }
            else if (i == 1)

            {
                MessageBox.Show("Kayıt güncelleme işlemi başarılı.");
            }
            con.Close();
            Griddol();
        }

        private void sil_btn_Click(object sender, EventArgs e)
        {
            
         /*  foreach (DataGridViewRow row in dataGridView1.SelectedRows)
           {
               dataGridView1.Rows.RemoveAt(row.Index);
           }
         */
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM [Digrchaz] WHERE Marka=@Marka";
            cmd.Parameters.AddWithValue("@Marka", dataGridView1.CurrentRow.Cells[0].Value);
            int i = cmd.ExecuteNonQuery();

            if (i == 0)

            {
                MessageBox.Show("Silme işlemi başarısız.Veritabanı Hatası!");
            }
            else if (i == 1)

            {
                MessageBox.Show("Silme işlemi başarılı.");
            }

            con.Close();
            Griddol();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex > -1)
            {
                e.PaintBackground(e.CellBounds, true);
                using (SolidBrush br = new SolidBrush(Color.Black))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                      e.CellStyle.Font, br, e.CellBounds, sf);
                }
                e.Handled = true;
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Programı kapatmak istiyor musun?";
            string title = "Programı Kapat";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)

            {
                this.Close();
            }

            else if (result == DialogResult.No)

            {
                // Do nothing  
            }

            else

            {
                // Do something  
            }
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("Select * FROM Digrchaz WHERE Marka Like '" + texsearch.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Digrchaz");
            con.Close();
            dataGridView1.DataSource = ds.Tables["Digrchaz"];
            string valueToSearch = texsearch.Text.ToString();
            searchData(valueToSearch);
        }
    }
} 
