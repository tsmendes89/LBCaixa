using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaixaLibrary.DAOImpl;
using CaixaLibrary.Model;

namespace CaixaLibrary.DAO
{
    public interface MovimentoDAO
    {
        void inserirMovimento(Movimento movimento);

        void alterarMovimento(Movimento movimento);

        void excluirMovimentoPorId(int idMovimento);

        List<Movimento> listarMovimentosDoDia(DateTime dataInclusao);
        
        Boolean existeMovimentoNestaData(DateTime dataInclusao);

        Movimento obterMovimentoPorId(int idMovimento);

    }
}
