using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SeguroApi.Data.Dto
{
    public class GarantiaUpdateDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Prazo é obrigatório.")]
        [Range(1, 365, ErrorMessage = "Prazo deve estar entre 1 e 365 dias.")]
        public int Prazo { get; set; }
    }
}