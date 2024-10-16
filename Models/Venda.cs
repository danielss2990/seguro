using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Models
{
    public class Venda
    {
        public Guid Id { get; set; }
        public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
        public decimal ValorTotal { get; set; }
       
    }
}