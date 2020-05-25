using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrajaComigo.Models
{
    public class DetalhesEncomendas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Encomenda")]
        public virtual Encomendas EncomendaId { get; set; }

        [ForeignKey("Produto")]
        public virtual Produtos ProdutoId { get; set; }

        [StringLength(128)]
        public string Tamanhos { get; set; }

        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoUni { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Preco { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal IVA { get; set; }
    }
}
