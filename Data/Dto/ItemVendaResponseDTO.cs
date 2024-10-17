using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Data.Dto
{
    public class ItemVendaResponseDTO
    {
        public Guid Id { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdGarantia { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}