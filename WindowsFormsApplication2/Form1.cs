using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        // Функция расчета скидки
        public static double CalculateDiscount(int amount)
        {
            if (amount < 10000) return 0;
            else if (amount < 50000) return 0.05; // 5%
            else if (amount < 300000) return 0.10; // 10%
            else return 0.15; // 15%
        }

        private void LoadData()
        {
            string connectionString = "Data Source=DESKTOP-9OEEF0T;Initial Catalog=MasterPol;Integrated Security=True";
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {
                string query1 = "SELECT * FROM Partner;";
                string query2 = "SELECT Import.name_Partner, SUM(Import.kol_Product) AS [Sum-Количество продукции] FROM Import GROUP BY Import.name_Partner;";
                SqlCommand command1 = new SqlCommand(query1, connection1);
                SqlCommand command2 = new SqlCommand(query2, connection2);
                connection1.Open();
                connection2.Open();
                SqlDataReader reader1 = command1.ExecuteReader();
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader1.Read())// Переход к первой строке данных
                {
                    label1.Text = reader1[0].ToString();
                    label2.Text = reader1[1].ToString();
                    label3.Text = reader1[2].ToString();
                    label4.Text = reader1[4].ToString();
                    label5.Text = reader1[7].ToString();
                }
                if (reader1.Read())  // Переход ко второй строке данных
                {
                    label12.Text = reader1[0].ToString();
                    label11.Text = reader1[1].ToString();
                    label10.Text = reader1[2].ToString();
                    label9.Text = reader1[4].ToString();
                    label8.Text = reader1[7].ToString();
                }
                if (reader1.Read())  // Переход ко третьей строке данных
                {
                    label18.Text = reader1[0].ToString();
                    label17.Text = reader1[1].ToString();
                    label16.Text = reader1[2].ToString();
                    label15.Text = reader1[4].ToString();
                    label14.Text = reader1[7].ToString();
                }
                if (reader1.Read())  // Переход к четвертой строке данных
                {
                    label24.Text = reader1[0].ToString();
                    label23.Text = reader1[1].ToString();
                    label22.Text = reader1[2].ToString();
                    label21.Text = reader1[4].ToString();
                    label20.Text = reader1[7].ToString();
                }
                if (reader1.Read())  // Переход к пятой строке данных
                {
                    label30.Text = reader1[0].ToString();
                    label29.Text = reader1[1].ToString();
                    label28.Text = reader1[2].ToString();
                    label27.Text = reader1[4].ToString();
                    label26.Text = reader1[7].ToString();
                }
                if (reader2.Read())  // Чтение данных для вычисления процента
                {
                    label31.Text= (CalculateDiscount(Convert.ToInt32(reader2[1])) * 100).ToString()+"%";
                                    }
                if (reader2.Read())  // Чтение данных для вычисления процента
                {
                    label32.Text = (CalculateDiscount(Convert.ToInt32(reader2[1])) * 100).ToString() + "%";
                }
                if (reader2.Read())  // Чтение данных для вычисления процента
                {
                    label33.Text = (CalculateDiscount(Convert.ToInt32(reader2[1])) * 100).ToString() + "%";
                }
                if (reader2.Read())  // Чтение данных для вычисления процента
                {
                    label34.Text = (CalculateDiscount(Convert.ToInt32(reader2[1])) * 100).ToString() + "%";
                }
                if (reader2.Read())  // Чтение данных для вычисления процента
                {
                    label35.Text = (CalculateDiscount(Convert.ToInt32(reader2[1])) * 100).ToString() + "%";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
              Application.Exit();
        }
       
        private void panel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2(0);
            newForm.Show();
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2(1);
            newForm.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2(2);
            newForm.Show();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2(3);
            newForm.Show();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 newForm = new Form2(4);
            newForm.Show();
        }
    }
}
