using CaixaLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaLibrary.DAO
{
    public interface CaixaDAO
    {
        void abrirCaixa(Caixa caixa);
        Boolean existeCaixaNestaData(DateTime data);
        Double saldoDia(DateTime dataInclusao);
        Boolean caixaAberto(DateTime data);
        Boolean caixaFechado(DateTime data);
        void alterarCaixa(Caixa caixa);
        Double saldoAtual(DateTime dataInclusao);
        Caixa obterCaixaDoDia(DateTime data);
        Caixa obterUltimoCaixa();
        Caixa obterUltimoCaixaAnteriorData(DateTime data);
    }
}
