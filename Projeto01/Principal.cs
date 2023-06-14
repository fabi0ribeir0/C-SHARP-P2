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
        
        private void ativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = true;
        }
        private void desativaBotoes()
        {
            btnCancelar.Enabled=false;
            btnSalvar.Enabled=false;
            btnExcluir.Enabled=false;
            btnNovo.Enabled=false;
        }

        private void ativaCampos()
        {
            txtNome.Clear();
            txtNome.Enabled=true;
            txtEndereco.Clear();
            txtEndereco.Enabled=true;
            mskCpf.Clear();
            mskCpf.Enabled=true;
            mskTel.Clear();
            mskTel.Enabled=true;
            txtNome.Focus();
        }

        private void desativaCampos()
        {
            txtNome.Clear();
            txtNome.Enabled = false;
            txtEndereco.Clear();
            txtEndereco.Enabled = false;
            mskCpf.Clear();
            mskCpf.Enabled = false;
            mskTel.Clear();
            mskTel.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativaBotoes();
            ativaCampos();
            btnNovo.Enabled=false;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            desativaBotoes();
            desativaCampos();
            btnNovo.Enabled=true;
        }
    }
}
