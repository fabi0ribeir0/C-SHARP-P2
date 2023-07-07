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
using System.IO;

namespace Projeto01
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        // Variaveis
        //===========================================
        string sql;
        Conexao conect = new Conexao();
        MySqlCommand cmd;

        //===========================================

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            conect.AbrirConexao();
            sql = "SELECT COUNT * FROM login WHERE nome=@nome AND senha=@senha"; 
            cmd = new MySqlCommand(sql, conect.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@nome", txtLogin.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("Login invalido");
                txtLogin.Clear();
                txtSenha.Clear();
                txtLogin.Focus();
            }

            MessageBox.Show(dt.Rows.Count.ToString());


        }
    }
}
