using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroApi.Models
{
    public class Garantia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Prazo { get; set; }
    }
}