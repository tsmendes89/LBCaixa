using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CaixaLibrary.DAO;
using CaixaLibrary.Model;
using CaixaLibrary.Enum;

namespace CaixaLibrary.DAOImpl
{
   public class MovimentoDAOImpl : MovimentoDAO
    {
        private SqlConnection connection;

        public void inserirMovimento (Movimento movimento)
        {
            connection = Conexao.obterConexao();
            
            try
            {
                String sql = "INSERT INTO movimento VALUES (@dataInclusao, @tipoMovimento, " +
                "@referencia, @descricao, @valor, @incluidoCaixa)";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", movimento.DataInclusao));
                sqlCommand.Parameters.Add(new SqlParameter("@tipoMovimento", movimento.TipoMovimento));
                sqlCommand.Parameters.Add(new SqlParameter("@referencia", movimento.Referencia));
                sqlCommand.Parameters.Add(new SqlParameter("@descricao", movimento.Descricao));
                sqlCommand.Parameters.Add(new SqlParameter("@valor", movimento.Valor));
                sqlCommand.Parameters.Add(new SqlParameter("@incluidoCaixa", true)); 
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao inserir o movimento. " + e.Message);
            }
        }

        public void alterarMovimento(Movimento movimento)
       {
           connection = Conexao.obterConexao();

           try
           {
               String sql = "UPDATE movimento SET "+
                "data_inclusao=@dataInclusao, tipo_movimento=@tipoMovimento, "+
                "referencia=@referencia, descricao=@descricao, " +
                "valor=@valor, incluido_caixa=@incluidoCaixa " + 
                "where id_movimento=@idMovimento";

               SqlCommand sqlCommand = new SqlCommand();
               sqlCommand = connection.CreateCommand();
               sqlCommand.CommandType = CommandType.Text;
               sqlCommand.CommandText = sql;
               sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", movimento.DataInclusao));
               sqlCommand.Parameters.Add(new SqlParameter("@tipoMovimento", movimento.TipoMovimento));
               sqlCommand.Parameters.Add(new SqlParameter("@referencia", movimento.Referencia));
               sqlCommand.Parameters.Add(new SqlParameter("@descricao", movimento.Descricao));
               sqlCommand.Parameters.Add(new SqlParameter("@valor", movimento.Valor));
               sqlCommand.Parameters.Add(new SqlParameter("@incluidoCaixa", movimento.IncluidoCaixa));
               sqlCommand.Parameters.Add(new SqlParameter("@idMovimento", movimento.Id));

               sqlCommand.ExecuteNonQuery();
               sqlCommand.Connection.Close();
           }
           catch (Exception e)
           {
               Console.WriteLine("Erro a excluir o movimento. " + e.Message);
           }
       }

        public void excluirMovimentoPorId(int idMovimento)
        {
            connection = Conexao.obterConexao();

            try
            {
                String sql = "DELETE movimento where id_movimento=@idMovimento";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@idMovimento", idMovimento));
   
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro a excluir o movimento. " + e.Message);
            }
        }

        public Boolean existeMovimentoNestaData(DateTime dataInclusao)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Boolean temMovimentoNestaData = false;
            try
            {
                String sql = "SELECT * FROM movimento WHERE data_inclusao=@dataInclusao";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", dataInclusao));
                sqlDataReader = sqlCommand.ExecuteReader();
                temMovimentoNestaData = sqlDataReader.HasRows;
                sqlCommand.Connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao verificar se existe movimento." + e.Message);
            }

            return temMovimentoNestaData;
        }

       public Movimento obterMovimentoPorId (int idMovimento)
        {
            Movimento movimento = new Movimento();
           
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                String sql = "SELECT * FROM movimento WHERE id_movimento=@idMovimento";          
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@idMovimento", idMovimento));

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    movimento = new Movimento();
                    movimento.Id = (int) sqlDataReader.GetValue(0);
                    movimento.DataInclusao = (DateTime) sqlDataReader.GetValue(1);
                    movimento.TipoMovimento = sqlDataReader.GetValue(2).ToString();
                    movimento.Referencia = sqlDataReader.GetValue(3).ToString();
                    movimento.Descricao = sqlDataReader.GetValue(4).ToString();
                    movimento.Valor = Convert.ToDouble (sqlDataReader.GetValue(5));
                    movimento.IncluidoCaixa = (Boolean) sqlDataReader.GetValue(6);
      
                }
                sqlCommand.Connection.Close();             
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao listar movimentos. " + e.Message);
            }
            return movimento;
        }
        

        public List<Movimento> listarMovimentosDoDia(DateTime dataInclusao)
        {
            List<Movimento> movimentos = new List<Movimento> ();
           
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                Movimento movimento;
                String sql = "SELECT * FROM movimento WHERE data_inclusao=@dataInclusao";          
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", dataInclusao));

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    movimento = new Movimento();
                    movimento.Id = (int) sqlDataReader.GetValue(0);
                    movimento.DataInclusao = (DateTime) sqlDataReader.GetValue(1);
                    movimento.TipoMovimento = sqlDataReader.GetValue(2).ToString();
                    movimento.Referencia = sqlDataReader.GetValue(3).ToString();
                    movimento.Descricao = sqlDataReader.GetValue(4).ToString();
                    movimento.Valor = Convert.ToDouble (sqlDataReader.GetValue(5));
                    movimento.IncluidoCaixa = (Boolean) sqlDataReader.GetValue(6);
                    movimentos.Add (movimento);
                }
                sqlCommand.Connection.Close();             
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao listar movimentos. " + e.Message);
            }
            return movimentos;
        }

    }
}
