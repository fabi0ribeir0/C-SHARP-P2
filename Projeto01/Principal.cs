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
        private void FormatarGD()
        {
            grid.Columns[0].HeaderText = "Código";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Endereço";
            grid.Columns[3].HeaderText = "CPF";
            grid.Columns[4].HeaderText = "Tel.";
            grid.Columns[4].Width += 3;
        }

        private void ListarGD()
        {
            conect.AbrirConexao();
            sql = "SELECT * FROM cliente ORDER BY Nome ASC";
            cmd = new MySqlCommand(sql, conect.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            conect.FecharConexao();

            FormatarGD();
        }

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
            btnAlterar.Enabled = false;
        }

        Conexao conect = new Conexao();
        string sql;
        MySqlCommand cmd;

        private void ativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = true;
        }
        private void desativaBotoes()
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnAlterar.Enabled= false;
        }

        private void ativaCampos()
        {
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCpf.Enabled = true;
            mskTel.Enabled = true;
            txtBusca.Enabled = false;
        }

        private void desativaCampos()
        {
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCpf.Enabled = false;
            mskTel.Enabled = false;
            txtBusca.Enabled = true;
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
            btnAlterar.Enabled = false;
            btnNovo.Enabled = false;
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Nome deve ser preenchido!");
                txtNome.Focus();
                return;
            }
            if (mskCpf.Text== "   .   .   -" || mskCpf.Text.Length < 14 )
            {
                MessageBox.Show("CPF invalido");
                mskCpf.Focus();
                return;
            }
            if (mskTel.Text== "+55(  )     -" || mskTel.Text.Length < 16)
            {
                MessageBox.Show("Telefone invalido");
                mskTel.Focus();
                return;
            }

            conect.AbrirConexao();

            sql = "SELECT COUNT(*) FROM cliente WHERE nome = @nome";
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                MessageBox.Show($"O Nome {txtNome.Text} já foi cadastrado");
                return;
            }
            
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

            ListarGD();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Excluir usuario?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (res == DialogResult.No)
            {
                MessageBox.Show("Usuario não exluido!");
                desativaBotoes();
                desativaCampos();
                limpaCampos();
                btnNovo.Enabled = true;
                return;
            }
            else
            {
                conect.AbrirConexao();
                sql = "DELETE FROM cliente WHERE id=@id";
                cmd = new MySqlCommand(sql, conect.con);
                cmd.Parameters.AddWithValue("@id", grid.CurrentRow.Cells[0].Value);
                cmd.ExecuteNonQuery();
                conect.FecharConexao();

                desativaCampos();
                limpaCampos();
                desativaBotoes();
                btnNovo.Enabled = true;

                ListarGD();
            }

            
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
            if (!char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Nome deve ser preenchido!");
                txtNome.Focus();
                return;
            }
            if (mskCpf.Text == "   .   .   -" || mskCpf.Text.Length < 14)
            {
                MessageBox.Show("CPF invalido");
                mskCpf.Focus();
                return;
            }
            if (mskTel.Text == "+55(  )     -" || mskTel.Text.Length < 16)
            {
                MessageBox.Show("Telefone invalido");
                mskTel.Focus();
                return;
            }

            conect.AbrirConexao();
            sql = "UPDATE cliente SET nome=@nome, endereço=@endereço, cpf=@cpf, telefone=@telefone WHERE id=@id";
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@id", grid.CurrentRow.Cells[0].Value);
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

            ListarGD();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ListarGD();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ativaBotoes();
            btnNovo.Enabled=false;
            btnSalvar.Enabled=false;
            btnAlterar.Enabled=true;
            btnExcluir.Enabled=true;
            ativaCampos();

            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[2].Value.ToString();
            mskCpf.Text = grid.CurrentRow.Cells[3].Value.ToString();
            mskTel.Text = grid.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
