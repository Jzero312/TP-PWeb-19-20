using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWebTudoDeNovo.Models
{
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        public String Nome { get; set; }
    }
}