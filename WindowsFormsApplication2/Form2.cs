using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-9OEEF0T;Initial Catalog=MasterPol;Integrated Security=True;");
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        private string recordId; // Переменная для хранения выбранного имени партнера

        public Form2()//Конструктор формы
        {
            InitializeComponent();
            LoadData();
        }
        public Form2(int recordId)//Конструктор формы при щелчке на панели
        {
            InitializeComponent();
            LoadData();
            int targetRowIndex = recordId;
            dataGridView1.CurrentCell = dataGridView1.Rows[targetRowIndex].Cells[0];
            dataGridView1.Rows[targetRowIndex].Selected = true;
        }

        private void FillTextBoxes(DataRow row)
        {
            textBox1.Text = row["name_Partner"].ToString();
            comboBox1.Text = row["type_Partner"].ToString();
            textBox2.Text = row["shef"].ToString();
            textBox3.Text = row["email"].ToString();
            textBox4.Text = row["phone"].ToString();
            textBox5.Text = row["adress"].ToString();
            textBox6.Text = row["inn"].ToString();
            textBox7.Text = row["reiting"].ToString();
        }

        private void PopulateComboBoxes()
        {
            comboBox1.Items.Clear(); // Очищаем ComboBox перед загрузкой новых данных
            comboBox2.Items.Clear(); // Очищаем ComboBox перед загрузкой новых данных

            foreach (DataRow row in dataTable.Rows)
            {
                string typePartner = row["type_Partner"].ToString();
                if (!comboBox1.Items.Contains(typePartner)) // Избегаем дублирования
                    comboBox1.Items.Add(typePartner);
                if (!comboBox2.Items.Contains(typePartner)) // Избегаем дублирования
                    comboBox2.Items.Add(typePartner);
            }

            // Устанавливаем первый элемент выбранным
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Partner;", connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                PopulateComboBoxes(); // Заполняем ComboBox элементами из DataTable

                // Привязываем DataTable к DataGridView
                dataGridView1.DataSource = dataTable;

                // Настройка DataGridView
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Добавляем обработчик события SelectionChanged
                dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

                // Заполняем текстовые поля данными из первой строки (если есть)
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    FillTextBoxes(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataRowView selectedRow = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
                    if (selectedRow != null)
                    {
                        FillTextBoxes(selectedRow.Row);
                        recordId = selectedRow["name_Partner"].ToString(); // Обновляем recordId
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в обработчике SelectionChanged: " + ex.Message);
            }
        }

        private void UpdateDataGridView() // Обновляем DataGridView
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9OEEF0T;Initial Catalog=MasterPol;Integrated Security=True;"))
                {
                    dataAdapter = new SqlDataAdapter("SELECT * FROM Partner;", conn);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }
        }

        // Добавление партнера
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9OEEF0T;Initial Catalog=MasterPol;Integrated Security=True;"))
                {
                    string query = "Insert into Partner (name_Partner, type_Partner, shef, email, phone, adress, inn, reiting) values (@Naim, @tip, @dir, @email, @tel, @adr, @inn, @reit)";
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Naim", textBox14.Text);
                    command.Parameters.AddWithValue("@tip", comboBox2.Text);
                    command.Parameters.AddWithValue("@dir", textBox13.Text);
                    command.Parameters.AddWithValue("@email", textBox12.Text);
                    command.Parameters.AddWithValue("@tel", textBox11.Text);
                    command.Parameters.AddWithValue("@adr", textBox10.Text);
                    command.Parameters.AddWithValue("@inn", textBox9.Text);
                    command.Parameters.AddWithValue("@reit", Convert.ToInt32(textBox8.Text));
                    command.ExecuteNonQuery();
                    UpdateDataGridView();
                    MessageBox.Show("Партнер успешно добавлен.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
            finally
            {
                // Очищаем текстовые поля после добавления
                textBox14.Clear();
                comboBox2.SelectedIndex = -1;
                textBox13.Clear();
                textBox12.Clear();
                textBox11.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
            }
        }

        // Обновить информацию о партнере
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(recordId))
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }
            try
            {
                DataRow[] rows = dataTable.Select($"[name_Partner] = '{recordId.Replace("'", "''")}'"); // Найдем измененную строку в DataTable
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    row["name_Partner"] = textBox1.Text;  // Сохраняем значение из текстового поля
                    row["type_Partner"] = comboBox1.Text;
                    row["shef"] = textBox2.Text;
                    row["email"] = textBox3.Text;
                    row["phone"] = textBox4.Text;
                    row["adress"] = textBox5.Text;
                    row["inn"] = textBox6.Text;
                    row["reiting"] = Convert.ToInt16(textBox7.Text); // Преобразуем в число, если это необходимо

                    using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9OEEF0T;Initial Catalog=MasterPol;Integrated Security=True;"))
                    {
                        dataAdapter.Update(dataTable); // Обновляем данные в базе
                    }

                    UpdateDataGridView(); // Обновляем DataGridView после изменений
                    MessageBox.Show("Изменения сохранены успешно.");
                }
                else
                {
                    MessageBox.Show("Не найдена запись для сохранения.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 newForm = new Form1();
            newForm.Show();
        }
    }
}