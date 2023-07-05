using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            grid.Columns[5].HeaderText = "Foto";
            grid.Columns[4].Width += 3;
            grid.Columns[5].Visible = false;
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
            btnFoto.Enabled = false;
        }

        private void limpaFoto()
        {
            pctFoto.Image = Properties.Resources.NULL;
            foto = "ft/NULL.png";
        }

        Conexao conect = new Conexao();
        string sql;
        string foto;
        MySqlCommand cmd;        

        private void BuscarNome() // Metodo para buscar nome no banco de dados
        {
            conect.AbrirConexao();
            sql = "SELECT * FROM cliente WHERE nome LIKE @nome ORDER BY nome ASC"; // LIKE, busca nome por aproximação
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@nome", txtBusca.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            conect.FecharConexao();

            FormatarGD();
        }

        private void ativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnFoto.Enabled = true;
        }
        private void desativaBotoes()
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnAlterar.Enabled = false;
            btnFoto.Enabled = false;
        }

        private byte[] img() //Metodo para enviar imagem para o banco de dados
        {
            byte[] imagemByte = null;

            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagemByte = br.ReadBytes((int)fs.Length);

            return imagemByte;
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
            limpaFoto();
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
            sql = "INSERT INTO cliente (nome, endereço, cpf, telefone, foto) VALUES (@nome, @endereço, @cpf, @telefone, @foto)";
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereço", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", mskCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", mskTel.Text);
            cmd.Parameters.AddWithValue("@foto", img()); //metodo img
            cmd.ExecuteNonQuery();
            conect.FecharConexao();

            desativaBotoes();
            desativaCampos();
            limpaCampos();
            btnNovo.Enabled = true;
            ListarGD();
            limpaFoto();
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
            limpaFoto();
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
            sql = "UPDATE cliente SET nome=@nome, endereço=@endereço, cpf=@cpf, telefone=@telefone, foto=@foto WHERE id=@id";
            cmd = new MySqlCommand(sql, conect.con);
            cmd.Parameters.AddWithValue("@id", grid.CurrentRow.Cells[0].Value);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereço", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", mskCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", mskTel.Text);
            cmd.Parameters.AddWithValue("@foto", img());
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
            limpaFoto();
            ListarGD();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                limpaFoto();
                ativaBotoes();
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                ativaCampos();

                txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtEndereco.Text = grid.CurrentRow.Cells[2].Value.ToString();
                mskCpf.Text = grid.CurrentRow.Cells[3].Value.ToString();
                mskTel.Text = grid.CurrentRow.Cells[4].Value.ToString();

                byte[] imagem = (byte[])grid.Rows[e.RowIndex].Cells[5].Value;
                MemoryStream ms = new MemoryStream(imagem);
                pctFoto.Image = Image.FromStream(ms);
            }
            else return;
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens(*jpg; *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString(); // pega o caminho da imagem
                pctFoto.ImageLocation = foto;
            }
        }
    }
}
