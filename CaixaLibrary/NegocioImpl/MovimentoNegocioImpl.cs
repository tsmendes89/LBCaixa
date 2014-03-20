using CaixaLibrary.DAO;
using CaixaLibrary.DAOImpl;
using CaixaLibrary.Model;
using CaixaLibrary.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CaixaLibrary.NegocioImpl
{
    public class MovimentoNegocioImpl : MovimentoNegocio
    {
        MovimentoDAO movimentoDAO = new MovimentoDAOImpl();
        CaixaDAO caixaDAO = new CaixaDAOImpl();
        CaixaNegocio caixaNegocio = new CaixaNegocioImpl();
        
        public void inserirMovimento (String tipoMovimento, String referencia,
            String descricao, Double valor)
        {
            Movimento movimento = new Movimento();
            movimento.DataInclusao = DateTime.Today;
            movimento.TipoMovimento = tipoMovimento;
            movimento.Referencia = referencia;
            movimento.Descricao = descricao;
            movimento.Valor = valor;
          
            if (!caixaDAO.existeCaixaNestaData(DateTime.Today))
            {
                Caixa caixaAuxiliar = caixaDAO.obterUltimoCaixa();
                if (caixaDAO.caixaAberto(caixaAuxiliar.Data))
                    caixaNegocio.encerrarCaixa(caixaAuxiliar.Id, caixaAuxiliar.Data,
                        caixaAuxiliar.SaldoAtual, caixaAuxiliar.SaldoDia,
                        caixaAuxiliar.SaldoAnterior, caixaAuxiliar.Encerrado);

                caixaNegocio.abrirCaixa(DateTime.Today, caixaAuxiliar.SaldoAtual,
                    valor);
                
            }
            else
            {
                caixaNegocio.somarMovimentoAoCaixa(DateTime.Today, valor);
            }

            movimentoDAO.inserirMovimento(movimento);         
            
        }

        public void alterarMovimento(int id, DateTime data, String tipoMovimento,
            String referencia, String descricao, Double valor, Boolean incluidoCaixa)
        {
            try
            {
                if (id > 0)
                {
                    Movimento movimento = new Movimento();
                    movimento.Id = id;
                    movimento.TipoMovimento = tipoMovimento;
                    movimento.DataInclusao = data;
                    movimento.Referencia = referencia;
                    movimento.Descricao = descricao;
                    movimento.Valor = valor;
                    movimento.IncluidoCaixa = incluidoCaixa;

                    movimentoDAO.alterarMovimento(movimento);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ID error." + e.Message);
            }
        }

        public void excluirMovimento(int idMovimento)
        {
            try
            {
                if (idMovimento > 0)
                {                   
                    movimentoDAO.excluirMovimentoPorId(idMovimento);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ID Error" + e.Message);
            }

        }

        public List<Movimento> listarMovimentosDataAtual()
        {
            return movimentoDAO.listarMovimentosDoDia(DateTime.Today);
        }

        public List<Movimento> listarMovimentosPorData(DateTime data)
        {
            return movimentoDAO.listarMovimentosDoDia(data);
        }


    }
}
