using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaixaLibrary.Model;

namespace CaixaLibrary.Negocio
{
    public interface MovimentoNegocio
    {
        void inserirMovimento(String tipoMovimento, String referencia,
            String descricao, Double valor);

        void alterarMovimento(int id, DateTime data, String tipoMovimento,
            String referencia, String descricao, Double valor, Boolean incluidoCaixa);

        void excluirMovimento(int idMovimento);

        List<Movimento> listarMovimentosDataAtual();

        List<Movimento> listarMovimentosPorData(DateTime data);
    }
}
