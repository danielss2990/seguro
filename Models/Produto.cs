using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueMaximo { get; set; }
        public int SaldoEmEstoque { get; set; }
        public string Fornecedor { get; set; }
        public bool PossuiGarantia { get; set; }
    }
}