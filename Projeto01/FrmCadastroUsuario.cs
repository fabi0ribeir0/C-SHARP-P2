using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Projeto01
{
    public partial class FrmCadastroUsuario : Form
    {
        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }

        Conexao conect = new Conexao();

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text != txtConfirmaSenha.Text)
            {
                MessageBox.Show("As senhas não correspodem, Digite novamente!");
                txtConfirmaSenha.Clear();
                txtSenha.Clear();
                txtSenha.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Digite um nome de usuário!");
                txtUsuario.Clear();
                txtUsuario.Focus();
                return;
            }//Verifica se não está nulo os campos de usuario ou senha
            conect.AbrirConexao();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO login (nome, senha) values (@nome, @senha)", conect.con);
            cmd.Parameters.AddWithValue("@nome", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);

            MySqlCommand verifica = new MySqlCommand("SELECT * FROM login WHERE nome=@nome", conect.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = verifica;
            verifica.Parameters.AddWithValue("@nome", txtUsuario.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Usuario já cadastrado!");
                txtUsuario.Clear();
                txtUsuario.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            conect.FecharConexao();
            txtUsuario.Clear();
            txtSenha.Clear();
            txtConfirmaSenha.Clear();
            MessageBox.Show($"Usuário {txtUsuario.Text} cadastrado!");
            txtUsuario.Focus();
        }
    }
}
