using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto01
{
    internal class Conexao
    {
        public string conect = "SERVER=localhost;DATABASE=aula;UID=root;PWD=;PORT=";

        public MySqlConnection con = null;

        //Abrir conexão 
        public void AbrirConexao()
        {
            //testar
            try
            {
                con = new MySqlConnection(conect);
                con.Open();
            }
            catch (Exception ex)
            {
                //erro
                MessageBox.Show("Erro de conexão " + ex.Message + "\nVerifique o servidor");
                
            }
        }
        //fexar conexão
        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conect);
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro de conexão " + ex.Message);
            }
        }
    }
}
