using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrajaComigo.Models
{
    public class Encomendas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public virtual Clientes ClienteId { get; set; }

        [Timestamp]
        public byte[] DataEncomenda { get; set; }

        [Column(TypeName = "Text")]
        [StringLength(255)]
        public string TipoPagamento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(128)]
        public string MoradaEntrega { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(64)]
        public string CodigoPostal { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoProduto { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal ValorEntrega { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Desconto { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoFinal { get; set; }

    }
}
