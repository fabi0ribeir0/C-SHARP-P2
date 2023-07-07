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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void menuCliente_Click(object sender, EventArgs e)
        {
            FrmCadastroCliente frm = new FrmCadastroCliente();
            frm.ShowDialog();
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastroUsuario frm = new FrmCadastroUsuario();
            frm.ShowDialog();
        }
    }
}
