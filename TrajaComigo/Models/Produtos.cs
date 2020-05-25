using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrajaComigo.Models
{
    public class Produtos
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Designacao { get; set; }

        [StringLength(255)]
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PrecoUni { get; set; }

        public string Imagem { get; set; }

        [StringLength(12)]
        public string Sexo { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal IVA { get; set; }
    }
}