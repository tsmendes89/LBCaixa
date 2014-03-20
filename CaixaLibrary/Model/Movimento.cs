using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaLibrary.Model {
    
    public class Movimento
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public String TipoMovimento { get; set; }
        public String Referencia { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public Boolean IncluidoCaixa { get; set; }

    }
}
