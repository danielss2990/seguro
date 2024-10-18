using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Data.Dto
{
    public class ProdutoDTO : IValidatableObject
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque mínimo não pode ser negativo.")]
        public int EstoqueMinimo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque máximo não pode ser negativo.")]
        public int EstoqueMaximo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O saldo em estoque não pode ser negativo.")]
        public int SaldoEmEstoque { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do fornecedor pode ter no máximo 50 caracteres.")]
        public string Fornecedor { get; set; }

        public bool PossuiGarantia { get; set; }

        // Validação personalizada: EstoqueMáximo não pode ser menor que EstoqueMínimo
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EstoqueMaximo < EstoqueMinimo)
            {
                yield return new ValidationResult(
                    "O estoque máximo não pode ser menor que o estoque mínimo.",
                    new[] { nameof(EstoqueMaximo) }
                );
            }

             if (SaldoEmEstoque < 0)
            {
                yield return new ValidationResult(
                    "O saldo em estoque não pode ser negativo.",
                    new[] { nameof(SaldoEmEstoque) }
                );
            }
        }
    }
}