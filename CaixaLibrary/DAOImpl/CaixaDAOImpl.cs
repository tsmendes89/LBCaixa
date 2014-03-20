using CaixaLibrary.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CaixaLibrary.Model;

namespace CaixaLibrary.DAOImpl
{
    public class CaixaDAOImpl : CaixaDAO 
    {
        private SqlConnection connection;

        public void abrirCaixa(Caixa caixa)
        {
            connection = Conexao.obterConexao();

            try
            {
                String sql = "INSERT INTO caixa VALUES (@dataAbertura, @saldoAtual, " +
                "@saldoDia, @saldoAnterior, @encerrado)";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataAbertura", caixa.Data));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoAtual", caixa.SaldoAtual));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoDia", caixa.SaldoDia));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoAnterior", caixa.SaldoAnterior));
                sqlCommand.Parameters.Add(new SqlParameter("@encerrado", false));

                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao abrir o caixa. " + e.Message);
            }
        }

        public Caixa obterUltimoCaixa()
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Caixa caixa = null;
            try
            {
                String sql = "SELECT * FROM caixa WHERE id_caixa=(SELECT MAX (id_caixa) FROM caixa) ";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    caixa = new Caixa();
                    caixa.Id = (int)sqlDataReader.GetValue(0);
                    caixa.Data = (DateTime)sqlDataReader.GetValue(1);
                    caixa.SaldoAtual = Convert.ToDouble(sqlDataReader.GetValue(2));
                    caixa.SaldoDia = Convert.ToDouble(sqlDataReader.GetValue(3));
                    caixa.SaldoAnterior = Convert.ToDouble(sqlDataReader.GetValue(4));
                    caixa.Encerrado = (Boolean)sqlDataReader.GetValue(5);

                }
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar caixa. " + e.Message);
            }
            return caixa;
        }

        public Caixa obterUltimoCaixaAnteriorData(DateTime data)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Caixa caixa = null;
            try
            {
                String sql = "SELECT * FROM caixa WHERE id_caixa=(SELECT MAX (id_caixa) FROM caixa WHERE data<@data) ";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", data));

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    caixa = new Caixa();
                    caixa.Id = (int)sqlDataReader.GetValue(0);
                    caixa.Data = (DateTime)sqlDataReader.GetValue(1);
                    caixa.SaldoAtual = Convert.ToDouble(sqlDataReader.GetValue(2));
                    caixa.SaldoDia = Convert.ToDouble(sqlDataReader.GetValue(3));
                    caixa.SaldoAnterior = Convert.ToDouble(sqlDataReader.GetValue(4));
                    caixa.Encerrado = (Boolean)sqlDataReader.GetValue(5);

                }
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar caixa. " + e.Message);
            }
            return caixa;
        }

        public void alterarCaixa(Caixa caixa)
        {
            connection = Conexao.obterConexao();

            try
            {
                String sql = "UPDATE caixa SET " +
                 "data=@data, " +
                 "saldo_atual=@saldoAtual, saldo_dia=@saldoDia, " +
                 "encerrado=@encerrado " +
                 "where id_caixa=@idCaixa";

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", caixa.Data));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoAtual", caixa.SaldoAtual));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoDia", caixa.SaldoDia));
                sqlCommand.Parameters.Add(new SqlParameter("@saldoAnterior", caixa.SaldoAnterior));
                sqlCommand.Parameters.Add(new SqlParameter("@encerrado", caixa.Encerrado));             
                sqlCommand.Parameters.Add(new SqlParameter("@idCaixa", caixa.Id));

                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao alterar o caixa. " + e.Message);
            }
        }

        public Boolean existeCaixaNestaData (DateTime data)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Boolean temCaixaNestaData = false;
            try
            {
                String sql = "SELECT * FROM caixa WHERE data=@data";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", data));
                sqlDataReader = sqlCommand.ExecuteReader();
                temCaixaNestaData = sqlDataReader.HasRows;
                sqlCommand.Connection.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao verificar se existe caixa." + e.Message);
            }

            return temCaixaNestaData;
        }
        
        public Caixa obterCaixaDoDia (DateTime data)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Caixa caixa = null;
            try
            {             
                String sql = "SELECT * FROM caixa WHERE data=@data";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", data));

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    caixa = new Caixa();
                    caixa.Id = (int)sqlDataReader.GetValue(0);
                    caixa.Data = (DateTime)sqlDataReader.GetValue(1);
                    caixa.SaldoAtual = Convert.ToDouble(sqlDataReader.GetValue(2));
                    caixa.SaldoDia = Convert.ToDouble(sqlDataReader.GetValue(3));
                    caixa.SaldoAnterior = Convert.ToDouble(sqlDataReader.GetValue(4));
                    caixa.Encerrado = (Boolean)sqlDataReader.GetValue(5);
                    
                }
                sqlCommand.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar caixa. " + e.Message);
            }
            return caixa;
        }

        public Boolean caixaAberto (DateTime data)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Boolean caixaAberto = false;
            try
            {
                String sql = "SELECT * FROM caixa WHERE data=@data AND encerrado=0";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", data));
                sqlDataReader = sqlCommand.ExecuteReader();
                caixaAberto = sqlDataReader.HasRows;
                sqlCommand.Connection.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao verificar se o caixa está aberto." + e.Message);
            }

            return caixaAberto;
        }

        public Boolean caixaFechado (DateTime data)
        {
            SqlDataReader sqlDataReader;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Boolean caixaFechado = false;
            try
            {
                String sql = "SELECT * FROM caixa WHERE data=@data AND encerrado=1";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@data", data));
                sqlDataReader = sqlCommand.ExecuteReader();
                caixaFechado = sqlDataReader.HasRows;
                sqlCommand.Connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao verificar se o caixa está fechado." + e.Message);
            }

            return caixaFechado;
        }

        public Double saldoDia (DateTime dataInclusao)
        {
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Double saldoDia = 0.0;
            try
            {
                String sql = "SELECT ROUND(SUM(valor),2) AS saldoDia FROM movimento WHERE data_inclusao=@dataInclusao";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", dataInclusao));
                saldoDia = Convert.ToDouble(sqlCommand.ExecuteScalar());
                sqlCommand.Connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao calcular o saldo do dia. " + e.Message);
            }
            return saldoDia;
        }

        public Double saldoAtual (DateTime dataInclusao)
        {
            int dia = dataInclusao.Day;
            int mes = dataInclusao.Month;
            int ano = dataInclusao.Year;
            connection = Conexao.obterConexao();
            SqlCommand sqlCommand = new SqlCommand();
            Double saldoAtual = 0.0;
            try
            {
                String sql = "SELECT ROUND(SUM(saldo_anterior + saldo_dia),2) AS saldoAtual FROM caixa WHERE data=@dataInclusao";
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(new SqlParameter("@dataInclusao", dataInclusao));
                saldoAtual = Convert.ToDouble(sqlCommand.ExecuteScalar());
                sqlCommand.Connection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao calcular o saldo atual. " + e.Message);
            }
            return saldoAtual;
        }

    }
}
