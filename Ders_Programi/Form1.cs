using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ders_Programi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-ANLV0LI;Initial Catalog=veri;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);

        public void veri_getir()
        {

            string getir = "Select *From Derss";

            SqlCommand komut = new SqlCommand(getir, baglan);

            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

        }
        public void verisil(int id)
        {
            string sil = "Delete From Derss Where ders_id =@id";

            SqlCommand komut = new SqlCommand(sil, baglan);
            baglan.Open();


            komut.Parameters.AddWithValue("@id", id);

            komut.ExecuteNonQuery();
            baglan.Close();
        }

        private void button_liste_Click(object sender, EventArgs e)
        {
            veri_getir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string veri = "insert into Derss (ders_adi,ders_gunu,ogr_adsoyad,email)values(@dersadi," +
                    "@dersgunu,@ogradsoyad,@email)";
                    SqlCommand komut = new SqlCommand(veri, baglan);
                    komut.Parameters.AddWithValue("@dersadi", textBox1.Text);
                    komut.Parameters.AddWithValue("@dersgunu", textBox2.Text);
                    komut.Parameters.AddWithValue("@ogradsoyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@email", textBox4.Text);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("kayıt ekleme işleminiz başarılı.");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("bir hata var!" + hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string veri = "Select*From Derss Where ders_adi=@dersadi";
            SqlCommand komut = new SqlCommand(veri, baglan);

            komut.Parameters.AddWithValue("@dersadi", textBox_ara.Text);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                verisil(id);
                veri_getir();
            }
        }
        int i = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string veriguncelle = ("Update Derss Set ders_adi = @dersadi,ders_gunu=@dersgunu,ogr_adsoyad=@ogradsoyad," +
               "email=@email  Where ders_id = @id");

            SqlCommand komut = new SqlCommand(veriguncelle, baglan);


            komut.Parameters.AddWithValue("@dersadi", textBox1.Text);
            komut.Parameters.AddWithValue("@dersgunu", textBox2.Text);
            komut.Parameters.AddWithValue("@ogradsoyad", textBox3.Text);
            komut.Parameters.AddWithValue("@email", textBox4.Text);
            komut.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);
            komut.ExecuteNonQuery();
            MessageBox.Show("kayıtlar Başarıyla Güncellendi");
            baglan.Close();
            veri_getir();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();

        }
    }
}
