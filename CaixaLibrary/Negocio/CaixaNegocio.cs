using CaixaLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaLibrary.Negocio
{
    public interface CaixaNegocio
    {
        void abrirCaixa(DateTime data, Double saldoAnterior, Double saldoDia);

        void somarMovimentoAoCaixa(DateTime data, Double valor);

        void encerrarCaixa(int id, DateTime data, Double saldoAtual,
            Double saldoDia, Double saldoAnterior, Boolean encerrado);

        void alterarCaixa(int id, DateTime data, Double saldoAtual,
            Double saldoDia, Double saldoAnterior, Boolean encerrado);
    }
}
