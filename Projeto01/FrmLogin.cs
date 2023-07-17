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
            //Meu metodo (funcionou)
            {
                //conect.AbrirConexao();
                //sql = "SELECT * FROM login WHERE nome=@nome AND senha=@senha"; 
                //cmd = new MySqlCommand(sql, conect.con);
                //MySqlDataAdapter da = new MySqlDataAdapter();
                //da.SelectCommand = cmd;
                //cmd.Parameters.AddWithValue("@nome", txtLogin.Text);
                //cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //if (dt.Rows.Count < 1)
                //{
                //    MessageBox.Show("Login invalido");
                //    txtLogin.Clear();
                //    txtSenha.Clear();
                //    txtLogin.Focus();
                //    return;
                //}
                //FrmMenu menu = new FrmMenu();
                //menu.ShowDialog();
                //this.Close();
            }

            try
            {
                conect.AbrirConexao();
                MySqlCommand cmdverificar;
                MySqlDataReader reader;
                cmdverificar = new MySqlCommand("SELECT * FROM login WHERE nome=@nome AND senha=@senha", conect.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdverificar;
                cmdverificar.Parameters.AddWithValue("@nome", txtLogin.Text);
                cmdverificar.Parameters.AddWithValue("@senha", txtSenha.Text);
                reader = cmdverificar.ExecuteReader();
                if (reader.HasRows)
                {
                    FrmMenu frm = new FrmMenu();                    
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário Invalido");
                    txtLogin.Clear();
                    txtSenha.Clear();
                    txtLogin.Focus();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possivel se conectar ao database!");
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar_Click(sender, e);
            }
        }    
    }
}
