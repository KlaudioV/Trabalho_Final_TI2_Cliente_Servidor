using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrajaComigo.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(40, ErrorMessage = "Este campo não pode ter mais de  {1} carateres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùâõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùâõäëïöüâêîôûñ]+){1,3}", ErrorMessage = "")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(40, ErrorMessage = "Este campo não pode ter mais de  {1} carateres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùâõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùâõäëïöüâêîôûñ]+){1,3}", ErrorMessage = "")]
        public string Apelido { get; set; }


        [StringLength(128)]
        public string Morada { get; set; }
        [StringLength(64)]
        public string CodigoPostal { get; set; }

        [StringLength(20)]
        public string Concelho { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(9)]
        public string Telemovel { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[12567][0-9][8]", ErrorMessage = "Errou na escrita do seu código")]
        public string NIF { get; set; }

    }
}