using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_Comida
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Juanc\texte\Erp Comida\loginRegistro.mdf"";Integrated Security=True");
            conn.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verifique se todos os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(Password.Text) || string.IsNullOrWhiteSpace(nome.Text) || string.IsNullOrWhiteSpace(email.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Using statement para gerenciamento de recursos
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Juanc\texte\Erp Comida\loginRegistro.mdf"";Integrated Security=True"))
            {
                conn.Open();

                // Verifique se o email já existe
                using (var checkCmd = new SqlCommand("SELECT * FROM Login WHERE email = @Email", conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email.Text);

                    using (var dr = checkCmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MessageBox.Show("Email já existe, por favor, tente outro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Insira um novo usuário se o email não existir
                using (var insertCmd = new SqlCommand("INSERT INTO Login (ID,nome, Password, email, type) VALUES (@Nome, @Password, @Email, @Type)", conn))
                {

                    insertCmd.Parameters.AddWithValue("@nome", nome.Text);
                    insertCmd.Parameters.AddWithValue("@Password", Password.Text);
                    insertCmd.Parameters.AddWithValue("@email", email.Text);
                    insertCmd.Parameters.AddWithValue("@type", radioButton1.Checked ? 1 : (radioButton2.Checked ? 2 : 0));

                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Sua conta foi criada. Por favor, faça login agora.", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            
            Password.PasswordChar = '*';
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
                
                
        }
    }
}
