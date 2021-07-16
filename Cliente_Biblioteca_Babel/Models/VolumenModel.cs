using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cliente_Biblioteca_Babel.Models
{
    public class VolumenModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Número de Volúmen")]
        [Required]
        public int No_Volumen { get; set; }
        [Display(Name = "Título del Volúmen")]
        [Required]
        public string Titulo { get; set; }
        [Required]
        public short Id_Localizacion { get; set; }
        [Display(Name = "Localización")]
        [Required]
        public string Localizacion { get; set; }
    }
}