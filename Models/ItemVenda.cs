using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeguroApi.Models
{
    public class ItemVenda
    {
        public Guid Id { get; set; }
        [ForeignKey("Produto")]
        public Guid IdProduto { get; set; }
        [ForeignKey("Garantia")]
        public Guid IdGarantia { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        
        [JsonIgnore]
        public Garantia Garantia { get; set; }
        [JsonIgnore]
        public Produto Produto { get; set; }
    }
}