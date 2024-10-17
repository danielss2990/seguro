using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Data.Dto
{
    public class VendaResponseDTO
    {
        public Guid Id { get; set; }
        public ICollection<ItemVendaResponseDTO> Itens { get; set; } = new List<ItemVendaResponseDTO>();
        public decimal ValorTotal { get; set; }
    }
}