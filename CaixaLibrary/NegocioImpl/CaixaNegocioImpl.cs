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
    public class CaixaNegocioImpl : CaixaNegocio
    {
        CaixaDAO caixaDAO = new CaixaDAOImpl();
        MovimentoDAO movimentoDAO = new MovimentoDAOImpl();

        public void somarMovimentoAoCaixa(DateTime data, Double valor)
        {
            Caixa caixa = caixaDAO.obterCaixaDoDia(data);
            Double saldoDia = caixa.SaldoDia;
            saldoDia = saldoDia + valor;
            caixa.SaldoDia = saldoDia;
            caixaDAO.alterarCaixa(caixa);
        }

        public void abrirCaixa (DateTime data, Double saldoAnterior, Double saldoDia)
        {
            Caixa caixa = new Caixa();
            caixa.Data = data;
            caixa.SaldoAnterior = saldoAnterior;
            caixa.SaldoDia = saldoDia;
            caixaDAO.abrirCaixa(caixa);
        }

        public void encerrarCaixa(int id, DateTime data, Double saldoAtual,
            Double saldoDia, Double saldoAnterior, Boolean encerrado)        
        {
            
            try
            {
                if (data != null)
                {
                    if (caixaDAO.existeCaixaNestaData(data))
                    {
                        if (caixaDAO.caixaAberto(data))
                        {
                            saldoAtual = caixaDAO.saldoAtual(data);
                            alterarCaixa(id, data, saldoAtual, saldoDia, saldoAnterior,
                                true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao encerrar caixa." + e.Message);
            }
        }

        public void alterarCaixa (int id, DateTime data, Double saldoAtual,
            Double saldoDia, Double saldoAnterior, Boolean encerrado)
        {
            Caixa caixa = new Caixa();
            caixa.Id = id;
            caixa.Data = data;
            caixa.SaldoAtual = saldoAtual;
            caixa.SaldoDia = saldoDia;
            caixa.SaldoAnterior = saldoAnterior;
            caixa.Encerrado = encerrado;

            caixaDAO.alterarCaixa(caixa);

        }
    }
}
