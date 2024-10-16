using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Models
{
    public class ItemVenda
    {
        public Guid Id { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
         public Garantia Garantia { get; set; }
    }
}