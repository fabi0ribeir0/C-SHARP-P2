using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto01
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCpf.Enabled = false;
            mskTel.Enabled = false;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        Conexao conect = new Conexao();
        string sql;
        MySqlCommand cmd;

        private void ativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = true;
        }
        private void desativaBotoes()
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
        }

        private void ativaCampos()
        {
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCpf.Enabled = true;
            mskTel.Enabled = true;
        }

        private void desativaCampos()
        {
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCpf.Enabled = false;
            mskTel.Enabled = false;
        }

        private void limpaCampos()
        {
            txtNome.Clear();
            txtEndereco.Clear();
            mskCpf.Clear();
            mskTel.Clear();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativaBotoes();
            ativaCampos();
            btnNovo.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            conect.AbrirConexao();
            sql = "INSERT INTO cliente (nome, endereço, cpf, telefone) VALUES (@nome, @endereço, @cpf, @telefone)";
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereço", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", mskCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", mskTel.Text);
            cmd.ExecuteNonQuery();
            conect.FecharConexao();

            desativaBotoes();
            desativaCampos();
            limpaCampos();
            btnNovo.Enabled = true;

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            desativaBotoes();
            desativaCampos();
            limpaCampos();
            btnNovo.Enabled = true;
        }

        private void mskCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verifica se a tecla precionada é um número ou backspace
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '\b') && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }
    }
}
