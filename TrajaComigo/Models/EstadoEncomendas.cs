using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrajaComigo.Models
{
    public class EstadoEncomendas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Encomenda")]
        public virtual Encomendas EncomendaId { get; set; }

        [Timestamp]
        public byte[] DataPrevisao { get; set; }

        [Column(TypeName = "Text")]
        [StringLength(255)]
        public string Notas { get; set; }
    }
}
