using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cliente_Biblioteca_Babel.Models
{
    public class LocalizacionModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Número de Estante")]
        [Required]
        public int Estante { get; set; }
        [Required]
        public string Sala { get; set; }
        [Display(Name = "Número de Librero")]
        [Required]
        public int Librero { get; set; }
        [Display(Name = "Posición del Volúmen")]
        [Required]
        public string Posicion { get; set; }
    }
}