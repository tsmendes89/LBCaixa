using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaLibrary.Model
{
    public class Caixa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Double SaldoAtual { get; set; }
        public Double SaldoDia { get; set; }
        public Double SaldoAnterior { get; set; }
        public Boolean Encerrado { get; set; }
    }
}
