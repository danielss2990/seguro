using System;
using System.Collections.Generic;
using SeguroApi.Data.Dto;

namespace SeguroApi.DTOs
{
    public class VendaDTO
    {
        public ICollection<ItemVendaDTO> Itens { get; set; } = new List<ItemVendaDTO>();
        public decimal ValorTotal { get; set; }
    }
}
