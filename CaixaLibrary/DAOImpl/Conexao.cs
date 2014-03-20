using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaLibrary.DAOImpl
{
    public class Conexao
    {
        private static String connString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\Thiago\\Documents\\Visual Studio 2013\\Projects\\Caixa\\CaixaLibrary\\CaixaLibrary\\Caixa.mdf;Integrated Security=True";

            private static SqlConnection conn = null;

            public static SqlConnection obterConexao()
            {
                conn = new SqlConnection(connString);
                
                try
                {
                    conn.Open();
                }
                catch (SqlException sqle)
                {
                    Console.WriteLine(sqle);
                    conn = null;
                }

                return conn;
            }

            public static Boolean fecharConexao()
            {
                if (conn != null)
                {
                    conn.Close();
                    return true;
                }
                return false;
            }
        }
}
