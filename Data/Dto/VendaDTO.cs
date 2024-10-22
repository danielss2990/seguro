using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeguroApi.Data.Dto;

namespace SeguroApi.DTOs
{
    public class VendaDTO
    {
        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "A venda deve conter pelo menos um item.")]
        public ICollection<ItemVendaDTO> Itens { get; set; } = new List<ItemVendaDTO>();

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }
    }
}
